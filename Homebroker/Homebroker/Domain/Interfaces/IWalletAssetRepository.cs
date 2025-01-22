using Homebroker.Application.DTO;

namespace Homebroker.Domain.Interfaces
{
    public interface IWalletAssetRepository
    {
        public Task<IEnumerable<WalletAssetOutputDTO>> GetWalletAssetsByWalletId(string walletId);
    }
}
