using DI_Test.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Test;

/// <summary>
/// 비동기 channal 서비스 생성
/// </summary>
internal class ChannalWorker : BackgroundService
{
    private readonly ChannalUnit _unit;

    public ChannalWorker(ChannalUnit _) => _unit = _;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _unit.ExecuteWaitAsync(stoppingToken);
    }
}
