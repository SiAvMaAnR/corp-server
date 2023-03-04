using CSN.Domain.Shared.Extensions.Exceptions;
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

            return exception is BadRequestException
                ? BadRequest(exception.Message)
                : exception is UnauthorizedException
                ? Unauthorized(exception.Message)
                : exception is NotFoundException
                ? NotFound(exception.Message)
                : exception is ForbiddenException ? Forbid() : BadRequest(exception.Message);
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

            return exception is BadRequestException
                ? BadRequest(errorInfo)
                : exception is UnauthorizedException
                ? Unauthorized(errorInfo)
                : exception is NotFoundException ? NotFound(errorInfo) : exception is ForbiddenException ? Forbid() : BadRequest(errorInfo);
        }
    }
}
