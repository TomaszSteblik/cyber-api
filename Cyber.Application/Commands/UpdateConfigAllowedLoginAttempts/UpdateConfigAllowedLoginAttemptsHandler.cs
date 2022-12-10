using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Commands.UpdateConfigAllowedLoginAttempts;

internal class UpdateConfigAllowedLoginAttemptsHandler : IRequestHandler<UpdateConfigAllowedLoginAttemptsCommand>
{
    private readonly IConfigRepository _configRepository;

    public UpdateConfigAllowedLoginAttemptsHandler(IConfigRepository configRepository)
    {
        _configRepository = configRepository;
    }

    public async Task<Unit> Handle(UpdateConfigAllowedLoginAttemptsCommand request, CancellationToken cancellationToken)
    {
        await _configRepository.SetAllowedFailedLoginAttemptsCount(request.NewValue);
        return Unit.Value;
    }
}