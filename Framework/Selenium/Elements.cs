using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Gf.Web.Automation.Framework.Selenium
{
    public class Elements : ReadOnlyCollection<IWebElement>
    {

        private readonly IList<IWebElement> _elements;

        public Elements(IList<IWebElement> list) : base(list)
        {
            _elements = list;
        }

        public By Foundby { get; set; }

        public bool IsEmpty => Count == 0;
    }
}
