using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolProject
{
    public class SocketTest
    {
        private Socket m_clientSocket = null;

        private Socket m_serviceSocket = null;

        private Socket m_userSocket = null;

        private byte[] m_clientData = new byte[1024];

        private byte[] m_userData = new byte[1024];

        private IPAddress ip = IPAddress.Parse("127.0.0.1");

        private IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8080);

        public SocketTest()
        {
            Init();
        }

        private void Init()
        {
            m_serviceSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_serviceSocket.Bind(endPoint);
            m_serviceSocket.Listen(10);

            //线程池中加入包含Accept的线程
            ThreadPool.QueueUserWorkItem(new WaitCallback(accept));

            //模拟客户端连接
            ThreadPool.QueueUserWorkItem(new WaitCallback(userComing));

            //模拟服务端接收到的客户端socket信息，负责接收和发送数据
            ThreadPool.QueueUserWorkItem(new WaitCallback(clientWorking));

            System.Diagnostics.Debug.Write(m_serviceSocket.ToString());

            Thread.Sleep(1000);

            //模拟客户端发送数据
            string temp = "";
            while (true)
            {
                temp = "456";
                m_userData = Encoding.UTF8.GetBytes(temp);
                m_userSocket.Send(m_userData);
            }

        }

        //模拟服务端与连接的客户端交接的管理client
        private void clientWorking(object obj)
        {
            int len = 0;
            while (true)
            {
                if (m_clientSocket != null)
                {
                    len = m_clientSocket.Receive(m_clientData, m_clientData.Length, 0);
                    System.Diagnostics.Debug.Write("m_clientData------:" + Encoding.UTF8.GetString(m_clientData));
                }
            }
        }

        //客户端
        private void userComing(object obj)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8000);
            m_userSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            m_userSocket.Connect(endPoint);
        }

        //模拟服务端对于用户的管理socket（unsafe）
        private void accept(object obj)
        {
            while (true)
            {
                m_clientSocket = m_serviceSocket.Accept();
                break;
            }
        }

    }
}
