using Homebroker.Application.DTO;

namespace Homebroker.Domain.Interfaces
{
    public interface IWalletAssetRepository : IRepository<WalletAsset>
    {
        public Task<IEnumerable<WalletAsset>> GetWalletAssetsByWalletId(Guid walletId);
    }
}
