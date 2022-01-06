using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MessageReceiver.Services;

public class ReceiverService
{
    private readonly ILogger<ReceiverService> _logger;

    // ReSharper disable once NotAccessedField.Local
    private readonly IConfiguration _configuration;
    private readonly ServiceBusReceiver _serviceBusReceiver;

    public ReceiverService(ILogger<ReceiverService> logger, IConfiguration configuration,
        ServiceBusClient serviceBusClient)
    {
        _configuration = configuration;
        _serviceBusReceiver = serviceBusClient.CreateReceiver("test/$DeadLetterQueue", new ServiceBusReceiverOptions{ ReceiveMode = ServiceBusReceiveMode.PeekLock });
        _logger = logger;
    }

    public async Task Run()
    {
        _logger.LogInformation("Starting...");
        var msg = await _serviceBusReceiver.ReceiveMessageAsync();
        _logger.LogInformation(msg.Body.ToString());
        _logger.LogInformation(msg.DeadLetterReason);
        _logger.LogInformation(msg.DeadLetterErrorDescription);
        
        await _serviceBusReceiver.CompleteMessageAsync(msg);
        _logger.LogInformation("Finished!");

        await Task.CompletedTask;
    }
}