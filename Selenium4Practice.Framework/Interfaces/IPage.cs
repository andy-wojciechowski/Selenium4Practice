namespace Selenium4Practice.Framework.Interfaces
{
    public interface IPage : ISeleniumObject
    {
        string BaseUrl { get; set; }
        string PageUrl { get; set; }
        bool IsAt();
        void Navigate();
        void WaitForPage();
    }
}