namespace CSN.WebApi.Extensions.ApplicationConfigurations
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
            webApplication.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            webApplication.UseHttpsRedirection();
            webApplication.UseRouting();
            webApplication.UseAuthentication();
            webApplication.UseAuthorization();
            webApplication.UseHttpsRedirection();
            webApplication.MapControllers();
        }

        public static void AddConfiguration(this WebApplicationBuilder? webApplicationBuilder)
        {
            webApplicationBuilder?.WebHost.UseKestrel(options => options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(1));
        }
    }
}
