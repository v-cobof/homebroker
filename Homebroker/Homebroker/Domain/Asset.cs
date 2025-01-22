using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Homebroker.Domain
{
    public class Asset
    {
        [BsonId]
        public string Id { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
    }
}
