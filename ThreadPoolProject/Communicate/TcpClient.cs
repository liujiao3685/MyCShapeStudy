using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPoolProject.Communicate
{
    public class TcpClient
    {
        private Socket m_clientSocket;

        public TcpClient() { }

        public TcpClient(Socket socket)
        {
            m_clientSocket = socket;
        }

    }
}
