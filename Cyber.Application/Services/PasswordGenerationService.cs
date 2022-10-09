namespace Cyber.Application.Services;

public class PasswordGenerationService : IPasswordGenerationService
{
    public string GeneratePassword()
    {
        var buffer = new byte[128];
        Random.Shared.NextBytes(buffer);
        return Convert.ToBase64String(buffer).Substring(0,12);
    }
}