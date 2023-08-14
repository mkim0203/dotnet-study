
using DI_Test;
using DI_Test.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var execConfig = builder.Configuration.GetSection("ExecuteWorker").Get<ExecuteWorkerConfig>() ?? throw new InvalidOperationException("ExecuteWorker configuration");

if (execConfig.MessageWorker)
{
    builder.Services.AddHostedService<Worker>();
    builder.Services.AddHostedService<Worker2>();
}

if (execConfig.ChannalWorker)
{
    // channal 활용한 비동기 작업 수행
    builder.Services.AddHostedService<ChannalWorker>();
    builder.Services.AddHostedService<ChannalRequestWorker>();
    builder.Services.AddSingleton<ChannalUnit>();
}

builder.Services.AddSingleton<ConsoleWriter>();
builder.Services.AddSingleton<IMessageWriter, LoggingMessageWriter>();


if (execConfig.QuartzWorker)
{

    // Quartz test
    builder.Services.AddQuartz(q =>
    {

        //// Just use the name of your job that you created in the Jobs folder.
        //var jobKey = new JobKey("worker1");
        //q.AddJob<QzWorker1>(opts => opts.WithIdentity(jobKey));

        //q.AddTrigger(opts => opts
        //    .ForJob(jobKey)
        //    .WithIdentity("worker1-trigger")
        //    //This Cron interval can be described as "run every minute" (when second is zero)
        //    .WithCronSchedule("* * * * * ?")
        //);

        //q.UseMicrosoftDependencyInjectionJobFactory();

        var jobKey = new JobKey("worker1");
        q.AddJob<QzWorker1>(opts => opts.WithIdentity(jobKey));
        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity("worker1-trigger")
            //This Cron interval can be described as "run every minute" (when second is zero)
            .WithCronSchedule("*/3 * * * * ?")
        );

    });

    builder.Services.AddQuartzHostedService(opt =>
    {
        opt.WaitForJobsToComplete = true;
    });

}

using IHost host = builder.Build();



//var schedulerFactory = host.Services.GetRequiredService<ISchedulerFactory>();
//var scheduler = await schedulerFactory.GetScheduler();

//// define the job and tie it to our HelloJob class
//var job = JobBuilder.Create<QzWorker1>()
//    .WithIdentity("myJob", "group1")
//    .Build();

//// Trigger the job to run now, and then every 40 seconds
//var trigger = TriggerBuilder.Create()
//    .WithIdentity("myTrigger", "group1")
//    .StartNow()
//    //.WithSimpleSchedule(x => x.WithIntervalInSeconds(40).RepeatForever())
//    .WithCronSchedule("*/3 * * * * ?")
//    .Build();

//await scheduler.ScheduleJob(job, trigger);

// 계속 실행
host.Run();

//host.Start();
//await Task.Delay(5_000);
//Console.WriteLine("end"); 
//await host.StopAsync();

//// CancellationToken로 작업중지
//CancellationTokenSource temp = new(20_000);
//CancellationToken token = temp.Token;
//await host.RunAsync(token);