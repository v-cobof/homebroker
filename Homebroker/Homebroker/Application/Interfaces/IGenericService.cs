using Homebroker.Domain;

namespace Homebroker.Application.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        public Task Create(T asset);
        public Task<IEnumerable<T>> GetAll();
    }
}
