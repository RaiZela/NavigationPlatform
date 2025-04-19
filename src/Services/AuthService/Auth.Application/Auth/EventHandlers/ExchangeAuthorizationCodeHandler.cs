
using global::Auth.Application.Auth.Command;
using global::Auth.Application.Models;
using global::Auth.Application.User.Interfaces;
using Auth.Application.User.Interfaces;
using Auth.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Auth.Application.Auth.EventHandlers;
public class ExchangeAuthorizationCodeHandler
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IUserRepository _userRepo;

    public ExchangeAuthorizationCodeHandler(
        IHttpClientFactory httpClientFactory,
        IUserRepository userRepo)
    {
        _httpClientFactory = httpClientFactory;
        _userRepo = userRepo;
    }

    public async Task HandleAsync(ExchangeAuthorizationCodeCommand command, HttpContext httpContext)
    {
        var client = _httpClientFactory.CreateClient();

        // 1. Exchange code for token
        var response = await client.PostAsync("https://your-oauth-provider.com/oauth/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", command.Code },
                { "client_id", "your-client-id" },
                { "redirect_uri", "http://localhost:3000/callback" },
                { "code_verifier", command.CodeVerifier }
            }));

        if (!response.IsSuccessStatusCode)
            throw new UnauthorizedAccessException("Invalid code or code_verifier.");

        var token = await response.Content.ReadFromJsonAsync<TokenResponse>();

        // 2. Get user info
        var userInfoRequest = new HttpRequestMessage(HttpMethod.Get, "https://your-oauth-provider.com/userinfo");
        userInfoRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        var userInfoResponse = await client.SendAsync(userInfoRequest);
        var userInfo = await userInfoResponse.Content.ReadFromJsonAsync<OAuthUserInfo>();

        // 3. Store user if new
        var user = await _userRepo.GetByOAuthSubjectAsync(userInfo.Sub);
        if (user == null)
        {
            user = new User(userInfo.Sub, userInfo.Email, userInfo.Name, "user");
            await _userRepo.AddAsync(user);
        }

        await _userRepo.SaveChangesAsync();

        // 4. Set cookies
        httpContext.Response.Cookies.Append("access_token", token.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(15)
        });

        httpContext.Response.Cookies.Append("refresh_token", token.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });
    }
}

