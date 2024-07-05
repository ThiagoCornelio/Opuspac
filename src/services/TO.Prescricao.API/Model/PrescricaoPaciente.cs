using FluentValidation;
using FluentValidation.Results;

namespace TO.Prescricao.API.Model;

public class PrescricaoPaciente
{
    internal const int MAX_QUANTIDADE_ITEM = 5;

    public Guid Id { get; set; }
    public string NomePaciente { get; set; }
    public string NomeMedico { get; set; }

    public List<PrescricaoMedicamento> Itens { get; set; } = new();
    public ValidationResult ValidationResult { get; set; }

    public PrescricaoPaciente(Guid clienteId)
    {
        Id = Guid.NewGuid();
    }
    public PrescricaoPaciente() { }

    internal bool PrescricaoItemExistente(PrescricaoMedicamento item) => Itens.Any(p => p.MedicamentoId == item.MedicamentoId);
    internal PrescricaoMedicamento ObterPorMedicamentoId(Guid medicamentoId) => Itens.FirstOrDefault(p => p.MedicamentoId == medicamentoId);
    internal void AdicionarItem(PrescricaoMedicamento item)
    {
        item.AssociarPrescricao(Id);

        if (PrescricaoItemExistente(item))
        {
            var itemExistente = ObterPorMedicamentoId(item.MedicamentoId);
            itemExistente.AdicionarUnidades(item.Quantidade);

            item = itemExistente;
            Itens.Remove(itemExistente);
        }

        Itens.Add(item);
    }
    internal void AtualizarItem(PrescricaoMedicamento item)
    {
        item.AssociarPrescricao(Id);

        var itemExistente = ObterPorMedicamentoId(item.MedicamentoId);

        Itens.Remove(itemExistente);
        Itens.Add(item);
    }

    internal void AtualizarUnidades(PrescricaoMedicamento item, int unidades)
    {
        item.AtualizarUnidades(unidades);
        AtualizarItem(item);
    }

    internal void RemoverItem(PrescricaoMedicamento item)
    {
        Itens.Remove(ObterPorMedicamentoId(item.MedicamentoId));
    }

    internal bool EhValido()
    {
        var erros = Itens.SelectMany(i => new PrescricaoMedicamento.ItemPrescricaoValidation().Validate(i).Errors).ToList();
        erros.AddRange(new PrescricaoPacienteValidation().Validate(this).Errors);
        ValidationResult = new ValidationResult(erros);

        return ValidationResult.IsValid;
    }

    public class PrescricaoPacienteValidation : AbstractValidator<PrescricaoPaciente>
    {
        public PrescricaoPacienteValidation()
        {
            RuleFor(c => c.Itens.Count)
                .GreaterThan(0)
                .WithMessage("O carrinho não possui itens");

            RuleFor(c => c.NomePaciente)
                .NotEmpty()
                .WithMessage("O nome do medicamento não foi informado");

            RuleFor(c => c.NomeMedico)
              .NotEmpty()
              .WithMessage("O nome do medicamento não foi informado");
        }
    }

}
