using Microsoft.AspNetCore.Authorization;

namespace CSN.WebApi.Extensions.PolicyConfigurations
{
    public static class PolicyExtension
    {
        public static void Config(this AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.AddPolicy("OnlyCompany", policy => policy
                .RequireRole("Company"));
            authorizationOptions.AddPolicy("OnlyEmployee", policy => policy
                .RequireRole("Employee"));
            authorizationOptions.AddPolicy("AccessToAll", policy => policy
                .RequireRole("Employee")
                .RequireRole("Company"));
        }
    }
}
