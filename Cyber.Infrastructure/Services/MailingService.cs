using Cyber.Domain.Entities;
using Cyber.Domain.Services;
using Cyber.Infrastructure.Factories;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace Cyber.Infrastructure.Services;

public class MailingService : IMailingService
{
    private readonly IMailMessageFactory _messageFactory;
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _senderMailAddress;
    private readonly string _senderPassword;
    private readonly string _senderUsername;

    public MailingService(IConfiguration configuration, IMailMessageFactory messageFactory)
    {
        _messageFactory = messageFactory;
        var smtp = configuration.GetSection("Smtp");
        _smtpHost = smtp["host"];
        _senderMailAddress = smtp["address"];
        _smtpPort = Convert.ToInt32(smtp["port"]);
        _senderPassword = smtp["password"];
        _senderUsername = smtp["login"];
    }
    public async Task<bool> SendPasswordMail(User user, string password)
    {
        //TODO: Check if email has been sent, for now we just believe for it to be true
        var message = _messageFactory.CreateSendPasswordEmail(user, password, _senderMailAddress);
        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpHost, _smtpPort, true);
        await client.AuthenticateAsync(_senderUsername, _senderPassword);
        var response = await client.SendAsync(message);
        await client.DisconnectAsync(true);
        return true;
    }
}