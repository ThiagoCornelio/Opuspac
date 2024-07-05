using Microsoft.EntityFrameworkCore;
using TO.Core.Data;
using TO.Funcionarios.API.Models;

namespace TO.Funcionarios.API.Data.Repository;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly FuncionariosContext _context;

    public FuncionarioRepository(FuncionariosContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Funcionario>> ObterTodos() => await _context.Funcionarios.AsNoTracking().ToListAsync();

    public Task<Funcionario> ObterPorCpf(string cpf) => _context.Funcionarios.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);

    public void Adicionar(Funcionario Funcionario) => _context.Funcionarios.Add(Funcionario);

    public void Dispose() => _context.Dispose();
}
