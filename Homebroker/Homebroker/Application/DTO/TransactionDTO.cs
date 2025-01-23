using Homebroker.Domain;

namespace Homebroker.Application.DTO
{
    public class TransactionDTO
    {
        public Guid OrderId { get; set; }
        public Guid InvestorId { get; set; }
        public int Shares { get; set; }
        public decimal Price { get; set; }
        public string BrokerTransactionId { get; set; }
    }
}
