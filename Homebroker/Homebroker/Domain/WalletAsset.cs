namespace Homebroker.Domain
{
    public class WalletAsset : Entity
    {
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }
        public int Shares { get; set; }
    }
}
