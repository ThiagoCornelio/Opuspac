using TO.WebApp.MVC.Services;
using TO.WebApp.MVC.Services.Handlers;
using TO.WebAPI.Core.Usuario;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using TO.WebApp.MVC.Extensions.Attribute;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace TO.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        #region HttpServices

        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IAutenticacaoService, AutenticacaoService>()
            .AddPolicyHandler(PollyExtensions.EsperarTentar())
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddHttpClient<IPrescricaoService, PrescricaoService>()
               .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
               .AddPolicyHandler(PollyExtensions.EsperarTentar())
               .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddHttpClient<IMedicamentoService, MedicamentoService>()
                 .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                 .AddPolicyHandler(PollyExtensions.EsperarTentar())
                 .AddTransientHttpErrorPolicy(
                     p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        //services.AddHttpClient<ICatalogoService, CatalogoService>()
        //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
        //    .AddPolicyHandler(PollyExtensions.EsperarTentar())
        //    .AddTransientHttpErrorPolicy(
        //        p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        //services.AddHttpClient<IComprasBffService, ComprasBffService>()
        //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
        //    .AddPolicyHandler(PollyExtensions.EsperarTentar())
        //    .AddTransientHttpErrorPolicy(
        //        p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        #endregion
    }
}

#region PollyExtension

public static class PollyExtensions //São projetos diferentes, por esse motivo será mantido.
{
    public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
    {
        var retry = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10),
            }, (outcome, timespan, retryCount, context) =>
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Tentando pela {retryCount} vez!");
                Console.ForegroundColor = ConsoleColor.White;
            });

        return retry;
    }
}

#endregion