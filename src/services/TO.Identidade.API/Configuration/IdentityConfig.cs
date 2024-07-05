using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TO.Identidade.API.Data;
using TO.Identidade.API.Extensions;
using TO.WebAPI.Core.Identidade; 

namespace TO.Identidade.API.Configuration;

public static class IdentityConfig
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddDbContext<IdentidadeContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddErrorDescriber<IdentityMensagensPortugues>()
            .AddEntityFrameworkStores<IdentidadeContext>() 
            .AddDefaultTokenProviders();

        services.AddJwtConfiguration(configuration);

        return services;
    }
}