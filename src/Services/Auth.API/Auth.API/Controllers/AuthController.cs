namespace Auth.API.Controllers;;


[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly HttpClient _http;
    private readonly ApplicationDbContext _db;

    public AuthController(IHttpClientFactory factory, ApplicationDbContext db)
    {
        _http = factory.CreateClient();
        _db = db;
    }

    [HttpPost("callback")]
    public async Task<IActionResult> Callback([FromForm] string code, [FromForm] string codeVerifier)
    {
        // Exchange code for tokens
        var tokenResponse = await _http.PostAsync("https://YOUR_DOMAIN/oauth/token", new FormUrlEncodedContent(new Dictionary<string, string>
    {
        { "grant_type", "authorization_code" },
        { "client_id", "YOUR_CLIENT_ID" },
        { "redirect_uri", "http://localhost:3000/callback" },
        { "code_verifier", codeVerifier },
        { "code", code }
    }));

        if (!tokenResponse.IsSuccessStatusCode)
            return Unauthorized();

        var tokenData = await tokenResponse.Content.ReadFromJsonAsync<Auth0TokenResponse>();

        // Get user info
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenData!.AccessToken);
        var userInfo = await _http.GetFromJsonAsync<Auth0UserInfo>("https://YOUR_DOMAIN/userinfo");

        // Upsert user
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Auth0Id == userInfo!.Sub);
        if (user == null)
        {
            user = new User
            {
                Auth0Id = userInfo.Sub,
                Email = userInfo.Email,
                FullName = userInfo.Name,
                Role = "user"
            };
            _db.Users.Add(user);
        }

        await _db.SaveChangesAsync();

        // Set HttpOnly cookies
        Response.Cookies.Append("access_token", tokenData.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(15)
        });

        Response.Cookies.Append("refresh_token", tokenData.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        return Ok(new { message = "Logged in!" });
    }
}

public class Auth0TokenResponse
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
    public string IdToken { get; set; } = "";
    public int ExpiresIn { get; set; }
}

public class Auth0UserInfo
{
    public string Sub { get; set; } = "";
    public string Email { get; set; } = "";
    public string Name { get; set; } = "";
}
