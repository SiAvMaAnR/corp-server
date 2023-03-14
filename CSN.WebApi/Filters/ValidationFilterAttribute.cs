using CSN.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CSN.WebApi.Filters;

public class ValidationFilterAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            throw new BadRequestException("Invalid input data");
        }
    }
    public void OnActionExecuted(ActionExecutedContext context) { }
}
