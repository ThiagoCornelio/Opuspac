namespace TO.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}
