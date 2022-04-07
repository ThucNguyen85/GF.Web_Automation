using System;
using System.Collections.Generic;
using System.Text;
using Gf.Web.Automation.Framework.Selenium;
using Gf.Web.Automation.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace Gf.Web.Automation.Page
{
    public class PreJudgePage
    {
        public readonly PreJudgePageMap Map;

        public PreJudgePage()
        {
            Map = new PreJudgePageMap();
        }

        public string GetBatchID()
        {
            return Driver.Wait.Until(WaitConditions.ElementIsDisplayed(Map.BatchIDLabel)).GetAttribute("innerText");
        }

        public void WaitForPreJudgePageIsLoaded()
        {
            FW.Log.Step("Wait for PreJudge page is loaded");
            Driver.Wait.Until(WaitConditions.PageIsLoadedCompletely());
            Driver.Wait.Until(WaitConditions.ElementNotDisplayed(Map.LoadIcon));
            Thread.Sleep(750);
        }

        public void CloseTheBatchCompletedMsg()
        {
            FW.Log.Step("Close the Batch Completed pop up");
            try
            {
                if (Map.BatchCompleteMsg.Displayed)
                {
                    Map.CloseBatchCompleteMsgBtn.ClickByJS();
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("The Batch Compelte Pop up is not displayed");
            }
        }
    }

    public class PreJudgePageMap
    {
        public Element BatchIDLabel => Driver.FindElement(By.CssSelector("label.h2.text-dark.mb-0"), "Batch ID label");
        public Element LoadIcon => Driver.FindElement(By.CssSelector(".spinner-border"), "Loading Icon");
        public Element FilterPouches => Driver.FindElement(By.CssSelector(".btn.dropdown-toggle.btn-icon"), "Filter Pouches button");
        public Element BatchCompleteMsg => Driver.FindElement(By.CssSelector("div.modal-content.bg-transparent div.modal-body"), "Batch Complete pop up");
        public Element CloseBatchCompleteMsgBtn => Driver.FindElement(By.CssSelector("div.modal-content.bg-transparent i.fa.fa-times"), "Close Batch Complete pop up button");
    }
}
