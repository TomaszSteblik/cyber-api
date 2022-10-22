using MediatR;

namespace Cyber.Application.Commands.BlockUser;

public class BlockUserCommand : IRequest
{
    public Guid UserId { get; set; }
    public bool Block { get; set; }

    public BlockUserCommand(Guid userId, bool block)
    {
        UserId = userId;
        Block = block;
    }
}