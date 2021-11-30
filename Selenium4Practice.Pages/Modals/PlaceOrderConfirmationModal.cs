using OpenQA.Selenium;
using Selenium4Practice.Framework.Extensions;
using Selenium4Practice.Pages.Infrastructure;

namespace Selenium4Practice.Pages.Modals;

public class PlaceOrderConfirmationModal : BaseModal
{
    #region Properties

    public override By Trait => By.ClassName("sweet-alert");
    private By SuccessIconLocator => By.ClassName("sa-success");
    private By TextLocator => By.ClassName("text-muted");
    private By ConfirmButtonLocator => By.ClassName("confirm");

    public IWebElement ModalElement => WebDriver.FindElement(Trait);
    public IWebElement SuccessIcon => ModalElement.FindElement(SuccessIconLocator);
    public IWebElement Text => ModalElement.FindElement(TextLocator);
    public IWebElement ConfirmButton => ModalElement.FindElement(ConfirmButtonLocator);

    #endregion

    #region Public Methods

    public void Confirm() => ConfirmButton.Click(WaitForModalToBeClosed);

    #endregion
}
