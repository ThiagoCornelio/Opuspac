using FluentValidation.Results;
using MediatR;
using TO.Core.Mediator;
using TO.Funcionarios.API.Application.Commands;
using TO.Funcionarios.API.Application.Events;
using TO.Funcionarios.API.Data;
using TO.Funcionarios.API.Data.Repository;

namespace TO.Funcionarios.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegistrarFuncionarioCommand, ValidationResult>, FuncionarioCommandHandler>();

        services.AddScoped<INotificationHandler<FuncionarioRegistradoEvent>, FuncionarioEventHandler>();

        services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
        services.AddScoped<FuncionariosContext>();

        return services;
    }
}
