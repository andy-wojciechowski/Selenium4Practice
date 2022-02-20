using OpenQA.Selenium;
using Selenium4Practice.Framework.Attributes;
using Selenium4Practice.Framework.SeleniumObjects.Interfaces;
using Selenium4Practice.Pages.Infrastructure;

namespace Selenium4Practice.Pages;

[PageUrl("/cart.html")]
public class CartPage : BasePage
{
    public CartPage(IWebDriver webDriver, ISeleniumObjectInitializer seleniumObjectInitializer) : base(webDriver, seleniumObjectInitializer) { }

    public override void WaitForPage()
    {
        throw new System.NotImplementedException();
    }
}
