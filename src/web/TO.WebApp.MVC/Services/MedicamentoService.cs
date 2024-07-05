using Microsoft.Extensions.Options;
using TO.WebApp.MVC.Extensions;
using TO.WebApp.MVC.Models.Medicamento;

namespace TO.WebApp.MVC.Services;

public class MedicamentoService : Service, IMedicamentoService
{
    private readonly HttpClient _httpClient;
    public MedicamentoService(HttpClient httpClient,
        IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.MedicamentoUrl);
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<MedicamentoViewModel>> ObterTodos()
    {
        var response = await _httpClient.GetAsync("/medicamentos");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<IEnumerable<MedicamentoViewModel>>(response);
    }

    public async Task<MedicamentoViewModel> ObterPorId(Guid id)
    {
        var response = await _httpClient.GetAsync($"/medicamentos/{id}");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<MedicamentoViewModel>(response);
    }
}
