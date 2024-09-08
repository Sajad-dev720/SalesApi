using FluentValidation.Results;

namespace SalesApi.CrossCutting.Exceptions;

public class SalesApiValidationException : Exception
{
    public SalesApiValidationException()
           : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public SalesApiValidationException(string propertyName, string[] errors)
       : this()
    {
        Errors.Add(propertyName, errors);
    }

    public SalesApiValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public SalesApiValidationException(string? message) : base(message)
    {
    }

    public SalesApiValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();
}
