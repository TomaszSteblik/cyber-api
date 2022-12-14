namespace Cyber.Application.DTOs.Update;

public class UpdateUserDto
{
    public Guid UserId { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? OldPassword { get; set; }
    public string Recaptcha { get; set; }
}