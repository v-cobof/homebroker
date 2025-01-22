using Homebroker.Application.DTO;
using Homebroker.Application.Interfaces;
using Homebroker.Domain;
using Homebroker.Domain.Interfaces;
using MongoDB.Driver.Linq;

namespace Homebroker.Application
{
    public class WalletAssetService : GenericService<WalletAsset>, IWalletAssetService
    {
        private readonly IWalletAssetRepository _walletAssetRepository;

        public WalletAssetService(IRepository<WalletAsset> repository, IWalletAssetRepository assetRepo) : base(repository)
        {
            _walletAssetRepository = assetRepo;
        }

        public async Task<IEnumerable<WalletAssetOutputDTO>> GetWalletAssetsByWalletId(string walletId)
        {
            return await _walletAssetRepository.GetWalletAssetsByWalletId(walletId);
        }
    }
}
