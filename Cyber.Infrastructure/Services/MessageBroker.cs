using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Cyber.Application.Messeges;
using Cyber.Application.Services;

namespace Cyber.Infrastructure.Services;

public class MessageBroker : IMessageBroker
{
    private readonly ServiceBusClient _serviceBusClient;
    private const string QueueName = "cyber-queue";
    public MessageBroker(ServiceBusClient serviceBusClient)
    {
        _serviceBusClient = serviceBusClient;
    }

    public async Task Send(IMessage message)
    {
        var sender = _serviceBusClient.CreateSender(QueueName);
        var messageToSerialize = new
        {
            Event = message.GetType().Name,
            Value = message
        };
        var messageBytes = JsonSerializer.SerializeToUtf8Bytes(messageToSerialize);
        var messageUtf = new ServiceBusMessage(messageBytes);
        await sender.SendMessageAsync(messageUtf);
    }
}