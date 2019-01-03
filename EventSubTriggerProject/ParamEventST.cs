using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSubTriggerProject.NoParam
{

    /// <summary>
    /// 没有参数的事件触发订阅
    /// </summary>
    class ParamEventST
    {
        //定义一个事件委托
        public delegate void mcEventHandler();

        void Test()
        {
            Cat c1 = new Cat("Tom");
            Cat c2 = new Cat("Ben");
            //两只老鼠，分别见到两只猫
            Mouse m = new Mouse(c1);
            Mouse m2 = new Mouse(c2);
            c1.Cry();
            Console.WriteLine("//-----------------而另一边---------------------//");
            c2.Cry();
        }

        //定义一个猫类
        class Cat
        {
            string cName;
            public event mcEventHandler CatCryEvent;
            //定义带有参数的事件，此处CryEventArgs可以为其他简单类，如是，下面订阅的函数的签名需要相应地改变
            public event EventHandler<CryEventArgs> CatCryEvent1;
            public Cat(string name)
            {
                cName = name;
            }

            public void Cry()
            {
                Console.WriteLine(cName + "来了");
                Console.ReadLine();
                //用这个保存参数
                CryEventArgs e = new CryEventArgs();
                e.CatName = cName;
                //触发事件，带参数
                CatCryEvent1(this, e);
            }
        }


        //定义一个鼠类
        class Mouse
        {
            public Mouse(Cat cat)
            {
                //订阅事件的两种形式
                //cat.CatCryEvent1 += Run;
                cat.CatCryEvent1 += new EventHandler<CryEventArgs>(Run);
            }

            private void Run(object sender, CryEventArgs e)
            {
                if (e.CatName == "Tom")
                {
                    Console.WriteLine("别怕，是Tom这只傻猫");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("快跑啊，是其他猫！");
                    Console.ReadLine();
                }
            }
        }

        class CryEventArgs : EventArgs
        {
            //存储一个字符串  
            public string CatName
            {
                get;
                set;
            }
        }
    }


}
