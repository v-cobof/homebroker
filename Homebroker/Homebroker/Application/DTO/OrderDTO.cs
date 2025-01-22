using Homebroker.Domain.Enums;
using System.Text.Json.Serialization;
using System.Transactions;

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
}
