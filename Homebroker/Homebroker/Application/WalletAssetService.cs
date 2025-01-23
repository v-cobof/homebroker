using Homebroker.Application.DTO;
using Homebroker.Application.Interfaces;
using Homebroker.Domain;
using Homebroker.Domain.Interfaces;

namespace Homebroker.Application
{
    public class WalletAssetService : IWalletAssetService
    {
        private readonly IWalletAssetRepository _walletAssetRepository;

        public WalletAssetService(IWalletAssetRepository assetRepo)
        {
            _walletAssetRepository = assetRepo;
        }

        public async Task Create(WalletAsset asset)
        {
            await _walletAssetRepository.Create(asset);
        }

        public async Task<IEnumerable<WalletAsset>> GetAll()
        {
            return await _walletAssetRepository.GetAllAsync();
        }

        public async Task<IEnumerable<WalletAsset>> GetWalletAssetsByWalletId(Guid walletId)
        {
            return await _walletAssetRepository.GetWalletAssetsByWalletId(walletId);
        }
    }
}
