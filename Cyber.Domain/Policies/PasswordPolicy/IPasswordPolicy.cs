namespace Cyber.Domain.Policies.PasswordPolicy;

public interface IPasswordPolicy
{
    public void CheckPassword(string password);
    public bool IsEnabled { get; }
}