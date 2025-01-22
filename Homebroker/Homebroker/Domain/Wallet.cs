using MongoDB.Bson.Serialization.Attributes;

namespace Homebroker.Domain
{
    public class Wallet
    {
        [BsonId]
        public string Id { get; set; }
    }
}
