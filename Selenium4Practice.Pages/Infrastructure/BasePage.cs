using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium4Practice.Framework.Interfaces;
using Selenium4Practice.Pages.Elements;
using System.Linq;

namespace Selenium4Practice.Pages.Infrastructure;

public abstract class BasePage : IPage
{
    #region Properties

    public IWebDriver WebDriver { get; set; }
    public WebDriverWait WebDriverWait { get; set; }
    public string BaseUrl { get; set; }
    public string PageUrl { get; set; }
    public string FullUrl => BaseUrl + PageUrl;

    private By NavbarLocator => By.TagName("nav");

    public Navbar Navbar => new(WebDriver.FindElement(NavbarLocator));

    #endregion

    #region Public Methods

    public virtual void Navigate(params object[] urlArgs)
    {
        var urlToUse = urlArgs.Any() ? string.Format(FullUrl, urlArgs) : FullUrl;
        WebDriver.Navigate().GoToUrl(urlToUse);
        WaitForPage();
    }
    public abstract void WaitForPage();
    public virtual bool IsAt() => WebDriver.Url == FullUrl;

    #endregion
}