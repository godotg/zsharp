using System;
using Spring.Core;
using Spring;
using Spring.Event;

namespace GameFramework
{

    public class HelloEvent : IEvent
    {
        public string message;
    }
    
    [Controller]
    public class Student
    {
        private int a = 1;
        private string b = "aaa";

        public override string ToString()
        {
            return "a=" + 1 + ";b=" + b;
        }

        [EventReceiver]
        public void hello(HelloEvent eve)
        {
            Console.WriteLine(eve.message);
        }
    }
}