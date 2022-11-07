using Cyber.Application.Messeges;

namespace Cyber.Application.Services;

public interface IMessageBroker
{
    Task Send(IMessage message);
}