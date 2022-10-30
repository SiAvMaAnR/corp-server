using CSN.WebApi.Extensions.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet("Development")]
        public IActionResult HandleErrorDevelopment()
        {
            IExceptionHandlerFeature? exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            Exception exception = exceptionHandlerFeature.Error;

            if (exception is BadRequestException) return BadRequest(exception.Message);
            if (exception is UnauthorizedException) return Unauthorized(exception.Message);
            if (exception is NotFoundException) return NotFound(exception.Message);
            if (exception is ForbiddenException) return Forbid();
            return BadRequest("Unknown error");
        }


        [HttpGet("Production")]
        public IActionResult HandleErrorProduction()
        {
            IExceptionHandlerFeature? exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            Exception exception = exceptionHandlerFeature.Error;

            var errorInfo = new
            {
                exception.Message,
                exception.StackTrace,
                exception.InnerException,
                exception.Source,
            };

            if (exception is BadRequestException) return BadRequest(errorInfo);
            if (exception is UnauthorizedException) return Unauthorized(errorInfo);
            if (exception is NotFoundException) return NotFound(errorInfo);
            if (exception is ForbiddenException) return Forbid();
            return BadRequest("Unknown error");
        }
    }
}
