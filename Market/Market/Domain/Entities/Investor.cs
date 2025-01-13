using Market.Domain.ValueObjects;

namespace Market.Domain.Entities
{
    public class Investor : Entity
    {
        public string Name { get; private set; }
        public IReadOnlyCollection<InvestorAssetPosition> AssetPosition { get => _assetPosition; }

        private List<InvestorAssetPosition> _assetPosition { get; set; }

        public Investor(string name)
        {
            Name = name;
            _assetPosition = new List<InvestorAssetPosition>();
        }

        public void AddAssetPosition(InvestorAssetPosition position)
        {
            _assetPosition.Add(position);
        }

        public void UpdateAssetPosition(Guid assetId, int sharesAmount)
        {

        }
    }
}
