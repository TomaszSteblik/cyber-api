using MediatR;

namespace Cyber.Application.Commands.ChangePolicyStatus;

public class ChangePolicyStatusCommand : IRequest
{
    public Guid UserId { get; }
    public string Key { get; }
    public bool Status { get; }

    public ChangePolicyStatusCommand(Guid userId, string key, bool status)
    {
        UserId = userId;
        Key = key;
        Status = status;
    }
}