using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TO.WebApp.MVC.Models.Medicamento;

namespace TO.WebApp.MVC.Models.Prescricao;

public class PrescricaoViewModel
{
    public Guid Id { get; set; }

    [DisplayName("Nome Paciente")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string NomePaciente { get; set; } = null!;

    [DisplayName("Nome Médico")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string NomeMedico { get; set; } = null!;

    public string? Image { get; set; }

    public IEnumerable<MedicamentoViewModel>? MedicamentoList { get; set; } //Apenas para montar a lista na tela dinamicamente.
    public IEnumerable<ItemPrescricaoViewModel>? Itens { get; set; }
    public IEnumerable<ItemPrescricaoViewModel> ItensNovos { get; set; } = new List<ItemPrescricaoViewModel>();
}
