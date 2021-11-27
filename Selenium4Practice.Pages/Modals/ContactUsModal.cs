using OpenQA.Selenium;
using Selenium4Practice.Framework.Extensions;

namespace Selenium4Practice.Pages.Modals
{
    public class ContactUsModal : BaseDemoblazeModal
    {
        #region Properties

        public override By Trait => By.Id("exampleModal");
        private By ContactEmailTextboxLocator => By.Id("recipient-email");
        private By ContactNameTextboxLocator => By.Id("recipient-name");
        private By MessageTextLocator => By.Id("message-text");
        private By SendMessageButtonLocator => By.ClassName("btn-primary");

        public IWebElement ContactEmailTextbox => ModalElement.FindElement(ContactEmailTextboxLocator);
        public IWebElement ContactNameTextoox => ModalElement.FindElement(ContactNameTextboxLocator);
        public IWebElement MessageText => ModalElement.FindElement(MessageTextLocator);
        public IWebElement SendMessageButton => ModalFooter.FindElement(SendMessageButtonLocator);

        #endregion

        #region Public Methods

        public ContactUsModal WithContactEmail(string contactEmail)
        {
            ContactEmailTextbox.EnterTextInInput(contactEmail);
            return this;
        }

        public ContactUsModal WithContactName(string contactName)
        {
            ContactNameTextoox.EnterTextInInput(contactName);
            return this;
        }

        public ContactUsModal WithMessage(string message)
        {
            MessageText.EnterTextInInput(message);
            return this;
        }

        public string SendMessage()
        {
            SendMessageButton.Click();
            var alert = WebDriver.SwitchTo().Alert();
            var alertText = alert.Text;
            alert.Accept();
            WaitForModalToBeClosed();
            return alertText;
        }

        #endregion
    }
}
