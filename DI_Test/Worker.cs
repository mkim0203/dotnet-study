using Microsoft.Extensions.Hosting;

namespace DI_Test;

public class Worker : BackgroundService
{
    private readonly ConsoleWriter _messageWriter;

    public Worker(ConsoleWriter messageWriter) =>
        _messageWriter = messageWriter;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _messageWriter.Write($"Worker running at: {DateTimeOffset.Now}");
            await Task.Delay(1_000, stoppingToken);
        }
    }
}
