using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DelegateEventProject
{
    public partial class Form1 : Form
    {
        Cat cat;

        Mouse mouse1;

        Mouse mouse2;

        public delegate int AddNumDelegate(int a, int b);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cat = new Cat("波比");
            mouse1 = new Mouse("大大");
            mouse2 = new Mouse("小小");

            //先调用事件
            cat.ShoutEvent += new Cat.ShoutDelegate(mouse1.Run);
            cat.ShoutEvent += new Cat.ShoutDelegate(mouse2.Run);
            cat.Shout();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //将Add函数的功能赋给委托AddNumDelegate执行
            AddNumDelegate addNum = new AddNumDelegate(Add);
            listBox1.Items.Add(addNum(1, 2));
        }

        public int Add(int a, int b)
        {
            return a + b;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BoilClassObserver boilClass_ = new BoilClassObserver();
            boilClass_.BoilWater();
        }
    }
}
