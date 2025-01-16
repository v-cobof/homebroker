namespace Market.Application.DTO
{
    public record TransactionOutput(string TransactionId,
                                    string BuyerId,
                                    string SellerId,
                                    string AssetId,
                                    decimal Price,
                                    int Shares);
}
