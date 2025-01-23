namespace Homebroker.Domain
{
    public class Asset : Entity
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public ICollection<WalletAsset> WalletAssets { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
