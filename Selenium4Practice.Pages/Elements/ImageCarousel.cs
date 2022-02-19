using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium4Practice.Pages.Elements;

public class ImageCarousel : IWrapsElement
{
    #region Properties

    private By CarouselImageLocator => By.ClassName("carousel-inner");
    private By NextImageButtonLocator => By.ClassName("carousel-control-next");
    private By PreviousImageButtonLocator => By.ClassName("carousel-control-prev");

    public IWebElement WrappedElement { get; }
    public IWebElement CarouselImage => WrappedElement.FindElement(CarouselImageLocator);
    public IWebElement NextImageButton => WrappedElement.FindElement(NextImageButtonLocator);
    public IWebElement PreviousImageButton => WrappedElement?.FindElement(PreviousImageButtonLocator);

    #endregion

    #region Public Methods

    public ImageCarousel(IWebElement wrappedElement)
    {
        if (wrappedElement.TagName != "div") throw new UnexpectedTagNameException("nav", wrappedElement.TagName);
        WrappedElement = wrappedElement;
    }

    public void NavigateToNextImage() => NextImageButton.Click();

    public void NavigateToPreviousImage() => PreviousImageButton.Click();

    #endregion
}
