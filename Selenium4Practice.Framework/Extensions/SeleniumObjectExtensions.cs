using OpenQA.Selenium;
using Selenium4Practice.Framework.Interfaces;
using System;

namespace Selenium4Practice.Framework.Extensions
{
    public static class SeleniumObjectExtensions
    {
        public static TSeleniumObject EnterTextInInput<TSeleniumObject>(this TSeleniumObject seleniumObject, Func<TSeleniumObject, IWebElement> elementFunc, string value) where TSeleniumObject : ISeleniumObject
        {
            var element = elementFunc(seleniumObject);
            element.EnterTextInInput(value);
            return seleniumObject;
        }
    }
}
