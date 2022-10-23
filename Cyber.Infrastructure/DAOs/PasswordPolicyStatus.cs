using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cyber.Infrastructure.DAOs;

public class PasswordPolicyStatus : IMongoModel
{
    public ObjectId Id { get; set; }
    public string Key { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    public bool Status { get; set; }
}