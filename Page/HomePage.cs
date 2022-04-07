using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gf.Web.Automation.Framework;
using Gf.Web.Automation.Framework.Selenium;
using OpenQA.Selenium;

namespace Gf.Web.Automation.Page
{
    public class HomePage
    {
        public readonly HomePageMap Map;

        public HomePage()
        {
            Map = new HomePageMap();
        }

        public void GoToCheckOutPage()
        {
            Driver.Wait.Until(WaitConditions.ElementIsDisplayed(Map.CheckOut)).Click();
            Thread.Sleep(500);
        }

        public void GoToInventoryPage()
        {
            Driver.Wait.Until(WaitConditions.ElementIsDisplayed(Map.Inventory)).Click();
            Thread.Sleep(500);
        }

        public void GoToPackingPage()
        {
            Driver.Wait.Until(WaitConditions.ElementIsDisplayed(Map.Packing)).Click();
            Thread.Sleep(500);
        }

        public void GoToLogisticPage()
        {
            Driver.Wait.Until(WaitConditions.ElementIsDisplayed(Map.Logistic)).Click();
            Thread.Sleep(500);
        }

        public void GoToDetectionPage()
        {
            Driver.Wait.Until(WaitConditions.ElementIsDisplayed(Map.Detection)).Click();
            Thread.Sleep(500);
        }

        public void GoToDataPage()
        {
            Driver.Wait.Until(WaitConditions.ElementIsDisplayed(Map.Data)).Click();
            Thread.Sleep(500);
        }
    }

    public class HomePageMap
    {
        public Element CheckOut => Driver.FindElement(By.XPath("//span[text()='Checkout']"), "Check Out link");
        public Element Inventory => Driver.FindElement(By.XPath("//span[text()='INVENTORY']"), "Inventory link");
        public Element Packing => Driver.FindElement(By.XPath("//span[text()='PACKING']"), "Packing link");
        public Element Logistic => Driver.FindElement(By.XPath("//span[text()='LOGISTIC']"), "Logistic link");
        public Element Detection => Driver.FindElement(By.XPath("//span[text()='DETECTION']"), "Detection link");
        public Element Data => Driver.FindElement(By.XPath("//span[text()='DATA']"), "Data link");
    }
}
