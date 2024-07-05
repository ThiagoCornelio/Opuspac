using TO.Medicamentos.API.Configuration;
using TO.WebAPI.Core.Identidade;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
builder.Configuration.AddEnvironmentVariables();

// ConfigureServices
builder.Services
    .AddApiConfiguration(builder.Configuration)
    .AddJwtConfiguration(builder.Configuration)
    .AddSwaggerConfiguration()
    .RegisterServices();

var app = builder.Build();
app
    .UseSwaggerConfiguration()
    .UseApiConfiguration(app.Environment);

app.Run();
