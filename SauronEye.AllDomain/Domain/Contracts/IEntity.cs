using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SauronEye.AllDomain.Domain.Contracts
{
    public interface IEntity
    {
        [BsonId, BsonRepresentation(BsonType.String)]
        string Id { get; set; }
    }
}
