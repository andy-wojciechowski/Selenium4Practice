using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.UI;
using Selenium4Practice.Framework.Attributes;
using Selenium4Practice.Framework.Extensions;
using Selenium4Practice.Framework.Interfaces;
using Selenium4Practice.Framework.SeleniumObjects.Interfaces;
using System;

namespace Selenium4Practice.Framework.SeleniumObjects;

public class SeleniumObjectInitializer : ISeleniumObjectInitializer
{
    private const int DefaultTimeout = 30;

    private readonly IConfiguration _config;

    public SeleniumObjectInitializer(IConfiguration config)
    {
        _config = config;
    }

    public void InitializeSeleniumObject(ISeleniumObject seleniumObject)
    {
        var seleniumObjectType = seleniumObject.GetType();
        var timeoutAttribute = seleniumObjectType.GetFirstAttributeOfType<TimeoutAttribute>();
        var timeout = timeoutAttribute != null ? timeoutAttribute.Timeout : DefaultTimeout;
        seleniumObject.WebDriverWait = new WebDriverWait(seleniumObject.WebDriver, TimeSpan.FromSeconds(timeout));
        if (typeof(IPage).IsAssignableFrom(seleniumObjectType))
        {
            var pageUrlAttribute = seleniumObjectType.GetFirstAttributeOfType<PageUrlAttribute>();
            if (pageUrlAttribute == null) throw new Exception("A page url attribute is required for page objects");
            ((IPage)seleniumObject).BaseUrl = _config["BaseUrl"];
            ((IPage)seleniumObject).PageUrl = pageUrlAttribute.PageUrl;
        }
    }
}
