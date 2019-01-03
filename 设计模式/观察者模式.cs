using System;
using System.Collections.Generic;

namespace 设计模式
{
    public class 观察者模式
    {
        //测试
        public void Test()
        {
            TenXun tx = new TenXunGame("狼人杀", "于明天正式上线！");

            //预约狼人杀玩家
            tx.AddObserver(new Subscriber("玩家A"));
            tx.AddObserver(new Subscriber("玩家B"));

            tx.Update();

            Console.ReadLine();
        }

        //订阅号抽象类
        public abstract class TenXun
        {
            //保存订阅者列表
            private List<IObserver> observers = new List<IObserver>();

            public string Symbol { set; get; }

            public string Info { set; get; }

            protected TenXun(string symbol, string info)
            {
                this.Symbol = symbol;
                this.Info = info;
            }

            public void AddObserver(IObserver observer)
            {
                observers.Add(observer);
            }

            public void RemoveObserver(IObserver observer)
            {
                observers.Remove(observer);
            }

            public void Update()
            {
                foreach (IObserver item in observers)
                {
                    if (item != null)
                    {
                        item.ReceiveAndPrint(this);
                    }
                }
            }
        }

        //具体订阅号类
        public class TenXunGame : TenXun
        {
            public TenXunGame(string symbol, string info) : base(symbol, info)
            {

            }
        }

        //具体订阅者类
        public class Subscriber : IObserver
        {
            public string Name { set; get; }

            public Subscriber(string name)
            {
                Name = name;
            }

            public void ReceiveAndPrint(TenXun tx)
            {
                Console.WriteLine("Notified {0} of {1}'s" + " Info is: {2}", Name, tx.Symbol, tx.Info);
            }
        }


        //订阅者接口
        public interface IObserver
        {
            void ReceiveAndPrint(TenXun tx);
        }


    }
}
