using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cyber.Infrastructure.DAOs;

public class OneTimePassword : IMongoModel
{
    public ObjectId Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }

    public int X { get; set; }
}