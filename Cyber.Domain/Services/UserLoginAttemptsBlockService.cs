using Cyber.Domain.Exceptions;
using Cyber.Domain.Repositories;

namespace Cyber.Domain.Services;

internal class UserLoginAttemptsBlockService : IUserLoginAttemptsBlockService
{
    private readonly ILoginAttemptsRepository _loginAttemptsRepository;
    private const int AttemptsLimit = 5;
    private const int LoginBlockTimeInMinutes = 15;

    public UserLoginAttemptsBlockService(ILoginAttemptsRepository loginAttemptsRepository)
    {
        _loginAttemptsRepository = loginAttemptsRepository;
    }

    public async Task CheckIfUserIsBlockedFromFailedLoginAttempts(Guid userId)
    {
        var attemptsCount = await _loginAttemptsRepository.GetFailedAttemptsCountForUser(userId);
        if (attemptsCount >= AttemptsLimit)
            throw new UserLoginBlockedException(LoginBlockTimeInMinutes);
    }

    public async Task RegisterFailedLoginAttempt(Guid userId)
    {
        await _loginAttemptsRepository.AddFailedAttempt(userId);
    }
}