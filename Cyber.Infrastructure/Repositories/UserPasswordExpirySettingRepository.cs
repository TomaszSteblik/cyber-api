using Cyber.Domain.Repositories;

namespace Cyber.Infrastructure.Repositories;

public class UserPasswordExpirySettingRepository : IUserPasswordExpirySettingRepository
{
    private readonly Dictionary<Guid, uint> _passwordExpiryTimeDictionary;
    private const uint DefaultLifetime = 30;
    public UserPasswordExpirySettingRepository()
    {
        _passwordExpiryTimeDictionary = new Dictionary<Guid, uint>();
    }

    public Task<uint> GetPasswordLifetimeForUserGuid(Guid userId)
    {
        if (_passwordExpiryTimeDictionary.TryGetValue(userId, out var lifetime) is false)
            lifetime = DefaultLifetime;
        return Task.FromResult(lifetime);
    }

    public Task SetPasswordLifetimeForUserGuid(Guid userId, uint days)
    {
        if (_passwordExpiryTimeDictionary.TryAdd(userId, days) is false)
            _passwordExpiryTimeDictionary.Add(userId, days);
        return Task.CompletedTask;
    }
}