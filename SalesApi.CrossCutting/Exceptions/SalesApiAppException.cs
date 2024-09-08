namespace SalesApi.CrossCutting.Exceptions;

public class SalesApiAppException : Exception
{
    public SalesApiAppException(string message) : base(message)
    {
    }

    public SalesApiAppException() : base()
    {
    }

    public SalesApiAppException(string? message,
                                Exception? innerException) : base(message, innerException)
    {
    }
}
