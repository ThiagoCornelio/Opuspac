namespace TO.WebAPI.Core.Identidade;

public class AppSettings
{
    public string Secret { get; set; } = null!;
    public int ExpiracaoHoras { get; set; }
    public string Emissor { get; set; } = null!;
    public string ValidoEm { get; set; } = null!;
}
