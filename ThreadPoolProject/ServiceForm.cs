using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ThreadPoolProject
{
    public partial class ServiceForm : Form
    {
        public ServiceForm()
        {
            InitializeComponent();
        }

        private void ServiceForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            ThreadPool.SetMaxThreads(50, 10);
        }

        private Socket serviceSocket;

        private List<Socket> listClients = new List<Socket>();

        private Socket clientSocket;
        private byte[] testBytes;
        //异步回调
        private void AsyncAcceptCallBack(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            try
            {
                clientSocket = serviceSocket.EndAccept(ar);
                clientSocket.SendTimeout = 1000;

                int sendLength = clientSocket.Send(testBytes, SocketFlags.None);
                testBytes = new byte[sendLength];

                clientSocket.BeginReceive(testBytes, 0, sendLength, SocketFlags.None,
                    new AsyncCallback(ReceiveFormClient), null);

                if (sendLength == testBytes.Length)
                {

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ReceiveFormClient(IAsyncResult ar)
        {
            int read = clientSocket.EndReceive(ar);
            if (read > 0)
            {
                string msg = UTF8Encoding.Default.GetString(testBytes, 0, read);
                Console.WriteLine("");
            }

        }

        private void btnListener_Click(object sender, EventArgs e)
        {
            try
            {
                //创建一个监听线程
                Socket serviceSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                /*
                 * 拆包：当一次发送（Socket）的数据量过大，而底层（TCP/IP）不支持一次发送那么大的数据量，则会发生拆包现象。
                    黏包：当在短时间内发送（Socket）很多数据量小的包时，底层（TCP/IP）会根据一定的算法（指Nagle）把一些包合作为一个包发送。
                 * */
                //serviceSocket.NoDelay = true;//设置为true即可防止在发送的时候黏包:

                IPAddress ip = IPAddress.Any;

                //接收目标端口
                IPEndPoint endPoint = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text.Trim()));
                //此处绑定经常发生异常的原因：
                /**
                 * 当连接主动关闭后，端口状态变为TIME_WAIT，其他程序依然不能使用这个端口，防止服务端因为超时重新发送的确认连接断开对新连接的程序造成影响。
                   TIME_WAIT的时间一般有底层决定，一般是2分钟，还有1分钟和30秒的。
                 */
                serviceSocket.Bind(endPoint);
                serviceSocket.Listen(500);//最大监听数

                //异步监听
                //serviceSocket.BeginAccept(new AsyncCallback(AsyncAcceptCallBack), serviceSocket);

                ShowMsg("监听成功！！");

                //开一个线程一直进行服务端的连接监听
                Thread thread = new Thread(Listen);
                thread.IsBackground = true;
                thread.Start(serviceSocket);

            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message + "\r\n");
            }

        }

        private Socket sendSocket;
        void Listen(object obj)
        {
            Socket so = obj as Socket;
            //循环接收客户端的请求
            while (true)
            {
                sendSocket = so.Accept();
                ShowMsg(sendSocket.RemoteEndPoint + "：连接成功！");

                //每有一个客户端请求连接成功后，单独为其开出一个线程
                Thread r_t = new Thread(Receive);
                r_t.IsBackground = true;
                r_t.Start(sendSocket);

                /**在实际生产中，创建的线程会交给线程池来处理，为了：
                //线程复用，创建线程耗时，回收线程慢
                //防止短时间内高并发，指定线程池大小，超过数量将等待，方式短时间创建大量线程导致资源耗尽，服务挂掉
                 * */
                //ThreadPool.QueueUserWorkItem(Receive);
            }
        }

        void Receive(object obj)
        {
            Socket so = obj as Socket;
            listClients.Add(so);
            txtNum.Text = listClients.Count.ToString();

            while (true)
            {
                sendSocket = so;

                byte[] data = new byte[1024];
                int readLength = sendSocket.Receive(data);
                if (readLength == 0)
                {
                    break;
                }

                string s = Encoding.UTF8.GetString(data, 0, readLength);
                if ("end".Equals(s))
                {
                    ShowMsg("客户端：" + sendSocket.RemoteEndPoint + "已经断开连接！");
                    sendSocket.Close();
                    //sendSocket.Shutdown(SocketShutdown.Both);//禁止socket发送跟接收数据
                    break;
                }
                ShowMsg(sendSocket.RemoteEndPoint + ":" + s);
            }
        }

        void Send(string s)
        {
            try
            {
                if (SocketUseState(sendSocket))
                {
                    byte[] data = Encoding.UTF8.GetBytes(s);
                    sendSocket.Send(data);
                }
                else
                {
                    ShowMsg("未找到连接的客户端！");
                }

            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }
        }

        /// <summary>
        /// TCP安全关闭
        /// </summary>
        /// <returns></returns>
        public bool SafeClose()
        {
            if (serviceSocket == null || serviceSocket.Connected == false) return false;

            try
            {
                serviceSocket.Shutdown(SocketShutdown.Both);
                Thread.Sleep(10);
                serviceSocket.Close();
                serviceSocket.Dispose();
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

        private void btnSendData_Click(object sender, EventArgs e)
        {
            Send(txtSendMsg.Text);
            txtSendMsg.Text = String.Empty;
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            ClientForm client = new ClientForm();
            client.Show();
        }

        private void ShowMsg(string s)
        {
            txtReceive.AppendText(s + "\r\n");
        }

    }
}
