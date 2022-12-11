using MediatR;

namespace Cyber.Application.Commands.UpdateConfigInactiveTimeout;

public class UpdateConfigInactiveTimeoutCommand : IRequest
{
    public int NewValue { get; set; }

    public UpdateConfigInactiveTimeoutCommand(int newValue)
    {
        NewValue = newValue;
    }
}