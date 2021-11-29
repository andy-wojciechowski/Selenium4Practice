using OpenQA.Selenium;

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
}
