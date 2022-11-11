using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Cyber.Application.Messages;
using Cyber.Application.Services;

namespace Cyber.Infrastructure.Services;

public class AzureServiceBusBroker : IMessageBroker
{
    private readonly ServiceBusClient _serviceBusClient;
    private const string QueueName = "cyber-queue";
    public AzureServiceBusBroker(ServiceBusClient serviceBusClient)
    {
        _serviceBusClient = serviceBusClient;
    }

    public async Task Send<T>(T message) where T : IMessage
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