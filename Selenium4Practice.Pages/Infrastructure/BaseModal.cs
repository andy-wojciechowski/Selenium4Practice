using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Selenium4Practice.Framework.Interfaces;
using Selenium4Practice.Framework.Extensions;

namespace Selenium4Practice.Pages.Infrastructure;

public abstract class BaseModal : IModal
{
    #region Properties

    public IWebDriver WebDriver { get; set; }
    public WebDriverWait WebDriverWait { get; set; }
    public abstract By Trait { get; set; }

    #endregion

    #region Methods

    public virtual bool IsModalOpen() => WebDriver.IsElementDisplayed(Trait);

    public virtual bool IsModalClosed() => !WebDriver.IsElementDisplayed(Trait);

    public virtual void WaitForModalToBeOpen() => WebDriverWait.Until(ExpectedConditions.ElementIsVisible(Trait));

    public virtual void WaitForModalToBeClosed() => WebDriverWait.WaitForElementNotToBePresent(Trait);

    #endregion
}