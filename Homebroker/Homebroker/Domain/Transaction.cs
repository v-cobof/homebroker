using MongoDB.Bson.Serialization.Attributes;

namespace Homebroker.Domain
{
    public class Transaction
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string InvestorId { get; set; }
        public int Shares { get; set; }
        public decimal Price { get; set; }
        public string BrokerTransactionId { get; set; }
    }
}
