using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式
{
    /// <summary>
    /// 装饰模式动态地给一个对象添加额外的职责
    /// 装饰模式提供了一种给类增加功能的方法。它通过动态地组合对象，可以给原有的类添加新的代码，而无须修改现有代码
    /// </summary>
    class 装饰模式
    {
        /// <summary>
        /// 定义一个对象接口，可以给这些对象动态地添加职责
        /// </summary>
        public abstract class Component
        {
            public abstract void Operation();
        }

        /// <summary>
        /// 定义一个对象，可以给这个对象添加一些职责
        /// </summary>
        public class ConcreateComponent : Component
        {
            public override void Operation()
            {
                Console.WriteLine("ConcreateComponent.Operation()");
            }
        }

        /// <summary>
        /// 维持一个指向Component的指针，并定义一个与Component接口一致的接口
        /// </summary>
        public class Decorator : Component
        {
            protected Component component;

            public void SetComponent(Component component)
            {
                this.component = component;
            }

            public override void Operation()
            {
                component?.Operation();
            }
        }

        /// <summary>
        /// 负责向ConcreteComponent添加功能
        /// </summary>
        public class ConcreateDecoratorA : Decorator
        {
            public override void Operation()
            {
                base.Operation();
                Console.WriteLine("ConcreateDecoratorA.Operation()");
            }
        }

        public class ConcreateDecrotorB : Decorator
        {
            public override void Operation()
            {
                base.Operation();
                AddBehavior();
                Console.WriteLine("ConcreateDecrotorB.Operation()");
            }

            private void AddBehavior()
            {
                throw new NotImplementedException();
            }
        }


    }
}
