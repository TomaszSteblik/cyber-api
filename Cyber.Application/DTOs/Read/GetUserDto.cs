namespace Cyber.Application.DTOs.Read;

public class GetUserDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsBlocked { get; set; }
    public UserRole Role { get; set; }
}

public enum UserRole
{
    User,
    Admin
}