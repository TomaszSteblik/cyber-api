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

    public void ChangePassword(string newPassword, IEnumerable<IPasswordPolicy> passwordPolicies, IEnumerable<UserPassword> previousPasswords)
    {
        foreach (var passwordPolicy in passwordPolicies.Where(policy=>policy.IsEnabled))
        {
            passwordPolicy.CheckPassword(newPassword);
        }
        
        if (previousPasswords.Any(x => x.IsMatch(newPassword)))
            throw new PasswordAlreadyUsedException(newPassword, UserId.ToString());
        
        Password = new UserPassword(newPassword);
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

    public void ChangeInformations(string username, string email, string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(username) is false)
            Username = username;
        
        if (string.IsNullOrWhiteSpace(email) is false)
            Email = email;
        
        if (string.IsNullOrWhiteSpace(firstName) is false)
            FirstName = firstName;
        
        if (string.IsNullOrWhiteSpace(lastName) is false)
            LastName = lastName;
    }
}