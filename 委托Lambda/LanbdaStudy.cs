using System;
using System.Collections.Generic;

namespace 委托Lambda
{
    public delegate Student GetT();

    public class LanbdaStudy
    {

    }

    public class Test<T> : List<Student>
    {
        public string Name { set; get; }

        public List<Student> List { set; get; }

        public Student Lambda5()
        {
            return this.Find((student) => student.Name == Name);
        }

        public Student Lambda4()
        {
            return this.Find((student) =>
            {
                if (student.Name == Name)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }


        public Student Lambda3()
        {
            return this.Find((student) =>
            {
                if (student.Name == Name)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public Student Lambda2()
        {
            return this.Find(new Predicate<Student>(student =>
            {
                if (student.Name == Name)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }));
        }

        public Student Lambda1()
        {
            return this.Find(new Predicate<Student>(delegate (Student student)
            {
                if (student.Name == Name)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }));
        }

    }
    public class Student : List<Student>
    {
        public string Name { set; get; }


        public Student Lambda5()
        {
            return this.Find((student) => student.Name == Name);
        }
    }
}