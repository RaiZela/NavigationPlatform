namespace Journey.Application.Data.Interfaces;

public interface ICurrentUserService
{
    public string? Username { get; }
    public string? Email { get; }
}
