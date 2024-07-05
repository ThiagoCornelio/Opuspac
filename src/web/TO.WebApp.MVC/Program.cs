using Microsoft.AspNetCore.Mvc;
using TO.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services
    .AddIdentityConfiguration()
    .AddMvcConfiguration(builder.Configuration)
    .RegisterServices(builder.Configuration);

//Ignorar o ModelStateValidation Automatico.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();
app.UseMvcConfiguration(app.Environment);

app.Run();
