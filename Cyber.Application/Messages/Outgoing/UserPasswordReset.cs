namespace Cyber.Application.Messages.Outgoing;

public class UserPasswordReset : IMessage
{
    public long UnixTimeStamp { get; set; }
    public string Username { get; set; }
    public Guid UserId { get; set; }

    public UserPasswordReset(DateTime dateTime, string username, Guid userId)
    {
        UnixTimeStamp = new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        Username = username;
        UserId = userId;
    }
}