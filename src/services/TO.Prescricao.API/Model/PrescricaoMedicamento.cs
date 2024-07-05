using FluentValidation;
using System.Text.Json.Serialization;

namespace TO.Prescricao.API.Model;

public class PrescricaoMedicamento
{
    public PrescricaoMedicamento()
    {
        Id = Guid.NewGuid();
    }

    public Guid? Id { get; set; }
    public Guid MedicamentoId { get; set; }
    public string? Nome { get; set; }
    public int Quantidade { get; set; }
    public Guid PrescricaoId { get; set; }
    public string? QRCode { get; set; } 
    public int? IntervaloHoras { get; set; }

    [JsonIgnore]
    public PrescricaoPaciente? PrescricaoPaciente { get; set; }

    internal void AssociarPrescricao(Guid prescricaoId) => PrescricaoId = prescricaoId;
    internal void AdicionarUnidades(int unidades) => Quantidade += unidades;
    internal void AtualizarUnidades(int unidades) => Quantidade = unidades;

    internal bool EhValido() => new ItemPrescricaoValidation().Validate(this).IsValid;
    public class ItemPrescricaoValidation : AbstractValidator<PrescricaoMedicamento>
    {
        public ItemPrescricaoValidation()
        {
            RuleFor(c => c.MedicamentoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do medicamento inválido");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do medicamento não foi informado");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage(item => $"A quantidade miníma para o item é 1");

            RuleFor(c => c.Quantidade)
                .LessThanOrEqualTo(PrescricaoPaciente.MAX_QUANTIDADE_ITEM)
                .WithMessage(item => $"A quantidade máxima do item é {PrescricaoPaciente.MAX_QUANTIDADE_ITEM}");
        }
    }
}
