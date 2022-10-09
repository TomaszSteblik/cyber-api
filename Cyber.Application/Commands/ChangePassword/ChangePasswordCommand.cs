using MediatR;

namespace Cyber.Application.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest
{
    public Guid UserId { get; set; }
    public string NewPassword { get; set; }

    public ChangePasswordCommand(string newPassword, Guid userId)
    {
        NewPassword = newPassword;
        UserId = userId;
    }
}