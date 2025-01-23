using Homebroker.Application.DTO;

namespace Homebroker.Domain.Interfaces
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        public Task<IEnumerable<WalletAsset>> GetWalletAssetsByWalletId(Guid walletId);
        public Task<WalletAsset> GetWalletAssetByWalletIdAndAssetId(Guid walletId, Guid assetId);

        public Task CreateWalletAsset(WalletAsset walletAsset);
        public Task UpdateWalletAsset(WalletAsset walletAsset);
    }
}
