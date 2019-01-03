using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlWinformProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process process = new Process();

            string strArguments = "";

            process.StartInfo.FileName = @"E:\WinformProject\WindowsFormsApplication1\WinformProject\bin\Debug\WinformProject.exe";
            process.StartInfo.Arguments = strArguments;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;

            process.Start();

            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.UseShellExecute = false;
            StreamReader sr = process.StandardOutput;
            StreamWriter sw = process.StandardInput;

            int len = sr.Read();

            string str = sr.ReadLine();

            listBox1.Items.Add(sr.ReadLine());
            listBox1.Items.Add(sr.ReadLine());
            listBox1.Items.Add(sr.ReadLine());

            //MessageBox.Show(sr.ReadLine());
            sw.WriteLine("12");
            //MessageBox.Show(sr.ReadLine());
            sw.WriteLine("12");
            //MessageBox.Show(sr.ReadLine());
            sw.WriteLine("12");

        }
    }
}
