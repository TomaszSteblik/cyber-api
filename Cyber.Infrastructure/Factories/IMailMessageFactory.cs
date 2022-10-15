using Cyber.Domain.Entities;
using MimeKit;

namespace Cyber.Infrastructure.Factories;

public interface IMailMessageFactory
{
    MimeMessage CreateSendPasswordEmail(User user, string password, string senderEmail);
}