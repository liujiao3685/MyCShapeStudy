using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 窗体退出优化
{
    public partial class Form1 : Form
    {
        private AutoResetEvent resetEvent = new AutoResetEvent(false);
        private bool isWindowShow = false;
        private Action<string> showInfo;
        private Thread thread;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thread = new Thread(ThreadCapture);
            thread.IsBackground = true;
            thread.Start();

        }

        private void ThreadCapture()
        {
            showInfo = new Action<string>(m =>
            {
                label1.Text = m;
            });
            isWindowShow = true;

            Thread.Sleep(200);

            while (true)
            {
                int data = random.Next(1000);

                // 接下来是跨线程的显示，并检测窗体是否关闭,发现了Invoke显示的时候，就会形成死锁,改成beginInvoke
                if (isWindowShow) BeginInvoke(showInfo, data.ToString());
                else break;

                Thread.Sleep(200);

                if (!isWindowShow) break;
            }

            // 通知主界面是否准备退出
            resetEvent.Set();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormQuit formQuit = new FormQuit(new Action(() =>
            {
                Thread.Sleep(200);
                isWindowShow = false;
                resetEvent.WaitOne();
            }));

            formQuit.ShowDialog();
        }

    }
}
