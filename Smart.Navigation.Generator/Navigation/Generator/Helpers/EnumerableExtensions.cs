namespace Smart.Navigation.Generator.Helpers;

using System;
using System.Collections.Generic;

internal static class EnumerableExtensions
{
    public static IEnumerable<TResult> SelectPart<T, TResult>(this IEnumerable<T> array, Func<T, TResult?> selector) =>
        array.Select(selector).OfType<TResult>();
}
