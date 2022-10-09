using Cyber.Domain.Exceptions;

namespace Cyber.Domain.Policies.PasswordPolicy;

public class NumbersPolicy : IPasswordPolicy
{
    private const int MinimalAmountOfNumbersInPassword = 3;

    public void CheckPassword(string password)
    {
        if (password.Count(char.IsDigit) < MinimalAmountOfNumbersInPassword)
            throw new PasswordPolicyNotFulfilledException(
                $"Password need to contain at least {MinimalAmountOfNumbersInPassword} numbers.");
    }

    public bool IsEnabled => true;
}