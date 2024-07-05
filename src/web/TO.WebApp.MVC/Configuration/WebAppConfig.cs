using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using TO.WebApp.MVC.Extensions;
using System.Globalization;

namespace TO.WebApp.MVC.Configuration;

public static class WebAppConfig
{
    public static IServiceCollection AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();
        services.Configure<AppSettings>(configuration);
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        return services;
    }

    public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler("/erro/500"); //Caso um erro passe, mesmo com a validação
        app.UseStatusCodePagesWithRedirects("/erro/{0}");
        app.UseHsts();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseIdentityConfiguration();

        var suportedCultures = new[] { new CultureInfo("pt-BR") };

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("pt-BR"),
            SupportedCultures = suportedCultures,
            SupportedUICultures = suportedCultures,
        });

        app.UseMiddleware<ExceptionMiddleware>();//Todo o request irá passar por aqui. evita que fique colocando try catch na aplicação inteira

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        return app;
    }
}