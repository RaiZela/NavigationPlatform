namespace Journey.Application.Exceptions;

public class JourneyNotFoundException : NotFoundException
{
    public JourneyNotFoundException(Guid id) : base("Journey",id)
    {
        
    }
}
