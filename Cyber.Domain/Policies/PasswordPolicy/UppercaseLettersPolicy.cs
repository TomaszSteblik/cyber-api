using Cyber.Domain.Exceptions;
using Cyber.Domain.Repositories;

namespace Cyber.Domain.Policies.PasswordPolicy;

public class UppercaseLettersPolicy : IPasswordPolicy
{
    private readonly IPasswordPoliciesRepository _passwordPoliciesRepository;
    private const int MinimalNumberOfUppercaseLetters = 1;

    public UppercaseLettersPolicy(IPasswordPoliciesRepository passwordPoliciesRepository)
    {
        _passwordPoliciesRepository = passwordPoliciesRepository;
    }

    public void CheckPassword(string password)
    {
        if (password.Count(char.IsUpper) < MinimalNumberOfUppercaseLetters)
            throw new PasswordPolicyNotFulfilledException(
                $"Password requires at least {MinimalNumberOfUppercaseLetters} uppercase letter");
    }

    public async Task<bool> IsEnabledForUser(Guid userId)
    {
        return await _passwordPoliciesRepository.GetEnabledStatusByPolicyKey(nameof(UppercaseLettersPolicy), userId);
    }
}