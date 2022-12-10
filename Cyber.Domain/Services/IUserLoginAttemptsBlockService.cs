namespace Cyber.Domain.Services;

public interface IUserLoginAttemptsBlockService
{
    Task CheckIfUserIsBlockedFromFailedLoginAttempts(Guid userId);
    Task RegisterFailedLoginAttempt(Guid userId);
}