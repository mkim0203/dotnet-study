using DI_Test.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DI_Test;

/// <summary>
///     비동기 쓰레드 작업 요청
/// </summary>
internal class ChannalRequestWorker : BackgroundService
{
    private ChannalUnit _unit;
    private readonly IMessageWriter _message;
    public ChannalRequestWorker(ChannalUnit _, IMessageWriter message)
    {
        _unit = _;
        _message = message;

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (DateTime.Now.Second % 10 == 0)
            {
                string id = $"ID-{DateTimeOffset.Now.ToString("mmss")}";
                _message.Write($"[{id}] {DateTimeOffset.Now.ToString("HH:mm:ss")}  작업 생성.");
                Random rm = new Random();
                int delay = rm.Next(20_000);
                //_unit.RunAsync(new ChannalUnitData { Id = id, StartTime = DateTimeOffset.Now, Delay = delay }, stoppingToken);
                _unit.WriteBatch(new ChannalUnitData { Id = id, StartTime = DateTimeOffset.Now, Delay = delay }, stoppingToken);
                _message.Write($"[{id}] {DateTimeOffset.Now.ToString("HH:mm:ss")}  요청 완료.");
            }
            await Task.Delay(1_000, stoppingToken);
        }
    }
}
