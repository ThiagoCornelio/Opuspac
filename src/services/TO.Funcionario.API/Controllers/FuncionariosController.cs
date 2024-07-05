using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TO.Core.Mediator;
using TO.Funcionarios.API.Application.Commands;
using TO.WebAPI.Core.Controllers;

namespace TO.Funcionarios.API.Controllers;

public class FuncionariosController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;

    public FuncionariosController(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    [AllowAnonymous]
    [HttpGet("api/funcionarios")]
    public async Task<IActionResult> Index()
    {
        var resultado = await _mediatorHandler.EnviarComando(
            new RegistrarFuncionarioCommand(Guid.NewGuid(), "Thiago", "thiago@teste.com", "060.363.653-57"));

        return CustomResponse(resultado);
    }
}
