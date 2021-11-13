using OpenQA.Selenium;

namespace Selenium4Practice.Framework.Interfaces;

public interface IModal : ISeleniumObject
{
    By Trait { get; }
}