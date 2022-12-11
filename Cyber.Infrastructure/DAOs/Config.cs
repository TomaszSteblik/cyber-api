using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cyber.Infrastructure.DAOs;

public class Config : IMongoModel
{
    public ObjectId Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public ConfigKey Key { get; set; }
    public int Value { get; set; }
}

public enum ConfigKey
{
    FailedLoginTimeoutInMinutes, InactiveTimeoutInMinutes, AllowedFailedLoginAttemptsCount
}