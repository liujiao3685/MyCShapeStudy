using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using COMPortProject.Enums;

namespace COMPortProject.Commons
{
    /// <summary>
    /// 串口通讯工具类
    /// </summary>
    public class SerialPortUtil
    {
        /// <summary>
        /// 接收事件是否有效，false为有效
        /// </summary>
        public bool ReceiveEventFlag = false;

        /// <summary>
        /// 结束符比特
        /// </summary>
        public byte EndByte = 0x23;//string Start = "~"  End = "#";

        public delegate void DataReceivedEventHandler(DataReceivedEventArgs e);

        /// <summary>
        /// 完整协议的记录处理事件
        /// </summary>
        public event DataReceivedEventHandler DataReceived;
        public event SerialErrorReceivedEventHandler Error;

        #region 变量属性
        private SerialPort _comPort = new SerialPort();
        private string _portName;//串口号，默认COM1
        private SerialPortBaudRates _baudRate;
        private SerialPortDatabit _dataBits;
        private Parity _parity;
        private StopBits _stopBits;

        /// <summary>
        /// 串口号
        /// </summary>
        public string PortName
        {
            set { _portName = value; }
            get { return _portName; }
        }

        /// <summary>
        /// 波特率
        /// </summary>
        public SerialPortBaudRates BaudRates
        {
            set { _baudRate = value; }
            get { return _baudRate; }
        }

        /// <summary>
        /// 数据位
        /// </summary>
        public SerialPortDatabit Databit
        {
            set { _dataBits = value; }
            get { return _dataBits; }
        }

        /// <summary>
        /// 奇偶校验位
        /// </summary>
        public Parity Parity
        {
            set { _parity = value; }
            get { return _parity; }
        }

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits
        {
            set { _stopBits = value; }
            get { return _stopBits; }
        }

        #endregion

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SerialPortUtil()
        {
            _portName = "COM1";
            _baudRate = SerialPortBaudRates.BaudRate_9600;
            _parity = Parity.None;
            _dataBits = SerialPortDatabit.EightBits;
            _stopBits = StopBits.One;

            _comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
            _comPort.ErrorReceived += new SerialErrorReceivedEventHandler(comPort_ErrorReceived);
        }

        /// <summary>
        /// 参数构造函数（使用枚举参数构造）
        /// </summary>
        /// <param name="name">串口号</param>
        /// <param name="baud">波特率</param>
        /// <param name="parity">奇偶校验位</param>
        /// <param name="databit">数据位</param>
        /// <param name="stopBits">停止位</param>
        public SerialPortUtil(string name, SerialPortBaudRates baud, Parity parity, SerialPortDatabit databit, StopBits stopBits)
        {
            _portName = name;
            _baudRate = baud;
            _parity = parity;
            _dataBits = databit;
            _stopBits = stopBits;

            _comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
            _comPort.ErrorReceived += new SerialErrorReceivedEventHandler(comPort_ErrorReceived);
        }

        /// <summary>
        /// 参数构造函数（使用字符串参数构造）
        /// </summary>
        /// <param name="name">串口号</param>
        /// <param name="baud">波特率</param>
        /// <param name="parity">奇偶校验位</param>
        /// <param name="databit">数据位</param>
        /// <param name="stopbit">停止位</param>
        public SerialPortUtil(string name, string baud, string parity, string databit, string stopbit)
        {
            _portName = name;
            _baudRate = (SerialPortBaudRates)Enum.Parse(typeof(SerialPortBaudRates), baud);
            _parity = (Parity)Enum.Parse(typeof(Parity), parity);
            _dataBits = (SerialPortDatabit)Enum.Parse(typeof(SerialPortDatabit), databit);
            _stopBits = (StopBits)Enum.Parse(typeof(StopBits), stopbit);

            _comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
            _comPort.ErrorReceived += new SerialErrorReceivedEventHandler(comPort_ErrorReceived);

        }

        /// <summary>
        /// 数据接收处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //禁止接收事件时直接退出
            if (ReceiveEventFlag) return;

            var readString = ReadData();

            //触发整条记录处理
            if (DataReceived != null)
            {
                DataReceived(new DataReceivedEventArgs(readString));
            }
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <returns></returns>
        public string ReadData()
        {
            if (!IsOpen) _comPort.Open();

            #region 根据结束字节来判断是否全部获取完成
            List<byte> bytes = new List<byte>();
            bool found = false; //是否检测到结束符号
            while (_comPort.BytesToRead > 0 || !found || bytes[0] == 0xAA)
            {
                byte[] readBuffer = new byte[_comPort.ReadBufferSize + 1];
                int count = _comPort.Read(readBuffer, 0, _comPort.ReadBufferSize);
                for (int i = 0; i < count; i++)
                {
                    bytes.Add(readBuffer[i]);
                    if (readBuffer[i] == EndByte)
                    {
                        found = true;
                    }
                }
            }
            #endregion

            //字符转换
            string readString = Encoding.UTF8.GetString(bytes.ToArray(), 0, bytes.Count);
            return readString;
        }

        /// <summary>
        /// 错误处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void comPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (Error != null)
            {
                Error(sender, e);
            }
        }

        #region 数据写入

        /// <summary>
        /// 写入数据-string
        /// </summary>
        /// <param name="msg">字符串</param>
        public void WriteData(string msg)
        {
            if (!IsOpen) _comPort.Open();

            _comPort.Write(msg);
        }

        /// <summary>
        /// 写入数据-byte
        /// </summary>
        /// <param name="msg">字节数组</param>
        public void WriteData(byte[] msg)
        {
            if (!IsOpen) _comPort.Open();

            _comPort.Write(msg, 0, msg.Length);
        }

