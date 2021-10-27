namespace Selenium4Practice.Framework.Interfaces
{
    public interface IPage : ISeleniumObject
    {
        bool IsAt();
        void Navigate();
        void WaitForPage();
    }
}