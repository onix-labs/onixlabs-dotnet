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
    /// <summary>
    /// Determines whether all elements of the current <see cref="System.Collections.Generic.IEnumerable{T}"/> are equal by a specified property.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="selector">The selector function which will be used to select the desired property from each element.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <typeparam name="TProperty">The underlying type of each selected <see cref="System.Collections.Generic.IEnumerable{T}"/> element.</typeparam>
    /// <returns>Returns <see langword="true"/> if all selected element properties are equal; otherwise <see langword="false"/>.</returns>
    public static bool AllEqualBy<T, TProperty>(this IEnumerable<T> enumerable, Func<T, TProperty> selector)
    {
        return enumerable.Select(selector).Distinct().IsSingle();
    }

    /// <summary>
    /// Determines whether any elements of the current <see cref="System.Collections.Generic.IEnumerable{T}"/> are equal by a specified property.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="selector">The selector function which will be used to select the desired property from each element.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <typeparam name="TProperty">The underlying type of each selected <see cref="System.Collections.Generic.IEnumerable{T}"/> element.</typeparam>
    /// <returns>Returns <see langword="true"/> if any selected element properties are equal; otherwise <see langword="false"/>.</returns>
    public static bool AnyEqualBy<T, TProperty>(this IEnumerable<T> enumerable, Func<T, TProperty> selector)
    {
        return enumerable.GroupBy(selector).Any(group => group.Count() > 1);
    }

    /// <summary>
    /// Obtains a number that represents how many elements in the current <see cref="System.Collections.Generic.IEnumerable{T}"/> do not satisfy the specified predicate condition.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a number that represents how many elements in the current <see cref="System.Collections.Generic.IEnumerable{T}"/> do not satisfy the specified predicate condition.</returns>
    public static int CountNot<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        return enumerable.Count(element => !predicate(element));
    }

    /// <summary>
    /// Performs the specified <see cref="System.Action{T}"/> for each element of the current <see cref="System.Collections.Generic.IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> over which to iterate.</param>
    /// <param name="action">The <see cref="System.Action{T}"/> to perform for each <see cref="System.Collections.Generic.IEnumerable{T}"/> element.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (T element in enumerable) action(element);
    }

    /// <summary>
    /// Gets the content hash code of the elements of the current <see cref="System.Collections.Generic.IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> from which to compute a content hash code.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns the computed content hash code of the current <see cref="System.Collections.Generic.IEnumerable{T}"/>.</returns>
    public static int GetContentHashCode<T>(this IEnumerable<T> enumerable)
    {
        HashCode result = new();
        enumerable.ForEach(element => result.Add(element));
        return result.ToHashCode();
    }

    /// <summary>
    /// Determines whether the current <see cref="System.Collections.Generic.IEnumerable{T}"/> is empty.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="System.Collections.Generic.IEnumerable{T}"/> is empty; otherwise, <see langword="false"/>.</returns>
    public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
    {
        return !enumerable.Any();
    }

    /// <summary>
    /// Determines whether the current <see cref="System.Collections.Generic.IEnumerable{T}"/> is not empty.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="System.Collections.Generic.IEnumerable{T}"/> is not empty; otherwise, <see langword="false"/>.</returns>
    public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.Any();
    }

    /// <summary>
    /// Determines whether the current <see cref="System.Collections.Generic.IEnumerable{T}"/> contains a single element.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="System.Collections.Generic.IEnumerable{T}"/> contains a single element; otherwise, <see langword="false"/>.</returns>
    public static bool IsSingle<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.LongCount() == 1;
    }

    /// <summary>
    /// Determines whether the current <see cref="System.Collections.Generic.IEnumerable{T}"/> contains an even number of elements.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="System.Collections.Generic.IEnumerable{T}"/> contains an even number of elements; otherwise, <see langword="false"/>.</returns>
    public static bool IsCountEven<T>(this IEnumerable<T> enumerable, Func<T, bool>? predicate = null)
    {
        return enumerable.LongCount(element => predicate?.Invoke(element) ?? true) % 2 == 0;
    }

    /// <summary>
    /// Determines whether the current <see cref="System.Collections.Generic.IEnumerable{T}"/> contains an odd number of elements.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="System.Collections.Generic.IEnumerable{T}"/> contains an odd number of elements; otherwise, <see langword="false"/>.</returns>
    public static bool IsCountOdd<T>(this IEnumerable<T> enumerable, Func<T, bool>? predicate = null)
    {
        return enumerable.LongCount(element => predicate?.Invoke(element) ?? true) % 2 != 0;
    }

    /// <summary>
    /// Joins the elements of the current <see cref="System.Collections.Generic.IEnumerable{T}"/> into a <see cref="System.String"/> instance.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> to join.</param>
    /// <param name="separator">The separator which will appear between joined elements.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns the elements of the current <see cref="System.Collections.Generic.IEnumerable{T}"/>, joined into a <see cref="System.String"/> instance.</returns>
    public static string JoinToString<T>(this IEnumerable<T> enumerable, string separator = ", ")
    {
        return string.Join(separator, enumerable);
    }

    /// <summary>
    /// Determines whether none of the elements of the current <see cref="System.Collections.Generic.IEnumerable{T}"/> satisfy the specified predicate condition.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if none of the elements of the current <see cref="System.Collections.Generic.IEnumerable{T}"/> satisfy the specified predicate condition; otherwise, <see langword="false"/>.</returns>
    public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        return !enumerable.Any(predicate);
    }

    /// <summary>
    /// Calculates the sum of the elements of the current <see cref="System.Collections.Generic.IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> to sum.</param>
    /// <typeparam name="T">The underlying <see cref="System.Numerics.INumberBase{T}"/> type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns the sum of the elements of the current <see cref="System.Collections.Generic.IEnumerable{T}"/>.</returns>
    public static T Sum<T>(this IEnumerable<T> enumerable) where T : INumberBase<T>
    {
        IEnumerable<T> numbers = enumerable as T[] ?? enumerable.ToArray();
        return numbers.IsEmpty() ? T.Zero : numbers.Aggregate((left, right) => left + right);
    }

    /// <summary>
    /// Calculates the sum of the elements of the current <see cref="System.Collections.Generic.IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> to sum.</param>
    /// <param name="selector">The selector function which will be used to select each <see cref="System.Numerics.INumberBase{T}"/> property from each element.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <typeparam name="TResult">The underlying <see cref="System.Numerics.INumberBase{T}"/> type of each element to sum.</typeparam>
    /// <returns>Returns the sum of the elements of the current <see cref="System.Collections.Generic.IEnumerable{T}"/>.</returns>
    public static TResult SumBy<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector) where TResult : INumberBase<TResult>
    {
        return enumerable.Select(selector).Sum();
    }

    /// <summary>
    /// Filters the current <see cref="System.Collections.Generic.IEnumerable{T}"/> elements that do not satisfy the specified predicate condition.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="System.Collections.Generic.IEnumerable{T}"/> that contains elements that do not satisfy the condition.</returns>
    public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        return enumerable.Where(element => !predicate(element));
    }

    /// <summary>
    /// Filters the current <see cref="System.Collections.Generic.IEnumerable{T}"/> elements that are not <see langword="null"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="System.Collections.Generic.IEnumerable{T}"/> that contains elements that are not <see langword="null"/>.</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> enumerable) where T : notnull
    {
        return enumerable.Where(element => element is not null)!;
    }

    /// <summary>
    /// Formats the current <see cref="System.Collections.Generic.IEnumerable{T}"/> as a collection string.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> to format.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="System.String"/> instance representing the current <see cref="System.Collections.Generic.IEnumerable{T}"/>.</returns>
    public static string ToCollectionString<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.JoinToString().Wrap('[', ']');
    }

    /// <summary>
    /// Formats the current <see cref="System.Collections.Generic.IEnumerable{T}"/> as a collection string.
    /// </summary>
    /// <param name="enumerable">The current <see cref="System.Collections.Generic.IEnumerable{T}"/> to format.</param>
    /// <param name="format">The format which will be applied to each element.</param>
    /// <param name="provider">An object that provides culture-specific formatting information.</param>
    /// <typeparam name="T">The underlying type of the <see cref="System.Collections.Generic.IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="System.String"/> instance representing the current <see cref="System.Collections.Generic.IEnumerable{T}"/>.</returns>
    public static string ToCollectionString<T>(this IEnumerable<T> enumerable, string format, IFormatProvider? provider = null) where T : IFormattable
    {
        return enumerable.Select(element => element.ToString(format, provider)).ToCollectionString();
    }
}
