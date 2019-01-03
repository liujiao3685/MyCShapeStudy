using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace 泛型使用
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyList list = new MyList();
            MyList lsit2 = new MyList();

            listBox1.Items.Add(MyList.Count.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyListT<int> int1 = new MyListT<int>();
            MyListT<int> int2 = new MyListT<int>();

            MyListT<string> string1 = new MyListT<string>();

            listBox1.Items.Add("MyListT<int>.Count：" + MyListT<int>.Count.ToString());//若为统一数据类型，依然共享静态变量
            listBox1.Items.Add("MyListT<string>.Count：" + MyListT<string>.Count.ToString());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(MyList2.Func<int>().ToString());
            listBox1.Items.Add(MyList2.Func<int>().ToString());
            listBox1.Items.Add(MyList2.Func<string>());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FormMainWhere form = new FormMainWhere())
            {
                form.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            object obj = GetT<int>();
            listBox1.Items.Add(obj.ToString());

            obj = GetT<MyList2>();
            
            string msg = string.Empty;
            if (obj == null) msg = "obj is null";
            else msg = obj.ToString();

            listBox1.Items.Add(msg);

        }

        public T GetT<T>()
        {
            //若为object则返回null，若为整型 则返回0
            T t = default(T);
            return t;
        }

    }


    //非泛型类中的泛型方法不会在运行时的本地代码中生成不同类型
    class MyList2
    {
        public static int Count { set; get; }

        public static int Func<T>()
        {
            return Count++;
        }
    }



    //在泛型类中，应避免使用静态变量
    class MyListT<T>//T为不同数据类型，MyList<T>的不同数据类型的类，它们之间并不共享静态成员
    {
        public static int Count { set; get; }

        public MyListT()
        {
            Count++;
        }
    }

    class MyList
    {
        public static int Count { set; get; }//静态变量

        public MyList()
        {
            Count++;
        }

    }

}
