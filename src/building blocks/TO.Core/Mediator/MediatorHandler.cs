using FluentValidation.Results;
using TO.Core.Messages;
using MediatR;

namespace TO.Core.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ValidationResult> EnviarComando<T>(T comando) where T : Command 
        => await _mediator.Send(comando);

    public async Task PublicarEvento<T>(T evento) where T : Event 
        => await _mediator.Publish(evento);
}