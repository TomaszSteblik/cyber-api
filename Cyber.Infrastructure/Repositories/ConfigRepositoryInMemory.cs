using Cyber.Domain.Repositories;

namespace Cyber.Infrastructure.Repositories;

internal class ConfigRepositoryInMemory : IConfigRepository
{
    private int FailedLoginTimeoutInMinutes { get; set; }
    private int InactiveTimeoutInMinutes { get; set; }
    private int AllowedFailedLoginAttemptsCount { get; set; }

    public ConfigRepositoryInMemory()
    {
        FailedLoginTimeoutInMinutes = 15;
        InactiveTimeoutInMinutes = 15;
        AllowedFailedLoginAttemptsCount = 5;
    }
    
    public Task<int> GetFailedLoginTimeoutInMinutes()
    {
        return Task.FromResult(FailedLoginTimeoutInMinutes);
    }

    public Task SetFailedLoginTimeoutInMinutes(int value)
    {
        FailedLoginTimeoutInMinutes = value;
        return Task.CompletedTask;
    }

    public Task<int> GetInactiveTimeoutInMinutes()
    {
        return Task.FromResult(InactiveTimeoutInMinutes);
    }

    public Task SetInactiveTimeoutInMinutes(int value)
    {
        InactiveTimeoutInMinutes = value;
        return Task.CompletedTask;
    }

    public Task<int> GetAllowedFailedLoginAttemptsCount()
    {
        return Task.FromResult(AllowedFailedLoginAttemptsCount);
    }

    public Task SetAllowedFailedLoginAttemptsCount(int value)
    {
        AllowedFailedLoginAttemptsCount = value;
        return Task.CompletedTask;
    }
}