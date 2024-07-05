namespace TO.Core.Messages.Integration;

public class UsuarioRegistradoIntegrationEvent : IntegrationEvent
{
    //Usuário já registrado.
    public UsuarioRegistradoIntegrationEvent(Guid id, string nome, string cpf, string email)
    {
        Id = id;
        Nome = nome;
        Cpf = cpf;
        Email = email;
    }

    public Guid Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public string Cpf { get; private set; } = null!;
    public string Email { get; private set; } = null!;
}
