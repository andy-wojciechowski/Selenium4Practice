using OpenQA.Selenium;
using Selenium4Practice.Framework.Attributes;
using Selenium4Practice.Framework.SeleniumObjects.Interfaces;
using Selenium4Practice.Pages.Elements;
using Selenium4Practice.Pages.Infrastructure;
using SeleniumExtras.WaitHelpers;

namespace Selenium4Practice.Pages;

[PageUrl("/")]
public class HomePage : BasePage
{
    #region Properties

    private By ContentLocator => By.Id("contcont");
    private By ImageCarouselLocator => By.Id("contcar");

    private ImageCarousel ImageCarousel => new(WebDriver.FindElement(ImageCarouselLocator));

    #endregion

    #region Methods

    public HomePage(IWebDriver webDriver, ISeleniumObjectInitializer seleniumObjectInitializer) : base(webDriver, seleniumObjectInitializer) { }

    public override void WaitForPage() =>
        WebDriverWait.Until(ExpectedConditions.ElementIsVisible(ContentLocator));

    #endregion
}