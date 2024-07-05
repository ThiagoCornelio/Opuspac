using MediatR;

namespace TO.Funcionarios.API.Application.Events;

public class FuncionarioEventHandler : INotificationHandler<FuncionarioRegistradoEvent>
{
    public Task Handle(FuncionarioRegistradoEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}