namespace Journey.Infrastructure.Data.Auth;

public interface ICurrentUserService
{
    public string? Username { get; }
}
