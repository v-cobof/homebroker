using Homebroker.Application.DTO;
using Homebroker.Domain;

namespace Homebroker.Application.Interfaces
{
    public interface IWalletAssetService
    {
        public Task<IEnumerable<WalletAsset>> GetWalletAssetsByWalletId(Guid walletId);
        public Task Create(WalletAsset asset);
        public Task<IEnumerable<WalletAsset>> GetAll();
    }
}
