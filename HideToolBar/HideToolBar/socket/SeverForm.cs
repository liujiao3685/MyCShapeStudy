using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HideToolBar
{
    public partial class SeverForm : Form
    {
        public SeverForm()
        {
            InitializeComponent();
        }

        string RemoteEndPoint;     //客户端的网络结点  
        private string strSRecMsg;   //接收到客户端发来的信息
        Thread threadwatch = null;//负责监听客户端的线程  
        Socket socketwatch = null;//负责监听客户端的套接字  
        //创建一个和客户端通信的套接字  
        IPAddress address;
        IPEndPoint point;

        Dictionary<string, Socket> dic = new Dictionary<string, Socket> { };   //定义一个集合，存储客户端信息  

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.btnConnect.Enabled = false;
            //定义一个套接字用于监听客户端发来的消息，包含三个参数（IP4寻址协议，流式连接，Tcp协议）  
            socketwatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //服务端发送信息需要一个IP地址和端口号  
            address = IPAddress.Any;//IPAddress.Parse(txtIP.Text.Trim());//获取文本框输入的IP地址  

            //将IP地址和端口号绑定到网络节点point上  
            point = new IPEndPoint(address, int.Parse(txtPort.Text.Trim()));//获取文本框上输入的端口号  
            //此端口专门用来监听的  

            //监听绑定的网络节点  
            socketwatch.Bind(point);

            //将套接字的监听队列长度限制为20  
            socketwatch.Listen(20);



            //创建一个监听线程  
            threadwatch = new Thread(watchconnecting);



            //将窗体线程设置为与后台同步，随着主线程结束而结束  
            threadwatch.IsBackground = true;

            //启动线程     
            threadwatch.Start();

            //启动线程后 textBox3文本框显示相应提示  
            this.txtReceive.AppendText("开始监听客户端传来的信息!" + "\r\n");
        }

        //监听客户端发来的请求  
        private void watchconnecting()
        {
            Socket connection = null;
            while (true)  //持续不断监听客户端发来的请求     
            {
                try
                {
                    connection = socketwatch.Accept();
                }
                catch (Exception ex)
                {
                    txtReceive.AppendText(ex.Message); //提示套接字监听异常     
                    break;
                }
                //获取客户端的IP和端口号  
                IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;

                //让客户显示"连接成功的"的信息  
                // string sendmsg = "连接服务端成功！\r\n" + "本地IP:" + clientIP + "，本地端口" + clientPort.ToString();
                string sendmsg = "欢迎秀奇妹妹！";
                byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendmsg);
                connection.Send(arrSendMsg);


                RemoteEndPoint = connection.RemoteEndPoint.ToString(); //客户端网络结点号  
                                                                       //  txtReceive.AppendText("成功与" + RemoteEndPoint + "客户端建立连接！\t\n");     //显示与客户端连接情况  
                this.Invoke(new MethodInvoker(delegate
                {
                    txtReceive.AppendText("秀奇妹妹来聊天啦\t\n");

                }));
                dic.Add(RemoteEndPoint, connection);    //添加客户端信息  

                OnlineList_Disp(RemoteEndPoint);    //显示在线客户端  


                //   IPEndPoint netpoint = new IPEndPoint(clientIP, clientPort);

                IPEndPoint netpoint = connection.RemoteEndPoint as IPEndPoint;

                //创建一个通信线程      
                ParameterizedThreadStart pts = new ParameterizedThreadStart(recv);
                Thread thread = new Thread(pts);
                thread.IsBackground = true;//设置为后台线程，随着主线程退出而退出     
                //启动线程     
                thread.Start(connection);
            }
        }

        private void recv(object socketclientpara)
        {

            Socket socketServer = socketclientpara as Socket;
            while (true)
            {
                //创建一个内存缓冲区 其大小为1024*1024字节  即1M     
                byte[] arrServerRecMsg = new byte[1024 * 1024];
                //将接收到的信息存入到内存缓冲区,并返回其字节数组的长度    
                try
                {
                    int length = socketServer.Receive(arrServerRecMsg);

                    //将机器接受到的字节数组转换为人可以读懂的字符串     
                    strSRecMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);

                    this.Invoke(new MethodInvoker(delegate
                    {
                        if (!string.IsNullOrEmpty(strSRecMsg))
                        {
                            sendRevData();
                        }

                    }));
                    //将发送的字符串信息附加到文本框txtMsg上     
                    //     txtReceive.AppendText(strSRecMsg + "\r\n");  
                }
                catch (Exception ex)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        txtReceive.AppendText("客户端" + socketServer.RemoteEndPoint + "已经中断连接" + "\r\n"); //提示套接字监听异常   
                        listBoxOnlineList.Items.Remove(socketServer.RemoteEndPoint.ToString());//从listbox中移除断开连接的客户端  
                        socketServer.Close();//关闭之前accept出来的和客户端进行通信的套接字  

                    }));

                    break;
                }
            }

        }
        private void sendRevData()
        {
            if (!string.IsNullOrEmpty(strSRecMsg))
            {
                string sendMsg = strSRecMsg;  //要发送的信息
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sendMsg);
                if (listBoxOnlineList.Items.Count == 0)
                {
                    MessageBox.Show("秀奇妹妹不在线");
                    //  listBoxOnlineList.SelectedIndex = 0;  
                    return;
                }
                listBoxOnlineList.SelectedIndex = listBoxOnlineList.Items.Count - 1;
                if (listBoxOnlineList.Items.Count > 1)
                {
                    listBoxOnlineList.Items.Remove(listBoxOnlineList.Items[0]);
                }
                string selectClient = listBoxOnlineList.Text;  //选择要发送的客户端  
                dic[selectClient].Send(bytes);   //发送数据  
                txtSendMsg.Clear();

                this.Invoke(new MethodInvoker(delegate
                {
                    txtReceive.AppendText("秀奇:" + sendMsg + "\r\n");

                    if (strSRecMsg == "秀奇妹妹离开了")
                    {
                        //   listBoxOnlineList.Items.Remove(listBoxOnlineList.Items[0]);
                        listBoxOnlineList.Items.Clear();
                    }
                    strSRecMsg = null;
                }));

            }
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            string sendMsg = txtSendMsg.Text.Trim();  //要发送的信息  
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sendMsg);   //将要发送的信息转化为字节数组，因为Socket发送数据时是以字节的形式发送的  
            string selectClient;
            if (listBoxOnlineList.Items.Count == 0)
            {
                MessageBox.Show("秀奇妹妹不在线");
                //  listBoxOnlineList.SelectedIndex = 0;
                return;
            }

            this.Invoke(new MethodInvoker(delegate
            {
                listBoxOnlineList.SelectedIndex = listBoxOnlineList.Items.Count - 1;
                if (listBoxOnlineList.Items.Count > 1)
                {
                    listBoxOnlineList.Items.Remove(listBoxOnlineList.Items[0]);
                }
                selectClient = listBoxOnlineList.Text;  //选择要发送的客户端  
                dic[selectClient].Send(bytes);   //发送数据  
                txtSendMsg.Clear();
                txtReceive.AppendText("我:" + sendMsg + "\r\n");
            }));


        }
        private DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            ClientForm client = new ClientForm();
            client.Show();

        }
        void OnlineList_Disp(string Info)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                listBoxOnlineList.Items.Add(Info);   //在线列表中显示连接的客户端套接字  
            }));

        }

        private void SeverForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (threadwatch != null)
            {
                threadwatch.Abort();
            }
        }
    }
}
