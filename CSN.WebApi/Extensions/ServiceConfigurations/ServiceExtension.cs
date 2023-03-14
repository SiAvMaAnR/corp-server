using CSN.Application.Services;
using CSN.Application.Services.Interfaces;
using CSN.Domain.Entities.Attachments;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Invitations;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.Extensions;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories;
using CSN.Persistence.UnitOfWork;
using CSN.WebApi.Extensions.PolicyConfigurations;
using CSN.WebApi.Extensions.SwaggerConfigurations;
using CSN.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using CSN.Application.AppData;
using CSN.Application.AppData.Interfaces;

namespace CSN.WebApi.Extensions.ServiceConfigurations
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddTransientDependencies(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        public static IServiceCollection AddScopedDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<ICompanyService, CompanyService>();
            serviceCollection.AddScoped<IEmployeeService, EmployeeService>();
            serviceCollection.AddScoped<IEmployeeControlService, EmployeeControlService>();
            serviceCollection.AddScoped<IChannelService, ChannelService>();
            serviceCollection.AddScoped<IInvitationService, InvitationService>();
            return serviceCollection;
        }

        public static IServiceCollection AddSingletonDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAppData, AppData>();
            return serviceCollection;
        }


        public static IServiceCollection AddCommonDependencies(this IServiceCollection serviceCollection, ConfigurationManager config)
        {
            string connection = config?.GetConnectionString("DefaultConnection") ?? "";
            serviceCollection.AddDbContext<EFContext>(options => options.UseSqlServer(connection));
            serviceCollection.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            serviceCollection.AddControllers(config =>
            {
                config.Filters.Add(new ValidationFilterAttribute());
            }).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddLogging();
            serviceCollection.AddCors(options => options.CorsConfig());
            serviceCollection.AddAuthorization(options => options.AuthConfig());
            serviceCollection.AddSwaggerGen(options => options.Config());
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.Config(config));
            serviceCollection.AddDataProtection();
            serviceCollection.AddSignalR();
            return serviceCollection;
        }
    }
}