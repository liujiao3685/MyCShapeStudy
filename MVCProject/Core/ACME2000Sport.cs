using MVCProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Core
{
    public class ACME2000Sport : AutoMobile
    {
        public ACME2000Sport(string name) : base(name, 200, 40, -20)
        {
        }

        public ACME2000Sport(string name, int maxSpeed, int maxTurnSpeed, int maxReverseSpeed) : base(name, maxSpeed, maxTurnSpeed, maxReverseSpeed)
        {
        }
    }
}
