namespace Auth.Application.User.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByOAuthSubjectAsync(string subject);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}
