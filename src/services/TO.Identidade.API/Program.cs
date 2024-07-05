using TO.Identidade.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
builder.Configuration.AddEnvironmentVariables();

// ConfigureServices
builder.Services
    .AddIdentityConfiguration(builder.Configuration)
    .AddApiConfiguration()
    .AddSwaggerConfiguration()
    .AddMessageBusConfiguration(builder.Configuration);

var app = builder.Build();
app
    .UseSwaggerConfiguration()
    .UseApiConfiguration(app.Environment);

app.Run();
