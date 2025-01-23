using Homebroker.Application.DTO;
using Homebroker.Domain;
using Homebroker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homebroker.Infrastructure
{
    public class OrdersRepository : Repository<Order>, IOrderRepository
    {
        public OrdersRepository(HomebrokerDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetByWallet(Guid walletId)
        {
            return await DbSet.Where(t => t.WalletId == walletId)
                .Include(t => t.Asset)
                .Include(t => t.Transactions)
                .ToListAsync();
        }
    }
}
