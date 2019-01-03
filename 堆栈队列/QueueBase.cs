using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 堆栈队列
{
    public class QueueBase
    {
        public static string ReverseStringByQueue(string origString)
        {
            StringBuilder sb = new StringBuilder();

            int length = origString.Length;
            Queue queue = new Queue(length);
            for (int i = 0; i < length; i++)
            {
                queue.Enqueue(origString.ToArray()[i]);
            }

            for (int i = 0; i < length; i++)
            {
                sb.Append(queue.Dequeue());
            }

            return sb.ToString();
        }

    }
}
