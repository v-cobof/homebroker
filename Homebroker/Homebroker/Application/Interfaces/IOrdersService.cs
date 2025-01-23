using Homebroker.Application.DTO;
using Homebroker.Domain;

namespace Homebroker.Application.Interfaces
{
    public interface IOrdersService
    {
        public Task<Order> InitTransaction(InitTransactionDTO dto);
        public Task ExecuteTransaction(InputExecuteTransactionDto dto);
        public Task<IEnumerable<Order>> GetByWallet(Guid walletId);
    }
}
