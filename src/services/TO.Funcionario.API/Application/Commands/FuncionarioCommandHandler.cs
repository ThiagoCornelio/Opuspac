using FluentValidation.Results;
using MediatR;
using TO.Core.Messages;
using TO.Funcionarios.API.Application.Events;
using TO.Funcionarios.API.Data.Repository;
using TO.Funcionarios.API.Models;

namespace TO.Funcionarios.API.Application.Commands;

public class FuncionarioCommandHandler : CommandHandler,
    IRequestHandler<RegistrarFuncionarioCommand, ValidationResult>
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public FuncionarioCommandHandler(IFuncionarioRepository funcionarioRepository)
        => _funcionarioRepository = funcionarioRepository;

    public async Task<ValidationResult> Handle(RegistrarFuncionarioCommand message, CancellationToken cancellationToken)
    {
        if (!message.EhValido()) return message.ValidationResult;

        var funcionario = new Funcionario(message.Id, message.Nome, message.Email, message.Cpf);

        var FuncionarioExistente = await _funcionarioRepository.ObterPorCpf(funcionario.Cpf.Numero);

        if (FuncionarioExistente != null)
        {
            AdicionarErro("Este CPF já está em uso.");
            return ValidationResult;
        }

        _funcionarioRepository.Adicionar(funcionario);

        funcionario.AdicionarEvento(new FuncionarioRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));

        return await PersistirDados(_funcionarioRepository.UnitOfWork);
    }
}
