using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1;

public class TaskTest
{
    [Test]
    public void Test1()
    {
        Console.WriteLine("test");
    }


    [Test]
    public void Test2()
    {
        Task.WhenAll(
            TaskDelayOutput(1),
            TaskDelayOutput(2),
            TaskDelayOutput(3)
        ).Wait();
    }

    [Test]
    public async Task Test3()
    {
        await Task.WhenAll(
            TaskDelayOutput(1),
            TaskDelayOutput(2),
            TaskDelayOutput(3)
        );
    }

    private async Task TaskDelayOutput(int seq)
    {
        Console.WriteLine($"[{seq,-5}] {DateTime.Now.ToString("HH:mm:ss ffff")}");

        Random rn = new Random();

        int delayTime = rn.Next(5000);
        Console.WriteLine($"[{seq,-5}] {delayTime}");
        await System.Threading.Tasks.Task.Delay(delayTime);
        // Thead.Sleep 사용시 단일 Thread로 묶여버림.
        //System.Threading.Thread.Sleep(delayTime);

        Console.WriteLine($"[{seq,-5}] {DateTime.Now.ToString("HH:mm:ss ffff")}");
    }

    [Test]
    public async Task 동시작업_먼저끝났을경우_return()
    {
        //Task<int> worker1 = TaskDelayOutputTime(1);
        //Task<int> worker2 = TaskDelayOutputTime(2);
        //Task<int> worker3 = TaskDelayOutputTime(3);

        var workers = new List<Task<int>> { TaskDelayOutputTime(1), TaskDelayOutputTime(2), TaskDelayOutputTime(3) };

        Task<int>? targetComplite = null;
        while (workers.Count > 0)
        {
            Task<int> finishedTask = await Task.WhenAny(workers);
        

            if (await finishedTask.ConfigureAwait(false) > 1500)
            {
                targetComplite = finishedTask;
                break;
            }
            
            workers.Remove(finishedTask);
        }

        if(targetComplite is not null) Console.WriteLine(targetComplite.Result);
    }

    private async Task<int> TaskDelayOutputTime(int seq)
    {
        Console.WriteLine($"[{seq,-5}] {DateTime.Now.ToString("HH:mm:ss ffff")}");

        Random rn = new Random();

        int delayTime = rn.Next(5000);
        Console.WriteLine($"[{seq,-5}] {delayTime}");
        await System.Threading.Tasks.Task.Delay(delayTime);
        // Thead.Sleep 사용시 단일 Thread로 묶여버림.
        //System.Threading.Thread.Sleep(delayTime);

        Console.WriteLine($"[{seq,-5}] {DateTime.Now.ToString("HH:mm:ss ffff")}");
        return delayTime;
    }

}
