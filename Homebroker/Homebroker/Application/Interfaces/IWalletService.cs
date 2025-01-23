using Homebroker.Application.DTO;
using Homebroker.Domain;

namespace Homebroker.Application.Interfaces
{
    public interface IWalletService
    {
        public Task Create(WalletInputDTO asset);
        public Task<IEnumerable<Wallet>> GetAll();
        public Task<IEnumerable<WalletAsset>> GetWalletAssetsByWalletId(Guid walletId);
    }
}
