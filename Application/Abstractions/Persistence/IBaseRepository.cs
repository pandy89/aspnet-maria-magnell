using System.Linq.Expressions;

namespace Application.Abstractions.Persistence;

public interface IBaseRepository<TEntity> where TEntity : class
{
    // Create
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct = default);

    // Exisits
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> findBy, CancellationToken ct = default);

    // Get one
    Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> where, CancellationToken ct = default);

    Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> where, bool tracking = false, CancellationToken ct = default, params Expression<Func<TEntity, object>>[] includes);

    Task<TSelect?> GetOneAsync<TSelect>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TSelect>> select, bool tracking = false, CancellationToken ct = default);

    // Get all
    Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool tracking = false, CancellationToken ct = default, params Expression<Func<TEntity, object>>[] includes);

    Task<IReadOnlyList<TSelect>> GetAllAsync<TSelect>(Expression<Func<TEntity, TSelect>> select, Expression<Func<TEntity, bool>>? where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool tracking = false, CancellationToken ct = default, params Expression<Func<TEntity, object>>[] includes);

    // Delete
    Task DeleteAsync(TEntity entity, CancellationToken ct = default);

    // Save
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
