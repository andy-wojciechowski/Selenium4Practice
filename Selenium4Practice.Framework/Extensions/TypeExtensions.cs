using System;
using System.Linq;

namespace Selenium4Practice.Framework.Extensions;

public static class TypeExtensions
{
    public static TAttribute GetFirstAttributeOfType<TAttribute>(this Type type) where TAttribute : Attribute
    {
        var attributes = type.GetCustomAttributes(typeof(TAttribute), true);
        return attributes.Any() ? (TAttribute)attributes.First() : null;
    }
}