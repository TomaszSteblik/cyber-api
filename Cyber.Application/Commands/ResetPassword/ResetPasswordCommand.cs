using MediatR;

namespace Cyber.Application.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest
{
    public string UserEmail { get; set; }

    public ResetPasswordCommand(string userEmail)
    {
        UserEmail = userEmail;
    }
}