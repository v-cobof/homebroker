using Homebroker.Application.DTO;
using Homebroker.Application.Interfaces;
using Homebroker.Domain;
using Homebroker.Domain.Enums;
using Homebroker.Domain.Interfaces;

namespace Homebroker.Application
{
    public class OrdersService : GenericService<Order>, IOrdersService
    {
        public OrdersService(IRepository<Order> repository) : base(repository)
        {
        }

        public Task<Order> ExecuteTransaction(InputExecuteTransactionDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> InitTransaction(InitTransactionDTO dto)
        {
            var order = new Order()
            {
                AssetId = dto.AssetId,
                WalletId = dto.WalletId,
                Shares = dto.Shares,
                Price = dto.Price,
                Type = (OrderType)Enum.Parse(typeof(OrderType), dto.Type),
                Status = Domain.Enums.OrderStatus.PENDING,
                Partial = dto.Shares
            };

            await _repository.CreateAsync(order);

            return order;
        }
    }
}
