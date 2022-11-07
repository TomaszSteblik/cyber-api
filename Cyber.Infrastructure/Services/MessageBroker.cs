using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Core.Serialization;
using Azure.Messaging.ServiceBus;
using Cyber.Application.Enums;
using Cyber.Application.Services;

namespace Cyber.Infrastructure.Services;

public class MessageBroker : IMessageBroker
{
    private readonly ServiceBusClient _serviceBusClient;

    public MessageBroker(ServiceBusClient serviceBusClient)
    {
        _serviceBusClient = serviceBusClient;
    }
    
    private string QueueName(MessageType messageType) => messageType switch
    {
        MessageType.UserTracking => "cyber-queue",
        _ => throw new ArgumentOutOfRangeException(nameof(messageType), messageType, "Message type not supported yet")
    };
    
    public async Task Send(object messageObject, MessageType type)
    {
        var sender = _serviceBusClient.CreateSender(QueueName(type));
        var messageBytes = JsonSerializer.SerializeToUtf8Bytes(messageObject);
        var message = new ServiceBusMessage(messageBytes);
        await sender.SendMessageAsync(message);
    }
}