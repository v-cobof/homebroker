using Homebroker.Application.DTO;
using Homebroker.Application.Interfaces;
using Homebroker.Domain;
using Homebroker.Domain.Enums;
using Homebroker.Domain.Interfaces;

namespace Homebroker.Application
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrderRepository _repository;
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IRepository<Asset> _assetRepository;
        private readonly IWalletAssetRepository _walletAssetRepository;

        public OrdersService(IOrderRepository repository, IRepository<Transaction> transactionRepository, IRepository<Asset> assetRepository, IWalletAssetRepository walletAssetRepository)
        {
            _repository = repository;
            _transactionRepository = transactionRepository;
            _assetRepository = assetRepository;
            _walletAssetRepository = walletAssetRepository;
        }

        // must be a DB transaction (atomic)
        // use opmistic lock, [ConcurrencyCheck] in  EF for versioning
        public async Task ExecuteTransaction(InputExecuteTransactionDto dto)
        {
            var orderId = Guid.Parse(dto.OrderId);
            var order = await _repository.GetByIdAsync(orderId);

            order.Partial -= dto.NegotiatedShares;

            await _repository.Update(order.Id, order); // filter on the WHERE of the update, version == order.version, to check if version changed since you queried the order

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
                await _assetRepository.Update(asset.Id, asset);

                var walletAsset = (await _walletAssetRepository.GetByFilterAsync(t => t.AssetId == order.AssetId && t.WalletId == order.WalletId)).FirstOrDefault();

                if (walletAsset is not null)
                {                  
                    walletAsset.Shares = order.Type == OrderType.BUY 
                        ? walletAsset.Shares + order.Shares
                        : walletAsset.Shares - order.Shares;

                    await _walletAssetRepository.Update(walletAsset.Id, walletAsset);
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

                    await _walletAssetRepository.Create(newWalletAsset);
                }
            }
        }

        public async Task<IEnumerable<Order>> GetByWallet(Guid walletId)
        {
            return await _repository.GetByWallet(walletId);
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

            return order;
        }
    }
}
