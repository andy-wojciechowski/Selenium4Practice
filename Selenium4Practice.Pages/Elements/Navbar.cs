using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium4Practice.Framework.Extensions;
using Selenium4Practice.Pages.Modals;

namespace Selenium4Practice.Pages.Elements;

public class Navbar : IWrapsElement
{
    #region Properties

    private By HomeImageLinkLocator => By.Id("nava");
    private By LogoLocator => By.TagName("img");
    private By HomeLinkLocator => By.XPath("//a[contains(text(), 'Home')]");
    private By ContactUsLinkLocator => By.XPath("//a[contains(text(), 'Contact')]");
    private By AboutUsLinkLocator => By.XPath("//a[contains(text(), 'About us')]");
    private By CartLinkLocator => By.XPath("//a[contains(text(), 'Cart')]");
    private By LogInLinkLocator => By.XPath("//a[contains(text(), 'Log in')]");
    private By SignUpLinkLocator => By.XPath("//a[contains(text(), 'Sign up')]");

    public IWebElement WrappedElement { get; }
    public IWebElement HomeImageLink => WrappedElement.FindElement(HomeImageLinkLocator);
    public IWebElement Logo => HomeImageLink.FindElement(LogoLocator);
    public IWebElement HomeLink => WrappedElement.FindElement(HomeLinkLocator);
    public IWebElement ContactUsLink => WrappedElement.FindElement(ContactUsLinkLocator);
    public IWebElement AboutUsLink => WrappedElement.FindElement(AboutUsLinkLocator);
    public IWebElement CartLink => WrappedElement.FindElement(CartLinkLocator);
    public IWebElement LogInLink => WrappedElement.FindElement(LogInLinkLocator);
    public IWebElement SignUpLink => WrappedElement.FindElement(SignUpLinkLocator);

    #endregion

    #region Public Methods

    public Navbar(IWebElement wrappedElement)
    {
        if (wrappedElement.TagName != "nav") throw new UnexpectedTagNameException("nav", wrappedElement.TagName);
        WrappedElement = wrappedElement;
    }

    public void ClickHomeImageLink(HomePage homePage) => HomeImageLink.Click(homePage.WaitForPage);

    public void ClickHomeLink(HomePage homePage) => HomeLink.Click(homePage.WaitForPage);

    public void ClickContactUsLink(ContactUsModal contactUsModal) => ContactUsLink.Click(contactUsModal.WaitForModalToBeOpen);

    public void ClickAboutUsLink(AboutUsModal aboutUsModal) => AboutUsLink.Click(aboutUsModal.WaitForModalToBeOpen);

    public void ClickCartLink(CartPage cartPage) => CartLink.Click(cartPage.WaitForPage);

    public void ClickLoginInLink(LogInModal logInModal) => LogInLink.Click(logInModal.WaitForModalToBeOpen);

    public void ClickSignUpLink(SignUpModal signUpModal) => SignUpLink.Click(signUpModal.WaitForModalToBeOpen);

    #endregion
}
