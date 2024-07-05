using TO.Core.Data;
using TO.Funcionarios.API.Models;

namespace TO.Funcionarios.API.Data.Repository;

public interface IFuncionarioRepository : IRepository<Funcionario>
{
    void Adicionar(Funcionario funcionario);

    Task<IEnumerable<Funcionario>> ObterTodos();
    Task<Funcionario> ObterPorCpf(string cpf);
}