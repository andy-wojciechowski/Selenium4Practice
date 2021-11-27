using OpenQA.Selenium;

namespace Selenium4Practice.Pages.Modals
{
    public class AboutUsModal : BaseDemoblazeModal
    {
        public override By Trait => By.Id("videoModal");
        private By VideoLocator => By.Id("example-video");

        public IWebElement Video => ModalElement.FindElement(VideoLocator);
    }
}
