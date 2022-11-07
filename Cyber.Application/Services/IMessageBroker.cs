using Cyber.Application.Enums;

namespace Cyber.Application.Services;

public interface IMessageBroker
{
    Task Send(object messageObject, MessageType type);
}