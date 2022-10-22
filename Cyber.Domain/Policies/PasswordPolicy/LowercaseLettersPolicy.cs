using Cyber.Domain.Exceptions;
using Cyber.Domain.Repositories;

namespace Cyber.Domain.Policies.PasswordPolicy;

public class LowercaseLettersPolicy : IPasswordPolicy
{
    private readonly IPasswordPoliciesRepository _passwordPoliciesRepository;
    private const int MinimalAmountOfLowercaseLetters = 1;

    public LowercaseLettersPolicy(IPasswordPoliciesRepository passwordPoliciesRepository)
    {
        _passwordPoliciesRepository = passwordPoliciesRepository;
    }

    public void CheckPassword(string password)
    {
        if (password.Count(char.IsLower) < MinimalAmountOfLowercaseLetters)
            throw new PasswordPolicyNotFulfilledException(
                $"Password is required to have at least {MinimalAmountOfLowercaseLetters} lowercase letter");
    }

    public async Task<bool> IsEnabledForUser(Guid userId)
    {
        return await _passwordPoliciesRepository.GetEnabledStatusByPolicyKey(nameof(LowercaseLettersPolicy), userId);
    }
}