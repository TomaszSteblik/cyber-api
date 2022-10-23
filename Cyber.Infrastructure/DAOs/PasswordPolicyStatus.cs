using MongoDB.Bson;

namespace Cyber.Infrastructure.DAOs;

public class PasswordPolicyStatus : IMongoModel
{
    public ObjectId Id { get; set; }
    public string Key { get; set; }
    public string UserId { get; set; }
    public bool Status { get; set; }
}