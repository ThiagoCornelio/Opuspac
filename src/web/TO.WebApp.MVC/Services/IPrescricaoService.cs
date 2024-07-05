using TO.Core.Communication;
using TO.WebApp.MVC.Models.Prescricao;

namespace TO.WebApp.MVC.Services;

public interface IPrescricaoService
{
    Task<PrescricaoViewModel> CriarPrescricao(PrescricaoViewModel prescricao);
    Task<IEnumerable<PrescricaoViewModel>> ObterTodos();
    Task<IEnumerable<ItemPrescricaoViewModel>> ObterItensPorPrescricaoId(Guid prescricaoId);
    Task<PrescricaoViewModel> ObterPrescricaoId(Guid prescricaoId);
    Task<ResponseResult> AdicionarItemPrescricao(PrescricaoViewModel prescricao);
    Task<ResponseResult> AtualizarItemPrescricao(Guid prescricaoId, ItemPrescricaoViewModel prescricao);
    Task<ResponseResult> RemoverItemPrescricao(Guid prescricaoId, Guid medicamentoId);
    Task<ResponseResult> RemoverPrescricao(Guid prescricaoId);
}
