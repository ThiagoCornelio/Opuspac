using Microsoft.AspNetCore.Mvc;
using System.Net;
using TO.WebApp.MVC.Extensions;
using TO.WebApp.MVC.Models.Medicamento;
using TO.WebApp.MVC.Models.Prescricao;
using TO.WebApp.MVC.Services;

namespace TO.WebApp.MVC.Controllers;

public class PrescricaoController : MainController
{
    private readonly IPrescricaoService _prescricaoService;
    private readonly IMedicamentoService _medicamentoService;

    public PrescricaoController(IPrescricaoService prescricaoService, IMedicamentoService medicamentoService)
    {
        _prescricaoService = prescricaoService;
        _medicamentoService = medicamentoService;
    }

    [HttpGet("prescricao/listar")]
    public async Task<IActionResult> Index()
        => View(await _prescricaoService.ObterTodos());

    [HttpGet("prescricao/criar-prescricao")]
    public async Task<IActionResult> Create()
    {
        var viewModel = new PrescricaoViewModel()
        {
            MedicamentoList = await _medicamentoService.ObterTodos()
        };
        return View(viewModel);
    }

    [HttpPost("prescricao/criar-prescricao")]
    public async Task<IActionResult> Create(PrescricaoViewModel prescricaoVm)
    {
        var medicamentos = await _medicamentoService.ObterTodos();

        foreach (var item in prescricaoVm.ItensNovos)
        {
            var medicamento = medicamentos.FirstOrDefault(m => m.Id == item.MedicamentoId);
            if (medicamento != null)
            {
                item.Nome = medicamento.Nome;
                item.IntervaloHoras = medicamento.IntervaloHoras;
                item.QRCode = QrCodeGenerator.GenerateImage(item.Id.ToString());
            }
            ValidarItemPrescricao(medicamento, item.Quantidade);
        }

        ValidarPrescricao(prescricaoVm);

        if (!OperacaoValida())
        {
            prescricaoVm.MedicamentoList = medicamentos;
            prescricaoVm.Itens = await _prescricaoService.ObterItensPorPrescricaoId(prescricaoVm.Id);
            return View(prescricaoVm);
        }

        prescricaoVm.Itens = prescricaoVm.ItensNovos;
        var prescricaoDomain = await _prescricaoService.CriarPrescricao(prescricaoVm);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("prescricao/editar/{prescricaoId:guid}")]
    public async Task<IActionResult> Edit(Guid prescricaoId)
    {
        var viewModel = await _prescricaoService.ObterPrescricaoId(prescricaoId);

        viewModel.MedicamentoList = await _medicamentoService.ObterTodos();
        viewModel.Image = QrCodeGenerator.GenerateImage(viewModel.Id.ToString());

        return View(viewModel);
    }

    [HttpPost("prescricao/editar/{prescricaoId:guid}")]
    public async Task<IActionResult> Edit(PrescricaoViewModel prescricaoVm)
    {
        var medicamentos = await _medicamentoService.ObterTodos();

        //Refatorar
        foreach (var item in prescricaoVm.ItensNovos)
        {
            var medicamento = medicamentos.FirstOrDefault(m => m.Id == item.MedicamentoId);
            if (medicamento != null)
            {
                item.Nome = medicamento.Nome;
                item.IntervaloHoras = medicamento.IntervaloHoras;
                item.QRCode = QrCodeGenerator.GenerateImage(item.Id.ToString());
            }
            ValidarItemPrescricao(medicamento, item.Quantidade);
        }

        ValidarPrescricao(prescricaoVm);

        if (!OperacaoValida())
        {
            prescricaoVm.MedicamentoList = medicamentos;
            prescricaoVm.Itens = await _prescricaoService.ObterItensPorPrescricaoId(prescricaoVm.Id);
            return View(prescricaoVm);
        }

        prescricaoVm.Itens = prescricaoVm.ItensNovos;
        var prescricaoDomain = await _prescricaoService.AdicionarItemPrescricao(prescricaoVm);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("prescricao/excluir/{id:guid}")]
    public async Task<IActionResult> ExcluirPrescricao(Guid id)
    {
        var resposta = await _prescricaoService.RemoverPrescricao(id);

        if (ResponsePossuiErros(resposta))
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("");
        }

        Response.StatusCode = (int)HttpStatusCode.OK;
        return Json("");
    }

    [HttpGet("prescricao/finalizar/{id:guid}")]
    public async Task<PartialViewResult> Finalizar(Guid id)
    {
        var viewModel = await _prescricaoService.ObterPrescricaoId(id);

        return PartialView("_Finalizar", viewModel);
    }

    [HttpPost("prescricao/atualizar-item-prescricao")]
    public async Task<JsonResult> AtualizarItemPrescricao(Guid prescricaoId, Guid medicamentoId, int quantidade)
    {
        var medicamento = await _medicamentoService.ObterPorId(medicamentoId);

        ValidarItemPrescricao(medicamento, quantidade);
        if (!OperacaoValida())
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Ocorreu um erro ao persistir os dados!");
        }

        var itemProduto = new ItemPrescricaoViewModel { MedicamentoId = medicamentoId, Quantidade = quantidade, QRCode = "" };
        var resposta = await _prescricaoService.AtualizarItemPrescricao(prescricaoId, itemProduto);

        if (ResponsePossuiErros(resposta))
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Ocorreu um erro ao persistir os dados!");
        }

        Response.StatusCode = (int)HttpStatusCode.OK;
        return Json("Alteração concluída!");
    }

    [HttpPost("prescricao/remover-item")]
    public async Task<JsonResult> RemoverItemPrescricao(Guid prescricaoId, Guid medicamentoId)
    {
        var produto = await _medicamentoService.ObterPorId(medicamentoId);

        if (produto == null)
        {
            return Json("Medicamento inexistente!");
        }

        var resposta = await _prescricaoService.RemoverItemPrescricao(prescricaoId, medicamentoId);

        if (ResponsePossuiErros(resposta))
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("");
        }

        Response.StatusCode = (int)HttpStatusCode.OK;
        return Json("");
    }

    private void ValidarPrescricao(PrescricaoViewModel prescricao)
    {
        if (string.IsNullOrWhiteSpace(prescricao.NomeMedico)) AdicionarErroValidacao("Nome Médico inválido");
        if (string.IsNullOrWhiteSpace(prescricao.NomePaciente)) AdicionarErroValidacao("Nome Paciente inválido");
    }

    private void ValidarItemPrescricao(MedicamentoViewModel medicamentoViewModel, int quantidade)
    {
        if (medicamentoViewModel == null) AdicionarErroValidacao("Medicamento inexistente!");
        if (quantidade < 1) AdicionarErroValidacao($"Escolha ao menos uma unidade para o medicamento");
    }

}
