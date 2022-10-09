using Cyber.Domain.Exceptions;

namespace Cyber.Domain.Policies.PasswordPolicy;

public class UppercaseLettersPolicy : IPasswordPolicy
{
    private const int MinimalNumberOfUppercaseLetters = 1;
    public void CheckPassword(string password)
    {
        if (password.Count(char.IsUpper) < MinimalNumberOfUppercaseLetters)
            throw new PasswordPolicyNotFulfilledException(
                $"Password requires at least {MinimalNumberOfUppercaseLetters} uppercase letter");
    }

    public bool IsEnabled => true;
}