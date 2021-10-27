using OpenQA.Selenium;

namespace Selenium4Practice.Framework.Interfaces
{
    public interface ISeleniumObject
    {
        IWebDriver WebDriver { get; internal set; }
        int Timeout { get; internal set; }
    }
}