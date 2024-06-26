﻿using CSN.SignalR.Hubs;
using CSN.WebApi.Middlewares;

namespace CSN.WebApi.Config.ApplicationConfigurations
{
    public static class ApplicationExtension
    {
        public static void ProductionConfiguration(this WebApplication webApplication)
        {
            webApplication.UseExceptionHandler(new ExceptionHandlerOptions()
            {
                AllowStatusCode404Response = true,
                ExceptionHandlingPath = "/Production"
            });
        }

        public static void DevelopmentConfiguration(this WebApplication webApplication)
        {
            webApplication.UseExceptionHandler(new ExceptionHandlerOptions()
            {
                AllowStatusCode404Response = true,
                ExceptionHandlingPath = "/Development"
            });
            webApplication.UseSwagger();
            webApplication.UseSwaggerUI();
        }

        public static void CommonConfiguration(this WebApplication webApplication)
        {
            webApplication.UseMiddleware<TimingMiddleware>();
            webApplication.UseCors("CorsPolicy");
            webApplication.UseHttpsRedirection();
            webApplication.UseRouting();
            webApplication.UseAuthentication();
            webApplication.UseAuthorization();
            webApplication.MapControllers();
        }


        public static void HubsConfiguration(this WebApplication webApplication)
        {
            webApplication.MapHub<ChatHub>("/chat");
            webApplication.MapHub<StateHub>("/state");
            webApplication.MapHub<NotificationHub>("/notifications");
        }

        public static void AddConfiguration(this WebApplicationBuilder? webApplicationBuilder)
        {
            webApplicationBuilder?.WebHost.UseKestrel(options => options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(1));
        }
    }
}
