using MediatR;

namespace Cyber.Application.Commands.GenerateOneTimePassword;

public class GenerateOneTimePasswordCommand : IRequest
{
    public Guid UserId { get; set; }

    public GenerateOneTimePasswordCommand(Guid userId)
    {
        UserId = userId;
    }

}