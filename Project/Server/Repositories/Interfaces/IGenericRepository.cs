using Server.Entities;
using Server.Specifications.Base;

namespace Server.Repositories.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<TResult>> ListEntity<TResult>(ISpecification<T, TResult> specification);
    Task<IReadOnlyList<T>> ListEntity(ISpecification<T> specification);
    Task<IReadOnlyList<T>> ListEntity();
    Task<T?> GetEntity(ISpecification<T> specification);
    Task<T?> GetEntity(int entityId);
    Task AddEntity(T entity);
    void UpdateEntity(T entity);
    void RemoveEntity(T entity);
    Task<bool> Save();
    Task<bool> Exists(int entityId);
}
