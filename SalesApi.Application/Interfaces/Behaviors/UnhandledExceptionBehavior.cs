using MediatR;
using SalesApi.CrossCutting.Exceptions;
using Serilog;

namespace SalesApi.Application.Interfaces.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;

    public UnhandledExceptionBehavior()
    {
        _logger = Log.ForContext<TRequest>();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex) when (ex.GetType() != typeof(SalesApiValidationException) && ex.GetType() != typeof(SalesApiAppException))
        {
            var requestName = typeof(TRequest).FullName;
            _logger.Error(" Error In :{CommandName} with Message : {Message} In Line {Line}", requestName, ex.Message,
                GetLineNumber(ex), ex.ToString());

            throw;
        }
    }

    public int GetLineNumber(Exception ex)
    {
        return Convert.ToInt32(ex.ToString()[ex.ToString()
            .IndexOf("line")..][..ex.ToString()[ex.ToString().IndexOf("line")..].ToString().IndexOf("\r\n")].Replace("line ", ""));
    }
}