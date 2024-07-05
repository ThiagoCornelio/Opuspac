using Microsoft.Extensions.Options;
using TO.Core.Communication;
using TO.WebApp.MVC.Extensions;
using TO.WebApp.MVC.Models.Prescricao;

namespace TO.WebApp.MVC.Services;

public class PrescricaoService : Service, IPrescricaoService
{
    private readonly HttpClient _httpClient;

    public PrescricaoService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.PrescricaoUrl);
    }

    public async Task<PrescricaoViewModel> CriarPrescricao(PrescricaoViewModel prescricao)
    {
        var itemContent = ObterConteudo(prescricao);

        var response = await _httpClient.PostAsync("/prescricao/criar-prescricao", itemContent);

        return await DeserializarObjetoResponse<PrescricaoViewModel>(response);
    }

    public async Task<IEnumerable<PrescricaoViewModel>> ObterTodos()
    {
        var response = await _httpClient.GetAsync($"/prescricao");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<IEnumerable<PrescricaoViewModel>>(response);
    }

    public async Task<PrescricaoViewModel> ObterPrescricaoId(Guid prescricaoId)
    {
        var response = await _httpClient.GetAsync($"/prescricao/{prescricaoId}");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<PrescricaoViewModel>(response);
    }

    public async Task<ResponseResult> AdicionarItemPrescricao(PrescricaoViewModel prescricao)
    {
        var itemContent = ObterConteudo(prescricao);

        var response = await _httpClient.PostAsync($"/prescricao", itemContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }

    public async Task<ResponseResult> AtualizarItemPrescricao(Guid prescricaoId, ItemPrescricaoViewModel prescricao)
    {
        var itemContent = ObterConteudo(prescricao);

        var response = await _httpClient.PutAsync($"/prescricao/{prescricaoId}", itemContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }

    public async Task<ResponseResult> RemoverItemPrescricao(Guid prescricaoId, Guid medicamentoId)
    {
        var response = await _httpClient.DeleteAsync($"/prescricao/{prescricaoId}/{medicamentoId}");

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }

    public async Task<IEnumerable<ItemPrescricaoViewModel>> ObterItensPorPrescricaoId(Guid prescricaoId)
    {
        var response = await _httpClient.GetAsync($"/prescricao/items/{prescricaoId}");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<IEnumerable<ItemPrescricaoViewModel>>(response);
    }

    public async Task<ResponseResult> RemoverPrescricao(Guid prescricaoId)
    {
        var response = await _httpClient.DeleteAsync($"/prescricao/excluir/{prescricaoId}");

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }
}
