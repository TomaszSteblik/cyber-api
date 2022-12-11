using Cyber.Domain.Exceptions;
using Cyber.Domain.Repositories;

namespace Cyber.Domain.Services;

internal class UserLoginAttemptsBlockService : IUserLoginAttemptsBlockService
{
    private readonly ILoginAttemptsRepository _loginAttemptsRepository;
    private readonly IConfigRepository _configRepository;

    public UserLoginAttemptsBlockService(ILoginAttemptsRepository loginAttemptsRepository, IConfigRepository configRepository)
    {
        _loginAttemptsRepository = loginAttemptsRepository;
        _configRepository = configRepository;
    }

    public async Task CheckIfUserIsBlockedFromFailedLoginAttempts(Guid userId)
    {
        var attemptsCount = await _loginAttemptsRepository.GetFailedAttemptsCountForUser(userId);
        if (attemptsCount >= await _configRepository.GetAllowedFailedLoginAttemptsCount())
            throw new UserLoginBlockedException(await _configRepository.GetFailedLoginTimeoutInMinutes());
    }

    public async Task RegisterFailedLoginAttempt(Guid userId)
    {
        await _loginAttemptsRepository.AddFailedAttempt(userId);
    }
}