namespace Cyber.Application.DTOs.Read;

public class LoginUserByOneTimePasswordDto
{
    public string Login { get; set; }
    public double Password { get; set; }
    public string CaptchaChallengeId { get; set; }
}