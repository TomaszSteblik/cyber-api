using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cyber.Infrastructure.DAOs;

public class PreviousPassword : IMongoModel
{
    public ObjectId Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    public UserPassword Password { get; set; }
}