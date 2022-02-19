using OpenQA.Selenium;
using Selenium4Practice.Framework.Attributes;
using Selenium4Practice.Pages.Elements;
using Selenium4Practice.Pages.Infrastructure;
using SeleniumExtras.WaitHelpers;

namespace Selenium4Practice.Pages;

[PageUrl("/")]
public class HomePage : BasePage
{
    #region Properties

    private By ContentLocator => By.Id("contcont");
    private Navbar Navbar => new(WebDriver.FindElement(By.Id("narvbarx")));
    private ImageCarousel ImageCarousel => new(WebDriver.FindElement(By.Id("contcar")));

    #endregion

    #region Methods

    public override void WaitForPage() =>
        WebDriverWait.Until(ExpectedConditions.ElementIsVisible(ContentLocator));

    #endregion
}