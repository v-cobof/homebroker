using Homebroker.Application.DTO;
using Homebroker.Domain;

namespace Homebroker.Application.Interfaces
{
    public interface IOrdersService : IGenericService<Order>
    {
        public Task<Order> InitTransaction(InitTransactionDTO dto);
        public Task<Order> ExecuteTransaction(InputExecuteTransactionDto dto);
    }
}
