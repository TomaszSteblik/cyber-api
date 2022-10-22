using Cyber.Domain.Repositories;

namespace Cyber.Infrastructure.Repositories;

public class PasswordPoliciesRepository : IPasswordPoliciesRepository
{
    private readonly Dictionary<(Guid userId, string policyKey), bool> _policyStatusDictionary;

    public PasswordPoliciesRepository()
    {
        _policyStatusDictionary = new Dictionary<(Guid userId, string policyKey), bool>();
    }

    public Task<bool> GetEnabledStatusByPolicyKey(string key, Guid userId)
    {
        if (_policyStatusDictionary.TryGetValue((userId, key), out var isEnabled) is false)
            isEnabled = true;
        return Task.FromResult(isEnabled);
    }

    public Task SaveEnabledStatus(string key, bool status, Guid userId)
    {
        if (_policyStatusDictionary.TryAdd((userId, key), status) is false)
            _policyStatusDictionary.Add((userId, key), status);
        return Task.CompletedTask;
    }
}