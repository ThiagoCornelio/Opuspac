using Microsoft.Extensions.DependencyInjection;

namespace TO.MessageBus;

public static class DependencyInjectionExtensions //Para extender e utilizar nas outras APIs
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
    {
        if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

        services.AddSingleton<IMessageBus>(new MessageBus(connection));

        return services;
    }
}
