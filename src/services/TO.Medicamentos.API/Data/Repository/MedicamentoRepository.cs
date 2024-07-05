using Microsoft.EntityFrameworkCore;
using TO.Core.Data;
using TO.Medicamentos.API.Models;

namespace TO.Medicamentos.API.Data.Repository;

public class MedicamentoRepository : IMedicamentoRepository
{
    private readonly MedicamentosContext _context;

    public MedicamentoRepository(MedicamentosContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context; //para realizar o commit.

    public async Task<IEnumerable<Medicamento>> ObterTodos()
        => await _context.Medicamentos.AsNoTracking().ToListAsync();

    public async Task<Medicamento> ObterPorId(Guid id)
        => await _context.Medicamentos.FindAsync(id);

    public void Adicionar(Medicamento produto)
    {
        _context.Medicamentos.Add(produto);
    }

    public void Atualizar(Medicamento produto)
    {
        _context.Medicamentos.Update(produto);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
