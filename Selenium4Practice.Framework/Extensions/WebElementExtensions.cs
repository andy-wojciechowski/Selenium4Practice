using OpenQA.Selenium;
using System;

namespace Selenium4Practice.Framework.Extensions
{
    public static class WebElementExtensions
    {
        public static void Click(this IWebElement webElement, Action callback)
        {
            webElement.Click();
            callback();
        }
    }
}
