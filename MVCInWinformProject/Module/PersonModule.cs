using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCInWinformProject.Module
{
    public class PersonModule : INotifyPropertyChanged
    {
        private string m_id;
        public string ID
        {
            set { m_id = value; OnPropertyChange("ID"); }
            get { return m_id; }
        }

        private string m_name;
        public string Name
        {
            set { m_name = value; OnPropertyChange("Name"); }
            get { return m_name; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
