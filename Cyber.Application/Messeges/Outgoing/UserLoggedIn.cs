namespace Cyber.Application.Messeges.Outgoing;

public class UserLoggedIn : IMessage
{
    public long UnixTimeStamp { get; set; }
    public string Username { get; set; }

    public UserLoggedIn(DateTime dateTime, string username)
    {
        UnixTimeStamp = new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        Username = username;
    }
}