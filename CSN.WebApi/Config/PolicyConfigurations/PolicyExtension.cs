using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace CSN.WebApi.Config.PolicyConfigurations
{
    public static class PolicyExtension
    {
        public static void AuthConfig(this AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.AddPolicy("OnlyCompany", policy => policy
                .RequireRole("Company"));
            authorizationOptions.AddPolicy("OnlyEmployee", policy => policy
                .RequireRole("Employee"));
            authorizationOptions.AddPolicy("AccessToAll", policy => policy
                .RequireRole("Employee")
                .RequireRole("Company"));
        }

        public static void CorsConfig(this CorsOptions corsOptions)
        {
            corsOptions.AddPolicy("CorsPolicy", policy => policy
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:3000", "https://localhost:3000"));
            // .AllowAnyOrigin());

        }
    }
}
