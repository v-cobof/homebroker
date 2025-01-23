using Homebroker.Application.DTO;

namespace Homebroker.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<IEnumerable<Order>> GetByWallet(Guid walletId);
    }
}
