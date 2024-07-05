using FluentValidation.Results;
using TO.Core.Mediator;
using TO.Core.Messages.Integration;
using TO.Funcionarios.API.Application.Commands;
using TO.MessageBus;

namespace TO.Funcionarios.API.Services;

public class RegistroFuncionarioIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public RegistroFuncionarioIntegrationHandler(
                        IServiceProvider serviceProvider,
                        IMessageBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
    }

    private void SetResponder()
    {
        _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
            await RegistrarFuncionario(request));

        _bus.AdvancedBus.Connected += OnConnect;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        return Task.CompletedTask;
    }

    private void OnConnect(object s, EventArgs e)
    {
        SetResponder();
    }

    private async Task<ResponseMessage> RegistrarFuncionario(UsuarioRegistradoIntegrationEvent message)
    {
        var FuncionarioCommand = new RegistrarFuncionarioCommand(message.Id, message.Nome, message.Email, message.Cpf);
        ValidationResult sucesso;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            sucesso = await mediator.EnviarComando(FuncionarioCommand);
        }

        return new ResponseMessage(sucesso);
    }
}
