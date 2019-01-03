namespace 设计模式
{
    public class 简单工厂模式
    {
        public void Main()
        {
            var oper = Factory.CraterOperator("1");

            oper.NumberB = 1;
            oper.NumberA = 2;
            oper.GetResult();
        }
    }

    public abstract class Opertor
    {
        public double NumberA { set; get; }
        public double NumberB { set; get; }

        public virtual double GetResult()
        {
            return 0;
        }
    }

    public class Adds : Opertor
    {
        public override double GetResult()
        {
            return NumberB + NumberA;
        }
    }

    public class Sub : Opertor
    {
        public override double GetResult()
        {
            return NumberB - NumberA;
        }
    }

    public class Factory
    {
        public static Opertor CraterOperator(string str)
        {
            switch (str)
            {
                case "1":
                    return new Adds();
                case "2":
                    return new Sub();
                default:
                    return null;
            }
        }

    }

}
