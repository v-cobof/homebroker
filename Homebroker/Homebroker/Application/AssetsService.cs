using Homebroker.Application.DTO;
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

        public async Task Create(AssetInputDTO dto)
        {
            var asset = new Asset()
            {
                Name = dto.Name,
                Price = dto.Price,
                Symbol = dto.Symbol,
            };

            await _repository.Create(asset);
            await _repository.UnitOfWork.Commit();
        }

        public async Task<IEnumerable<Asset>> GetAll()
        {
            return await _repository.GetAllAsync();
        }
    }
}
