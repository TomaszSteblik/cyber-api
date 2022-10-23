using System.Text;

namespace Cyber.Domain.ValueObjects;

public class UserPassword
{
    public string Password { get; }
    public string Salt { get; }
    public DateTime CreatedAt { get; set; }

    public UserPassword(string plainPassword)
    {
        Salt = GenerateRandomSalt();
        Password = HashPassword(Salt, plainPassword);
        CreatedAt = DateTime.Now;
    }

    public bool IsMatch(string password) =>
        string.Compare(Password, HashPassword(Salt, password), StringComparison.InvariantCulture) == 0;

    private static string HashPassword(string saltString, string password)
    {
        var saltedPasswordString = saltString + password;
        var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPasswordString);
        var hashedPasswordBytes = System.Security.Cryptography.SHA512.HashData(saltedPasswordBytes);
        var hashedPasswordString = Convert.ToBase64String(hashedPasswordBytes);
        return hashedPasswordString;
    }

    private static string GenerateRandomSalt()
    {
        var saltBuffer = new byte[64];
        Random.Shared.NextBytes(saltBuffer);
        return Convert.ToBase64String(saltBuffer);
    }
}