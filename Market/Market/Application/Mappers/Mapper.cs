using Market.Application.DTO;
using Market.Domain.Entities;
using Market.Domain.Enum;
using Market.Domain.ValueObjects;

namespace Market.Application.Mappers
{
    public class Mapper
    {
        public static Order MapOrderFromInput(TradeInput input)
        {
            var asset = new Asset(input.AssetId, input.AssetId, 1000);
            var investor = new Investor(input.InvestorId, input.InvestorId);
            var order = new Order(input.OrderId, investor, asset, input.Shares, input.Price, (OrderType)Enum.Parse(typeof(OrderType), input.OrderType));

            if (input.CurrentShares > 0)
            {
                var assetPosition = new InvestorAssetPosition(asset.Id, input.CurrentShares);
                investor.AddAssetPosition(assetPosition);
            }
            
            return order;
        }

        public static OrderOutput MapOutputFromOrder(Order order)
        {
            var transactions = order.Transactions.Select(t => new TransactionOutput(t.Id.ToString(),
                                                                                    t.BuyingOrder.Investor.Id.ToString(),
                                                                                    t.SellingOrder.Investor.Id.ToString(),
                                                                                    t.SellingOrder.Asset.Id.ToString(),
                                                                                    t.Price,
                                                                                    t.SellingOrder.Shares - t.SellingOrder.PendingShares)).ToList();

            return new OrderOutput(order.Id.ToString(),
                                   order.Investor.Id.ToString(),
                                   order.Asset.Id.ToString(),
                                   order.OrderType.ToString(),
                                   order.OrderStatus.ToString(),
                                   order.PendingShares,
                                   order.Shares,
                                   transactions);
        }
    }
}
