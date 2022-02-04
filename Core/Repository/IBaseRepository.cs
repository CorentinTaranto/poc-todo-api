using System.Linq.Expressions;

namespace Core.Repository;

public interface IBaseRepository<T> where T : class
{
    Task<T> CreateAsync(T obj);

    Task<T> UpdateAsync(T obj);

    Task<T> GetByIdAsync(string id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<bool> DeleteAsync(string id);

    Task<bool> Exists(Expression<Func<T, bool>> predicate);
}

