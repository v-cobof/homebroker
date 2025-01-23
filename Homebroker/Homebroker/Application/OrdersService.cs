using Homebroker.Application.DTO;
using Homebroker.Application.Interfaces;
using Homebroker.Domain;
using Homebroker.Domain.Enums;
using Homebroker.Domain.Interfaces;

namespace Homebroker.Application
{
    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IRepository<Asset> _assetRepository;
        private readonly IOrderRepository _repository;
        private readonly IWalletRepository _walletRepository;

        public OrdersService(IOrderRepository repository, IRepository<Transaction> transactionRepository, IRepository<Asset> assetRepository, IWalletRepository walletAssetRepository)
        {
            _repository = repository;
            _transactionRepository = transactionRepository;
            _assetRepository = assetRepository;
            _walletRepository = walletAssetRepository;
        }

        // must be a DB transaction (atomic)
        // use opmistic lock, [ConcurrencyCheck] in  EF for versioning
        public async Task ExecuteTransaction(InputExecuteTransactionDto dto)
        {
            var orderId = Guid.Parse(dto.OrderId);
            var order = await _repository.GetByIdAsync(orderId);

            order.Partial -= dto.NegotiatedShares;

            await _repository.Update(order); // filter on the WHERE of the update, version == order.version, to check if version changed since you queried the order

            var transaction = new Transaction()
            {
                BrokerTransactionId = dto.BrokerTransactionId,
                InvestorId = Guid.Parse(dto.InvestorId),
                OrderId = orderId,
                Price = dto.Price,
                Shares = dto.NegotiatedShares
            };

            await _transactionRepository.Create(transaction);

            var status = (OrderStatus)Enum.Parse(typeof(OrderStatus), dto.Status);

            if (status == OrderStatus.CLOSED)
            {
                var asset = await _assetRepository.GetByIdAsync(order.AssetId);
                asset.Price = dto.Price;
                await _assetRepository.Update(asset);

                var walletAsset = await _walletRepository.GetWalletAssetByWalletIdAndAssetId(order.WalletId, order.AssetId);

                if (walletAsset is not null)
                {                  
                    walletAsset.Shares = order.Type == OrderType.BUY 
                        ? walletAsset.Shares + order.Shares
                        : walletAsset.Shares - order.Shares;

                    await _walletRepository.UpdateWalletAsset(walletAsset);
                }
                else
                {
                    // just create if its a buying order
                    var newWalletAsset = new WalletAsset()
                    {
                        AssetId = order.AssetId,
                        WalletId = order.WalletId,
                        Shares = dto.NegotiatedShares,
                    };

                    await _walletRepository.CreateWalletAsset(newWalletAsset);
                }
            }

            await _walletRepository.UnitOfWork.Commit();
        }

        public async Task<IEnumerable<OrderResultDTO>> GetByWallet(Guid walletId)
        {
            var orders = await _repository.GetByWallet(walletId);

            return orders.Select(o => new OrderResultDTO()
            {
                AssetId = o.AssetId.ToString(),
                Asset = new AssetResultDTO()
                {
                    Id = o.Asset.Id,
                    Name = o.Asset.Name,
                    Price = o.Asset.Price,
                    Symbol = o.Asset.Symbol
                },
                Id = o.Id.ToString(),
                Price = o.Price,
                Partial = o.Partial,
                Shares = o.Shares,
                Status = o.Status,
                Type = o.Type,
                WalletId = o.WalletId.ToString(),
                Transactions = o.Transactions.Select(t =>  new TransactionDTO()
                {
                    BrokerTransactionId = t.BrokerTransactionId,
                    Shares = t.Shares,
                    Price = t.Price,
                    InvestorId = t.InvestorId,
                    OrderId = t.OrderId
                }).ToList()                
            });
        }

        public async Task<Order> InitTransaction(InitTransactionDTO dto)
        {
            var order = new Order()
            {
                AssetId = Guid.Parse(dto.AssetId),
                WalletId = Guid.Parse(dto.WalletId),
                Shares = dto.Shares,
                Price = dto.Price,
                Type = (OrderType)Enum.Parse(typeof(OrderType), dto.Type),
                Status = Domain.Enums.OrderStatus.PENDING,
                Partial = dto.Shares
            };

            await _repository.Create(order);
            await _repository.UnitOfWork.Commit();
            return order;
        }
    }
}
