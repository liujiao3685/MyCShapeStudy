using MVCProject.Core;
using MVCProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCProject.View
{
    public class AutoView : UserControl, IVehicleView
    {
        private IVehicleControl Control = new AutoMobileControl();

        private IVehicleModule Module = new ACME2000Sport("NB");

        public AutoView()
        {
            InitializeComponent();
            WrapUp(Control, Module);

        }

        private void WrapUp(IVehicleControl control, IVehicleModule module)
        {   


        }

        public void DisableAcceleration()
        {
            throw new NotImplementedException();
        }

        public void DisableDeceleration()
        {
            throw new NotImplementedException();
        }

        public void DisableTurning()
        {
            throw new NotImplementedException();
        }

        public void EnableAcceleration()
        {
            throw new NotImplementedException();
        }

        public void EnableDeceleration()
        {
            throw new NotImplementedException();
        }

        public void EnableTurning()
        {
            throw new NotImplementedException();
        }

        public void Update(IVehicleModule module)
        {
            throw new NotImplementedException();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AutoView
            // 
            this.Name = "AutoView";
            this.Size = new System.Drawing.Size(307, 203);
            this.ResumeLayout(false);

        }
    }
}
