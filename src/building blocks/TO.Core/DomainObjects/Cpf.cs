using TO.Core.DomainObjects.Validation;

namespace TO.Core.DomainObjects;

public class Cpf
{
    public const int CpfMaxLength = 11;
    public string Numero { get; private set; }

    protected Cpf() { }

    public Cpf(string numero)
    {
        if (!CpfValidation.Validar(numero)) throw new DomainException("CPF inválido");
        Numero = numero;
    }
}