using CSN.Domain.Exceptions;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CSN.WebApi.Filters;

public class ValidationAntiForgeryTokenAttribute : IAsyncActionFilter
{
    private readonly IAntiforgery antiforgery;

    public ValidationAntiForgeryTokenAttribute(IAntiforgery antiforgery)
    {
        this.antiforgery = antiforgery;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        try
        {
            await this.antiforgery.ValidateRequestAsync(context.HttpContext);
        }
        catch (Exception)
        {
            string message = "The CSRF token is missing or does not match the expected value. Please refresh the page and try again.";
            throw new BadRequestException(message);
        }


        await next.Invoke();
    }
}
