using SendMailWs;
using HCMDataAccess;
using Serilog;
using SendMailWs.Services;
using MailLib.Interfaces;
using MailLib.Services;
using SendMailWs.Interfaces;

var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production"}.json", true)
              .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)    
    .ConfigureServices(services =>
    {
        services.AddSingleton<ISettingsData, SettingsService>();
        services.AddSingleton<IMailQueue, MailQueueService>();
        services.AddSingleton<IMailService, MailService>();
        services.AddSingleton<IUnitOfWork, UnitOfWorkService>();

        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

await host.RunAsync();
