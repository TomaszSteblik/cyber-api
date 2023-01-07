using System.Net;

namespace Cyber.Application.Exceptions;

public class CaptchaChallengeFailedException : ApplicationException
{
    public CaptchaChallengeFailedException(string message) : base(message)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
    public override string ErrorCode => "captcha_challenge_failed";
}