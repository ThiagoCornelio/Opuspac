using TO.Core.Data;
using TO.Medicamentos.API.Models;

namespace TO.Medicamentos.API.Data.Repository;

public interface IMedicamentoRepository : IRepository<Medicamento>
{
    Task<IEnumerable<Medicamento>> ObterTodos();
    Task<Medicamento> ObterPorId(Guid id);

    void Adicionar(Medicamento medicamento);
    void Atualizar(Medicamento medicamento);
}
