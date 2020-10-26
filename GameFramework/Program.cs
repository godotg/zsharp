using System;
using Spring;

namespace GameFramework
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Gf!");

            SpringContext.componentScan();

            var teacher = SpringContext.getComponent<Teacher>();
            teacher.teachStudent();
            
            Console.WriteLine(Utility.Text.Format("Hello {0} {1}", "aa", "bb"));
        }
    }
}