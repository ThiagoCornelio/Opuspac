using TO.MessageBus;
using TO.Core.Utils;


namespace TO.Identidade.API.Configuration;

public static class MessageBusConfig
{
    public static IServiceCollection AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
        return services;
    }
}
