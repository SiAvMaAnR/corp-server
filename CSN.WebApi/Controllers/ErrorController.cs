using CSN.WebApi.Extensions.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
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

            if (exception is BadRequestException) return BadRequest(exception.Message);
            if (exception is UnauthorizedException) return Unauthorized(exception.Message);
            if (exception is NotFoundException) return NotFound(exception.Message);
            if (exception is ForbiddenException) return Forbid();
            return BadRequest(exception.Message);
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

            if (exception is BadRequestException) return BadRequest(errorInfo);
            if (exception is UnauthorizedException) return Unauthorized(errorInfo);
            if (exception is NotFoundException) return NotFound(errorInfo);
            if (exception is ForbiddenException) return Forbid();
            return BadRequest(errorInfo);
        }
    }
}
