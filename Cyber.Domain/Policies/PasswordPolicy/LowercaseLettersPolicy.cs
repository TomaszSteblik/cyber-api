using Cyber.Domain.Exceptions;

namespace Cyber.Domain.Policies.PasswordPolicy;

public class LowercaseLettersPolicy : IPasswordPolicy
{
    private const int MinimalAmountOfLowercaseLetters = 1;
    public void CheckPassword(string password)
    {
        if (password.Count(char.IsLower) < MinimalAmountOfLowercaseLetters)
            throw new PasswordPolicyNotFulfilledException(
                $"Password is required to have at least {MinimalAmountOfLowercaseLetters} lowercase letter");
    }

    public bool IsEnabled => true;
}