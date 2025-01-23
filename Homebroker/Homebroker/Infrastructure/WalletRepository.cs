using Homebroker.Domain;
using Homebroker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homebroker.Infrastructure
{
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        private DbSet<WalletAsset> _walletAssets;

        public WalletRepository(HomebrokerDbContext context) : base(context)
        {
            _walletAssets = Db.Set<WalletAsset>();
        }

        public async Task CreateWalletAsset(WalletAsset walletAsset)
        {
            _walletAssets.Add(walletAsset);
        }

        public async Task<WalletAsset> GetWalletAssetByWalletIdAndAssetId(Guid walletId, Guid assetId)
        {
            return await _walletAssets.FirstOrDefaultAsync(t => t.AssetId == assetId && t.WalletId == walletId);
        }

        public async Task<IEnumerable<WalletAsset>> GetWalletAssetsByWalletId(Guid walletId)
        {
            return await _walletAssets.Where(t => t.WalletId == walletId).Include(t => t.Asset).ToListAsync();
        }

        public async Task UpdateWalletAsset(WalletAsset walletAsset)
        {
            _walletAssets.Update(walletAsset);
        }
    }
}
