using Homebroker.Application.DTO;
using Homebroker.Domain;

namespace Homebroker.Application.Interfaces
{
    public interface IAssetsService
    {
        public Task Create(AssetInputDTO asset);
        public Task<IEnumerable<Asset>> GetAll();
    }
}
