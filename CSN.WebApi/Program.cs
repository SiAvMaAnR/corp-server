using CSN.WebApi.Extensions.ApplicationConfigurations;
using CSN.WebApi.Extensions.ServiceConfigurations;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.AddConfiguration();
builder.Services.AddCommonDependencies(config);
builder.Services.AddTransientDependencies();
builder.Services.AddScopedDependencies();
builder.Services.AddSingletonDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.DevelopmentConfiguration();
else
    app.ProductionConfiguration();

app.CommonConfiguration();
app.HubsConfiguration();
app.Run();
