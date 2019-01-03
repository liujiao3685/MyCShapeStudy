using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 堆栈队列
{
    class Program
    {
        static void Main(string[] args)
        {
            string origString = "ASFDGH";

            Console.WriteLine("origString：" + origString + "\r\n");

            string reverseStrStack = StackBase.ReverseStringByStack(origString);
            Console.WriteLine("reverseStrStack：" + reverseStrStack + "\r\n");


            string reverseStrQueue = QueueBase.ReverseStringByQueue(origString);
            Console.WriteLine("reverseStrQueue：" + reverseStrQueue + "\r\n");

            Console.ReadKey();

        }
    }
}
