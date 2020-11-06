using System;

namespace Cbase
{
    /**
     * 事件是委托的特殊实现，事件是建立在对委托的语言支持之上的。
     * 
     * 委托是一种类型，事件是委托类型的一个实例，加上了event的权限控制，限制权限，只允许在事件声明类里面去invoke和赋值，不允许外面，甚至子类调用。
     */

    //定义猫叫委托
    public delegate void CatCallEventHandler();

    public class Cat
    {
        //定义猫叫事件
        public event CatCallEventHandler CatCall;

        public void OnCatCall()
        {
            Console.WriteLine("猫叫了一声");
            CatCall?.Invoke();
        }
    }

    public class Mouse
    {
        //定义老鼠跑掉方法
        public void MouseRun()
        {
            Console.WriteLine("老鼠跑了");
        }
    }

    public class People
    {
        //定义主人醒来方法
        public void WakeUp()
        {
            Console.WriteLine("主人醒了");
        }
    }

    class EventTest
    {
        // static void Main(string[] args)
        static void MainTest(string[] args)
        {
            Cat cat = new Cat();
            Mouse m = new Mouse();
            People p = new People();
            //关联绑定 
            cat.CatCall += m.MouseRun;
            cat.CatCall += p.WakeUp;
            cat.OnCatCall();

            Console.ReadKey();
        }
    }
}