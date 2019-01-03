using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;


namespace HideToolBar
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
        }
        private string msgSend;
        Socket sendSocket;
        Thread ReceiveThread;
        public string IP = "192.168.0.104";
        public string Port = "5000";

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                //创建客户端socket
                sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP), Int32.Parse(Port));

                sendSocket.Connect(endPoint);

                txtReceive.Visible = true;
                txtSendMsg.Visible = true;
                btnSendData.Visible = true;
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                btnConnect.Visible = false;
           //     txtReceive.AppendText("连接成功！\r\n");
                ReceiveThread = new Thread(Receive);
                ReceiveThread.IsBackground = true;
                ReceiveThread.Start(sendSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败");
               // txtReceive.AppendText(ex.Message.ToString() + "\r\n");
            }

        }

        void Receive(object o)
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[1024 * 1024];

                    int len = sendSocket.Receive(data);
                    if (len == 0)
                    {
                        break;
                    }
                    string msg = Encoding.UTF8.GetString(data, 0, len);
                    if (msgSend == msg)
                    {
                        txtReceive.Font =new Font("黑体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                        txtReceive.ForeColor = Color.Black;
                        txtReceive.AppendText("秀奇" + ":" + msg + "\r\n");
                    }
                    else
                    {
                        txtReceive.Font = new Font("楷体", 9F, FontStyle.Bold ,GraphicsUnit.Point, ((byte)(134)));
                        txtReceive.ForeColor = Color.Red;
                        txtReceive.AppendText("刘群" + ":" + msg + "\r\n");                 
                    }
                    

                }
                catch (Exception)
                {
                   
                    throw  ;
                }
            }

        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            txtReceive.Visible = false;
            txtSendMsg.Visible = false;
            btnSendData.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            btnConnect.Visible = true;
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            msgSend = txtSendMsg.Text;
            SendMessage(msgSend);
        }
        
        private void SendMessage(string msgSend)
        {
            try
            {
                byte[] data = new byte[1024 * 1024];
              //  msgSend = txtSendMsg.Text;
                data = Encoding.UTF8.GetBytes(msgSend);
                sendSocket.Send(data);
                this.txtSendMsg.Clear();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }

        private void txtSendMsg_TextChanged(object sender, EventArgs e)
        {
            
            
        }
        private void txtSendMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                msgSend = txtSendMsg.Text.Trim();
                SendMessage(msgSend);  
            }
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ReceiveThread != null)
            {
                SendMessage("秀奇妹妹离开了");
                ReceiveThread.Abort();
            }
            
        }
        
    }
}
