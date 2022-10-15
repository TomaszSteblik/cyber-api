using Cyber.Domain.Entities;

namespace Cyber.Domain.Services;

public interface IMailingService
{
    Task<bool> SendPasswordMail(User user, string password);
}