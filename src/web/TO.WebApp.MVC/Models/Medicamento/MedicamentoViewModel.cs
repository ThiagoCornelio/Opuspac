using System.ComponentModel.DataAnnotations;

namespace TO.WebApp.MVC.Models.Medicamento;

public class MedicamentoViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public int QuantidadeEstoque { get; set; }
    public int IntervaloHoras { get; set; }
}
