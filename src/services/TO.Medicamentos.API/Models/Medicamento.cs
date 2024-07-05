using TO.Core.DomainObjects;

namespace TO.Medicamentos.API.Models;

public class Medicamento : Entity, IAggregateRoot
{
    public string Nome { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public int QuantidadeEstoque { get; set; }
    public int IntervaloHoras { get; set; }
}
