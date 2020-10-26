using System;
using Spring;

namespace GameFramework
{
    [Controller]
    public class Teacher
    {
        [Autowired]
        private Student student;

        public void teachStudent()
        {
            Console.WriteLine(student);
        }
    }
}