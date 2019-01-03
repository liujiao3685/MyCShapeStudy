using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式
{
    /// <summary>
    /// 外观模式主要解决的问题：当有多个类要处理时，需要一个个类去调用，没有复用性和扩展性。
    /// </summary>
    public class 外观模式
    {
        class ClassAdd
        {
            public void Add()
            {
                Console.WriteLine("Add");
            }
        }

        class ClassSub
        {
            public void Sub()
            {
                Console.WriteLine("Sub");
            }
        }

        class ClassDes
        {
            public void Des()
            {
                Console.WriteLine("Des");
            }
        }

        class Facade
        {
            private ClassAdd add;
            private ClassDes dec;
            private ClassSub sub;


            public Facade()
            {
                add = new ClassAdd();
                dec = new ClassDes();
                sub = new ClassSub();
            }

            public void OneMethod()
            {
                add.Add();
            }

            public void TwoMethod()
            {
                add.Add();
                dec.Des();
            }

            public void ThreeMethod()
            {
                add.Add();
                dec.Des();
                sub.Sub();
            }

        }

    }
}
