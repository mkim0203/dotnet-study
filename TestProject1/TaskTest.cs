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
            Output(1),
            Output(2),
            Output(3)
        ).Wait();
    }

    [Test]
    public async Task Test3()
    {
        await Task.WhenAll(
            Output(1),
            Output(2),
            Output(3)
        );
    }

    private async Task Output(int seq)
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
}
