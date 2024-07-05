using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TO.WebApp.MVC.Models.Usuario;
using TO.WebApp.MVC.Services;

namespace TO.WebApp.MVC.Controllers;

public class IdentidadeController : MainController
{
    private readonly IAutenticacaoService _autenticacaoService;

    public IdentidadeController(IAutenticacaoService autenticacaoService)
    {
        _autenticacaoService = autenticacaoService;
    }

    [HttpGet("nova-conta")]
    public IActionResult Registro()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("nova-conta")]
    public async Task<IActionResult> Registro(UsuarioRegistroViewModel usuarioRegistro)
    {
        if (!ModelState.IsValid) return View(usuarioRegistro);

        //Bate na API para realizar o registro 
        var resposta = await _autenticacaoService.Registro(usuarioRegistro);

        if (ResponsePossuiErros(resposta.ResponseResult)) return View(usuarioRegistro);

        await RealizarLogin(resposta);

        return RedirectToAction("Listar", "Prescricao");
    }

    [HttpGet("login")]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(UsuarioLoginViewModel usuarioLogin, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (!ModelState.IsValid) return View(usuarioLogin);

        var resposta = await _autenticacaoService.Login(usuarioLogin);

        if (ResponsePossuiErros(resposta.ResponseResult)) return View(usuarioLogin);

        await RealizarLogin(resposta);

        if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Listar", "Prescricao");

        return LocalRedirect(returnUrl);
    }

    [HttpGet("sair")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home"); 
    }

    private async Task RealizarLogin(UsuarioRespostaLoginViewModel resposta)
    {
        var token = ObterTokenFormatado(resposta.AccessToken);

        var claims = new List<Claim>
        {
            new Claim("JWT", resposta.AccessToken)
        };
        claims.AddRange(token.Claims); 

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            IsPersistent = true 
        }; 

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

    private static JwtSecurityToken ObterTokenFormatado(string jwtToken)
        => new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
}