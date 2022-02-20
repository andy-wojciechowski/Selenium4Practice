using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium4Practice.Framework.Interfaces;

public interface ISeleniumObject
{
    public IWebDriver WebDriver { get; set; }
    WebDriverWait WebDriverWait { get; set; }
}