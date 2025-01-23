using Homebroker.Domain;
using Homebroker.Domain.Enums;

namespace Homebroker.Application.DTO
{
    public class InitTransactionDTO
    {
        public string AssetId { get; set; }
        public string WalletId { get; set; }
        public int Shares { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
    }

    public class InputExecuteTransactionDto
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string InvestorId { get; set; }
        public string BrokerTransactionId { get; set; }
        public int NegotiatedShares { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderResultDTO
    {
        public string Id { get; set; }
        public string WalletId { get; set; }
        public string AssetId { get; set; }
        public int Shares { get; set; }
        public decimal Price { get; set; }
        public OrderType Type { get; set; }
        public OrderStatus Status { get; set; }
        public int Partial { get; set; }
        public List<TransactionDTO> Transactions { get; set; }
        public AssetResultDTO Asset { get; set; }
    }
}
