// Copyright 2020-2022 ONIXLabs
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

namespace OnixLabs.Core.Linq;

/// <summary>
/// Provides LINQ-like extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
// ReSharper disable InconsistentNaming
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IEnumerableExtensions
{
    /// <summary>
    /// Determines whether all elements of this <see cref="IEnumerable{T}"/> are equal by a specified property.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="selector">The selector function which will be used to select each property from each element.</param>
    /// <typeparam name="TElement">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <typeparam name="TProperty">The underlying type of each selected <see cref="IEnumerable{T}"/> element.</typeparam>
    /// <returns>Returns true if all selected element properties are equal; otherwise false.</returns>
    public static bool AllEqualBy<TElement, TProperty>(this IEnumerable<TElement> enumerable, Func<TElement, TProperty> selector)
    {
        IEnumerable<TElement> elements = enumerable.ToArray();

        if (elements.IsEmpty())
        {
            return false;
        }

        if (elements.IsSingle())
        {
            return true;
        }

        TProperty first = selector(elements.First());
        return elements.All(element => Equals(first, selector(element)));
    }

    /// <summary>
    /// Determines whether any elements of this <see cref="IEnumerable{T}"/> are equal by a specified property.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="selector">The selector function which will be used to select each property from each element.</param>
    /// <typeparam name="TElement">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <typeparam name="TProperty">The underlying type of each selected <see cref="IEnumerable{T}"/> element.</typeparam>
    /// <returns>Returns true if any selected element properties are equal; otherwise false.</returns>
    public static bool AnyEqualBy<TElement, TProperty>(this IEnumerable<TElement> enumerable, Func<TElement, TProperty> selector)
    {
        IEnumerable<TElement> elements = enumerable.ToArray();

        if (elements.IsEmpty())
        {
            return false;
        }

        if (elements.IsSingle())
        {
            return true;
        }

        TProperty first = selector(elements.First());
        return elements.Any(element => Equals(first, selector(element)));
    }

    /// <summary>
    /// Performs the specified <see cref="Action{T}"/> for each element of this <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> over which to iterate.</param>
    /// <param name="action">The <see cref="Action{T}"/> to perform for each <see cref="IEnumerable{T}"/> element.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (T element in enumerable)
        {
            action(element);
        }
    }

    /// <summary>
    /// Gets the content hash code of the elements of this <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> from which to compute a content hash code.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns the  computed content hash code of this <see cref="IEnumerable{T}"/>.</returns>
    public static int GetContentHashCode<T>(this IEnumerable<T> enumerable)
    {
        return new HashCode().AddItems(enumerable).ToHashCode();
    }

    /// <summary>
    /// Determines whether this <see cref="IEnumerable{T}"/> is empty.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">he underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns true if the <see cref="IEnumerable{T}"/> is empty; otherwise, false.</returns>
    public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
    {
        return !enumerable.Any();
    }

    /// <summary>
    /// Determines whether this <see cref="IEnumerable{T}"/> is not empty.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns true if the <see cref="IEnumerable{T}"/> is not empty; otherwise, false.</returns>
    public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable)
    {
        return !enumerable.IsEmpty();
    }

    /// <summary>
    /// Determines whether this <see cref="IEnumerable{T}"/> contains a single element.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns true if the <see cref="IEnumerable{T}"/> contains a single element; otherwise, false.</returns>
    public static bool IsSingle<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.LongCount() == 1;
    }

    /// <summary>
    /// Determines whether this <see cref="IEnumerable{T}"/> contains an even number of elements.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns true if the <see cref="IEnumerable{T}"/> contains an even number of elements; otherwise, false.</returns>
    public static bool IsCountEven<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.LongCount() % 2 == 0;
    }

    /// <summary>
    /// Determines whether this <see cref="IEnumerable{T}"/> contains an odd number of elements.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns true if the <see cref="IEnumerable{T}"/> contains an odd number of elements; otherwise, false.</returns>
    public static bool IsCountOdd<T>(this IEnumerable<T> enumerable)
    {
        return !enumerable.IsCountEven();
    }

    /// <summary>
    /// Filters this <see cref="IEnumerable{Object}"/> to only elements of the specified type.
    /// </summary>
    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="IEnumerable{T}"/> containing only elements of the specified type.</returns>
    public static IEnumerable<T> WhereIs<T>(this IEnumerable<object?> enumerable)
    {
        foreach (object? element in enumerable)
        {
            if (element is T elementOfType)
            {
                yield return elementOfType;
            }
        }
    }
}
