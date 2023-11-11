using BL.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;
public class BaseRepository<T, TKey>(ApplicationDbContext context)
    : IBaseRepository<T, TKey>
    where T : class {
    public IEnumerable<T> ToList() {
        return context.Set<T>().ToList();
    }

    public T? GetById(TKey key) {
        return context.Set<T>().Find(key);
    }

    public T? Find(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[]? includesProperties) {
        var query = context.Set<T>().AsQueryable();
        if (includesProperties is not null)
            query = includesProperties.Aggregate(
                query,
                (current, includeProperty) => current.Include(includeProperty));
        return query.FirstOrDefault(predicate);
    }

    public IEnumerable<T> FindAll(
        Expression<Func<T, bool>> predicate,
        int? skip = null,
        int? take = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        params Expression<Func<T, object>>[]? includesProperties) {
        var query = context.Set<T>().AsQueryable();
        if (predicate is not null)
            query = query.Where(predicate);
        if (includesProperties is not null)
            query = includesProperties.Aggregate(
                query,
                (current, includeProperty) => current.Include(includeProperty));

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        return orderBy is not null
            ? orderBy(query).ToList()
            : query.ToList();
    }

    public T Add(T entity) {
        context.Set<T>().Add(entity);
        return entity;
    }

    public T Update(T entity) {
        context.Set<T>().Update(entity);
        return entity;
    }

    public void Remove(T entity) {
        context.Set<T>().Remove(entity);
    }

    public void Attach(T entity) {
        context.Set<T>().Attach(entity);
    }

    public int Count(Expression<Func<T, bool>>? predicate = null) {
        return predicate is not null
            ? context.Set<T>().Count(predicate)
            : context.Set<T>().Count();
    }

    public long CountLong(Expression<Func<T, bool>>? predicate = null) {
        return predicate is not null
            ? context.Set<T>().LongCount(predicate)
            : context.Set<T>().LongCount();
    }

    public async Task<IEnumerable<T>> ToListAsync() {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(TKey key) {
        return await context.Set<T>().FindAsync(key);
    }

    public async Task<T?> FindAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[]? includesProperties) {
        var query = context.Set<T>().AsQueryable();
        if (includesProperties is not null)
            includesProperties.Aggregate(
                query,
                (current, includeProperty) => current.Include(includeProperty));

        return await query.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> predicate,
        int? skip = null,
        int? take = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        params Expression<Func<T, object>>[]? includesProperties) {
        var query = context.Set<T>().AsQueryable();
        if (predicate is not null)
            query = query.Where(predicate);

        includesProperties?.Aggregate(
                query,
                (current, includeProperty) => query.Include(includeProperty));

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);


        return orderBy is not null
            ? await orderBy(query).ToListAsync()
            : await query.ToListAsync();
    }

    public async Task<T> AddAsync(T entity) {
        await context.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity) {
        await Task.CompletedTask;
        context.Set<T>().Update(entity);
        return entity;
    }

    public async Task RemoveAsync(T entity) {
        await Task.CompletedTask;
        context.Set<T>().Remove(entity);
    }

    public async Task AttachAsync(T entity) {
        await Task.CompletedTask;
        context.Set<T>().Attach(entity);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null) {
        return predicate is not null
            ? await context.Set<T>().CountAsync(predicate)
            : await context.Set<T>().CountAsync();
    }

    public Task<long> LongCountAsync(Expression<Func<T, bool>>? predicate = null) {
        return predicate is not null
            ? context.Set<T>().LongCountAsync(predicate)
            : context.Set<T>().LongCountAsync();
    }
}
