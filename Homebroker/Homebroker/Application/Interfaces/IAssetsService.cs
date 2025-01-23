using Homebroker.Domain;

namespace Homebroker.Application.Interfaces
{
    public interface IAssetsService
    {
        public Task Create(Asset asset);
        public Task<IEnumerable<Asset>> GetAll();
    }
}
