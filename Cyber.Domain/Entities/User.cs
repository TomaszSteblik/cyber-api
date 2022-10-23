using System.Net.Mail;
using Cyber.Domain.Enums;
using Cyber.Domain.Exceptions;
using Cyber.Domain.Policies.PasswordPolicy;
using Cyber.Domain.ValueObjects;

namespace Cyber.Domain.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public UserPassword Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsBlocked { get; set; }
    public UserRole Role { get; set; }

    public User(string username, string password, string firstName, string lastName, string email, UserRole role)
    {
        UserId = Guid.NewGuid();
        Username = username;
        Password = new UserPassword(password);
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        IsBlocked = false;
        Role = role;
    }

    public async Task ChangePassword(string newPassword, IEnumerable<IPasswordPolicy> passwordPolicies, IEnumerable<UserPassword> previousPasswords)
    {
        foreach (var passwordPolicy in passwordPolicies)
        {
            if (await passwordPolicy.IsEnabledForUser(UserId))
                passwordPolicy.CheckPassword(newPassword);
        }

        if (previousPasswords.Any(x => x.IsMatch(newPassword)))
            throw new PasswordAlreadyUsedException(newPassword, UserId.ToString());

        Password = new UserPassword(newPassword);

        if (Role == UserRole.PasswordChangeRequired)
            Role = UserRole.User;
    }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username))
            throw new InvalidUserDataException($"User cannot have empty username.");
        if (string.IsNullOrWhiteSpace(FirstName))
            throw new InvalidUserDataException($"User cannot have empty first name.");
        if (string.IsNullOrWhiteSpace(LastName))
            throw new InvalidUserDataException($"User cannot have empty last name.");
        if (string.IsNullOrWhiteSpace(Email))
            throw new InvalidUserDataException($"User need to have email specified.");

        if (MailAddress.TryCreate(Email, out _) is false)
            throw new InvalidUserDataException($"Invalid email");
    }

    public void ChangeInformations(string? username, string? email, string? firstName, string? lastName)
    {
        if (username is not null)
            Username = username;

        if (email is not null)
            Email = email;

        if (firstName is not null)
            FirstName = firstName;

        if (lastName is not null)
            LastName = lastName;
    }

    public void Block()
    {
        IsBlocked = true;
    }

    public void Unblock()
    {
        IsBlocked = false;
    }

    public void CheckPasswordExpiryDate()
    {
        if (Password.CreatedAt <= DateTime.Now.AddDays(-30))
            throw new PasswordExpiredException(Username);
    }
}