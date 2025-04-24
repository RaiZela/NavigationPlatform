namespace Journey.Application.Data;

public interface ICurrentUserService
{
    public string? Username { get; }
    public string? Email { get; }
}
