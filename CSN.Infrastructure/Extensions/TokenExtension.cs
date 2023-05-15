using CSN.Domain.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CSN.Infrastructure.Extensions;

public static class TokenExtension
{
    public static void Config(this JwtBearerOptions options, ConfigurationManager? config)
    {
        if (config == null) throw new BadRequestException("Incorrect config");

        string secretKey = config.GetSection("Authorization:SecretKey").Value
            ?? throw new BadRequestException("Incorrect secretKey");

        // options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = config.GetSection("Authorization:Issuer").Value,
            ValidAudience = config.GetSection("Authorization:Audience").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
                 (expires != null) ? DateTime.UtcNow < expires : false
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    }
}
