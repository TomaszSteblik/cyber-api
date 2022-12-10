using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Commands.UpdateConfigFailedLoginTimeout;

public class UpdateConfigFailedLoginTimeoutHandler : IRequestHandler<UpdateConfigFailedLoginTimeoutCommand>
{
    private readonly IConfigRepository _configRepository;

    public UpdateConfigFailedLoginTimeoutHandler(IConfigRepository configRepository)
    {
        _configRepository = configRepository;
    }

    public async Task<Unit> Handle(UpdateConfigFailedLoginTimeoutCommand request, CancellationToken cancellationToken)
    {
        await _configRepository.SetFailedLoginTimeoutInMinutes(request.NewValue);
        return Unit.Value;
    }
}