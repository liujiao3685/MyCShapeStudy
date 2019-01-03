using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 窗体退出优化
{
    public partial class FormQuit : Form
    {
        private Action action;

        public FormQuit()
        {
            InitializeComponent();
        }

        public FormQuit(Action action)
        {
            InitializeComponent();
            this.action = action;
        }

        private void FormQuit_Shown(object sender, EventArgs e)
        {
            //调用主界面关闭程序
            action.Invoke();
            Close();
        }
    }
}
