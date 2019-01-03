using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.UI;

namespace WindowsFormsApplication1
{
    public partial class ManyForms : Form
    {
        private MontionControlForm m_montionControl;

        private ParamSetForm m_paramSet;

        private List<FormBase> m_listForm;

        public ManyForms()
        {
            InitializeComponent();

            m_montionControl = new MontionControlForm();
            m_paramSet = new ParamSetForm();

            m_listForm = new List<FormBase>();
            m_listForm.Add(m_montionControl);
            m_listForm.Add(m_paramSet);
        }

        private void ManyForms_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            m_montionControl.DataChange += new MontionControlForm.DataChangeHandle(DataChange);
            m_paramSet.DataChange += new ParamSetForm.DataChangeHandle(ParamDataChange);
            this.tabControl1.TabPages.Clear();
            foreach (var item in m_listForm)
            {
                TabPage page = new TabPage();
                //page.Name = item.ControlName;
                page.Text = item.ControlName;
                page.Controls.Add(item);
                this.tabControl1.TabPages.Add(page);
            }
        }

        private void btnMontionControl_Click(object sender, EventArgs e)
        {
            this.panelParent.Controls.Clear();
            this.panelParent.Controls.Add(m_montionControl);
            //this.m_montionControl.DataChange += new MontionControlForm.DataChangeHandle(DataChange);
            this.m_montionControl.Show();
        }

        private void btnParamSet_Click(object sender, EventArgs e)
        {
            this.panelParent.Controls.Clear();
            this.panelParent.Controls.Add(m_paramSet);
            //this.m_paramSet.DataChange += new ParamSetForm.DataChangeHandle(DataChange);
            this.m_paramSet.Show();
        }

        public void DataChange(object sender, MyEvent args)
        {
            this.OutPut(args.type + "-----" + args.GetString("MontionControl"));
        }

        public void ParamDataChange(object sender, MyEvent args)
        {
            this.OutPut(args.type + "-----" + args.GetString("ParamSet"));
        }

        private void OutPut(String str)
        {
            this.txtOutput.AppendText(str + "\r\n");
        }

    }
}
