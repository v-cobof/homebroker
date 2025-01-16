using System.Data.Common;
using System.Transactions;

namespace Market.Domain.Entities
{
    // Match between a buying order and a selling order
    public class Transaction
    {
        public string Id { get; set; }
        public Order SellingOrder { get; set; }
        public Order BuyingOrder { get; set; }
        public int Shares { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; private set; }
        public DateTime DateTime { get; set; }

        public Transaction(Order sell, Order buy, int shares, decimal price) : base()
        {
            Id = Guid.NewGuid().ToString();
            Shares = shares;
            Price = price;
            SellingOrder = sell;
            BuyingOrder = buy;
            Total = Shares * Price;
            DateTime = DateTime.Now;
        }

        public void CalculateTotal()
        {
            Total = Shares * Price;
        }

        public void CloseBuyOrder()
        {
            if (BuyingOrder.PendingShares == 0)
            {
                BuyingOrder.OrderStatus = Enum.OrderStatus.Closed;
            }
        }

        public void CloseSellOrder()
        {
            if (SellingOrder.PendingShares == 0)
            {
                SellingOrder.OrderStatus = Enum.OrderStatus.Closed;
            }
        }

        public void AddBuyOrderPendingShares(int shares)
        {
            BuyingOrder.PendingShares += shares;
        }

        public void AddSellOrderPendingShares(int shares)
        {
            SellingOrder.PendingShares += shares;
        }
    }
}
