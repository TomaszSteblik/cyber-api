using MediatR;

namespace Cyber.Application.Commands.UserLogout;

public class UserLogoutCommand : IRequest
{
    public Guid UserId { get; set; }

    public UserLogoutCommand(Guid userId)
    {
        UserId = userId;
    }
}