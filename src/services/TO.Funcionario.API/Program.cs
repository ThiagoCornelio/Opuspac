using TO.Funcionarios.API.Configuration;
using TO.WebAPI.Core.Identidade;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
builder.Configuration.AddEnvironmentVariables();

//Contexto
builder.Services
    .AddApiConfiguration(builder.Configuration)
    .AddJwtConfiguration(builder.Configuration)
    .AddSwaggerConfiguration()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
    .RegisterServices()
    .AddMessageBusConfiguration(builder.Configuration);

var app = builder.Build();
app.UseSwaggerConfiguration()
   .UseApiConfiguration(builder.Environment);

app.Run();
