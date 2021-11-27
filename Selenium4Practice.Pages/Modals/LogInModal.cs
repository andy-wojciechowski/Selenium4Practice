﻿using OpenQA.Selenium;
using Selenium4Practice.Framework.Extensions;

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

    public LogInModal WithUserName(string userName)
    {
        UserNameTextbox.EnterTextInInput(userName);
        return this;
    }

    public LogInModal WithPassword(string password)
    {
        PasswordTextbox.EnterTextInInput(password);
        return this;
    }

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