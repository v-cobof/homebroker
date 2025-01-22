using Homebroker.Application.DTO;
using Homebroker.Domain;

namespace Homebroker.Application.Interfaces
{
    public interface IWalletAssetService : IGenericService<WalletAsset>
    {
        // incluir assets
        public Task<IEnumerable<WalletAssetOutputDTO>> GetWalletAssetsByWalletId(string walletId);
    }
}
