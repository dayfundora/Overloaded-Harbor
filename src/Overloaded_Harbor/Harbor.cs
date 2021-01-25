using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overloaded_Harbor
{
    public class Harbor
    {

        public Harbor(int docks = 4)
        {
            DocksCount = docks;
        }

        public int DocksCount { get; set; }

    }
}
