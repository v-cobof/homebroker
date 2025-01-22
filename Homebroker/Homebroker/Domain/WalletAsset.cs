using MongoDB.Bson.Serialization.Attributes;

namespace Homebroker.Domain
{
    public class WalletAsset
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }

        public string WalletId { get; set; }

        public string AssetId { get; set; }

        public int Shares { get; set; }

        // pair walletId and assetId must be unique

    }
}