        /// <summary>
        /// 写入数据-offset/count
        /// </summary>
        /// <param name="msg">数据源</param>
        /// <param name="offset">数从0字节开始的字节偏移量</param>
        /// <param name="count">写入的字节数量</param>
        public void WriteData(byte[] msg, int offset, int count)
        {
            if (!IsOpen) _comPort.Open();

            _comPort.Write(msg, offset, count);
        }

        /// <summary>
        /// 发送串口指令
        /// </summary>
        /// <param name="sendBytes">发送数据</param>
        /// <param name="receivedBytes"/>接收数据
        /// <param name="repeatTime">重复次数</param>
        /// <returns></returns>
        public int SendCommand(byte[] sendBytes, ref byte[] receivedBytes, int repeatTime)
        {
            if (!IsOpen)
            {
                _comPort.Open();
            }

            ReceiveEventFlag = true;//关闭接收事件
            _comPort.DiscardInBuffer();//清空缓存区
            _comPort.Write(sendBytes, 0, sendBytes.Length);

            int num = 0, ret = 0;
            while (num++ < repeatTime)
            {
                if (_comPort.BytesToRead >= receivedBytes.Length) break;
                Thread.Sleep(1);
            }

            if (_comPort.BytesToRead >= receivedBytes.Length)
            {
                ret = _comPort.Read(receivedBytes, 0, receivedBytes.Length);
            }
            ReceiveEventFlag = false;
            return ret;
        }

        #endregion

        #region 格式转换

        /// <summary>
        /// 转换十六进制字符到字节数组
        /// </summary>
        /// <param name="msg">待转换字符</param>
        /// <returns>字节数组</returns>
        public static byte[] HexToBytes(string msg)
        {
            msg = msg.Replace(" ", "");//移除空格

            //create a byte array the length of the
            //divided by 2 (Hex is 2 characters in length)
            byte[] comBuffer = new byte[msg.Length / 2];

            for (int i = 0; i < msg.Length; i += 2)
            {
                //convert each set of 2 characters to a byte and add to the array
                comBuffer[i / 2] = Convert.ToByte(msg.Substring(i, 2), 16);
            }
            return comBuffer;
        }

        /// <summary>
        /// 转换字节数组到十六进制字符中
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToHex(Byte[] bytes)
        {
            StringBuilder builder = new StringBuilder(bytes.Length * 3);
            foreach (byte data in bytes)
            {
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));
            }

            return builder.ToString().ToUpper();
        }

        #endregion

        /// <summary>
        /// 检测断开是否存在
        /// </summary>
        /// <param name="portName"></param>
        /// <returns></returns>
        public static bool Exist(string portName)
        {
            foreach (string port in SerialPort.GetPortNames())
            {
                if (port == portName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 格式化端口相关属性
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static string Format(SerialPort port)
        {
            return String.Format("{0} ({1},{2},{3},{4},{5})",
                port.PortName, port.BaudRate, port.DataBits, port.StopBits, port.Parity, port.Handshake);
        }

        /// <summary>
        /// 端口是否打开
        /// </summary>
        public bool IsOpen
        {
            get { return _comPort.IsOpen; }
        }

        /// <summary>
        /// 打开端口
        /// </summary>
        public void OpenPort()
        {
            if (_comPort.IsOpen) _comPort.Close();

            _comPort.PortName = _portName;
            _comPort.BaudRate = (int)_baudRate;
            _comPort.Parity = _parity;
            _comPort.DataBits = (int)_dataBits;
            _comPort.StopBits = _stopBits;

            _comPort.Open();
        }

        /// <summary>
        /// 关闭端口
        /// </summary>
        public void ClosePort()
        {
            if (_comPort.IsOpen)
            {
                _comPort.Close();
            }
        }

        /// <summary>
        /// 丢弃来着串行驱动程序的接收和发送缓存区的数据
        /// </summary>
        public void DiscardBuffer()
        {
            _comPort.DiscardInBuffer();
            _comPort.DiscardOutBuffer();
        }

        #region 常用的列表数据获取和绑定操作

        /// <summary>
        /// 设置串口号
        /// </summary>
        /// <param name="comBox"></param>
        public static void SetPortNameValue(ComboBox comBox)
        {
            comBox.Items.Clear();
            foreach (string item in SerialPort.GetPortNames())
            {
                comBox.Items.Add(item);
            }

        }

        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="comBox"></param>
        public static void SetBaudRateValues(ComboBox comBox)
        {
            comBox.Items.Clear();
            foreach (SerialPortBaudRates rate in Enum.GetValues(typeof(SerialPortBaudRates)))
            {
                comBox.Items.Add(((int)rate).ToString());
            }

        }

        /// <summary>
        /// 设置数据位
        /// </summary>
        /// <param name="comBox"></param>
        public static void SetDataBitsValues(ComboBox comBox)
        {
            comBox.Items.Clear();
            foreach (SerialPortDatabit databit in Enum.GetValues(typeof(SerialPortDatabit)))
            {
                comBox.Items.Add(((int)databit).ToString());
            }
        }

        /// <summary>
        /// 设置校验位列表
        /// </summary>
        /// <param name="comBox"></param>
        public static void SetParityValues(ComboBox comBox)
        {
            comBox.Items.Clear();
            foreach (string item in Enum.GetValues(typeof(Parity)))
            {
                comBox.Items.Add(item);
            }

        }

        /// <summary>
        /// 设置停止位
        /// </summary>
        /// <param name="comBox"></param>
        public static void SetStopBitsValues(ComboBox comBox)
        {
            comBox.Items.Clear();
            foreach (string item in Enum.GetValues(typeof(StopBits)))
            {
                comBox.Items.Add(item);
            }
        }

        #endregion
    }
}
