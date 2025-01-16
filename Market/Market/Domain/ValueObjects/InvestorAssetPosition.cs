namespace Market.Domain.ValueObjects
{
    public record InvestorAssetPosition
    {
        public string AssetId { get; set; }
        public int Shares { get; set; }

        public InvestorAssetPosition(string assetId, int shares)
        {
            AssetId = assetId;
            Shares = shares;
        }
    }
}
