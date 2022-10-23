using MediatR;

namespace Cyber.Application.Commands.ChangePasswordLifetime;

public class ChangePasswordLifetimeCommand : IRequest
{
    public Guid UserId { get; set; }
    public uint ExpireTimeInDays { get; set; }

    public ChangePasswordLifetimeCommand(Guid userId, uint expireTimeInDays)
    {
        UserId = userId;
        ExpireTimeInDays = expireTimeInDays;
    }
}