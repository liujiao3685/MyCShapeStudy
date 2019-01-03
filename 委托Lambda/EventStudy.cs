using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 委托Lambda
{

    public class EventStudy
    {
        public void Test()
        {
            Upload upload = new Upload();
            upload.UploadEvent += Upload_UploadEvent;
            upload.OnUpload(this, new FileUploadedEventArgs() { Process = 100 });
        }

        private void Upload_UploadEvent(object sender, FileUploadedEventArgs e)
        {
            Console.WriteLine(e.Process.ToString());
        }

    }

    public class Upload
    {
        public delegate void UploadDelegate(object sender, FileUploadedEventArgs e);

        public event UploadDelegate UploadEvent;

        public void OnUpload(object sender, FileUploadedEventArgs e)
        {
            while (e.Process > 0)
            {
                e.Process--;
                UploadEvent?.Invoke(sender, e);

                Thread.Sleep(100);
            }
        }
    }

    public class FileUploadedEventArgs : EventArgs
    {
        public int Process { set; get; }

    }

}
