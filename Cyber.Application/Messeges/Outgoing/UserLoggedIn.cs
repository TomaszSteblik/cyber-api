namespace Cyber.Application.Messeges.Outgoing;

public class UserLoggedIn : IMessage
{
    public DateTime DateTime { get; set; }
    public string Username { get; set; }

    public UserLoggedIn(DateTime dateTime, string username)
    {
        DateTime = dateTime;
        Username = username;
    }
}