namespace Cyber.Application.Messeges.Outgoing;

public class UserCreated : IMessage
{
    public long UnixTimeStamp { get; set; }
    public string Username { get; set; }
    public Guid UserId { get; set; }

    public UserCreated(DateTime dateTime, string username, Guid userId)
    {
        UnixTimeStamp = new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        Username = username;
        UserId = userId;
    }
}