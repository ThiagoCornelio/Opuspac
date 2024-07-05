using TO.Medicamentos.API.Data;
using TO.Medicamentos.API.Data.Repository;

namespace TO.Medicamentos.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMedicamentoRepository, MedicamentoRepository>();
        services.AddScoped<MedicamentosContext>();

        return services;
    }
}