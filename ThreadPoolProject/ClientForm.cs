using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;


namespace ThreadPoolProject
{
    public partial class ClientForm : Form
    {
        private Socket clientSocket;

        private IPAddress ip;

        private int port;

        private IPEndPoint endPoint;

        public ClientForm()
        {
            InitializeComponent();
            SetIp();
        }

        private IPAddress SetIp()
        {
            ip = IPAddress.Parse(txtIP.Text.Trim());
            return ip;
        }

        private int SetPort()
        {
            port = Int32.Parse(txtPort.Text.Trim());
            return port;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                txtIP.Text = GetLocalIP();

                //创建客户端socket
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                endPoint = new IPEndPoint(SetIp(), SetPort());

                clientSocket.Connect(endPoint);

                ShowMsg("连接成功！");

                Thread r_t = new Thread(Receive);
                r_t.IsBackground = true;
                r_t.Start();
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }

        }

        public string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取本机IP出错:" + ex.Message);
                return "";
            }
        }

        private void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024];

                    int len = clientSocket.Receive(data);
                    if (len == 0) break; 
                    string msg = Encoding.UTF8.GetString(data, 0, len);
                    ShowMsg(clientSocket.RemoteEndPoint + ":" + msg);

                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }

        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1024];
            string msg = txtSendMsg.Text;
            data = Encoding.UTF8.GetBytes(msg);

            try
            {
                if (SocketUseState(clientSocket))
                {
                    clientSocket.SendTimeout = 1000;

                    clientSocket.Send(data);
                    if (msg.Equals("end"))
                    {
                        ShowMsg("已经和服务端断开连接！");
                        clientSocket.Close();
                    }
                    txtSendMsg.Text = String.Empty;
                }
                else
                {
                    ShowMsg("未连接到服务端！");
                }

            }
            catch (Exception ex)
            {
                //在客户端连接断开的情况下重新连接并发送数据
                //sendSocket.Connect(endPoint);
                //sendSocket.Send(data);

                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }

        /// <summary>
        /// TCP安全关闭
        /// </summary>
        /// <returns></returns>
        public bool SafeClose()
        {
            if (clientSocket == null || clientSocket.Connected == false) return false;

            try
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                Thread.Sleep(10);
                clientSocket.Close();
                clientSocket.Dispose();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断socket是否可用
        /// </summary>
        /// <returns></returns>
        private bool SocketUseState(Socket socket)
        {
            if (socket != null && socket.Connected && socket.IsBound)
            {
                return true;
            }
            return false;

        }

        private void ShowMsg(string str)
        {
            txtReceive.AppendText(str + "\r\n");
        }

    }
}
