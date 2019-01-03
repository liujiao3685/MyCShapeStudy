using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            WindowGrid main = new WindowGrid();
            WpfApplication1.MainWindow window = new MainWindow();
            app.ShutdownMode = ShutdownMode.OnLastWindowClose;
            app.Run(main);

        }

        private void Application_Activited(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
