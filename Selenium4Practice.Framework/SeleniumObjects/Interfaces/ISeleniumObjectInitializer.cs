using Selenium4Practice.Framework.Interfaces;

namespace Selenium4Practice.Framework.SeleniumObjects.Interfaces;

public interface ISeleniumObjectInitializer
{
    void InitializeSeleniumObject(ISeleniumObject seleniumObject);
}
