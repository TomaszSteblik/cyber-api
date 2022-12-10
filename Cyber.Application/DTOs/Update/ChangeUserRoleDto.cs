namespace Cyber.Application.DTOs.Update;

public class ChangeUserRoleDto
{
    public Guid UserId { get; set; }
    public UserRole NewRole { get; set; }
}

public enum UserRole
{
    Admin,
    User,
    Guest,
    Manager,
    Owner
}