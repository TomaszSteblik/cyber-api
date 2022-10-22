namespace Cyber.Domain.Repositories;

public interface IPasswordPoliciesRepository
{
    Task<bool> GetEnabledStatusByPolicyKey(string key, Guid userId);
    Task SaveEnabledStatus(string key, bool status, Guid userId);
}