using MediatR;

namespace Cyber.Application.Commands.UpdateConfigAllowedLoginAttempts;

public class UpdateConfigAllowedLoginAttemptsCommand : IRequest
{
    public int NewValue { get; set; }

    public UpdateConfigAllowedLoginAttemptsCommand(int newValue)
    {
        NewValue = newValue;
    }
}