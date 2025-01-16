namespace Market.Application.DTO
{
    public record OrderOutput(string OrderId,
                              string InvestorId,
                              string AssetId,
                              string OrderType,
                              string Status,
                              int Partial,
                              int Shares,
                              List<TransactionOutput> Transactions);
}
