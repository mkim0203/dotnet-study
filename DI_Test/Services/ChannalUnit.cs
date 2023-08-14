using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DI_Test.Services;

public class ChannalUnit
{
    private readonly Channel<ChannalUnitData> _batchChannel;
    private readonly IMessageWriter _message;

    public ChannalUnit(IMessageWriter messageWriter)
    {
        _batchChannel = Channel.CreateUnbounded<ChannalUnitData>();
        _message = messageWriter;
    }

    public void Dispose() => _batchChannel.Writer.Complete();

    public async Task ExecuteWaitAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested && await _batchChannel.Reader.WaitToReadAsync(stoppingToken).ConfigureAwait(false))
        {
            ChannalUnitData messages = await _batchChannel.Reader.ReadAsync(stoppingToken).ConfigureAwait(false);

            _ = Task.Run(
                async () =>
                {
                    try
                    {
                        _message.Write($"execute batch Channal [{messages.Id}]");
                        await RunAsync(messages, stoppingToken);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                },
                stoppingToken);
        }
    }

    public async Task RunAsync(ChannalUnitData data, CancellationToken stoppingToken) {

        await Task.Delay(data.Delay, stoppingToken);
        _message.Write($"[{data.Id}] {DateTimeOffset.Now.ToString("HH:mm:ss")} run ChannalUnit async start : {data.StartTime.ToString("HH:mm:ss")}, delay : {data.Delay}");
    }

    public void WriteBatch(ChannalUnitData request, CancellationToken stoppingToken)
    {
        _batchChannel.Writer.WriteAsync(request, stoppingToken);
    }
}

public sealed record ChannalUnitData
{
    public string Id { get; set; } = string.Empty;
    public DateTimeOffset StartTime { get; set; }
    public int Delay { get; set; }
}