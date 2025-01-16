using Market.Domain.Enum;

namespace Market.Domain.Entities
{
    public class Order
    {
        public string Id { get; private set; }
        public Investor Investor {  get; private set; }
        public Asset Asset { get; private set; }
        public int Shares { get; private set; }
        public int PendingShares { get; set; }
        public decimal Price { get; private set; }
        public OrderType OrderType { get; private set; }
        public OrderStatus OrderStatus { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Order(string id, Investor investor, Asset asset, int shares, decimal price, OrderType type)
        {
            Id = id;
            Investor = investor;
            PendingShares = shares;
            Asset = asset;
            Shares = shares;
            Price = price;
            OrderStatus = OrderStatus.Open;
            OrderType = type;
            Transactions = new List<Transaction>();
        }
    }
}
