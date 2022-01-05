using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MessageSender.Services;


//  var credential = new ChainedTokenCredential(new AzureCliCredential(),new ManagedIdentityCredential());
// //var credential = new DefaultAzureCredential();
// var serviceBusClient = new ServiceBusClient("mnl2022.servicebus.windows.net", credential);
// var sender = serviceBusClient.CreateSender("test");
// await sender.SendMessageAsync(new ServiceBusMessage("Test message"));
//
// Console.WriteLine("Hello, World!");

public class SenderService
{
    private readonly ILogger<SenderService> _logger;
    private readonly IConfiguration _configuration;
    private readonly ServiceBusClient _serviceBusClient;

    public SenderService( ILogger<SenderService> logger, IConfiguration configuration,ServiceBusClient serviceBusClient)
    {
         _configuration = configuration;
         _serviceBusClient = serviceBusClient;
         _logger = logger;
    }

    public async Task Run()
    {
        _logger.LogInformation("Starting...");

        var sender = _serviceBusClient.CreateSender("test");
        await sender.SendMessageAsync(new ServiceBusMessage("this is from console app"));

        _logger.LogInformation("Finished!");

        await Task.CompletedTask;
    }
}