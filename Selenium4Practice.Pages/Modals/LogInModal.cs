using OpenQA.Selenium;
using Selenium4Practice.Framework.Extensions;
using Selenium4Practice.Framework.SeleniumObjects.Interfaces;

namespace Selenium4Practice.Pages.Modals;

public class LogInModal : BaseDemoblazeModal
{
    #region Properties

    public override By Trait => By.Id("logInModal");
    private By UserNameTextboxLocator => By.Id("loginusername");
    private By PasswordTextboxLocator => By.Id("loginpassword");
    private By LoginButtonLocator => By.ClassName("btn-primary");

    public IWebElement UserNameTextbox => ModalElement.FindElement(UserNameTextboxLocator);
    public IWebElement PasswordTextbox => ModalElement.FindElement(PasswordTextboxLocator);
    public IWebElement LoginButton => ModalFooter.FindElement(LoginButtonLocator);

    #endregion

    #region Public Methods

    public LogInModal(IWebDriver webDriver, ISeleniumObjectInitializer seleniumObjectInitializer) : base(webDriver, seleniumObjectInitializer) { }

    public LogInModal WithUserName(string userName) => this.EnterTextInInput(p => p.UserNameTextbox, userName);

    public LogInModal WithPassword(string password) => this.EnterTextInInput(p => p.PasswordTextbox, password);

    public string LogIn(bool alertPresentAfterLogin = false)
    {
        LoginButton.Click();
        if (alertPresentAfterLogin)
        {
            var alert = WebDriver.SwitchTo().Alert();
            var alertText = alert.Text;
            alert.Accept();
            WaitForModalToBeClosed();
            return alertText;
        }
        WaitForModalToBeClosed();
        return null;
    }

    #endregion
}