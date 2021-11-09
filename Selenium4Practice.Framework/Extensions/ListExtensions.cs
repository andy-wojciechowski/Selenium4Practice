using System.Collections.Generic;
using System;

namespace Selenium4Practice.Framework.Extensions
{
    internal static class ListExtensions
    {
        public static void ForEach<T>(this IList<T> source, Action<T> action)
        {
            foreach (var element in source)
                action(element);
        }
    }
}