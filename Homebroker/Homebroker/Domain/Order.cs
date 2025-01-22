using Homebroker.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Homebroker.Domain
{
    public class Order
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }
        public string WalletId { get; set; }
        public string AssetId { get; set; }
        public int Shares { get; set; }
        public decimal Price { get; set; }
        public OrderType Type { get; set; }
        public OrderStatus Status { get; set; }
        public int Partial {  get; set; }
    }
}
