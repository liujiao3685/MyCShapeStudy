using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RobotTCP
{
    public partial class RobotForm : Form
    {
        private Socket ClientSocket;

        private string ip;

        private int port;

        public RobotForm()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1024];

            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            ip = textBox1.Text.ToString();
            port = Convert.ToInt32(textBox2.Text);

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            try
            {
                ClientSocket.Connect(endPoint);
                button1.Enabled = false;

                SendToRobot("as\r\n");//建立连接后，首先发送as，则可以通过计算机发送as语言指令  

                SetTips("机器人连接成功...");//连接成功

            }
            catch (Exception ex)
            {
                SetTips("机器人连接失败..." + ex.Message);//连接失败
            }

            //开线程一直监听机器人发出的信息
            Thread thread = new Thread(Receive);
            thread.IsBackground = true;
            thread.Start();

        }

        public void Receive()
        {
            int thelastData = 99;
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024];

                    int len = ClientSocket.Receive(data);
                    if (len == 0) return;

                    string stringdata = Encoding.UTF8.GetString(data, 0, len);

                    #region 解析机器人返回的数据
                    //利用AS语言的检测变量的指令list实现监听功能  
                    //因为发送‘list/r photo’监测变量photo的值返回的还有其它字符，所以要去掉这些字符  
                    stringdata = stringdata.Trim();
                    string strTemp = "photo    =";
                    int iCount = stringdata.IndexOf(strTemp);//得到多余字符的长度  
                    if (iCount > 0)
                    {
                        string read = stringdata.Substring(strTemp.Length + iCount + 1, 2);//去掉多余字符以及photo  
                        int kk = int.Parse(read);//得到变量photo的值  
                        if (kk != 0)
                        {

                            if (thelastData != kk)//信号从0->1才认为收到信号  
                            {
                                SetTips("接收到机器人拍照信号...");
                                SendToRobot("over=1\r\n");//给机器人完成信号  
                                SetTips("接收到消息：" + stringdata);
                            }
                        }
                        thelastData = kk;
                    }
                    #endregion

                    listBox1.Items.Add(stringdata);
                     
                    // 通过不断向机器人获取变量photo的值，当检测到photo=1时，就可以向机器人发送完成信号over=1，然后机器人程序往下执行。
                    SendToRobot("list/r photo\r\n");//向机器人发送as语言的指令，查询photo的值  
                    Thread.Sleep(100);
                }

            }
            catch (Exception ex)
            {
                SetTips(ex.Message);
            }

        }

        private void SetTips(string tips)
        {
            label3.Text = "tips:" + tips;
        }

        private void SendToRobot(string order)
        {
            byte[] data = Encoding.UTF8.GetBytes(order);

            try
            {
                ClientSocket.Send(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
