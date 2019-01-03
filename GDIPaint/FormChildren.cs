using System;
using System.Drawing;
using System.Windows.Forms;

namespace GDIPaint
{
    public partial class FormChildren : Form
    {
        private string content = string.Empty;

        private Point point;

        private FormMain m_formMain;

        private Func<string, Point> FuncValue;

        public FormChildren(string text, Point p)
        {
            InitializeComponent();
            content = text;
            point = p;
        }

        public FormChildren(Func<string, Point> func)
        {
            InitializeComponent();
            FuncValue = func;

        }

        public FormChildren(FormMain main)
        {
            InitializeComponent();
            m_formMain = main;
            m_formMain.ValueChangeHandle += ValueChange;
        }

        private void FormChildren_Load(object sender, System.EventArgs e)
        {
            point = FuncValue("123");

            button1.Text = content;
            Location = point;
        }

        public void ValueChange(object sender, MyEvent e)
        {
            content = e.Content;
            point = e.StartPos;

            button1.Text = content;
            Location = point;

        }
    }
}
