namespace SalesApi.CrossCutting.Exceptions;

public class SalesApiNotFoundException : Exception
{
    public SalesApiNotFoundException() : base()
    {
    }

    public SalesApiNotFoundException(string? message) : base(message)
    {
    }

    public SalesApiNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
