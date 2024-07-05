using FluentValidation;
using TO.Core.DomainObjects.Validation;
using TO.Core.Messages;

namespace TO.Funcionarios.API.Application.Commands;

public class RegistrarFuncionarioCommand : Command
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public RegistrarFuncionarioCommand(Guid id, string nome, string email, string cpf)
    {
        AggregateId = id;
        Id = id;
        Nome = nome;
        Email = email;
        Cpf = cpf;
    }

    public override bool EhValido()
    {
        ValidationResult = new RegistrarFuncionarioValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class RegistrarFuncionarioValidation : AbstractValidator<RegistrarFuncionarioCommand>
    {
        public RegistrarFuncionarioValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do Funcionário inválido");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do Funcionário não foi informado");

            RuleFor(c => c.Cpf)
                .Must(TerCpfValido)
                .WithMessage("O CPF informado não é válido.");

            RuleFor(c => c.Email)
                .Must(TerEmailValido)
                .WithMessage("O E-Mail informado não é válido.");
        }

        protected static bool TerCpfValido(string cpf)
        {
            return CpfValidation.Validar(cpf);
        }

        protected static bool TerEmailValido(string email)
        {
            return EmailValidation.Validar(email);
        }
    }
}