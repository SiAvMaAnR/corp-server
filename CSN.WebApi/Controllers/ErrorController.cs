using CSN.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("Production")]
        public IActionResult HandleErrorProduction()
        {
            IExceptionHandlerFeature? exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            Exception exception = exceptionHandlerFeature.Error;

            return exception switch
            {
                BadRequestException => BadRequest(exception.Message),
                UnauthorizedException => Unauthorized(exception.Message),
                NotFoundException => NotFound(exception.Message),
                ForbiddenException => Forbid(),
                _ => BadRequest(exception.Message)
            };
        }


        [Route("Development")]
        public IActionResult HandleErrorDevelopment()
        {
            IExceptionHandlerFeature? exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            Exception exception = exceptionHandlerFeature.Error;

            var errorInfo = new
            {
                exception.Message,
                exception.Source,
                exception.StackTrace,
                exception.InnerException,
                exception.Data,
                exception.HelpLink,
                exception.HResult
            };

            return exception switch
            {
                BadRequestException => BadRequest(errorInfo),
                UnauthorizedException => Unauthorized(errorInfo),
                NotFoundException => NotFound(errorInfo),
                ForbiddenException => Forbid(),
                _ => BadRequest(errorInfo)
            };
        }
    }
}
