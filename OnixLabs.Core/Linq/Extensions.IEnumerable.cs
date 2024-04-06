// Copyright © 2020 ONIXLabs
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
/// Provides LINQ-like extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
// ReSharper disable InconsistentNaming
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IEnumerableExtensions
{
    /// <summary>
    /// Determines whether all elements of the current <see cref="IEnumerable{T}"/> are equal by a specified property.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="selector">The selector function which will be used to select each property from each element.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <typeparam name="TProperty">The underlying type of each selected <see cref="IEnumerable{T}"/> element.</typeparam>
    /// <returns>Returns <see langword="true"/> if all selected element properties are equal; otherwise <see langword="false"/>.</returns>
    public static bool AllEqualBy<T, TProperty>(this IEnumerable<T> enumerable, Func<T, TProperty> selector)
    {
        ISet<TProperty> elements = enumerable.Select(selector).ToHashSet();
        return elements.IsNotEmpty() && elements.IsSingle();
    }

    /// <summary>
    /// Determines whether any elements of the current <see cref="IEnumerable{T}"/> are equal by a specified property.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="selector">The selector function which will be used to select each property from each element.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <typeparam name="TProperty">The underlying type of each selected <see cref="IEnumerable{T}"/> element.</typeparam>
    /// <returns>Returns <see langword="true"/> if any selected element properties are equal; otherwise <see langword="false"/>.</returns>
    public static bool AnyEqualBy<T, TProperty>(this IEnumerable<T> enumerable, Func<T, TProperty> selector)
    {
        IList<TProperty> elements = enumerable.Select(selector).ToList();
        ISet<TProperty> distinctElements = elements.ToHashSet();
        return distinctElements.IsNotEmpty() && distinctElements.Count < elements.Count;
    }

    /// <summary>
    /// Obtains a number that represents how many elements in the the current <see cref="IEnumerable{T}"/> do not satisfy the specified predicate condition.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a number that represents how many elements in the the current <see cref="IEnumerable{T}"/> do not satisfy the specified predicate condition.</returns>
    public static int CountNot<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        return enumerable.Count(element => !predicate(element));
    }

    /// <summary>
    /// Performs the specified <see cref="Action{T}"/> for each element of the current <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> over which to iterate.</param>
    /// <param name="action">The <see cref="Action{T}"/> to perform for each <see cref="IEnumerable{T}"/> element.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (T element in enumerable) action(element);
    }

    /// <summary>
    /// Gets the content hash code of the elements of the current <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> from which to compute a content hash code.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns the computed content hash code of the current <see cref="IEnumerable{T}"/>.</returns>
    public static int GetContentHashCode<T>(this IEnumerable<T> enumerable)
    {
        HashCode hashCode = new();
        enumerable.ForEach(item => hashCode.Add(item));
        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable{T}"/> is empty.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">he underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> is empty; otherwise, <see langword="false"/>.</returns>
    public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
    {
        return !enumerable.Any();
    }

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable{T}"/> is not empty.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> is not empty; otherwise, <see langword="false"/>.</returns>
    public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable)
    {
        return !enumerable.IsEmpty();
    }

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable{T}"/> contains a single element.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> contains a single element; otherwise, <see langword="false"/>.</returns>
    public static bool IsSingle<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.LongCount() == 1;
    }

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable{T}"/> contains an even number of elements.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> contains an even number of elements; otherwise, <see langword="false"/>.</returns>
    public static bool IsCountEven<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.LongCount() % 2 == 0;
    }

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable{T}"/> contains an odd number of elements.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> contains an odd number of elements; otherwise, <see langword="false"/>.</returns>
    public static bool IsCountOdd<T>(this IEnumerable<T> enumerable)
    {
        return !enumerable.IsCountEven();
    }

    /// <summary>
    /// Joins the elements of the current <see cref="IEnumerable{T}"/> into a <see cref="string"/>.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> to join.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns the elements of the current <see cref="IEnumerable{T}"/>, joined into a string.</returns>
    public static string JoinToString<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.JoinToString(", ");
    }

    /// <summary>
    /// Joins the elements of the current <see cref="IEnumerable{T}"/> into a <see cref="string"/>.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> to join.</param>
    /// <param name="separator">The separator which will appear between joined elements.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns the elements of the current <see cref="IEnumerable{T}"/>, joined into a string.</returns>
    public static string JoinToString<T>(this IEnumerable<T> enumerable, string separator)
    {
        return string.Join(separator, enumerable);
    }

    /// <summary>
    /// Determines whether none of the elements of the current <see cref="IEnumerable{T}"/> satisfy the specified predicate condition.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if none of the elements of the current <see cref="IEnumerable{T}"/> satisfy the specified predicate condition; otherwise, <see langword="false"/>.</returns>
    public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        return !enumerable.Any(predicate);
    }

    /// <summary>
    /// Calculates the sum of the elements of the current <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> to sum.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns the sum of the elements of the current <see cref="IEnumerable{T}"/>.</returns>
    public static T Sum<T>(this IEnumerable<T> enumerable) where T : INumber<T>
    {
        IEnumerable<T> elements = enumerable.ToArray();
        return elements.IsEmpty() ? T.Zero : elements.Aggregate((left, right) => left + right);
    }

    /// <summary>
    /// Calculates the sum of the elements of the current <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> to sum.</param>
    /// <param name="selector">The selector function which will be used to select each <see cref="INumber{TSelf}"/> property from each element.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <typeparam name="TResult">The underlying <see cref="INumber{TSelf}"/> type of each element to sum.</typeparam>
    /// <returns>Returns the sum of the elements of the current <see cref="IEnumerable{T}"/>.</returns>
    public static TResult SumBy<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector) where TResult : INumber<TResult>
    {
        return enumerable.Select(selector).Sum();
    }

    /// <summary>
    /// Filters the current <see cref="IEnumerable{Object}"/> to only elements of the specified type.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="IEnumerable{T}"/> containing only elements of the specified type.</returns>
    public static IEnumerable<T> WhereInstanceOf<T>(this IEnumerable<object?> enumerable)
    {
        foreach (object? element in enumerable)
        {
            if (element is T elementOfType) yield return elementOfType;
        }
    }

    /// <summary>
    /// Filters the current <see cref="IEnumerable{T}"/> elements that do not satisfy the specified predicate condition.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="IEnumerable{T}"/> that contains elements that do not satisfy the condition.</returns>
    public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        return enumerable.Where(element => !predicate(element));
    }

    /// <summary>
    /// Filters the current <see cref="IEnumerable{T}"/> elements that are not null.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="IEnumerable{T}"/> that contains elements that are not null.</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> enumerable)
    {
        return enumerable.Where(element => element is not null)!;
    }

    /// <summary>
    /// Formats the current <see cref="IEnumerable{T}"/> as a collection string.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> to format.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a <see cref="string"/> collection representation of the current <see cref="IEnumerable{T}"/>.</returns>
    public static string ToCollectionString<T>(this IEnumerable<T> enumerable)
    {
        return string.Join(", ", enumerable).Wrap('[', ']');
    }

    /// <summary>
    /// Formats each <see cref="IFormattable"/> element of the current current <see cref="IEnumerable{T}"/> as a collection string.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> to format.</param>
    /// <param name="format">The format which will be applied to each element.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a <see cref="string"/> collection representation of the current <see cref="IEnumerable{T}"/>.</returns>
    public static string ToCollectionString<T>(this IEnumerable<T> enumerable, string format, IFormatProvider? formatProvider = null) where T : IFormattable
    {
        return enumerable.Select(element => element.ToString(format, formatProvider)).ToCollectionString();
    }
}
