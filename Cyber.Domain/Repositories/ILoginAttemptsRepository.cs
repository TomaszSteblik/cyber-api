namespace Cyber.Domain.Repositories;

public interface ILoginAttemptsRepository
{
    Task AddFailedAttempt(Guid userId);
    Task<int> GetFailedAttemptsCountForUser(Guid userId);
    Task RemoveAttemptsOlderThan(DateTime time);
}