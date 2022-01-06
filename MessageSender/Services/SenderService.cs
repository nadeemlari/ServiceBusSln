using Azure.Messaging.ServiceBus;
using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MessageSender.Services;

public class SenderService
{
    private readonly ILogger<SenderService> _logger;
    // ReSharper disable once NotAccessedField.Local
    private readonly IConfiguration _configuration;
    private readonly ServiceBusSender _serviceBusSender;
    private List<Order> _orders;

    public SenderService( ILogger<SenderService> logger, IConfiguration configuration,ServiceBusClient serviceBusClient)
    {
         _configuration = configuration;
         _serviceBusSender = serviceBusClient.CreateSender("test");
         _logger = logger;
         _orders = new List<Order>
         {
             new Order {Id = 1, Name = "name 1", Qty = 2, Price = 3},
             new Order {Id = 2, Name = "name 2", Qty = 4, Price = 6},
             new Order {Id = 3, Name = "name 2", Qty = 6, Price = 9},
         };

    }

    public async Task Run()
    {
        _logger.LogInformation("Starting...");
        _orders.ForEach(o =>
        {
            var msg = new ServiceBusMessage(o.ToString())
            {
                ContentType = "application/json",
                MessageId = "msg101"
                
            };
            _serviceBusSender.SendMessageAsync(msg).GetAwaiter().GetResult();
        });

        _logger.LogInformation("Finished!");

        await Task.CompletedTask;
    }
}