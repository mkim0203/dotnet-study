using System;
using System.Threading;

namespace UnitTestProject1
{
    public static class StaticRandom
    {
        static int seed = Environment.TickCount;

        static readonly ThreadLocal<Random> random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        public static int Rand(int min, int max)
        {
            return random.Value.Next(min, max);
        }
    }
}
