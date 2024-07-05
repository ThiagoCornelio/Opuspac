namespace TO.WebApp.MVC.Models.Usuario;

public class UsuarioTokenViewModel
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<UsuarioClaimViewModel> Claims { get; set; }
}
