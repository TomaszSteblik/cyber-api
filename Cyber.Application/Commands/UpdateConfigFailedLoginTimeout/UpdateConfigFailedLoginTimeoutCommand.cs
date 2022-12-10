using MediatR;

namespace Cyber.Application.Commands.UpdateConfigFailedLoginTimeout;

public class UpdateConfigFailedLoginTimeoutCommand : IRequest
{
    public int NewValue { get; set; }

    public UpdateConfigFailedLoginTimeoutCommand(int newValue)
    {
        NewValue = newValue;
    }
}