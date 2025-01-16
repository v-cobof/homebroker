using Market.Application.DTO;
using Market.Domain.DataStructures;
using Market.Domain.Entities;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Market.Domain.Services
{
    public class BookService
    {
        private List<Transaction> _transactions;

        private BlockingCollection<Order> _inputChannel;
        private BlockingCollection<Order> _outputChannel;


        public BookService()
        {
            _transactions = [];
            _inputChannel = new BlockingCollection<Order>(boundedCapacity: 100);
            _outputChannel = new BlockingCollection<Order>(boundedCapacity: 100);
        }

        public void AddOrder(Order order)
        {
            _inputChannel.Add(order);
            Console.WriteLine($"Mensagem adicionada à fila: {order.Id}");
        }

        public async Task Trade(CancellationToken cancellationToken)
        {
            var buyOrders = new Dictionary<string, OrderQueue>();
            var sellOrders = new Dictionary<string, OrderQueue>();

            try
            {
                foreach (var order in _inputChannel.GetConsumingEnumerable(cancellationToken))
                {
                    var asset = order.Asset.Name;

                    if (buyOrders.GetValueOrDefault(asset) == null)
                    {
                        buyOrders[asset] = new OrderQueue();
                    }

                    if (sellOrders.GetValueOrDefault(asset) == null)
                    {
                        sellOrders[asset] = new OrderQueue();
                    }

                    if (order.OrderType == Enum.OrderType.BUY)
                    {
                        buyOrders[asset].Push(order);

                        if (sellOrders[asset].Len() > 0 && sellOrders[asset].Peek().Price <= order.Price)
                        {
                            var sellOrder = sellOrders[asset].Pop();

                            if (sellOrder.PendingShares > 0)
                            {
                                var transaction = new Transaction(sellOrder, order, order.Shares, sellOrder.Price);

                                AddTransaction(transaction);

                                sellOrder.Transactions.Add(transaction);
                                order.Transactions.Add(transaction);

                                _outputChannel.Add(order);
                                _outputChannel.Add(sellOrder);

                                if (sellOrder.PendingShares > 0)
                                {
                                    sellOrders[asset].Push(sellOrder);
                                }
                            }
                        }
                    }
                    else if (order.OrderType == Enum.OrderType.SELL)
                    {
                        sellOrders[asset].Push(order);

                        if (buyOrders[asset].Len() > 0 && buyOrders[asset].Peek().Price >= order.Price)
                        {
                            var buyOrder = buyOrders[asset].Pop();

                            if (buyOrder.PendingShares > 0)
                            {
                                var transaction = new Transaction(order, buyOrder, order.Shares, buyOrder.Price);

                                AddTransaction(transaction);

                                buyOrder.Transactions.Add(transaction);
                                order.Transactions.Add(transaction);

                                _outputChannel.Add(buyOrder);
                                _outputChannel.Add(order);

                                if (buyOrder.PendingShares > 0)
                                {
                                    buyOrders[asset].Push(buyOrder);
                                }
                            }
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Processamento de trades cancelado.");
            }
            /*
            finally
            {
                _inputChannel.CompleteAdding();
                _outputChannel.CompleteAdding();
            }*/
        }

        public BlockingCollection<Order> GetOutputChannel()
        {
            return _outputChannel;
        }

        private void AddTransaction(Transaction transaction)
        {
            var sellingShares = transaction.SellingOrder.PendingShares;
            var buyingShares = transaction.BuyingOrder.PendingShares;

            var minShares = sellingShares;

            if (buyingShares < minShares)
            {
                minShares = buyingShares;
            }

            transaction.SellingOrder.Investor.UpdateAssetPosition(transaction.SellingOrder.Asset.Id, -minShares);
            transaction.AddSellOrderPendingShares(-minShares);

            transaction.BuyingOrder.Investor.UpdateAssetPosition(transaction.BuyingOrder.Asset.Id, minShares);
            transaction.AddBuyOrderPendingShares(-minShares);

            transaction.CalculateTotal();
            transaction.CloseBuyOrder();
            transaction.CloseSellOrder();
            
            _transactions.Add(transaction);
        }
    }
}
