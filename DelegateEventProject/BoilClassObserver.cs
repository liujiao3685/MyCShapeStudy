using System;
using System.Threading;

namespace DelegateEventProject
{
    public class BoilClassObserver
    {
        public void BoilWater()
        {
            Boiler b = new Boiler();
            Alarm a = new Alarm();
            Monitor m = new Monitor();

            //先给报警器、显示器注册烧水事件
            b.BoilEvent += a.OnAlarm;
            //b.BoilEvent += (new Alarm()).OnAlarm;//给匿名对象注册方法
            b.BoilEvent += m.OnDisplay;

            #region  .Net Framework中的委托与事件

            b.Boiled += a.OnAlarm;
            b.Boiled += Monitor.OnDisplay;//注册静态方法

            b.BoiledWater();

            #endregion

            //开始烧水
            b.BoilWater();
        }
    }

    /// <summary>
    /// 自定义烧水事件
    /// </summary>
    public class BoiledEventArgs : EventArgs
    {
        public readonly int temperture;

        public readonly bool stop;

        public BoiledEventArgs(int temp)
        {
            temperture = temp;
        }

    }
    /// <summary>
    /// 热水器 - 事件发布者
    /// </summary>
    public class Boiler
    {
        /// <summary>
        /// 水温
        /// </summary>
        private int temperautre;

        /// <summary>
        /// 热水器型号 
        /// </summary>
        public string type = "RealFire 001";

        #region Observer

        public delegate void BoilHandle(int temp);
        public event BoilHandle BoilEvent;

        /// <summary>
        /// 烧水
        /// </summary>
        public void BoilWater()
        {
            for (int i = 0; i < 100; i++)
            {
                temperautre = i;
                Thread.Sleep(50);

                Console.WriteLine("当前水温：" + i);

                if (i > 95 && BoilEvent != null)
                {
                    BoilEvent(temperautre);
                }

            }
        }
        #endregion

        #region .Net Framework中的委托与事件

        public delegate void BoiledEventHandle(object sender, BoiledEventArgs e);
        public event BoiledEventHandle Boiled;

        public delegate int StopEventHandle(object sender, BoiledEventArgs e);
        public event StopEventHandle Stop;

        /// <summary>
        /// 调用事件:就是订阅事件的方法的命名，通常为“On事件名”
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBoiled(BoiledEventArgs e)
        {
            if (Boiled != null)
            {
                Boiled(this, e);
            }
        }

        /// <summary>
        /// 烧水
        /// </summary>
        public void BoiledWater()
        {
            for (int i = 0; i < 100; i++)
            {
                temperautre = i;
                if (i > 95)
                {
                    BoiledEventArgs boiledEvent = new BoiledEventArgs(temperautre);
                    OnBoiled(boiledEvent);
                }
            }
        }

        #endregion

    }

    /// <summary>
    /// 报警器 - 订阅者
    /// </summary>
    public class Alarm
    {
        public void OnAlarm(int temp)
        {
            Console.WriteLine("报警器：水温到达{0}，水开啦，嗡嗡嗡嗡.....", temp);
        }

        #region .Net Framework中的委托与事件

        public void OnAlarm(object sender, BoiledEventArgs e)
        {
            Boiler boiler = sender as Boiler;

            Console.WriteLine("报警器：{0}", boiler.type);
            Console.WriteLine("报警器：水温到达{0}，水开啦，嗡嗡嗡嗡.....", e.temperture);
        }
        #endregion
    }

    /// <summary>
    /// 显示器
    /// </summary>
    public class Monitor
    {
        public void OnDisplay(int temp)
        {
            Console.WriteLine("显示器：水温到达{0}，水开啦，屏幕闪烁.....", temp);
        }

        #region .Net Framework中的委托与事件
        public static void OnDisplay(object sender, BoiledEventArgs e)//静态方法
        {
            Boiler boiler = sender as Boiler;

            Console.WriteLine("显示器：{0}", boiler.type);
            Console.WriteLine("显示器：水温到达{0}，水开啦，屏幕闪烁......", e.temperture);
        }
        #endregion
    }


}
