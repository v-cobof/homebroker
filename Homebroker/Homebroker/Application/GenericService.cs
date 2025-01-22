using Homebroker.Application.Interfaces;
using Homebroker.Domain.Interfaces;

namespace Homebroker.Application
{
    public abstract class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly IRepository<T> _repository;

        public GenericService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public Task Create(T asset)
        {
            return _repository.CreateAsync(asset);
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return _repository.GetAllAsync();
        }
    }
}
