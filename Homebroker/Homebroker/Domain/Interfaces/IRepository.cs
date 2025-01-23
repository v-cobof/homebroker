using System.Linq.Expressions;

namespace Homebroker.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task Create(T entity);
        Task Update(Guid id, T entity);
        Task Delete(Guid id);
        Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> filter);
    }
}
