using Auth.Application.Models;

namespace Auth.Application.User.Services;

public interface IUserService
{
    Task<User> GetOrCreateAsync(OAuthUserInfo userInfo);
}
