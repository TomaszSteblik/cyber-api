using MediatR;

namespace Cyber.Application.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest
{
    public Guid UserId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string RecaptchaToken { get; set; }

    public ChangePasswordCommand(string newPassword, Guid userId, string oldPassword, string recaptchaToken)
    {
        NewPassword = newPassword;
        UserId = userId;
        OldPassword = oldPassword;
        RecaptchaToken = recaptchaToken;
    }
}