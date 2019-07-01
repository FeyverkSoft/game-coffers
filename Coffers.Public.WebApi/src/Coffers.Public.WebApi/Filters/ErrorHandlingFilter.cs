using System.Net;
using System.Threading.Tasks;
using Coffers.Public.WebApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Coffers.Public.WebApi.Filters
{
    public class ErrorHandlingFilter : IAsyncActionFilter
    {
        private readonly ILoggerFactory _loggerFactory;

        public ErrorHandlingFilter(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var execution = await next();
            if (execution.Exception != null && !execution.ExceptionHandled)
            {
                var logger = _loggerFactory.CreateLogger(context.ActionDescriptor.RouteValues["controller"]);
                logger.LogError(execution.Exception, string.Empty);

                var resp = new ProblemDetails
                {
                    Type = ErrorCodes.InternalServerError,
                    Status = (int)HttpStatusCode.InternalServerError,
#if DEBUG
                    Detail = execution.Exception.Message,
#else
                    Detail = "internal server error"
#endif
                };

                resp.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                execution.ExceptionHandled = true;

                if (execution.Exception is ApiException apiException)
                {
                    resp.Type = apiException.Code;
                    resp.Detail = apiException.Message;
                    resp.Status = apiException.GetHttpStatusCode();

                    if (apiException.Fields != null)
                        foreach (var apiExceptionField in apiException.Fields.Keys)
                        {
                            resp.Extensions.Add(apiExceptionField, apiException.Fields[apiExceptionField]);
                        }

                    execution.Result = new ObjectResult(resp)
                    {
                        StatusCode = apiException.GetHttpStatusCode()
                    };

                    return;
                }

                execution.Result = new ObjectResult(resp)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}