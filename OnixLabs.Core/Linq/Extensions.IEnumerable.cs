// Copyright 2020 ONIXLabs
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;

namespace OnixLabs.Core.Linq;

/// <summary>
/// Provides LINQ-like extension methods for <see cref="System.Collections.Generic.IEnumerable{T}"/>.
/// </summary>
// ReSharper disable InconsistentNaming
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IEnumerableExtensions
{
    public static bool AllEqualBy<T, TProperty>(this IEnumerable<T> enumerable, Func<T, TProperty> selector)
    {
        return enumerable.Select(selector).Distinct().IsSingle();
    }

    public static bool AnyEqualBy<T, TProperty>(this IEnumerable<T> enumerable, Func<T, TProperty> selector)
    {
        return enumerable.GroupBy(selector).Any(group => group.Count() > 1);
    }

    public static int CountNot<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        return enumerable.Count(element => !predicate(element));
    }

    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (T element in enumerable) action(element);
    }

    public static int GetContentHashCode<T>(this IEnumerable<T> enumerable)
    {
        HashCode result = new();
        enumerable.ForEach(element => result.Add(element));
        return result.ToHashCode();
    }

    public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
    {
        return !enumerable.Any();
    }

    public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.Any();
    }

    public static bool IsSingle<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.LongCount() == 1;
    }

    public static bool IsCountEven<T>(this IEnumerable<T> enumerable, Func<T, bool>? predicate = null)
    {
        return enumerable.LongCount(element => predicate?.Invoke(element) ?? true) % 2 == 0;
    }

    public static bool IsCountOdd<T>(this IEnumerable<T> enumerable, Func<T, bool>? predicate = null)
    {
        return enumerable.LongCount(element => predicate?.Invoke(element) ?? true) % 2 != 0;
    }

    public static string JoinToString<T>(this IEnumerable<T> enumerable, string separator = ", ")
    {
        return string.Join(separator, enumerable);
    }

    public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        return !enumerable.Any(predicate);
    }

    public static T Sum<T>(this IEnumerable<T> enumerable) where T : INumberBase<T>
    {
        IEnumerable<T> numbers = enumerable as T[] ?? enumerable.ToArray();
        return numbers.IsEmpty() ? T.Zero : numbers.Aggregate((left, right) => left + right);
    }

    public static TResult SumBy<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector) where TResult : INumberBase<TResult>
    {
        return enumerable.Select(selector).Sum();
    }

    public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        return enumerable.Where(element => !predicate(element));
    }

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> enumerable) where T : notnull
    {
        return enumerable.Where(element => element is not null)!;
    }

    public static string ToCollectionString<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.JoinToString().Wrap('[', ']');
    }

    public static string ToCollectionString<T>(this IEnumerable<T> enumerable, string format, IFormatProvider? provider = null) where T : IFormattable
    {
        return enumerable.Select(element => element.ToString(format, provider)).ToCollectionString();
    }
}
