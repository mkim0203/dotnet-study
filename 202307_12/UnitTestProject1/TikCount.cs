using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class TikCount
    {
        [TestMethod]
        public void TestMethod1()
        {
            Parallel.For(1, 101, (index) =>
            {
                Console.WriteLine($"{index,3} => {StaticRandom.Rand(1, 100)}");
            });
        }

        [TestMethod]
        public void  ExecuteSumAsync()
        {
            var numbers = Enumerable.Range(1, 1_000_000);
            double sumValue = 0;
            numbers.AsParallel().ForAll(x => sumValue += x);

            Console.WriteLine(sumValue);
        }

        [TestMethod]
        public void ExecuteSumAsync3()
        {
            var numbers = Enumerable.Range(1, 1_000_000);
            double sumValue = 0;
            sumValue = numbers.AsParallel().Select(x => (long)x).Sum();

            Console.WriteLine(sumValue);
        }

        [TestMethod]
        public void ExecuteSumAsync4()
        {
            var numbers = Enumerable.Range(1, 1_000_000);
            double sumValue = 0;
            numbers.ToList().ForEach(x => sumValue += x);

            Console.WriteLine(sumValue);
        }


        [TestMethod]
        public void ExecuteSumAsync2()
        {
            var numbers = Enumerable.Range(1, 100);
            long sumValue = 0;

            // Parallel.For를 사용하여 안전하게 합계를 계산
            ParallelLoopResult result = Parallel.For(0, numbers.Count(), () => 0L,
                (i, loopState, localSum) => localSum + numbers.ElementAt(i),
                localSum => Interlocked.Add(ref sumValue, localSum)
            );

            Console.WriteLine(sumValue);
        }


    }

}
