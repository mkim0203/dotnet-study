
using DI_Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<Worker2>();
builder.Services.AddSingleton<ConsoleWriter>();
builder.Services.AddSingleton<IMessageWriter, LoggingMessageWriter>();

using IHost host = builder.Build();

// 계속 실행
//host.Run();

host.Start();
await Task.Delay(5_000);
Console.WriteLine("end"); 
await host.StopAsync();

//// CancellationToken로 작업중지
//CancellationTokenSource temp = new(20_000);
//CancellationToken token = temp.Token;
//await host.RunAsync(token);