using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 委托Lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test test = new Test();
            //test.MyTest();
            //test.MyTest2();
            //test.MyTest3();

            EventStudy e = new EventStudy();
            e.Test();

            Console.ReadKey();

        }
    }

    public class Test
    {
        public void MyTest3()
        {
            //Lambda   真爽~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            Func<int, int, int> add = (int a, int b) =>
            {
                return a + b;
            };
            Action<string> print = (string str) =>
            {
                Console.WriteLine(str);
            };
            print("msg3:" + add(3, 4).ToString());

        }

        public void MyTest2()
        {
            //匿名委托

            Func<int, int, int> add = new Func<int, int, int>(delegate (int a, int b)
            {
                return a + b;
            });
            Action<string> print = new Action<string>(delegate (string str)
            {
                Console.WriteLine(str);
            });


            Func<int, int, int> add2 = delegate (int a, int b)
            {
                return a + b;
            };
            Action<string> print2 = delegate (string str)
            {
                Console.WriteLine(str);
            };

            print2("msg2:" + add2(2, 3).ToString());

        }

        public void MyTest()
        {
            //方式一 
            Func<int, int, int> add = new Func<int, int, int>(Add);
            Action<string> print = new Action<string>(Print);
            print("msg:" + add(1, 2).ToString());

        }

        public int Add(int a, int b)
        {
            return a + b;
        }

        public void Print(string msg)
        {
            Console.WriteLine(msg);
        }

    }
}
