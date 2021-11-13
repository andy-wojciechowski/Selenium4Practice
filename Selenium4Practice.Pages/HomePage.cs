using OpenQA.Selenium;
using Selenium4Practice.Framework.Attributes;
using Selenium4Practice.Pages.Infrastructure;
using SeleniumExtras.WaitHelpers;

namespace Selenium4Practice.Pages;

[PageUrl("/")]
public class HomePage : BasePage
{
    #region Properties

    private By ContentLocator => By.Id("contcont");

    #endregion

    #region Methods

    public override void WaitForPage() =>
        WebDriverWait.Until(ExpectedConditions.ElementIsVisible(ContentLocator));

    #endregion
}