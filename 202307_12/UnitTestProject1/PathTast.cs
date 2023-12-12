using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class PathTast
    {
        [TestMethod]
        public void TestMethod1()
        {

            string appLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            Console.WriteLine(appLocal);

            string path = System.IO.Path.Combine(appLocal, "AMIS");

            Console.WriteLine(path);
        }
    }
}
