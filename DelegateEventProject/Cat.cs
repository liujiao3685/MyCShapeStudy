using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEventProject
{
    /// <summary>
    /// 猫类
    /// </summary>
    public class Cat
    {
        private string m_name;

        public Cat(string name)
        {
            m_name = name;
        }

        public delegate void ShoutDelegate();

        public event ShoutDelegate ShoutEvent;

        public void Shout()
        {
            Console.WriteLine("喵~，我是{0}", m_name);

            //if (ShoutEvent != null) ShoutEvent();

            ShoutEvent?.Invoke();
        }
    }

    /// <summary>
    /// 老鼠类
    /// </summary>
    public class Mouse
    {
        private string m_name;

        public Mouse(string name)
        {
            m_name = name;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("小猫{0}来了，快跑！", m_name);
            }
        }

    }
}
