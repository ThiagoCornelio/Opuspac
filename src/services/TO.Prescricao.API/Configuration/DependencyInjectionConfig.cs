using TO.Prescricao.API.Data;
using TO.WebAPI.Core.Usuario;

namespace TO.Prescricao.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();
        services.AddScoped<PrescricaoContext>();
        return services;
    }
}
