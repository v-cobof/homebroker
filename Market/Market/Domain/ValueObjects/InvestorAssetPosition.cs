namespace Market.Domain.ValueObjects
{
    public class InvestorAssetPosition
    {
        public Guid AssetId { get; set; }
        public int Shares { get; set; }
    }
}
