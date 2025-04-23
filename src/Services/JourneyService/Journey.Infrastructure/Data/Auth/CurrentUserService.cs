
namespace Journey.Infrastructure.Data.Auth;

public class CurrentUserService : ICurrentUserService
{
    public string? Username { get; }
    public string? Role { get; }
    public string? Email { get; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        Username = httpContextAccessor.HttpContext?.User?.FindFirst("preferred_username")?.Value;
        //var roles = httpContextAccessor.HttpContext?.User?.FindAll("roles");
        //var localRoles = new List<string> { "admin", "user" };
        //Role = roles.Where(x => localRoles.Contains(x.Value)).FirstOrDefault()?.Value;
        Email = httpContextAccessor.HttpContext?.User.FindFirst("email")?.Value;
    }
}


