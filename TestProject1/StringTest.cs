using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1;

public class StringTest
{
    [Test]
    public void EnumTostringTest()
    {
        Console.WriteLine(Codes.None);
        Console.WriteLine(Codes.Success);
        Console.WriteLine(Codes.Fail);

        Console.WriteLine($"{Codes.None}");
        Console.WriteLine($"{Codes.Success}");
        Console.WriteLine($"{Codes.Fail}");

        Console.WriteLine($"{Codes.None.ToString()}");
        Console.WriteLine($"{Codes.Success.ToString()}");
        Console.WriteLine($"{Codes.Fail.ToString()}");
    }


    public enum Codes
    {
        None = 0,
        Success = 1,
        Fail = 2
    }
}
