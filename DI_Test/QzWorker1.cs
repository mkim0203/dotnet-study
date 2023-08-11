using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Test;

public class QzWorker1 : IJob
{
    private readonly IMessageWriter _messageWriter;

    public QzWorker1(IMessageWriter messageWriter) =>
        _messageWriter = messageWriter;

    public Task Execute(IJobExecutionContext context)
    {
        //Console.WriteLine($"run {nameof(QzWorker1)}");
        _messageWriter.Write($"{nameof(QzWorker1)} running at: {DateTimeOffset.Now}");

        return Task.FromResult(true);
    }
}
