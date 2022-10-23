using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cyber.Infrastructure.DAOs;

public class UserPasswordExpirySetting : IMongoModel
{
    public ObjectId Id { get; set; }
    public uint ExpiryTimeInDays { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
}