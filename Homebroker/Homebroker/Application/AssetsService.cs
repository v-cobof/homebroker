using Homebroker.Application.Interfaces;
using Homebroker.Domain;
using Homebroker.Domain.Interfaces;

namespace Homebroker.Application
{
    public class AssetsService : IAssetsService
    {
        private readonly IRepository<Asset> _repository;

        public AssetsService(IRepository<Asset> repository)
        {
            _repository = repository;
        }

        public async Task Create(Asset asset)
        {
            await _repository.Create(asset);
        }

        public async Task<IEnumerable<Asset>> GetAll()
        {
            return await _repository.GetAllAsync();
        }
    }
}
