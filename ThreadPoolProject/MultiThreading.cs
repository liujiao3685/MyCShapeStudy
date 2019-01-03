using System;
using System.Threading;
using System.Windows.Forms;

namespace ThreadPoolProject
{
    public partial class MultiThreading : Form
    {
        //创建工作线程
        private Thread[] threads = new Thread[10];

        //创建委托用于异步调用listbox中的Item
        private delegate void AddItemCallBack(string txt);

        public MultiThreading()
        {
            InitializeComponent();
        }

        //在线程安全的情况下调用window窗体上的控件
        private void AddItem(string str)
        {
            if (this.listBox1.InvokeRequired)
            {
                AddItemCallBack callBack = new AddItemCallBack(AddItem);
                this.Invoke(callBack, new object[] { str });
            }
            else
            {
                this.listBox1.Items.Add(str);
            }
        }

        //获取数据方法
        private void DataGet()
        {
            //此方法退出，线程也退出
            while (true)
            {
                int i = 0;
                i++;
                AddItem(Thread.CurrentThread.Name + "---item" + i);
                Thread.Sleep(1000);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //循环创建并启动线程
            for (int i = 0; i < threads.Length; i++)
            {
                //如果线程不存在则创建线程
                if (threads[i] == null)
                {
                    threads[i] = new Thread(new ThreadStart(DataGet));
                    threads[i].Name = "Thread" + i;
                    threads[i].Start();
                }
                //若存在但未运行，则启动
                else
                {
                    if (threads[i].ThreadState == ThreadState.Aborted || threads[i].ThreadState == ThreadState.Stopped)
                    {
                        threads[i] = new Thread(new ThreadStart(DataGet));
                        threads[i].Name = "Thread" + i;
                        threads[i].Start();
                    }
                    else
                    {
                        threads[i].Start();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < threads.Length; i++)
            {
                if (threads[i] != null && threads[i].ThreadState != ThreadState.Stopped && threads[i].ThreadState != ThreadState.Aborted)
                {
                    threads[i].Abort();
                }
            }
        }

        private void MultiThreading_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < threads.Length; i++)
            {
                if (threads[i] != null && threads[i].ThreadState != ThreadState.Stopped && threads[i].ThreadState != ThreadState.Aborted)
                {
                    threads[i].Abort();
                }

            }
        }

    }
}
