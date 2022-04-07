using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gf.Web.Automation.Page
{
    public class Pages
    {
        [ThreadStatic]
        public static LoginPage Login;

        [ThreadStatic]
        public static HomePage Home;

        [ThreadStatic]
        public static CheckOutPage CheckOut;

        [ThreadStatic]
        public static PreJudgePage PreJudge;

        public static void Init()
        {
            Login = new LoginPage();
            Home = new HomePage();
            CheckOut = new CheckOutPage();
            PreJudge = new PreJudgePage();
        }
    }
}
