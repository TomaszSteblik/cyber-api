using Cyber.Application.Messages;

namespace Cyber.Application.Services;

public interface IMessageBroker
{
    Task Send(IMessage message);
}