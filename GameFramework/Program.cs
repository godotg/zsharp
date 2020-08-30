using System;

namespace GameFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Gf!");
            var bytes = Utility.Converter.GetBytes("Hello");
            foreach (var b in bytes)
            {
                Console.WriteLine(b);
            }
            Console.WriteLine(Utility.Text.Format("Hello {0} {1}", "aa", "bb"));
        }
    }
}