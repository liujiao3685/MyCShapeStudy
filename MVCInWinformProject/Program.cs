using MVCInWinformProject.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCInWinformProject
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Controllor-First模式,先创建Controllor(PersonControllor)再将View(PersonForm)注入到Controllor(PersonControllor)中
            PersonControl control = new PersonControl(new FormMain());

            Application.Run(control.View);
        }
    }
}
