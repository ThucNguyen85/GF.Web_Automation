using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gf.Web.Automation.Framework;
using Gf.Web.Automation.Framework.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Gf.Web.Automation.Page
{
    public class LoginPage
    {
        public readonly LoginPageMap Map;

        public LoginPage()
        {
            Map = new LoginPageMap();
        }

        public void SubmitLogin(string username, string password)
        {
            Driver.Wait.Until(WaitConditions.ElementDisplayed(Map.UserName));
            if (Map.UserName.GetAttribute("value") != "" || Map.Password.GetAttribute("value") != "")
            {
                Map.UserName.Clear();
                Map.Password.Clear();
            }
            Map.UserName.SendKeys(username);
            Map.Password.SendKeys(password);
            Driver.Wait.Until(WaitConditions.ElementIsDisplayed(Map.LoginButton)).Click();
        } 
        
        public bool CheckLoginPageDisplay()
        {
            Driver.Wait.Until(WaitConditions.ElementDisplayed(Map.LoginButton));
            Console.WriteLine(Driver.GetPageTitle);
            return Driver.GetPageTitle == "IdentityServer";
        }

        public void CheckErrorMessageDisplays()
        {
            FW.Log.Step("Check error message displays when Login Failed");
            Assert.That(Map.ErrorMessage.Text == "Invalid username or password", "The error message 'Invalid username or password' should display");
        }
    }

    public class LoginPageMap
    {
        public Element UserName => Driver.FindElement(By.Id("Username"), "UserName textbox");
        public Element Password => Driver.FindElement(By.Id("Password"), "Password textbox");
        public Element LoginButton => Driver.FindElement(By.CssSelector(".btn-login"), "Login button");
        public Element ErrorMessage => Driver.FindElement(By.CssSelector(".validation-summary-errors"), "Error Message");
    }
}
