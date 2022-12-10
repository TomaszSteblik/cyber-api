using Cyber.Domain.Entities;
using MimeKit;

namespace Cyber.Infrastructure.Factories;

public class MailMessageFactory : IMailMessageFactory
{
    public MimeMessage CreateSendPasswordEmail(User user, string password, string senderEmail)
    {
        var message = new MimeMessage();
        message.To.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email));
        message.From.Add(new MailboxAddress("Cybersec website", senderEmail));
        message.Subject = "New password";
        message.Body = new TextPart("plain") { Text = $"Hello, your password is: {password}" };
        return message;
    }

    public MimeMessage CreateSendOneTimePasswordEmail(User user, int x, string senderMailAddress)
    {
        var message = new MimeMessage();
        message.To.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email));
        message.From.Add(new MailboxAddress("Cybersec website", senderMailAddress));
        message.Subject = "One time password";
        message.Body = new TextPart("plain") { Text = $"Hello, your one time password X value is: {x}" };
        return message;
    }
}