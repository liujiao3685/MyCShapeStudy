using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// WindowGrid.xaml 的交互逻辑
    /// </summary>
    public partial class WindowGrid : Window
    {
        public WindowGrid()
        {
            InitializeComponent();
        }

        private void UpdateUI()
        {

            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                //Thread.Sleep(TimeSpan.FromSeconds(2));
                labHello.Content = "欢迎你光临WPF的世界,Dispatche  同步方法 ！！";
            });

        }

        private void Btn_Thd(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(UpdateUI);
            t.Start();
        }

        private void Btn_Invoke(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    //Thread.Sleep(TimeSpan.FromSeconds(2));
                    this.labHello.Content = "欢迎你光临WPF的世界,Dispatche  异步方法 ！！" + DateTime.Now.ToString();

                }));

            }).Start();

        }

    }
}
