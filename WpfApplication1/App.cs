using System;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// 三种方式启动应用程序
    /// </summary>
    public class App
    {
        [STAThread]
        static void Main()
        {
            //定义application对象作为整个程序的入口
            Application app = new Application();

            //方法一：调用run方法，与winform一样
            WindowGrid win = new WindowGrid();
            app.ShutdownMode = ShutdownMode.OnLastWindowClose;
            app.Run(win);

            app.Activated += app_Activited;
            app.Exit += app_Exit;

            //方法二：//指定Application对象的MainWindow属性为启动窗体，然后调用无参数的Run方法  
            //WindowGrid win = new WindowGrid();
            //app.MainWindow = win;
            ////必须调用show，否则无法显示
            //win.Show();
            //app.Run();


            //方法三：Url的方式启动
            //app.StartupUri = new Uri("WindowGrid.xaml", UriKind.Relative);
            //app.Run();

        }

        private static void app_Exit(object sender, ExitEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void app_Activited(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}