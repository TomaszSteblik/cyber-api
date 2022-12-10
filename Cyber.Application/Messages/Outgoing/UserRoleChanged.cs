using Cyber.Application.DTOs.Read;

namespace Cyber.Application.Messages.Outgoing;

public class UserRoleChanged : IMessage
{
    public long UnixTimeStamp { get; set; }
    public string Username { get; set; }
    public Guid UserId { get; set; }
    public UserRole OldRole { get; set; }
    public UserRole NewRole { get; set; }

    public UserRoleChanged(DateTime dateTime, string username, Guid userId, UserRole oldRole, UserRole newRole)
    {
        UnixTimeStamp = new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        Username = username;
        UserId = userId;
        OldRole = oldRole;
        NewRole = newRole;
    }
}