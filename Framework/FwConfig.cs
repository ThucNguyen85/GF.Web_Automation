using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gf.Web.Automation.Framework
{
    public class FwConfig
    {
        public DriverSettings Driver { get; set; }
        public TestSettings Test { get; set; }
    }

    public class DriverSettings
    { 
        public string Browser { get; set; }
        public int WaitSecond { get; set; }
    }

    public class TestSettings
    { 
        public string URL { get; set; }
    }

}
