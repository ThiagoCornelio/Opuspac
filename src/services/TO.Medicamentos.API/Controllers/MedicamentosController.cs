using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TO.Medicamentos.API.Data.Repository;
using TO.Medicamentos.API.Models;
using TO.WebAPI.Core.Controllers;

namespace TO.Medicamentos.API.Controllers;

public class MedicamentosController : MainController
{
    private readonly IMedicamentoRepository _medicamentoRepository;

    public MedicamentosController(IMedicamentoRepository medicamentoRepository)
    {
        _medicamentoRepository = medicamentoRepository;
    }

    [AllowAnonymous]
    [HttpGet("medicamentos")]
    public async Task<IEnumerable<Medicamento>> Index()
        => await _medicamentoRepository.ObterTodos();

    //[ClaimsAuthorize("Catalogo", "Ler")]
    [HttpGet("medicamentos/{id:guid}")]
    public async Task<Medicamento> ProdutoDetalhe(Guid id)
        => await _medicamentoRepository.ObterPorId(id);
}
