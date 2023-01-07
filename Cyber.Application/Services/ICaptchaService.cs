namespace Cyber.Application.Services;

public interface ICaptchaService
{
    Task<bool> VerifyReCaptchaToken(string token);
    Task<bool> VerifyPuzzleCaptchaChallenge(string challengeId);
}