using OpenQA.Selenium;
using Selenium4Practice.Framework.Attributes;
using Selenium4Practice.Framework.SeleniumObjects.Interfaces;
using Selenium4Practice.Pages.Infrastructure;
using System;

namespace Selenium4Practice.Pages;

[PageUrl("/prod.html?idp_={0}")]
public class ProductPage : BasePage
{
    public ProductPage(IWebDriver webDriver, ISeleniumObjectInitializer seleniumObjectInitializer) : base(webDriver, seleniumObjectInitializer) { }

    public override void WaitForPage()
    {
        throw new NotImplementedException();
    }
}
