using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetBarProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sideNavItem2_Click(object sender, EventArgs e)
        {
            string s = "40";
            if (s == "40" && s.Length > 0)
            {
                Console.WriteLine();
            }

            if (s == "60" && s.Length > 0)
            {
                Console.WriteLine();
            }
        }

        private void d_Click(object sender, EventArgs e)
        {

        }
    }
}
