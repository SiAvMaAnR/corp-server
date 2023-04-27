using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CSN.WebApi.Filters
{
    public class AntiDuplicateFilterAttribute : IActionFilter
    {
        private const string AntiDuplicateFormCookieName = "AntiDuplicateFormCookie";
        private const string AntiDuplicateFormHeaderName = "X-AntiDuplicateForm";

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpMethods.IsPost(context.HttpContext.Request.Method))
            {
                var antiDuplicateFormValue = Guid.NewGuid().ToString();

                context.HttpContext.Response.Cookies.Append(AntiDuplicateFormCookieName, antiDuplicateFormValue, new CookieOptions
                {
                    HttpOnly = true
                });

                context.HttpContext.Request.Headers.Add(AntiDuplicateFormHeaderName, antiDuplicateFormValue);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (HttpMethods.IsPost(context.HttpContext.Request.Method))
            {
                context.HttpContext.Response.Cookies.Delete(AntiDuplicateFormCookieName);
            }
        }
    }
}