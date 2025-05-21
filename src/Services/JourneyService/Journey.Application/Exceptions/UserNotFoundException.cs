namespace Journey.Application.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string? userName) : base("User", userName)
    {
    }
}

