using TO.Core.DomainObjects.Validation;

namespace TO.Core.DomainObjects;

public class Email
{
    public const int EnderecoMaxLength = 254;
    public const int EnderecoMinLength = 5;
    public string Endereco { get; private set; }

    protected Email() { }

    public Email(string endereco)
    {
        if (!EmailValidation.Validar(endereco)) throw new DomainException("E-mail inválido");
        Endereco = endereco;
    }
}