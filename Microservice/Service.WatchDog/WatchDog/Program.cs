using DynoTools;
using Serilog;
using Server.Kafka;
using WatchDog;

LoggerConfiguration LogConfig = new LoggerConfiguration();
var builder = Host.CreateDefaultBuilder(args);
builder.UseSerilog();
builder.ConfigureServices((hostContext, services) =>
{
    Log.Logger = LogConfig.LoggerConfig().CreateLogger();
    services.AddSingleton<IProducer<HeartBeatMessage>>(producer => new Producer<HeartBeatMessage>(Topic.TOPIC_WATCHDOG_SEND_MESSAGE));
    services.AddSingleton<IConsumer<HeartBeatMessage>>(consumer => new Consumer<HeartBeatMessage>(Topic.TOPIC_WATCHDOG_RECEIVE_MESSAGE));
    services.AddHostedService<WatchDogWorker>();
});
using var host = builder.Build();
host.Run();


