using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式
{
    public class 抽象工厂模式
    {

        public void Test()
        {
            AbstractFactory nc = new NanChangFactory();
            YaBo ncyb = nc.CreateYaBo();
            ncyb.Print();
            YaJia ncyj = nc.CreateYaJia();
            ncyj.Print();

            AbstractFactory sh = new ShangHaiFactory();
            sh.CreateYaBo();
            sh.CreateYaJia();

            Console.ReadLine();
        }


        /// <summary>
        /// 抽象工厂类
        /// </summary>
        public abstract class AbstractFactory
        {
            public abstract YaBo CreateYaBo();

            public abstract YaJia CreateYaJia();

        }

        /// <summary>
        /// 南昌鸭脖厂
        /// </summary>
        public class NanChangFactory : AbstractFactory
        {
            //生成南昌鸭脖
            public override YaBo CreateYaBo()
            {
                return new NanChangYaBo();
            }

            //生成南昌鸭架
            public override YaJia CreateYaJia()
            {
                return new NanChangYaJia();
            }
        }

        /// <summary>
        /// 上海鸭脖厂
        /// </summary>
        public class ShangHaiFactory : AbstractFactory
        {
            public override YaBo CreateYaBo()
            {
                return new ShangHaiYaBo();
            }

            public override YaJia CreateYaJia()
            {
                return new ShangHaiYaJia();
            }
        }

        /// <summary>
        /// 南昌鸭脖
        /// </summary>
        public class NanChangYaBo : YaBo
        {
            public override void Print()
            {
                Console.WriteLine("江西人喜欢吃辣，南昌鸭脖狠辣的！");
            }
        }

        public class NanChangYaJia : YaJia
        {
            public override void Print()
            {
                Console.WriteLine("南昌鸭架！");
            }
        }

        /// <summary>
        /// 上海鸭脖
        /// </summary>
        public class ShangHaiYaBo : YaBo
        {
            public override void Print()
            {
                Console.WriteLine("上海人不吃辣，卤鸭脖！");
            }
        }

        public class ShangHaiYaJia : YaJia
        {
            public override void Print()
            {
                Console.WriteLine("上海鸭架！");
            }
        }

        /// <summary>
        /// 鸭脖抽象类
        /// </summary>
        public abstract class YaBo
        {
            //输出鸭脖信息
            public abstract void Print();
        }

        /// <summary>
        /// 鸭架抽象类
        /// </summary>
        public abstract class YaJia
        {
            public abstract void Print();
        }


    }
}
