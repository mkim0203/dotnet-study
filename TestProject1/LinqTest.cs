using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using TestProject1.Db;

namespace TestProject1;

public class LinqTest
{
    public List<LinqTestData> TestData { get; set; } =  new List<LinqTestData>();
    public int[] datas = new int[] { 1, 2, 3, 4, 5 };

    [SetUp]
    public void Setup()
    {
        for (int i = 1; i <= 10; i++)
        {
            TestData.Add(new LinqTestData() { Id = $"ID-{i}", index = i });
        }
    }


    [Test]
    public Task FindTest()
    {
        int findData = Array.Find(datas, x => x == 10);
        Console.WriteLine(findData);

        return Task.CompletedTask;
    }

    [Test]
    public Task FindClassTest()
    {
        LinqTestData? findData = TestData.Find(x => x.index == 11);
        if(findData != null)
        {
            Console.WriteLine(findData.ToString());
        }

        return Task.CompletedTask;
    }
}

public class LinqTestData
{
    public string Id { get; set; } = string.Empty;
    public int index { get; set; }
}
