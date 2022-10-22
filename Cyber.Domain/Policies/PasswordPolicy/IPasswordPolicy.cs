namespace Cyber.Domain.Policies.PasswordPolicy;

public interface IPasswordPolicy
{
    public void CheckPassword(string password);
    public Task<bool> IsEnabledForUser(Guid userId);
}