using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSubTriggerProject.NoParam
{

    //定义一个事件委托
    public delegate void mcEventHandler();

    /// <summary>
    /// 没有参数的事件触发订阅
    /// </summary>
    class NoParamEventST
    {
        void Test()
        {
            Cat cat1 = new Cat("Tom");
            Mouse m1 = new Mouse(cat1);
            //调用函数，以触发猫叫事件
            cat1.Cry();
        }
    }

    //定义一个猫类
    class Cat
    {
        string cName;
        //定义一个猫叫事件
        public event mcEventHandler CatCryEvent;
        public Cat(string name)
        {
            cName = name;
        }
        //当猫叫时候，触发事件
        public void Cry()
        {
            Console.WriteLine(cName + "来了");
            Console.ReadLine();
            //触发事件
            CatCryEvent();
        }
    }

    //定义一个鼠类
    class Mouse
    {
        public string mName;
        //在构造函数中进行订阅
        public Mouse(Cat cat)
        {
            //订阅事件的两种形式
            cat.CatCryEvent += Run;
            cat.CatCryEvent += new mcEventHandler(See);
        }

        private void Run()
        {
            Console.WriteLine("猫来了，" + mName + "先走一步");
        }
        private void See()
        {
            Console.WriteLine("看看猫还在不在");
            Console.ReadLine();
        }

    }


}
