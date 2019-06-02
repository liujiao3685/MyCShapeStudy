using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIPaint;

namespace LambdaProject
{
    //Lambda 的一般规则如下：
    //Lambda 包含的参数数量必须与委托类型包含的参数数量相同。
    //Lambda 中的每个输入参数必须都能够隐式转换为其对应的委托参数。
    //Lambda 的返回值（如果有）必须能够隐式转换为委托的返回类型。
    public partial class LambdaForm : Form
    {
        //语句lambda
        private delegate void StateDelegate(string s);

        #region Lambda表达式

        //写入可作为参数传递或作为函数调用值返回的本地函数
        private delegate int Del(int i);

        private delegate bool IsEqual(object a, object b);
        #endregion

        public LambdaForm()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Compare();

            //asyncExample();
            //HellowLambda();
            //EqualObject();
            //Calculate();
        }

        private IEnumerable<Customoer> customoers;

        //Lambda 中的类型推理
        private void Inference()
        {
            IEnumerable<Customoer> cs = customoers.Where(n => n.City == "Landom");
        }

        //带有标准查询运算符的 lambda
        private static void Compare()
        {
            //二
            int[] numInts = new[] { 8, 6, 9, 2, 3, 1, 4, 7, 12, 0 };
            int count = numInts.Count(n => n % 2 == 0);
            //下面一行代码将生成一个序列，其中包含 numbers 数组中在 9 左侧的所有元素，因为它是序列中第一个不满足条件的数字：
            var firstNumLessThan6 = numInts.Count(n => n < 6);
            //通过将输入参数括在括号中来指定多个输入参数
            var firstSmallNumber = numInts.TakeWhile((n, index) => n >= index);

            //一,第一个参数，第二个返回值
            Func<int, bool> func = i => (i == 5);
            bool res = func(6);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await ExampleMethodAsync();
            MessageBox.Show("AsyncMethode");
        }

        private async void asyncExample()
        {
            //可以使用异步 lambda 添加同一事件处理程序
            button1.Click += async (sender, e) =>
            {
                await ExampleMethodAsync();
                MessageBox.Show("AsyncMethode DoEvents");
            };
        }

        //异步 lambda,使用 async 和 await 关键字
        async Task ExampleMethodAsync()
        {
            await Task.Delay(1000);
        }

        private static void HellowLambda()
        {
            StateDelegate stateDelegate = str =>
            {
                string s = str += "Lambda";
                MessageBox.Show(s);
            };

            stateDelegate("Hello  ");
        }

        private static void EqualObject()
        {
            IsEqual isEqual = (a, b) => (Object.ReferenceEquals(a, b));

            bool result = isEqual(1, 3);
        }

        private static void Calculate()
        {
            Del myDel = i => i * i;

            int res = myDel(5);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PaintBead(new Region(new Rectangle(pictureBox1.Location, pictureBox1.Size)));
        }

        public void PaintBead(Region region)
        {
            GraphicsPath oPath = new GraphicsPath();
            int x = 0;
            int y = 0;
            int thisWidth = 60;
            int thisHeight = 60;
            int angle = 60;
            if (angle > 0)
            {
                Graphics g = CreateGraphics();
                g.SmoothingMode = SmoothingMode.AntiAlias;
                oPath.AddArc(x, y, angle, angle, 180, 90);                                 // 左上角
                oPath.AddArc(thisWidth - angle, y, angle, angle, 270, 90);                 // 右上角
                oPath.AddArc(thisWidth - angle, thisHeight - angle, angle, angle, 0, 90);  // 右下角
                oPath.AddArc(x, thisHeight - angle, angle, angle, 90, 90);                 // 左下角
                oPath.CloseAllFigures();
                region = new Region(oPath);
            }
            else
            {
                oPath.AddLine(x + angle, y, thisWidth - angle, y);                         // 顶端
                oPath.AddLine(thisWidth, y + angle, thisWidth, thisHeight - angle);        // 右边
                oPath.AddLine(thisWidth - angle, thisHeight, x + angle, thisHeight);       // 底边
                oPath.AddLine(x, y + angle, x, thisHeight - angle);                        // 左边
                oPath.CloseAllFigures();
                region = new Region(oPath);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormMain rose = new FormMain();
            rose.Show();
        }
    }

    public class Customoer
    {
        public string City { set; get; }
        public Point Location { set; get; }
    }
}
