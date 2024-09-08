using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SalesApi.CrossCutting.Exceptions;

namespace SalesApi.Web.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    private readonly ILogger<ApiExceptionFilterAttribute> _Logger;

    public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(SalesApiValidationException), HandleValidationException },
                { typeof(SalesApiAppException), HandleApiException },
                { typeof(SalesApiNotFoundException), HandleApiNotFoundException },
            };
        _Logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as SalesApiValidationException;

        //var details = new ValidationProblemDetails(exception.Errors)
        //{
        //    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        //};
        var details = new SalesApiErrorResult
        {
            Code = StatusCodes.Status400BadRequest,
            Errors = exception?.Errors.ToDictionary(
                x => x.Key,
                x => x.Value) ?? [],
            Message = "ValidationError"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleApiException(ExceptionContext context)
    {
        var exception = context.Exception as SalesApiAppException;

        var details = new SalesApiErrorResult
        {
            Code = StatusCodes.Status422UnprocessableEntity,
            Message = exception?.Message
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity
        };

        context.ExceptionHandled = true;
    }

    private void HandleApiNotFoundException(ExceptionContext context)
    {
        var exception = context.Exception as SalesApiNotFoundException;

        var details = new SalesApiErrorResult
        {
            Code = StatusCodes.Status404NotFound,
            Message = exception?.Message
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status404NotFound
        };

        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        _Logger.LogError(context.Exception, context.Exception.Message);
        var result = new SalesApiErrorResult
        {
            Code = StatusCodes.Status500InternalServerError,
#if DEBUG
            Message = context.Exception.Message
#else
                Message = "خطای عمومی"
#endif
        };

        context.Result = new ObjectResult(result)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}