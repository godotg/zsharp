using Spring;

namespace GameFramework
{
    [Controller]
    public class Student
    {
        private int a = 1;
        private string b = "aaa";

        public override string ToString()
        {
            return "a=" + 1 + ";b=" + b;
        }
    }
}