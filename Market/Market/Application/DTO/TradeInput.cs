using System.Text.Json.Serialization;

namespace Market.Application.DTO
{
    public record TradeInput(
            [property: JsonPropertyName("order_id")] string OrderId,
            [property: JsonPropertyName("investor_id")] string InvestorId,
            [property: JsonPropertyName("asset_id")] string AssetId,
            [property: JsonPropertyName("current_shares")] int CurrentShares,
            [property: JsonPropertyName("shares")] int Shares,
            [property: JsonPropertyName("price")] decimal Price,
            [property: JsonPropertyName("order_type")] string OrderType);
}
