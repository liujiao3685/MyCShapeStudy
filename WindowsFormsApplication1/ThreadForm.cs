using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ThreadForm : Form
    {
        public ThreadForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int width = this.pictureBox1.Width;
            int height = this.pictureBox1.Height;

            Bitmap bm = new Bitmap(width, height);

            Graphics g = Graphics.FromImage(bm);
            Matrix matrix = new Matrix(1, 0, 0, -1, 0, height);
            g.Transform = matrix;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //绘制网格
            Pen pen = new Pen(Color.Green);
            g.DrawLine(pen, new Point(width / 3, height / 2), new Point(0, height / 2 - width / 3));
            g.DrawLine(pen, new Point(width / 3, height / 2), new Point(width, height / 2));
            g.DrawLine(pen, new Point(width / 3, height / 2), new Point(width / 3, height));
            //g.DrawLine(pen, new Point(width / 3, height / 2), new Point(width, height));

            SolidBrush brush = new SolidBrush(Color.Black);
            g.ResetTransform();
            g.DrawString("0", this.Font, brush, width / 3 - 15, height / 2 - 15);
            g.DrawString(String.Format("({0},{1})", 0, (height / 2 - width / 3) - 10),
                this.Font, brush, 0, (height - 60));

            g.Transform = matrix;

            //轨迹绘制
            pen = new Pen(Color.Black);

            //....

            g.Dispose();
            pictureBox1.Image = bm;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task<Int32> t = new Task<int>(n => Sum((Int32)n), 1000);
            t.Start();
            Task cwt = t.ContinueWith(task => System.Diagnostics.Debug.Write(String.Format("The result is {0}!!!\r\n", t.Result)));

        }

        private Int32 Sum(int n)
        {
            int sum = 0;
            for (; n > 0; n--)
            {
                sum += n;
            }
            return sum;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(NotParam));
            thread.Name = "11111";
            thread.Start();
            System.Diagnostics.Debug.Write("Name:" + thread.Name + ".........State:" + thread.ThreadState);

            Thread t2 = new Thread(new ParameterizedThreadStart(HaveParma));
            t2.Start("有参数噢！！");

        }

        private void NotParam()
        {
            MessageBox.Show("This is no param!");
        }

        private void HaveParma(object obj)
        {
            MessageBox.Show("This is HAS param!" + obj);
        }

        public delegate string MyDelegate(object obj);

        private void button4_Click(object sender, EventArgs e)
        {
            MyDelegate dele = new MyDelegate(TestMethod);
            //第一个参数回调成功后的结果，第二个参数执行的回调函数
            IAsyncResult result = dele.BeginInvoke("Thread PARMA", AsyncResult, "CallBACK PARAM");

            //异步完成结果
            string str = dele.EndInvoke(result);
            MessageBox.Show(str);
        }

        //线程函数
        public string TestMethod(object str)
        {
            return (string)str;
        }

        //异步回调函数
        public void AsyncResult(IAsyncResult data)
        {
            MessageBox.Show(data.AsyncState.ToString());
        }

    }


}
