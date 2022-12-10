using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cyber.Infrastructure.DAOs;

public class User : IMongoModel
{
    public ObjectId Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public UserPassword Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsBlocked { get; set; }
    public UserRole Role { get; set; }
}

public enum UserRole
{
    Admin,
    User,
    PasswordChangeRequired,
    Guest,
    Manager,
    Owner
}