namespace BuildingBlocks.Exceptions;

public class NoContentException : Exception
{
    public NoContentException()
        : base("There was no content for the requested resource")
    {
    }

    public NoContentException(string message)
        : base(message)
    {
    }

    public NoContentException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}