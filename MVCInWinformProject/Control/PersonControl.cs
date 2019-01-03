using MVCInWinformProject.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCInWinformProject.Control
{
    public class PersonControl
    {
        public FormMain View;

        public PersonModule Module;

        public PersonControl(FormMain view)
        {
            //初始化一个默认module
            Module = new PersonModule() { ID = "1", Name = "Json" };
            //通过构造函数将view传入到control中
            View = view;

            //建立view与controllor关联
            //使view能使用control中的业务逻辑，module也能与viewUI进行双向绑定
            View.Controllor = this;
        }

        /// <summary>
        /// 执行更新操作的业务逻辑
        /// </summary>
        public void UpdatePerson()
        {
            UpdateToDbDataBase(Module);
        }

        private void UpdateToDbDataBase(PersonModule module)
        {
            //do something....

            //将数据更新到数据库，返回结果
            MessageBox.Show(module.ID + "\r\n" + module.Name);
        }

        //其他业务逻辑.....
    }
}
