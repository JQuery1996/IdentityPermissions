using System.Linq.Expressions;

namespace BL.Repositories;
public interface IBaseRepositorySync<T, in TKey> where T : class {
    IEnumerable<T> ToList();
    T? GetById(TKey key);

    T? Find(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[]? includesProperties
    );

    IEnumerable<T> FindAll(
        Expression<Func<T, bool>> predicate,
        int? skip = null,
        int? take = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        params Expression<Func<T, object>>[]? includesProperties);


    T Add(T entity);
    T Update(T entity);

    void Remove(T entity);

    void Attach(T entity);

    int Count(Expression<Func<T, bool>>? predicate = null);
    long CountLong(Expression<Func<T, bool>>? predicate = null);
}
