using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TO.Prescricao.API.Data;
using TO.Prescricao.API.Model;
using TO.WebAPI.Core.Controllers;
using TO.WebAPI.Core.Usuario;

namespace TO.Prescricao.API.Controllers;

public class PrescricaoController : MainController
{
    private readonly IAspNetUser _user;
    private readonly PrescricaoContext _context;

    public PrescricaoController(IAspNetUser user, PrescricaoContext context)
    {
        _user = user;
        _context = context;
    }

    [HttpGet("prescricao/items/{prescricaoId:guid}")]
    public async Task<List<PrescricaoMedicamento>> ObterItensPorPrescricaoId(Guid prescricaoId)
        => await _context.PrescricaoMedicamentos.Where(x => x.PrescricaoId == prescricaoId).ToListAsync();

    [HttpGet("prescricao")]
    public async Task<List<PrescricaoPaciente>> ObterTodos()
       => await _context.PrescricaoPaciente.Include(c => c.Itens).ToListAsync();

    [HttpGet("prescricao/{prescricaoId}")]
    public async Task<PrescricaoPaciente> ObterPrescricaoId(Guid prescricaoId)
            => await ObterPrescricaoPaciente(prescricaoId);

    [HttpPost("prescricao/criar-prescricao")]
    public async Task<PrescricaoPaciente> CriarPrescricao(PrescricaoPaciente prescricao)
    {
        _context.PrescricaoPaciente.Add(prescricao);

        await PersistirDados();

        return prescricao;
    }

    [HttpDelete("prescricao/excluir/{prescricaoId:guid}")]
    public async Task<IActionResult> RemoverPrescricao(Guid prescricaoId)
    {
        var prescricao = await _context.PrescricaoPaciente.FirstOrDefaultAsync(x=>x.Id ==prescricaoId);

        var prescricaoMedicamentos = _context.PrescricaoMedicamentos.Where(x => x.PrescricaoId == prescricaoId);
        _context.PrescricaoPaciente.Remove(prescricao);
        _context.PrescricaoMedicamentos.RemoveRange(prescricaoMedicamentos);

        await PersistirDados();

        return CustomResponse();
    }

    #region Adicionar Medicamento na prescrição já criada.

    [HttpPost("prescricao/")]
    public async Task<IActionResult> AdicionarItemPrescricao(PrescricaoPaciente prescricao)
    {
        foreach (var item in prescricao.Itens)
        {
            var medicamento = await ObterPrescricaoPaciente(prescricao.Id);
            ManipularPrescricaoExistente(medicamento, item);
            await PersistirDados();
        }
        var prescricaoDomain = await ObterPrescricaoId(prescricao.Id);
        prescricaoDomain.Itens = null;

        prescricaoDomain.NomeMedico = prescricao.NomeMedico;
        prescricaoDomain.NomePaciente = prescricao.NomePaciente;

        _context.PrescricaoPaciente.Update(prescricaoDomain);
        await PersistirDados();

        //ValidarPrescricao(prescricao);

        if (!OperacaoValida()) return CustomResponse();

        return CustomResponse();
    }

    [HttpPut("prescricao/{prescricaoId:guid}")]
    public async Task<IActionResult> AtualizarItemPrescricao(Guid prescricaoId, PrescricaoMedicamento item)
    {
        var prescricao = await ObterPrescricaoPaciente(prescricaoId);
        var itemPrescricao = await ObterItemPrescricaoValidada(item.MedicamentoId, prescricao, item);
        if (itemPrescricao == null) return CustomResponse();

        prescricao.AtualizarUnidades(itemPrescricao, item.Quantidade);

        ValidarPrescricao(prescricao);
        if (!OperacaoValida()) return CustomResponse();

        _context.PrescricaoMedicamentos.Update(itemPrescricao);
        _context.PrescricaoPaciente.Update(prescricao);

        await PersistirDados();
        return CustomResponse();
    }

    [HttpDelete("prescricao/{prescricaoId:guid}/{medicamentoId:guid}")]
    public async Task<IActionResult> RemoverItemPrescricao(Guid prescricaoId, Guid medicamentoId)
    {
        var prescricao = await ObterPrescricaoPaciente(prescricaoId);

        var itemPrescricao = await ObterItemPrescricaoValidada(medicamentoId, prescricao);
        if (itemPrescricao == null) return CustomResponse();

        ValidarPrescricao(prescricao);
        if (!OperacaoValida()) return CustomResponse();

        prescricao.RemoverItem(itemPrescricao);

        _context.PrescricaoMedicamentos.Remove(itemPrescricao);
        _context.PrescricaoPaciente.Update(prescricao);

        await PersistirDados();
        return CustomResponse();
    }

    private async Task<PrescricaoPaciente> ObterPrescricaoPaciente(Guid prescricaoId)
         => await _context.PrescricaoPaciente
             .Include(c => c.Itens)
             .FirstOrDefaultAsync(c => c.Id == prescricaoId) ?? new PrescricaoPaciente();

    private void ManipularPrescricaoExistente(PrescricaoPaciente prescricao, PrescricaoMedicamento item)
    {
        var medicamentoItemExistente = prescricao.PrescricaoItemExistente(item);

        prescricao.AdicionarItem(item);
        ValidarPrescricao(prescricao);

        if (medicamentoItemExistente)
        {
            var teste = prescricao.ObterPorMedicamentoId(item.MedicamentoId);
            teste.PrescricaoPaciente = null;

            _context.PrescricaoMedicamentos.Update(teste);
        }
        else
        {
            _context.PrescricaoMedicamentos.Add(item);
        }

    }

    private async Task<PrescricaoMedicamento> ObterItemPrescricaoValidada(Guid medicamentoId, PrescricaoPaciente prescricao, PrescricaoMedicamento item = null)
    {
        if (item != null && medicamentoId != item.MedicamentoId)
        {
            AdicionarErroProcessamento("O item não corresponde ao informado");
            return null;
        }

        if (prescricao == null)
        {
            AdicionarErroProcessamento("Prescrição não encontrada");
            return null;
        }

        var itemPrescricao = await _context.PrescricaoMedicamentos
            .FirstOrDefaultAsync(i => i.PrescricaoId == prescricao.Id && i.MedicamentoId == medicamentoId);

        if (itemPrescricao == null || !prescricao.PrescricaoItemExistente(itemPrescricao))
        {
            AdicionarErroProcessamento("O item não está no prescrição");
            return null;
        }

        return itemPrescricao;
    }
    private async Task PersistirDados()
    {
        var result = await _context.SaveChangesAsync();
        if (result <= 0) AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
    }

    private bool ValidarPrescricao(PrescricaoPaciente prescricao)
    {
        if (prescricao.EhValido()) return true;

        prescricao.ValidationResult.Errors.ToList().ForEach(e => AdicionarErroProcessamento(e.ErrorMessage));
        return false;
    }
    #endregion
}
