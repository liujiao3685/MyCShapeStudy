using System;

namespace COMPortProject.Commons
{
    public class DataReceivedEventArgs : EventArgs
    {
        public string DataReceived;

        public DataReceivedEventArgs(string m_data)
        {
            DataReceived = m_data;
        }

    }
}