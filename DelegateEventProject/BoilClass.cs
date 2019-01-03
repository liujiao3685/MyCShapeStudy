using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelegateEventProject
{
    /// <summary>
    /// 烧水类
    /// </summary>
    public class BoilClass
    {
        //水温
        private int temperature;

        /// <summary>
        /// 热水器开始烧水
        /// </summary>
        public void Boil()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(250);

                temperature = i;
                Console.WriteLine("当前水温：" + i);

                if (i >= 95)
                {
                    Alarm(temperature);
                    Monitor(temperature);
                }
            }
        }

        /// <summary>
        /// 水开报警器报警
        /// </summary>
        /// <param name="temp"></param>
        public void Alarm(int temp)
        {
            Console.WriteLine("报警器：水温到达{0}，水开啦，嗡嗡嗡嗡.....", temp);
        }


        /// <summary>
        /// 水开显示器屏幕闪烁
        /// </summary>
        /// <param name="temp"></param>
        public void Monitor(int temp)
        {
            Console.WriteLine("显示器：水温到达{0}，水开啦，屏幕闪烁.....", temp);
        }

    }
}
