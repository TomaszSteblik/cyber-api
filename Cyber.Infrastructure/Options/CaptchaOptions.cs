namespace Cyber.Infrastructure.Options;

public sealed class CaptchaOptions
{
    public const string CaptchaSectionName = "Captcha";
    public string Secret { get; set; }
}