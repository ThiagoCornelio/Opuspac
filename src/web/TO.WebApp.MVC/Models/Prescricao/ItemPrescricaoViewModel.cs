using System.ComponentModel;

namespace TO.WebApp.MVC.Models.Prescricao;

public class ItemPrescricaoViewModel
{
    public ItemPrescricaoViewModel()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public Guid MedicamentoId { get; set; }
    [DisplayName("Nome Medicamento")]
    public string? Nome { get; set; }
    public int Quantidade { get; set; }

    [DisplayName("Intervalo da Medicação em Horas")]
    public int? IntervaloHoras { get; set; }
    public string? QRCode { get; set; }
}
