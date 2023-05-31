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
        if (context.HttpContext.Request.Method == "POST")
        {
            if (await this.antiforgery.IsRequestValidAsync(context.HttpContext))
            {
                await this.antiforgery.ValidateRequestAsync(context.HttpContext);
            }
            else
            {
                throw new BadRequestException("Invalid request");
            }
        }
        await next.Invoke();
    }

    public async Task OnActionExecutedAsync(ActionExecutedContext context)
    {
        if (context.HttpContext.Request.Method == "GET")
        {
            var tokens = this.antiforgery.GetAndStoreTokens(context.HttpContext);
            if (tokens?.RequestToken != null)
            {
                context.HttpContext.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions
                {
                    HttpOnly = false
                });
            }
        }
        await Task.CompletedTask;
    }
}
