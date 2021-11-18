using NUnit.Framework;

namespace Selenium4Practice.Framework.TestAttachments.Interfaces;

public interface ITestAttachmentHandler
{
    void Execute(TestContext context);
}