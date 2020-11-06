using System;
using System.Diagnostics;
using System.Reflection;

namespace Cbase
{
    class Test
    {
        public void Meth()
        {
        }
    }

    class MainClass
    {
        const int loops = 100000000;


        public void Test1()
        {
            Test test = new Test();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < loops; ++i)
            {
                test.Meth();
            }

            stopWatch.Stop();

            Console.WriteLine("Test1 - direct invoke: " + stopWatch.ElapsedMilliseconds);
        }

        public void Test2()
        {
            Test test = new Test();
            Action action = test.Meth;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < loops; ++i)
            {
                action();
            }

            stopWatch.Stop();

            Console.WriteLine("Test2 - delegate invoke: " + stopWatch.ElapsedMilliseconds);
        }

        public void Test3()
        {
            Test test = new Test();
            Type type = typeof(Test);
            MethodInfo methodInfo = type.GetMethod("Meth");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < loops; ++i)
            {
                methodInfo.Invoke(test, null);
            }

            stopWatch.Stop();

            Console.WriteLine("Test3 - reflect invoke: " + stopWatch.ElapsedMilliseconds);
        }

        public static void Main(string[] args)
        {
            MainClass main = new MainClass();
            main.Test1();
            main.Test2();
            main.Test3();
            Console.ReadKey();
        }
    }
}