using OpenQA.Selenium;
using Selenium4Practice.Framework.Extensions;
using Selenium4Practice.Pages.Infrastructure;
using SeleniumExtras.WaitHelpers;

namespace Selenium4Practice.Pages.Modals;

public abstract class BaseDemoblazeModal : BaseModal
{
    #region Properties

    private By TitleLocator => By.ClassName("modal-title");
    private By UpperRightCloseButtonLocator => By.ClassName("close");
    private By ModalFooterLocator => By.ClassName("modal-footer");
    private By FooterCloseButtonLocator => By.ClassName("btn-secondary");

    public IWebElement ModalElement => WebDriver.FindElement(Trait);
    public IWebElement Title => ModalElement.FindElement(TitleLocator);
    public IWebElement UpperRightCloseButton => ModalElement.FindElement(UpperRightCloseButtonLocator);
    public IWebElement ModalFooter => ModalElement.FindElement(ModalFooterLocator);
    public IWebElement FooterCloseButton => ModalFooter.FindElement(FooterCloseButtonLocator);

    #endregion

    #region Public Methods

    public override void WaitForModalToBeClosed() => WebDriverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(Trait));
    public void ClickUpperRightCloseButton() => UpperRightCloseButton.Click(WaitForModalToBeClosed);
    public void ClickFooterCloseButton() => FooterCloseButton.Click(WaitForModalToBeClosed);

    #endregion
}
