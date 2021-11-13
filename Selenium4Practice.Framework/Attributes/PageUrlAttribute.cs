using System;

namespace Selenium4Practice.Framework.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PageUrlAttribute : Attribute
{
    public string PageUrl { get; }

    public PageUrlAttribute(string pageUrl)
    {
        PageUrl = pageUrl;
    }
}