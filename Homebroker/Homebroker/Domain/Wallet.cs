namespace Homebroker.Domain
{
    public class Wallet : Entity
    {
        public string Name { get; set; }
        public ICollection<WalletAsset> WalletAssets { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
