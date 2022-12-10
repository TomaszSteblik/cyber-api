namespace Cyber.Domain.Repositories;

public interface IConfigRepository
{
    Task<int> GetFailedLoginTimeoutInMinutes();
    Task SetFailedLoginTimeoutInMinutes(int value);
    Task<int> GetInactiveTimeoutInMinutes();
    Task SetInactiveTimeoutInMinutes(int value);
    Task<int> GetAllowedFailedLoginAttemptsCount();
    Task SetAllowedFailedLoginAttemptsCount(int value);
}