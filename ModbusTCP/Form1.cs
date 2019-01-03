using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ModbusTCP
{
    public partial class Form1 : Form
    {
        private Socket clientSocket;

        private bool Connected;

        private Thread myThread;

        private delegate void MyDelegate(string str);

        public Form1()
        {
            InitializeComponent();
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        public void Connect()
        {
            byte[] data = new byte[1024];

            try
            {
                string ip = txtIP.Text.Trim();
                string port = txtPort.Text.Trim();

                IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ip), Int32.Parse(port));

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                clientSocket.Connect(ie);

                btnConnect.Enabled = false;
                Connected = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("连接服务器失败！" + ex.Message);
                return;
            }

            myThread = new Thread(new ThreadStart(ReceiveMsg));
            myThread.IsBackground = true;
            myThread.Start();
            btnSend0x01.Enabled = true;

        }
        private void btnSend0x01_Click(object sender, EventArgs e)
        {

        }

        private void ReceiveMsg()
        {
            byte[] data = new byte[1024];
            while (true)
            {
                //0x00,0x01,0x00,0x00,0x00,0x06,0x01,0x01,0x00,0x14,0x00,0x13
                clientSocket.Receive(data);
                int length = data[5];//获取数据长度
                byte[] dataShow = new byte[length + 6];//获取要显示的数据
                for (int i = 0; i <= length + 5; i++)
                {
                    dataShow[i] = data[i];
                }

                string stringData = BitConverter.ToString(dataShow);

                if (data[7] == 0x01)
                {
                    ShowMsg01(stringData + "\r\n");
                }

            }

        }

        private void ShowMsg01(string str)
        {
            if (txtReceive0x01.InvokeRequired)
            {
                MyDelegate myDelegate = new MyDelegate(ShowMsg01);
                txtReceive0x01.Invoke(myDelegate, new object[] { str });
            }
            else
            {
                txtReceive0x01.AppendText(str);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 5000;
            byte[] date = new byte[] { 0x00, 0x0f, 0x00, 0x00, 0x00, 0x06, 0x01, 0x04, 0x00, 0x00, 0x00, 0x01 };
            clientSocket.Send(date);
        }
    }
}
