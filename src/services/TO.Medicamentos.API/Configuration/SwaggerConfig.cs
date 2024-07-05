using Microsoft.OpenApi.Models;

namespace TO.Medicamentos.API.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Teste Opuspac Medicamentos API",
                Description = "Teste Opuspac",
                Contact = new OpenApiContact() { Name = "Thiago Cornélio", Email = "thiago.cornelio@outlook.com.br" },
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });

        return app;
    }
}