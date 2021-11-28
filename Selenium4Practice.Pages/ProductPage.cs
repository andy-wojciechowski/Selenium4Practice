using Selenium4Practice.Framework.Attributes;
using Selenium4Practice.Pages.Infrastructure;
using System;

namespace Selenium4Practice.Pages;

[PageUrl("/prod.html?idp_={0}")]
public class ProductPage : BasePage
{
    public override void WaitForPage()
    {
        throw new NotImplementedException();
    }
}
