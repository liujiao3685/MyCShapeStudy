using System;

namespace 设计模式
{
    public class 代理模式
    {
        public void Main()
        {

        }

    }

    public abstract class Persion
    {
        public abstract void BuySomeThing();
    }

    /// <summary>
    /// 实际上要操作的类
    /// </summary>
    public class RelBuyPersion : Persion
    {
        public override void BuySomeThing()
        {
            Console.WriteLine("我要买一个苹果！");
        }
    }

    /// <summary>
    /// 实际上代理类在操作,隐匿对象的位置
    /// </summary>
    public class AgentBuyPersion : Persion
    {
        private RelBuyPersion _relBuyPersion;

        public override void BuySomeThing()
        {
            if (_relBuyPersion == null)
            {
                _relBuyPersion = new RelBuyPersion();
            }

            //代理类增加的其他操作
            OthersThings();

            _relBuyPersion.BuySomeThing();

            ResultThing();

        }

        public void OthersThings()
        {
            Console.WriteLine("我要不要讲价呢？还是直接买吧！");
        }

        public void ResultThing()
        {
            Console.WriteLine("买好了，准备回家！");
        }
    }

}
