namespace TO.Identidade.API.Models;

public class UsuarioTokenViewModel
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public IEnumerable<UsuarioClaimViewModel> Claims { get; set; } = new List<UsuarioClaimViewModel>();
}
