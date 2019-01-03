using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 堆栈队列
{
    public class StackBase
    {
        /// <summary>
        /// 堆栈实际应用-字符反转，LIFO
        /// </summary>
        /// <param name="origStr"></param>
        /// <returns></returns>
        public static string ReverseStringByStack(string origStr)
        {
            StringBuilder sb = new StringBuilder();

            int len = origStr.Length;

            Stack stack = new Stack(len);
            for (int i = 0; i < len; i++)
            {
                stack.Push(origStr.ToCharArray()[i]);
            }

            for (int i = 0; i < len; i++)
            {
                sb.Append(stack.Pop());
            }

            return sb.ToString();
        }


    }
}
