using System;

namespace Cbase
{
    public delegate void GreetingDelegate(string name);
    
    class DelegateTest
    {
        private static void EnglishGreeting(string name)
        {
            Console.WriteLine("Good Morning, " + name);
        }
 
        private static void ChineseGreeting(string name)
        {
            Console.WriteLine("早上好, " + name);
        }
 
        private static void GreetPeople(string name, GreetingDelegate MakeGreeting)
        {
            MakeGreeting(name);
        }
 
        // static void Main(string[] args)
        static void MainTest(string[] args)
        {
            GreetingDelegate delegate1;
            delegate1 = EnglishGreeting;
            delegate1 += ChineseGreeting; 
            delegate1 -= EnglishGreeting;
            delegate1("Liker");
            Console.ReadLine();
        }
        
    }
}