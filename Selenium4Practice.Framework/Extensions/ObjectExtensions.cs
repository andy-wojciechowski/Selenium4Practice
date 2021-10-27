using System;
using System.Linq;

namespace Selenium4Practice.Framework.Extensions
{
    public static class ObjectExtensions
    {
        public static TAttribute GetFirstAttributeOfType<TAttribute>(this object source) where TAttribute : Attribute
        {
            var attributes = source.GetType().GetCustomAttributes(typeof(TAttribute), true);
            return attributes.Any() ? (TAttribute)attributes.First() : default(TAttribute);
        }
    }
}