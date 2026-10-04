using VetFlow.Shared.Results;

namespace VetFlow.Application.Common.Interfaces;

/// <summary>
/// Minimal repository contract — slices define their own specific interfaces.
/// This base exists only for common patterns like GetByIdAsync/SaveChangesAsync.
/// Avoid fat generic repositories.
/// </summary>
public interface IRepository<TEntity, TId>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}

/// <summary>
/// Unit of work — controls transaction boundaries.
/// Implemented by EF Core DbContext in Infrastructure.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
