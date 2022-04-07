using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Gf.Web.Automation.Framework.Selenium
{
    public static class Driver
    {
        [ThreadStatic]
        private static IWebDriver _driver;

        [ThreadStatic]
        public static Wait Wait;

        public static void Init()
        {
            FW.Log.Info($"Browser: {FW.fwConfig.Driver.Browser}");
            _driver = DriverFactory.Build(FW.fwConfig.Driver.Browser);
            _driver.Manage().Window.Maximize();
            Wait = new Wait(FW.fwConfig.Driver.WaitSecond);
        }

        public static IWebDriver Current => _driver ?? throw new NullReferenceException("_driver is null");

        public static Element FindElement (By by, string elementname)
        {
            try
            {
                var element = Wait.Until(drv => drv.FindElement(by));
                return new Element(element, elementname)
                {
                    FoundBy = by
                };
            }
            catch (WebDriverTimeoutException) 
            { 
                Console.WriteLine($"uanble to find element: {elementname}");
                FW.Log.Error($"Unable to find the element: {elementname}");
                return null;  
            }            
        }

        public static Elements FindElements(By by)
        {
            try
            {
                var elements = Wait.Until(drv => drv.FindElements(by));
                /*var elements = Wait.Until(delegate (IWebDriver drv)
                {
                    return drv.FindElements(by);
                });*/
                
                return new Elements(elements)
                {
                    Foundby = by
                };
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("uanble to find");
                FW.Log.Error("Unable to find");
                return null;
            }
        }

        public static void GoToPageURL(string url)
        {
            FW.Log.Info($"URL: {url}");
            Current.Navigate().GoToUrl(url);
        }

        public static string GetPageTitle => Current.Title;

        public static void Quit()
        {
            FW.Log.Info("Close Browser");
            Current.Quit();
            Current.Dispose();
        }
    }
}
