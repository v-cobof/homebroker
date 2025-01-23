using System.Linq.Expressions;

namespace Homebroker.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork {  get; }
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task Create(T entity);
        Task Update(T entity);
        Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> filter);
    }
}
