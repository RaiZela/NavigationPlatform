
namespace Journey.Infrastructure.Data.Auth;

public class CurrentUserService : ICurrentUserService
{
    public string? Username { get; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        Username = httpContextAccessor.HttpContext?.User?.FindFirst("preferred_username")?.Value;
    }
}


