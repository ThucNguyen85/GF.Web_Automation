using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Gf.Web.Automation.Framework.Selenium
{
    public sealed class WaitConditions
    {
        public static Func<IWebDriver, bool> ElementDisplayed(IWebElement element)
        { 
            bool condition (IWebDriver driver)
            {
                return element.Displayed && element.Enabled;
            }
            return condition;
        }

        public static Func<IWebDriver, bool> ElementNotDisplayed(IWebElement element)
        { 
            bool condition (IWebDriver driver)
            {
                try
                {
                    return !element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
                catch (NullReferenceException)
                {
                    return true;
                }
                
            }
            return condition;
        }

        public static Func<IWebDriver, IWebElement> ElementIsDisplayed(IWebElement element)
        {
            IWebElement condition(IWebDriver driver)
            {
                try
                {                    
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (ElementNotVisibleException)
                {
                    return null;
                }
            }
            return condition;
        }

        public static Func<IWebDriver, bool> TableLoadCompletely(IWebElement table)
        {
            bool condition(IWebDriver driver)
            {
                if (table.GetAttribute("aria-busy") == null)
                {
                    return true;
                }
                else return table.GetAttribute("aria-busy") == "false";
            }
            return condition;
        }

        public static Func<IWebDriver, bool> PageIsLoadedCompletely()
        {
            bool condition(IWebDriver driver)
            {
                return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
            }
            return condition;
        }        
    }
}
