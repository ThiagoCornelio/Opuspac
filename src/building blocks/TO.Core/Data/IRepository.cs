using TO.Core.DomainObjects;

namespace TO.Core.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}