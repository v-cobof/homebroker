namespace Homebroker.Domain
{
    public class Transaction : Entity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid InvestorId { get; set; }
        public int Shares { get; set; }
        public decimal Price { get; set; }
        public string BrokerTransactionId { get; set; }
    }
}
