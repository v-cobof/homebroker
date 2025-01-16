using Market.Domain.ValueObjects;

namespace Market.Domain.Entities
{
    public class Investor
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<InvestorAssetPosition> AssetPositions { get => _assetPositions; }

        private List<InvestorAssetPosition> _assetPositions { get; set; }

        public Investor(string id, string name) : base()
        {
            Id = id;
            Name = name;
            _assetPositions = new List<InvestorAssetPosition>();
        }

        public void AddAssetPosition(InvestorAssetPosition position)
        {
            _assetPositions.Add(position);
        }

        public void UpdateAssetPosition(string assetId, int sharesAmount)
        {
            var assetPosition = _assetPositions.Find(t => t.AssetId == assetId);

            if (assetPosition is null)
            {
                _assetPositions.Add(new InvestorAssetPosition(assetId, sharesAmount));
            }
            else
            {
                assetPosition.Shares += sharesAmount;
            }
        }
    }
}
