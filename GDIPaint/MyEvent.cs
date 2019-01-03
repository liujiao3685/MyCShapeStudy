using System;
using System.Drawing;

namespace GDIPaint
{
    public class MyEvent : EventArgs
    {
        public int Index { set; get; }

        /// <summary>
        /// 文本
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 起始坐标
        /// </summary>
        public Point StartPos { set; get; }


    }
}
