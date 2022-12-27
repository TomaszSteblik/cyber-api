namespace Cyber.Application.Services;

public interface IReCaptchaService
{
    Task<bool> VerifyToken(string token);
}