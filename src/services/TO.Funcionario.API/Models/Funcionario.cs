using TO.Core.DomainObjects;

namespace TO.Funcionarios.API.Models;

public class Funcionario : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public Cpf Cpf { get; private set; }
    public bool Excluido { get; private set; }

    // EF Relation
    protected Funcionario() { }

    public Funcionario(Guid id, string nome, string email, string cpf)
    {
        Id = id;
        Nome = nome;
        Email = new Email(email);
        Cpf = new Cpf(cpf);
        Excluido = false;
    }

    public void TrocarEmail(string email)
    {
        Email = new Email(email);
    }
}