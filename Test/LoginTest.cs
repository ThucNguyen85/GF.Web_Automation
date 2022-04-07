using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gf.Web.Automation.Framework.Selenium;
using Gf.Web.Automation.Page;
using NUnit.Framework;

namespace Gf.Web.Automation.Test
{
    public class LoginTest : TestBase
    {
        [Test, Category("Login")]
        public void VerifyErrorMessageDisplayWhenLoginFailed()
        {
            //Click on Checkout on Homepage
            Pages.Home.GoToCheckOutPage();
            Assert.Multiple(() => {
                //Enter invalid UserName and Password
                Pages.Login.SubmitLogin("invalidUserName", "invalidPassword");
                Pages.Login.CheckErrorMessageDisplays();

                //leave UserName blank 
                Pages.Login.SubmitLogin("", "Global@123");
                Pages.Login.CheckErrorMessageDisplays();

                //leave Username and Password blank
                Pages.Login.SubmitLogin("", "");
                Pages.Login.CheckErrorMessageDisplays();

                //leave Password blank
                Pages.Login.SubmitLogin("admin", "");
                Pages.Login.CheckErrorMessageDisplays();
            });            
        }

        [Test, Category("Login")]
        public void VerifyCheckoutPageDisplaysAfterLoginSuccessfully()
        {
            Pages.Home.GoToCheckOutPage();
            //Enter valid UserName and Password
            Pages.Login.SubmitLogin("admin", "Global@123");            
            Assert.That(Pages.CheckOut.Map.MainContent.Displayed, "The CheckOut Page should display after Login");
        }
    }
}
