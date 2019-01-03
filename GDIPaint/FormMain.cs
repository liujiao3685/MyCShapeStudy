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

namespace GDIPaint
{
    public partial class FormMain : Form
    {
        private Graphics m_graphios;

        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            //m_graphios = this.CreateGraphics();
            //m_graphios.DrawEllipse(new Pen(Color.Blue, 2), new Rectangle(e.X - 1, e.Y - 1, 2, 2));
            //m_graphios.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        public delegate void ValueChange(object sender, MyEvent e);

        public event ValueChange ValueChangeHandle;

        public void OnValueChange(object sender, MyEvent e)
        {
            //if (ValueChangeHandle != null)
            //{
            //    ValueChangeHandle(e);
            //}

            ValueChangeHandle?.Invoke(this, e);
        }

        Thread t;

        private void button2_Click(object sender, EventArgs e)
        {
            //方法一 自定义事件传值
            //FormChildren form = new FormChildren(this);
            //form.Show();
            //OnValueChange(new MyEvent() { Content = "你", StartPos = new Point(100, 200) });

            //方法二 构造函数传值
            //FormChildren form = new FormChildren("是", new Point(150, 250));
            //form.Show();

            timer1.Interval = 500;
            timer1.Start();
        }

        private int flag = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            flag++;
            if (flag == 1)
            {
                using (FormChildren form = new FormChildren(FuncValue1))
                {
                    form.ShowDialog();
                }

                //FormChildren form = new FormChildren("我", new Point(100, 100));
                //FormChildren form = new FormChildren(this);
                //OnValueChange(form, new MyEvent() { Content = "我", StartPos = new Point(100, 100) });
                //form.Show();
            }
            if (flag == 2)
            {
                using (FormChildren form = new FormChildren(FuncValue2))
                {
                    form.ShowDialog();
                }

                //FormChildren form = new FormChildren("爱", new Point(300, 100));
                //FormChildren form = new FormChildren(this);
                //OnValueChange(form, new MyEvent() { Content = "爱", StartPos = new Point(300, 100) });
                //form.Show();
            }
            //if (flag == 3)
            //{
            //    //FormChildren form = new FormChildren("你", new Point(500, 100));
            //    FormChildren form = new FormChildren(this);
            //    OnValueChange(form, new MyEvent() { Content = "你", StartPos = new Point(500, 100) });
            //    form.Show();
            //}
            if (flag == 12)
            {
                flag = 0;
                timer1.Stop();
            }
        }

        private Point FuncValue1(string str)
        {
            return new Point(100, 200);
        }
        private Point FuncValue2(string str)
        {
            return new Point(400, 200);
        }
    }
}
