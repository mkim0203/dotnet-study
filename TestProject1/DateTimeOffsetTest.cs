using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1;

public class DateTimeOffsetTest
{
    [Test]
    public void DateTimeOffset_변경테스트()
    {
        DateTimeOffset dt = DateTimeOffset.UtcNow;
        Console.WriteLine(dt);
        Console.WriteLine(dt.Offset);
        Console.WriteLine(dt.UtcDateTime);
        Console.WriteLine(dt.ToLocalTime());

        DateTimeOffset dt2 = dt;
        dt2 = dt2.ToOffset(new TimeSpan(8, 0, 0));
        Console.WriteLine(dt2);
    }
}
