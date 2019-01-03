using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// 自定义事件参数类型，根据需要可设定多种参数便于传递
    /// </summary>
    public class MyEvent : EventArgs
    {
        public string name { get; set; }

        public string pass { get; set; }

        public string type { get; set; }

        public MyEvent(string type)
        {
            this.type = type;
        }

        public MyEvent(string s1, string s2)
        {
            name = s1;
            pass = s2;
        }

        public string GetString(string str)
        {
            return str; 
        }
    }
}
