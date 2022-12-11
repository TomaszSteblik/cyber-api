using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cyber.Infrastructure.DAOs;

public class LoginAttempt : IMongoModel
{
    public ObjectId Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }

    public DateTime AttemptTime { get; set; }
}