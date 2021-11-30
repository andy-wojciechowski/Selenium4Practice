using OpenQA.Selenium;
using Selenium4Practice.Framework.Extensions;

namespace Selenium4Practice.Pages.Modals;

public class SignUpModal : BaseDemoblazeModal
{
    #region Properties

    public override By Trait => By.Id("signInModal");
    private By UserNameTextboxLocator => By.Id("loginusername");
    private By PasswordTextboxLocator => By.Id("loginpassword");
    private By LoginButtonLocator => By.ClassName("btn-primary");

    public IWebElement UserNameTextbox => ModalElement.FindElement(UserNameTextboxLocator);
    public IWebElement PasswordTextbox => ModalElement.FindElement(PasswordTextboxLocator);
    public IWebElement LoginButton => ModalFooter.FindElement(LoginButtonLocator);

    #endregion

    #region Public Methods

    public SignUpModal WithUserName(string userName) => this.EnterTextInInput(p => p.UserNameTextbox, userName);

    public SignUpModal WithPassword(string password) => this.EnterTextInInput(p => p.PasswordTextbox, password);

    public string LogIn()
    {
        LoginButton.Click();
        var alert = WebDriver.SwitchTo().Alert();
        var alertText = alert.Text;
        alert.Accept();
        WaitForModalToBeClosed();
        return alertText;
    }

    #endregion
}