using Microsoft.Extensions.Options;
using TO.Core.Communication;
using TO.WebApp.MVC.Extensions;
using TO.WebApp.MVC.Models.Usuario;

namespace TO.WebApp.MVC.Services;

public interface IAutenticacaoService
{
    Task<UsuarioRespostaLoginViewModel> Login(UsuarioLoginViewModel usuarioLogin);

    Task<UsuarioRespostaLoginViewModel> Registro(UsuarioRegistroViewModel usuarioRegistro);
}
public class AutenticacaoService : Service, IAutenticacaoService
{
    private readonly HttpClient _httpClient; //Precisa ser injetada por dependencia no construtor
    //Aqui que irei chamar a API. conversando com o mundo exterior atraves de request e response
    public AutenticacaoService(HttpClient httpClient,
                               IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.AutenticacaoUrl);

        _httpClient = httpClient;
    }

    public async Task<UsuarioRespostaLoginViewModel> Login(UsuarioLoginViewModel usuarioLogin)
    {
        var loginContent = ObterConteudo(usuarioLogin);

        var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);

        if (!TratarErrosResponse(response))
        {
            return new UsuarioRespostaLoginViewModel
            {
                ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
            };
        }

        return await DeserializarObjetoResponse<UsuarioRespostaLoginViewModel>(response);
    }

    public async Task<UsuarioRespostaLoginViewModel> Registro(UsuarioRegistroViewModel usuarioRegistro)
    {
        var registroContent = ObterConteudo(usuarioRegistro);

        var response = await _httpClient.PostAsync("/api/identidade/nova-conta", registroContent);

        if (!TratarErrosResponse(response))
        {
            return new UsuarioRespostaLoginViewModel
            {
                ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
            };
        }

        return await DeserializarObjetoResponse<UsuarioRespostaLoginViewModel>(response);
    }
}