using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPortProject.Core.Command
{
     public class AxisDevice:CommandBase
    {
        protected override int GetBodyLength()
        {
            throw new NotImplementedException();
        }

        protected override void SerializationBody(byte[] data, ref int offset)
        {
            throw new NotImplementedException();
        }
    }
}
