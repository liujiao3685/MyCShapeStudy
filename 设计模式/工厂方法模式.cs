namespace 设计模式
{
    /// <summary/>
    /// /// <summary>
    /// 工厂模式存在类与switch语句的高耦合，增加新的类 需要去增加case分支，违背了开放-封闭原则
    /// 工厂方法模式可以解决这个问题。
    /// </summary>
    public class 工厂方法模式
    {
        public void Main()
        {
            AddFctory add = new AddFctory();
            Operator oper = add.CreateOperator();
            oper.NumberB = 1;
            oper.NumberA = 2;
            oper.GetResult();
        }
    }

    public abstract class Operator
    {
        public double NumberA;
        public double NumberB;

        public virtual double GetResult()
        {
            return 0;
        }

    }

    public class Add1 : Operator
    {
        public override double GetResult()
        {
            return NumberA + NumberB;
        }
    }

    public class Dec1 : Operator
    {
        public override double GetResult()
        {
            return NumberA - NumberB;
        }
    }

    interface IFactory
    {
        Operator CreateOperator();
    }

    class AddFctory : IFactory
    {
        public Operator CreateOperator()
        {
            return new Add1();
        }
    }

    class DecFactory : IFactory
    {
        public Operator CreateOperator()
        {
            return new Dec1();
        }
    }

}
