using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Gf.Web.Automation.Framework.Selenium
{
    public class Element : IWebElement
    {
        private readonly IWebElement _element;

        public IWebElement Current => _element ?? throw new System.NullReferenceException("_elemnt is Null.");

        public readonly string Name;

        public By FoundBy { get; set; }
        public Element(IWebElement element, string name)
        {
            _element = element;
            Name = name;
        }        

        public string TagName => Current.TagName;

        public string Text => Current.Text;

        public bool Enabled => Current.Enabled;

        public bool Selected => Current.Selected;

        public Point Location => Current.Location;

        public Size Size => Current.Size;

        public bool Displayed => Current.Displayed;

        public void Clear()
        {
            FW.Log.Step($"Clear Text on {Name}");
            Current.Clear();
        }

        public void Click()
        {
            FW.Log.Step($"Click on {Name}");
            Current.Click();
        }
        public void ClickByJS()
        {
            FW.Log.Step($"Click on {Name}");
            IJavaScriptExecutor ex = (IJavaScriptExecutor)Driver.Current;
            ex.ExecuteScript("arguments[0].click();", Current);
        }

        public IWebElement FindElement(By by)
        {
            return Current.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return Current.FindElements(by);
        }

        public string GetAttribute(string attributeName)
        {
            return Current.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return Current.GetCssValue(propertyName);
        }

        public string GetProperty(string propertyName)
        {
            return Current.GetProperty(propertyName);
        }

        public void SendKeys(string text)
        {
            FW.Log.Step($"Sendkey {text} to {Name}");
            Current.SendKeys(text);
        }

        public void Submit()
        {
            Current.Submit();
        }
    }
}
