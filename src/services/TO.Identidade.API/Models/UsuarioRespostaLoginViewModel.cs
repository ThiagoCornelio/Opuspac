namespace TO.Identidade.API.Models;

public class UsuarioRespostaLoginViewModel
{
    public string AccessToken { get; set; } = null!;
    public double ExpiresIn { get; set; }
    public UsuarioTokenViewModel UsuarioToken { get; set; } = new UsuarioTokenViewModel();
}
