using Cyber.Domain.Exceptions;
using Cyber.Domain.Repositories;

namespace Cyber.Domain.Policies.PasswordPolicy;

public class NumbersPolicy : IPasswordPolicy
{
    private readonly IPasswordPoliciesRepository _passwordPoliciesRepository;
    private const int MinimalAmountOfNumbersInPassword = 3;

    public NumbersPolicy(IPasswordPoliciesRepository passwordPoliciesRepository)
    {
        _passwordPoliciesRepository = passwordPoliciesRepository;
    }

    public void CheckPassword(string password)
    {
        if (password.Count(char.IsDigit) < MinimalAmountOfNumbersInPassword)
            throw new PasswordPolicyNotFulfilledException(
                $"Password need to contain at least {MinimalAmountOfNumbersInPassword} numbers.");
    }

    public async Task<bool> IsEnabledForUser(Guid userId)
    {
        return await _passwordPoliciesRepository.GetEnabledStatusByPolicyKey(nameof(NumbersPolicy), userId);
    }
}