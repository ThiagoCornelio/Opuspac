using TO.Prescricao.API.Configuration;
using TO.WebAPI.Core.Identidade;

var builder = WebApplication.CreateBuilder(args);

//Contexto
builder.Services
    .AddApiConfiguration(builder.Configuration)
    .AddJwtConfiguration(builder.Configuration)
    .AddSwaggerConfiguration()
    .RegisterServices();

var app = builder.Build();
app.UseSwaggerConfiguration()
   .UseApiConfiguration(builder.Environment);

app.Run();