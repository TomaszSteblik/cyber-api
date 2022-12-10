using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Commands.UpdateConfigInactiveTimeout;

internal class UpdateConfigInactiveTimeoutHandler : IRequestHandler<UpdateConfigInactiveTimeoutCommand>
{
    private readonly IConfigRepository _configRepository;

    public UpdateConfigInactiveTimeoutHandler(IConfigRepository configRepository)
    {
        _configRepository = configRepository;
    }
    
    public async Task<Unit> Handle(UpdateConfigInactiveTimeoutCommand request, CancellationToken cancellationToken)
    {
        await _configRepository.SetInactiveTimeoutInMinutes(request.NewValue);
        return Unit.Value;
    }
}