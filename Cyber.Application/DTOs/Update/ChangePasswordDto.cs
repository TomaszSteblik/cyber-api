namespace Cyber.Application.DTOs.Update;

public class ChangePasswordDto
{
    public string NewPassword { get; set; }
    public string OldPassword { get; set; }
    public string RecaptchaToken { get; set; }
}