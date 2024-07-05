using TO.Core.Communication;

namespace TO.WebApp.MVC.Models.Usuario;

public class UsuarioRespostaLoginViewModel
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UsuarioTokenViewModel UsuarioToken { get; set; }
    public ResponseResult ResponseResult { get; set; }
}
