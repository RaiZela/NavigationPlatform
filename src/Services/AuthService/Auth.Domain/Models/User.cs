using Auth.Domain.Enums;

namespace Auth.Domain.Models;

public class User
{
    public Guid Id { get; private set; }
    public string OAuthSubject { get; private set; } // from ID Token (sub)
    public string Email { get; private set; }
    public string Name { get; private set; }
    public Roles Role { get; private set; } = Roles.User;

}
