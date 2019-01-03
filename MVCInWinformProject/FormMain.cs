using MVCInWinformProject.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCInWinformProject
{
    public partial class FormMain : Form
    {
        Random random = new Random();

        private PersonControl m_personControl;
        public PersonControl Controllor
        {
            set
            {
                m_personControl = value;
                textBox1.DataBindings.Add("Text", Controllor.Module, "ID");
                textBox2.DataBindings.Add("Text", Controllor.Module, "Name");
            }
            get
            {
                return m_personControl;
            }

        }

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        //更新view
        private void button1_Click(object sender, EventArgs e)
        {
            Controllor.Module.ID = textBox1.Text = random.Next(1000).ToString();
            Controllor.Module.Name = textBox2.Text = "Tom" + random.Next(1000).ToString();
            Controllor.UpdatePerson();
        }

        //更新module
        private void button2_Click(object sender, EventArgs e)
        {
            Controllor.Module.ID = random.Next(10).ToString();
            Controllor.Module.Name = "Jon" + random.Next(10).ToString();
            Controllor.UpdatePerson();
        }
    }
}
