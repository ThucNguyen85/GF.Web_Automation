using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gf.Web.Automation.Framework;
using Gf.Web.Automation.Framework.Selenium;
using Gf.Web.Automation.Page;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Gf.Web.Automation.Test
{
    public abstract class TestBase
    {
        [OneTimeSetUp]
        public virtual void BeforeAll()
        {
            FW.CreateTestResultDirectory();
            FW.SetConfig();
        }

        [SetUp]
        public virtual void BeforeEach()
        {
            FW.SetLogger();
            Driver.Init();           
            Pages.Init();            
            Driver.GoToPageURL(FW.fwConfig.Test.URL);            
        }

        [TearDown]
        public virtual void AfterEach()
        {
            if (TestContext.CurrentContext.Result.Outcome.ToString() == "Failed")
            {
                FW.Log.Info($"Test case: {TestContext.CurrentContext.Test.FullName} is {TestContext.CurrentContext.Result.Outcome} because {TestContext.CurrentContext.Result.Message}");
            }
            else
            {
                FW.Log.Info($"Test case: {TestContext.CurrentContext.Test.FullName} is {TestContext.CurrentContext.Result.Outcome}");
            }
            Driver.Quit();
        }
    }
}
