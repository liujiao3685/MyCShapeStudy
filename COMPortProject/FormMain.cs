using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using COMPortProject.Commons;
using COMPortProject.Enums;
using DataReceivedEventArgs = COMPortProject.Commons.DataReceivedEventArgs;

namespace COMPortProject
{
    public partial class FormMain : Form
    {
        private SerialPortUtil serial;

        private Thread m_receiveT = null;

        private bool isOpen = false;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            //绑定端口号
            SerialPortUtil.SetPortNameValue(cmbPort);
            cmbPort.SelectedIndex = 0;

            //波特率
            SerialPortUtil.SetBaudRateValues(cmbBaudRate);
            cmbBaudRate.SelectedText = "9600";

            //数据位
            SerialPortUtil.SetDataBitsValues(cmbDataBit);
            cmbDataBit.SelectedText = "8";

            //校验位
            SerialPortUtil.SetParityValues(cmbParity);
            cmbParity.SelectedIndex = 0;

            //停止位
            SerialPortUtil.SetStopBitsValues(cmbStopBit);
            cmbStopBit.SelectedIndex = 1;


            this.btnSend.Enabled = isOpen;

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (serial == null)
                {
                    try
                    {
                        string portName = this.cmbPort.Text;

                        SerialPortBaudRates baud =
                            (SerialPortBaudRates)Enum.Parse(typeof(SerialPortBaudRates), cmbBaudRate.Text);
                        SerialPortDatabit databit =
                            (SerialPortDatabit)Enum.Parse(typeof(SerialPortDatabit), cmbDataBit.Text);
                        Parity parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
                        StopBits stopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBit.Text);

                        //使用枚举构造函数
                        //serial = new SerialPortUtil(portName, baud, parity, databit, stopBits);

                        //使用字符串构造函数
                        serial = new SerialPortUtil(portName, cmbBaudRate.Text, cmbParity.Text, cmbDataBit.Text, cmbStopBit.Text);
                        serial.DataReceived += serial_DataReceived;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        serial = null;
                        return;
                    }
                }

                if (!isOpen)
                {
                    //打开串口通讯
                    serial.OpenPort();
                    btnConnect.Text = "断开";

                    //m_receiveT = new Thread(Receive);
                    //m_receiveT.IsBackground = true;
                    //m_receiveT.Start();
                }
                else
                {
                    serial.ClosePort();
                    //m_receiveT.Abort();
                    btnConnect.Text = "连接";
                }

                isOpen = !isOpen;
                this.btnSend.Enabled = isOpen;
                this.labState.Text = isOpen ? "已连接" : "未连接";

            }
            catch (Exception ex)
            {
                this.labState.Text = ex.Message;
                MessageBox.Show(ex.Message);
            }
        }

        private void Receive()
        {
            while (true)
            {
                if (serial != null)
                {
                    string data = serial.ReadData();
                    txtReceive.Text += data;
                }
            }
        }

        /// <summary>
        /// 事件实时更新串口数据
        /// </summary>
        /// <param name="e"></param>
        private void serial_DataReceived(DataReceivedEventArgs e)
        {
            this.txtReceive.Invoke(new MethodInvoker(delegate
            {
                this.txtReceive.AppendText(e.DataReceived + Environment.NewLine);
            }));
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            string msg = txtSendData.Text;

            serial.WriteData(msg);
        }
    }
}
