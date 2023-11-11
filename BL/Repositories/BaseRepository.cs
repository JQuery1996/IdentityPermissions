namespace BL.Repositories;

public interface IBaseRepository<T, TKey> :
    IBaseRepositorySync<T, TKey>,
    IBaseRepositoryAsync<T, TKey>
    where T : class { }

public interface IBaseRepository<T> :
    IBaseRepositorySync<T, int>,
    IBaseRepositoryAsync<T, int>
    where T : class { }
