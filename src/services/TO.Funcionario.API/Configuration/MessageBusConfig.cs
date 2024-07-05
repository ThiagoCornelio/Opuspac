using TO.Core.Utils;
using TO.Funcionarios.API.Services;
using TO.MessageBus;

namespace TO.Funcionarios.API.Configuration;

public static class MessageBusConfig
{
    public static IServiceCollection AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus") ?? string.Empty)
            .AddHostedService<RegistroFuncionarioIntegrationHandler>();
        return services;
    }
}