using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadPoolProject
{
    public partial class FormMain : Form
    {

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            m_synContext = SynchronizationContext.Current;

            //new Thread(DoWorkY).Start();

            Thread thread = new Thread(new ThreadStart(DoWorkX)) { IsBackground = true };
            thread.Start();

            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.RunWorkerAsync();

        }

        #region UI线程的SynchronizationContext的Post/Send方法更新

        //定义线程的主体方法

        /// <summary>
        /// UI的同步上下文
        /// </summary>
        private SynchronizationContext m_synContext = null;
        private void DoWorkY()
        {
            while (true)
            {
                m_synContext.Post(SetYPosistion, 1);
                Thread.Sleep(500);
            }
        }

        //定义更新UI控件的方法
        private void SetYPosistion(object txt)
        {
            this.txtYLocation.Text = txt.ToString();
        }
        #endregion

        #region UI控件的Invoke/BeginInvoke方法更新-主流方法

        //将text更新的界面控件的委托类型
        private delegate void SetXPosHandle( string txt);

        //线程的主体方法
        private void DoWorkX()
        {
            int i = 0;
            while (true)
            {
                i++;
                UpdatePosition(i.ToString());
                Thread.Sleep(500);
            }
        }

        //定义更新UI控件的方法
        private void UpdatePosition( string txt)
        {
            if (this.txtXLocation.InvokeRequired)
            {
                this.BeginInvoke((EventHandler)delegate
                {
                    while (!this.txtXLocation.IsHandleCreated)
                    {
                        if (this.txtXLocation.Disposing || this.txtXLocation.IsDisposed)
                        {
                            return;
                        }
                    }
                    SetXPosHandle set = new SetXPosHandle(UpdatePosition);
                    this.txtXLocation.Invoke(set, new object[] { txt });
                });
            }
            else
            {
                this.txtXLocation.Text = txt;
            }
        }
        #endregion

        #region BackgroundWorker取代Thread执行异步操作
        /* 设置报告进度更新
         * backgroundWorker1.WorkerReportsProgress = true;
         * **/

        //线程主体方法
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            for (int i = 0; i < 60; i++)
            {
                this.backgroundWorker1.ReportProgress(50, i);
                Thread.Sleep(100);
            }
        }

        //UI更新方法
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.txtZLocation.Text = e.UserState.ToString();
        }
        #endregion


        private void btnJian_MouseDown(object sender, MouseEventArgs e)
        {
            this.label1.Text = Int32.Parse(label1.Text) - 1 + "";
            timer2.Interval = 500;
            timer2.Enabled = true;
        }

        private void btnJian_MouseUp(object sender, MouseEventArgs e)
        {
            //timer1.Enabled = false;
            timer2.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = Int32.Parse(label1.Text) - 1 + "";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //timer1.Interval = 50;
            //timer1.Enabled = true;
            this.label1.Text = Int32.Parse(label1.Text) - 1 + "";
            //timer2.Enabled = false;

        }

        private void btnJia_MouseDown(object sender, MouseEventArgs e)
        {
            this.label1.Text = Int32.Parse(label1.Text) + 1 + "";
            timer4.Interval = 500;
            timer4.Enabled = true;
        }

        private void btnJia_MouseUp(object sender, MouseEventArgs e)
        {
            timer3.Enabled = false;
            timer4.Enabled = false;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            this.label1.Text = Int32.Parse(label1.Text) + 1 + "";
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer3.Interval = 50;
            timer3.Enabled = true;
            timer4.Enabled = false;
        }

        private void btnMove_Click(object sender, EventArgs e)
        {

        }


    }
}
