using OpenQA.Selenium;
using Selenium4Practice.Framework.Extensions;
using Selenium4Practice.Framework.SeleniumObjects.Interfaces;

namespace Selenium4Practice.Pages.Modals;

public class PlaceOrderModal : BaseDemoblazeModal
{
    #region Properties

    public override By Trait => By.Id("orderModal");
    private By NameTextboxLocator => By.Id("name");
    private By CountryTextboxLocator => By.Id("country");
    private By CityTextboxLocator => By.Id("city");
    private By CreditCardTextboxLocator => By.Id("card");
    private By MonthTextboxLocator => By.Id("month");
    private By YearTextboxLocator => By.Id("year");
    private By PurchaseButtonLocator => By.ClassName("btn-primary");

    public IWebElement NameTextbox => ModalElement.FindElement(NameTextboxLocator);
    public IWebElement CountryTextbox => ModalElement?.FindElement(CountryTextboxLocator);
    public IWebElement CityTextbox => ModalElement.FindElement(CityTextboxLocator);
    public IWebElement CreditCardTextbox => ModalElement.FindElement(CreditCardTextboxLocator);
    public IWebElement MonthTextbox => ModalElement.FindElement(MonthTextboxLocator);
    public IWebElement YearTextbox => ModalElement.FindElement(YearTextboxLocator);
    public IWebElement PurchaseButton => ModalFooter.FindElement(PurchaseButtonLocator);

    #endregion

    #region Public Methods

    public PlaceOrderModal(IWebDriver webDriver, ISeleniumObjectInitializer seleniumObjectInitializer) : base(webDriver, seleniumObjectInitializer) { }

    public PlaceOrderModal WithName(string name) => this.EnterTextInInput(p => p.NameTextbox, name);

    public PlaceOrderModal WithCountry(string country) => this.EnterTextInInput(p => p.CountryTextbox, country);
    
    public PlaceOrderModal WithCity(string city) => this.EnterTextInInput(p => p.CityTextbox, city);

    public PlaceOrderModal WithCreditCard(string creditCard) => this.EnterTextInInput(p => p.CreditCardTextbox, creditCard);

    public PlaceOrderModal WithMonth(string month) => this.EnterTextInInput(p => p.MonthTextbox, month);

    public PlaceOrderModal WithYear(string year) => this.EnterTextInInput(p => p.YearTextbox, year);

    public string PlaceOrder(PlaceOrderConfirmationModal placeOrderConfirmationModal, bool alertShouldShow = false)
    {
        PurchaseButton.Click();
        if (alertShouldShow)
        {
            var alert = WebDriver.SwitchTo().Alert();
            var alertText = alert.Text;
            alert.Accept();
            WaitForModalToBeClosed();
            return alertText;
        }
        placeOrderConfirmationModal.WaitForModalToBeOpen();
        return null;
    }

    #endregion
}
