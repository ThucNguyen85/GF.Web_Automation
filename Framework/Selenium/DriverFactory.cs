using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Gf.Web.Automation.Framework.Selenium
{
    public class DriverFactory
    {
        public static IWebDriver Build(string browserName)
        {
            switch (browserName)
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    var chromeservice = ChromeDriverService.CreateDefaultService();
                    chromeservice.LogPath = "D:\\chromedriver.log";
                    chromeservice.EnableVerboseLogging = true;
                    var experimentalFlags = new List<string>();
                    //experimentalFlags.Add("same-site-by-default-cookies@2");
                    //experimentalFlags.Add("cookies-without-same-site-must-be-secure@2");
                    experimentalFlags.Add("disable-popup-blocking");
                    chromeOptions.AddLocalStatePreference("browser.enabled_labs_experiments",
                        experimentalFlags);
                    return new ChromeDriver(chromeservice, chromeOptions);

                case "firefox":
                    
                    return new FirefoxDriver();

                default: return new ChromeDriver();
            }
        }
    }
}
