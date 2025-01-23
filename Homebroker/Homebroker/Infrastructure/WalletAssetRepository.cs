using Homebroker.Domain;
using Homebroker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homebroker.Infrastructure
{
    public class WalletAssetRepository : GenericRepository<WalletAsset>, IWalletAssetRepository
    {
        public WalletAssetRepository(HomebrokerDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WalletAsset>> GetWalletAssetsByWalletId(Guid walletId)
        {
            return await DbSet.Where(t => t.WalletId == walletId).Include(t => t.Asset).ToListAsync();
        }
    }
}
