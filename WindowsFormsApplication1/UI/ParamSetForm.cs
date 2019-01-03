using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.UI
{
    public partial class ParamSetForm : FormBase
    {
        //定义委托
        public delegate void DataChangeHandle(object sender, MyEvent arges);

        //定义事件
        public event DataChangeHandle DataChange;

        //调用事件函数
        public void OnDataChange(object sender, MyEvent args)
        {
            if (DataChange != null)
            {
                DataChange(this, args);
            }
        }

        public ParamSetForm()
        {
            InitializeComponent();
            this.ControlName = "参数设置";
        }

        private void ParamSetForm_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                DataChange(this, new MyEvent(this.ControlName + ":" + this.textBox1.Text));
            }
        }

    }
}
