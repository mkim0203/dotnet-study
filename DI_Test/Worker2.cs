using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Test;

public class Worker2 : BackgroundService
{
    private readonly IMessageWriter _messageWriter;
    private readonly ConsoleWriter _consoleWriter;

    public Worker2(IMessageWriter messageWriter, ConsoleWriter consoleWriter)
    {
        _messageWriter = messageWriter;
        _consoleWriter = consoleWriter;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _messageWriter.Write($"Worker2 running at: {DateTimeOffset.Now}");
            _consoleWriter.Write("worker2 run");
            await Task.Delay(5_000, stoppingToken);
        }
    }
}
