using Homebroker.Domain.Enums;

namespace Homebroker.Domain
{
    public class Order : Entity
    {
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }
        public int Shares { get; set; }
        public decimal Price { get; set; }
        public OrderType Type { get; set; }
        public OrderStatus Status { get; set; }
        public int Partial {  get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
