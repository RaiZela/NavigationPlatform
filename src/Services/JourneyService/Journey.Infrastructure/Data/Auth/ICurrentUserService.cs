namespace Journey.Infrastructure.Data.Auth;

public interface ICurrentUserService
{
    public string? UserId { get; }
}
