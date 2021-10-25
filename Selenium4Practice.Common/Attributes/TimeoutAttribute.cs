using System;

namespace Selenium4Practice.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TimeoutAttribute : Attribute
    {
        public int Timeout { get; }

        public TimeoutAttribute(int timeout)
        {
            Timeout = timeout;
        }
    }
}