namespace Cyber.Infrastructure.DAOs;

public class UserPassword
{
    public string Password { get; set; }
    public string Salt { get; set; }
    public DateTime CreatedAt { get; set; }
}