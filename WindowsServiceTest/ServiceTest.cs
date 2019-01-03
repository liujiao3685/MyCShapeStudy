using System;
using System.Data.SqlClient;
using System.ServiceProcess;

namespace WindowsServiceTest
{
    public partial class ServiceTest : ServiceBase
    {
        public ServiceTest()
        {
            InitializeComponent();
        }

        //定时器
        System.Timers.Timer tmBak = new System.Timers.Timer();

        protected override void OnStart(string[] args)
        {
            //using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            //{
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " DATABASENAME Service Start.");
            //}

            //到时间的时候执行事件 
            //tmBak.Interval = 60000;//一分钟执行一次
            //tmBak.AutoReset = true;//执行一次 false，一直执行true 
            //                       //是否执行System.Timers.Timer.Elapsed事件 
            //tmBak.Enabled = true;
            //tmBak.Start();
            //tmBak.Elapsed += new System.Timers.ElapsedEventHandler(SQLBak);

        }

        /// <summary>
        /// 备份数据库的服务
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void SQLBak(object source, System.Timers.ElapsedEventArgs e)
        {
            //如果当前时间是10点30分
            if (DateTime.Now.Hour == 9 && DateTime.Now.Minute == 50)
            {
                string sql = string.Format(@"
                    BACKUP DATABASE DATABASENAME 
                    TO DISK = N'E:\DBBak\DATABASENAME {0}{1}{2}.bak'--目录一定要存在
                    WITH INIT , NOUNLOAD , 
                    NAME = N'数据库备份', --名字随便取
                    NOSKIP , 
                    STATS = 10, 
                    NOFORMAT",
                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                try
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 正在备份DATABASENAME 数据库......");
                    }

                    SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=sa;pwd=44545454;database=DATABASENAME ");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                catch (Exception ex)
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 备份DATABASENAME 数据库出现异常：" + ex.Message);
                        return;
                    }
                }

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 备份DATABASENAME 数据库成功！");
                }

            }

        }

        protected override void OnStop()
        {
            //using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            //{
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " DATABASENAME  Service Stop.");
            //}
        }
    }
}
