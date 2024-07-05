using TO.WebApp.MVC.Models.Medicamento;

namespace TO.WebApp.MVC.Services;

public interface IMedicamentoService
{
    Task<IEnumerable<MedicamentoViewModel>> ObterTodos();
    Task<MedicamentoViewModel> ObterPorId(Guid id);
}
