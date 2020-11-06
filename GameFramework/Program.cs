using System;
using Spring.Core;
using Spring;
using Spring.Event;

namespace GameFramework
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Gf!");

            SpringContext.Scan();
            EventBus.GetInstance().Scan();

            var teacher = SpringContext.GetBean<Teacher>();
            teacher.teachStudent();
            
            var helloEvent = new HelloEvent();
            helloEvent.message = "zzzzzzzzz";
            EventBus.GetInstance().SyncSubmit(helloEvent);
            Console.WriteLine(Utility.Text.Format("Hello {0} {1}", "aa", "bb"));
        }
    }
}