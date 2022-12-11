using Cyber.Application.DTOs.Read;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Queries.GetConfig;

internal class GetConfigHandler : IRequestHandler<GetConfigQuery, ConfigDto>
{
    private readonly IConfigRepository _configRepository;

    public GetConfigHandler(IConfigRepository configRepository)
    {
        _configRepository = configRepository;
    }

    public async Task<ConfigDto> Handle(GetConfigQuery request, CancellationToken cancellationToken)
    {
        var configDto = new ConfigDto
        {
            InactiveTimeout = await _configRepository.GetInactiveTimeoutInMinutes(),
            AllowedLoginAttempts = await _configRepository.GetAllowedFailedLoginAttemptsCount(),
            FailedLoginTimeout = await _configRepository.GetFailedLoginTimeoutInMinutes()
        };
        return configDto;
    }
}