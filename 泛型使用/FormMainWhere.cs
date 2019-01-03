using System.Windows.Forms;

namespace 泛型使用
{
    public partial class FormMainWhere : Form
    {
        public FormMainWhere()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Salary s1 = new Salary() { BaseSalary = 100 };
            Salary s2 = new Salary() { BaseSalary = 200 };

            SalaryManageWhere where = new SalaryManageWhere();
            listBox1.Items.Add(where.Compare(s1, s2).ToString());

        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            Salary s1 = new Salary() { BaseSalary = 300 };
            Salary s2 = new Salary() { BaseSalary = 200 };

            SalaryManageWhereT<Salary> salary = new SalaryManageWhereT<Salary>();
            listBox1.Items.Add(salary.Compare(s1, s2));

        }
    }

    //约束结构，注意:不可约束 object
    class MyClass<T> where T : struct
    {
        //约束类
        public void MyMethod<T>() where T : class
        {

        }
    }

    //泛型类约束
    class SalaryManageWhereT<T> : Salary where T : Salary
    {
        public int Compare(T t1, T t2)
        {
            if (t1.BaseSalary > t2.BaseSalary)
            {
                return 1;
            }
            else if (t1.BaseSalary == t2.BaseSalary)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }
    }

    //泛型方法约束
    class SalaryManageWhere
    {
        public int Compare<T>(T t1, T t2) where T : Salary
        {
            if (t1.BaseSalary > t2.BaseSalary)
            {
                return 1;
            }
            else if (t1.BaseSalary == t2.BaseSalary)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }
    }



    //不进行泛型约束
    class SalaryMannage
    {
        public int Compare<T>(T t1, T t2)
        {
            return 0;
        }

    }


    class Salary
    {
        public double BaseSalary { set; get; }

        public double Bonus { set; get; }

    }

}
