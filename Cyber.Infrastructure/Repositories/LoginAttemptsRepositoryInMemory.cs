using Cyber.Domain.Repositories;

namespace Cyber.Infrastructure.Repositories;

public class LoginAttemptsRepositoryInMemory : ILoginAttemptsRepository
{
    private readonly List<(Guid userId, DateTime attemptTime)> _loginAttempts;

    public LoginAttemptsRepositoryInMemory()
    {
        _loginAttempts = new List<(Guid userId, DateTime attemptTime)>();
    }

    public Task AddFailedAttempt(Guid userId)
    {
        _loginAttempts.Add((userId, DateTime.Now));
        return Task.CompletedTask;
    }

    public Task<int> GetFailedAttemptsCountForUser(Guid userId)
    {
        var attemptsCount = _loginAttempts.Count(loginAttempt => loginAttempt.userId == userId);
        return Task.FromResult(attemptsCount);
    }

    public Task RemoveAttemptsOlderThan(DateTime time)
    {
        _loginAttempts.RemoveAll(loginAttempt => loginAttempt.attemptTime <= time);
        return Task.CompletedTask;
    }
}