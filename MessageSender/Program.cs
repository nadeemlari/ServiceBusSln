using MessageSender;
using MessageSender.Services;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();
ConfigureServices(services);
var serviceProvider = services.BuildServiceProvider();

var app = serviceProvider.GetService<SenderService>();
if (app != null) await app.Run();

Console.Read();

static void ConfigureServices(IServiceCollection services)
{
    // configure logging
    services.AddLogging(builder =>
    {
        builder.AddConsole();
        builder.AddDebug();
    });

    // build config
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false)
        .AddEnvironmentVariables()
        .Build();

    services.AddSingleton<IConfiguration>(configuration);
    
    services.AddAzureClients(cfg =>
    {
        var ns = configuration.GetValue<string>("ServiceBus:NameSpace");
            
        cfg.AddServiceBusClientWithNamespace(ns)
            .WithCredential(AzureCredentialBuilder.Credential());
    });
    
    services.AddTransient<SenderService>();
}