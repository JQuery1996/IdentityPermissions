using System.Linq.Expressions;

namespace BL.Repositories;
public interface IBaseRepositoryAsync<T, in TKey> where T : class {
    Task<IEnumerable<T>> ToListAsync();
    Task<T?> GetByIdAsync(TKey key);
    Task<T?> FindAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[]? includesProperties
    );
    Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> predicate,
        int? skip = null,
        int? take = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        params Expression<Func<T, object>>[]? includesProperties
    );

    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task AttachAsync(T entity);

    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    Task<long> LongCountAsync(Expression<Func<T, bool>>? predicate = null);
}
