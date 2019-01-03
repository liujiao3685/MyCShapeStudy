using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式
{
    public class 模板方法模式
    {

        public class MyTest
        {
            public static void Test()
            {
                ClassA a = new ClassA();
                a.TemplateMethod();

                ClassB b = new ClassB();
                b.TemplateMethod();

            }
        }

        public abstract class AbstractClass
        {
            public abstract void OperationA();

            public abstract void OperationB();


            public void TemplateMethod()
            {
                OperationA();
                OperationB();

                Console.WriteLine("TemplateMethod");
            }

        }

        public class ClassA : AbstractClass
        {
            public override void OperationA()
            {
                Console.WriteLine("ClassA.OperationA");
            }

            public override void OperationB()
            {
                Console.WriteLine("ClassA.OperationB");
            }
        }

        public class ClassB : AbstractClass
        {
            public override void OperationA()
            {
                Console.WriteLine("ClassB.OperationA");
            }

            public override void OperationB()
            {
                Console.WriteLine("ClassB.OperationB");
            }
        }


    }
}
