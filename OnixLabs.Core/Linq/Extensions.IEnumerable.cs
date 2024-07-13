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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using OnixLabs.Core.Text;

namespace OnixLabs.Core.Linq;

/// <summary>
/// Provides LINQ-like extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
// ReSharper disable InconsistentNaming
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IEnumerableExtensions
{
    private const string CollectionSeparator = ", ";
    private const char CollectionOpenBracket = '[';
    private const char CollectionCloseBracket = ']';

    private const string EnumerableNullExceptionMessage = "Enumerable must not be null.";
    private const string SelectorNullExceptionMessage = "Selector must not be null.";
    private const string PredicateNullExceptionMessage = "Predicate must not be null.";
    private const string ActionNullExceptionMessage = "Action must not be null.";

    /// <summary>
    /// Determines whether all elements of the current <see cref="IEnumerable{T}"/> are equal by a specified property.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="selector">The selector function which will be used to select the desired property from each element.</param>
    /// <param name="comparer">The equality comparer that will be used to compare objects of type <typeparamref name="TProperty"/>, or <see langword="null"/> to use the default equality comparer.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <typeparam name="TProperty">The underlying type of each selected <see cref="IEnumerable{T}"/> element.</typeparam>
    /// <returns>Returns <see langword="true"/> if all selected element properties are equal; otherwise <see langword="false"/>.</returns>
    public static bool AllEqualBy<T, TProperty>(this IEnumerable<T> enumerable, Func<T, TProperty> selector, IEqualityComparer<TProperty>? comparer = null)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));
        RequireNotNull(selector, SelectorNullExceptionMessage, nameof(selector));

        using IEnumerator<T> enumerator = enumerable.GetEnumerator();

        // AllEqualBy of empty IEnumerable is considered true.
        if (!enumerator.MoveNext()) return true;

        TProperty firstValue = selector(enumerator.Current);

        while (enumerator.MoveNext())
            if (!(comparer ?? EqualityComparer<TProperty>.Default).Equals(firstValue, selector(enumerator.Current)))
                return false;

        return true;
    }

    /// <summary>
    /// Determines whether any elements of the current <see cref="IEnumerable{T}"/> are equal by a specified property.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="selector">The selector function which will be used to select the desired property from each element.</param>
    /// <param name="comparer">The equality comparer that will be used to compare objects of type <typeparamref name="TProperty"/>, or <see langword="null"/> to use the default equality comparer.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <typeparam name="TProperty">The underlying type of each selected <see cref="IEnumerable{T}"/> element.</typeparam>
    /// <returns>Returns <see langword="true"/> if any selected element properties are equal; otherwise <see langword="false"/>.</returns>
    public static bool AnyEqualBy<T, TProperty>(this IEnumerable<T> enumerable, Func<T, TProperty> selector, IEqualityComparer<TProperty>? comparer = null)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));
        RequireNotNull(selector, SelectorNullExceptionMessage, nameof(selector));

        // ReSharper disable once HeapView.ObjectAllocation.Evident
        HashSet<TProperty> seenValues = new(comparer);

        // ReSharper disable once LoopCanBeConvertedToQuery, HeapView.ObjectAllocation.Possible
        foreach (T item in enumerable)
        {
            TProperty value = selector(item);

            if (seenValues.Add(value)) continue;

            return true;
        }

        return false;
    }

    /// <summary>
    /// Obtains a number that represents how many elements are in the current <see cref="IEnumerable"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable"/> on which to perform the operation.</param>
    /// <returns>Returns a number that represents how many elements are in the current <see cref="IEnumerable"/>.</returns>
    public static int Count(this IEnumerable enumerable)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        int count = 0;

        // ReSharper disable once HeapView.ObjectAllocation.Possible
        foreach (object? _ in enumerable)
            checked
            {
                ++count;
            }

        return count;
    }

    /// <summary>
    /// Obtains a number that represents how many elements in the current <see cref="IEnumerable{T}"/> do not satisfy the specified predicate condition.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a number that represents how many elements in the current <see cref="IEnumerable{T}"/> do not satisfy the specified predicate condition.</returns>
    public static int CountNot<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));
        RequireNotNull(predicate, PredicateNullExceptionMessage, nameof(predicate));

        int result = 0;

        // ReSharper disable once HeapView.ObjectAllocation.Possible
        foreach (T element in enumerable)
            if (!predicate(element))
                checked
                {
                    ++result;
                }

        return result;
    }

    /// <summary>
    /// Obtains the first element of the current <see cref="IEnumerable{T}"/> that satisfies the specified predicate;
    /// otherwise <see cref="Optional{T}.None"/> if no such element is found.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> from which to return the first satisfactory element.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>
    /// Returns the first element of the current <see cref="IEnumerable{T}"/> that satisfies the specified predicate;
    /// otherwise <see cref="Optional{T}.None"/> if no such element is found.
    /// </returns>
    public static Optional<T> FirstOrNone<T>(this IEnumerable<T> enumerable, Func<T, bool>? predicate = null) where T : notnull
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        return Optional<T>.Of(enumerable.FirstOrDefault(predicate ?? (_ => true)));
    }

    /// <summary>
    /// Performs the specified <see cref="Action{T}"/> for each element of the current <see cref="IEnumerable"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable"/> over which to iterate.</param>
    /// <param name="action">The <see cref="Action{T}"/> to perform for each <see cref="IEnumerable"/> element.</param>
    public static void ForEach(this IEnumerable enumerable, Action<object?> action)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));
        RequireNotNull(action, ActionNullExceptionMessage, nameof(action));

        // ReSharper disable once HeapView.ObjectAllocation.Possible
        foreach (object? element in enumerable) action(element);
    }

    /// <summary>
    /// Performs the specified <see cref="Action{T}"/> for each element of the current <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> over which to iterate.</param>
    /// <param name="action">The <see cref="Action{T}"/> to perform for each <see cref="IEnumerable{T}"/> element.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));
        RequireNotNull(action, ActionNullExceptionMessage, nameof(action));

        // ReSharper disable once HeapView.ObjectAllocation.Possible
        foreach (T element in enumerable) action(element);
    }

    /// <summary>
    /// Gets the content hash code of the elements of the current <see cref="IEnumerable"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable"/> from which to compute a content hash code.</param>
    /// <returns>Returns the computed content hash code of the current <see cref="IEnumerable"/>.</returns>
    public static int GetContentHashCode(this IEnumerable? enumerable)
    {
        if (enumerable is null) return default;

        HashCode result = new();

        // ReSharper disable once HeapView.ObjectAllocation.Possible
        foreach (object? element in enumerable) result.Add(element);

        return result.ToHashCode();
    }

    /// <summary>
    /// Gets the content hash code of the elements of the current <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> from which to compute a content hash code.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns the computed content hash code of the current <see cref="IEnumerable{T}"/>.</returns>
    public static int GetContentHashCode<T>(this IEnumerable<T>? enumerable)
    {
        if (enumerable is null) return default;

        HashCode result = new();

        // ReSharper disable once HeapView.ObjectAllocation.Possible
        foreach (T element in enumerable) result.Add(element);

        return result.ToHashCode();
    }

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable"/> is empty.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable"/> on which to perform the operation.</param>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable"/> is empty; otherwise, <see langword="false"/>.</returns>
    public static bool IsEmpty(this IEnumerable enumerable)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        // ReSharper disable once LoopCanBeConvertedToQuery, HeapView.ObjectAllocation.Possible
        foreach (object? _ in enumerable) return false;

        return true;
    }

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable{T}"/> is empty.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> is empty; otherwise, <see langword="false"/>.</returns>
    public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        // ReSharper disable once LoopCanBeConvertedToQuery, HeapView.ObjectAllocation.Possible
        foreach (T _ in enumerable) return false;

        return true;
    }

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable"/> is not empty.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable"/> on which to perform the operation.</param>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable"/> is not empty; otherwise, <see langword="false"/>.</returns>
    public static bool IsNotEmpty(this IEnumerable enumerable) => !enumerable.IsEmpty();

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable{T}"/> is not empty.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> is not empty; otherwise, <see langword="false"/>.</returns>
    public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.IsEmpty();

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable"/> contains a single element.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable"/> on which to perform the operation.</param>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable"/> contains a single element; otherwise, <see langword="false"/>.</returns>
    public static bool IsSingle(this IEnumerable enumerable)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        long count = 0;

        // ReSharper disable once HeapView.ObjectAllocation.Possible
        foreach (object? _ in enumerable)
        {
            checked
            {
                ++count;
            }

            if (count > 1) return false;
        }

        return count is 1;
    }

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable{T}"/> contains a single element.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> contains a single element; otherwise, <see langword="false"/>.</returns>
    public static bool IsSingle<T>(this IEnumerable<T> enumerable)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        long count = 0;

        // ReSharper disable once HeapView.ObjectAllocation.Possible
        foreach (T _ in enumerable)
        {
            checked
            {
                ++count;
            }

            if (count > 1) return false;
        }

        return count is 1;
    }

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable"/> contains an even number of elements.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable"/> on which to perform the operation.</param>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable"/> contains an even number of elements; otherwise, <see langword="false"/>.</returns>
    public static bool IsCountEven(this IEnumerable enumerable)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        return enumerable.LongCount() % 2 is 0;
    }

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable{T}"/> contains an even number of elements.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> contains an even number of elements; otherwise, <see langword="false"/>.</returns>
    public static bool IsCountEven<T>(this IEnumerable<T> enumerable, Func<T, bool>? predicate = null)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        return enumerable.LongCount(predicate ?? (_ => true)) % 2 is 0;
    }

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable"/> contains an odd number of elements.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable"/> on which to perform the operation.</param>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable"/> contains an odd number of elements; otherwise, <see langword="false"/>.</returns>
    public static bool IsCountOdd(this IEnumerable enumerable) => !enumerable.IsCountEven();

    /// <summary>
    /// Determines whether the current <see cref="IEnumerable{T}"/> contains an odd number of elements.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> contains an odd number of elements; otherwise, <see langword="false"/>.</returns>
    public static bool IsCountOdd<T>(this IEnumerable<T> enumerable, Func<T, bool>? predicate = null) => !enumerable.IsCountEven(predicate);

    /// <summary>
    /// Joins the elements of the current <see cref="IEnumerable"/> into a new <see cref="String"/> instance.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable"/> to join.</param>
    /// <param name="separator">The separator which will appear between joined elements.</param>
    /// <returns>Returns the elements of the current <see cref="IEnumerable"/>, joined into a new <see cref="String"/> instance.</returns>
    public static string JoinToString(this IEnumerable enumerable, string separator = CollectionSeparator)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        // ReSharper disable once HeapView.ObjectAllocation.Evident
        StringBuilder builder = new();

        // ReSharper disable once HeapView.ObjectAllocation.Possible
        foreach (object? element in enumerable) builder.Append(element.ToStringOrNull()).Append(separator);

        builder.TrimEnd(separator);

        return builder.ToString();
    }

    /// <summary>
    /// Joins the elements of the current <see cref="IEnumerable{T}"/> into a new <see cref="String"/> instance.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> to join.</param>
    /// <param name="separator">The separator which will appear between joined elements.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns the elements of the current <see cref="IEnumerable{T}"/>, joined into a new <see cref="String"/> instance.</returns>
    public static string JoinToString<T>(this IEnumerable<T> enumerable, string separator = CollectionSeparator)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        // ReSharper disable once HeapView.ObjectAllocation.Evident
        StringBuilder builder = new();

        // ReSharper disable once HeapView.ObjectAllocation.Possible, HeapView.PossibleBoxingAllocation
        foreach (T element in enumerable) builder.Append(element.ToStringOrNull()).Append(separator);

        builder.TrimEnd(separator);

        return builder.ToString();
    }

    /// <summary>
    /// Obtains the last element of the current <see cref="IEnumerable{T}"/> that satisfies the specified predicate;
    /// otherwise <see cref="Optional{T}.None"/> if no such element is found.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> from which to return the last satisfactory element.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>
    /// Returns the last element of the current <see cref="IEnumerable{T}"/> that satisfies the specified predicate;
    /// otherwise <see cref="Optional{T}.None"/> if no such element is found.
    /// </returns>
    public static Optional<T> LastOrNone<T>(this IEnumerable<T> enumerable, Func<T, bool>? predicate = null) where T : notnull
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        return Optional<T>.Of(enumerable.LastOrDefault(predicate ?? (_ => true)));
    }

    /// <summary>
    /// Obtains a number that represents how many elements are in the current <see cref="IEnumerable"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable"/> on which to perform the operation.</param>
    /// <returns>Returns a number that represents how many elements are in the current <see cref="IEnumerable"/>.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static long LongCount(this IEnumerable enumerable)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        long count = 0;

        // ReSharper disable once HeapView.ObjectAllocation.Possible
        foreach (object? _ in enumerable)
            checked
            {
                ++count;
            }

        return count;
    }

    /// <summary>
    /// Determines whether none of the elements of the current <see cref="IEnumerable{T}"/> satisfy the specified predicate condition.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns <see langword="true"/> if none of the elements of the current <see cref="IEnumerable{T}"/> satisfy the specified predicate condition; otherwise, <see langword="false"/>.</returns>
    public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        return !enumerable.Any(predicate);
    }

    /// <summary>Determines whether two sequences are <see langword="null"/>, or equal by comparing their elements by using a specified <see cref="T:IEqualityComparer{T}" />.</summary>
    /// <param name="enumerable">An <see cref="T:IEnumerable{T}" /> to compare to <paramref name="other" />.</param>
    /// <param name="other">An <see cref="T:IEnumerable{T}" /> to compare to the first sequence.</param>
    /// <param name="comparer">An <see cref="T:IEqualityComparer{T}" /> to use to compare elements.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns> Returns <see langword="true" /> if the two source sequences are <see langword="null"/>, or of equal length and their corresponding elements compare equal according to <paramref name="comparer" />; otherwise, <see langword="false" />.</returns>
    public static bool SequenceEqualOrNull<T>(this IEnumerable<T>? enumerable, IEnumerable<T>? other, IEqualityComparer<T>? comparer = null)
    {
        if (enumerable is null || other is null) return enumerable is null && other is null;
        return enumerable.SequenceEqual(other, comparer ?? EqualityComparer<T>.Default);
    }

    /// <summary>
    /// Obtains a single element of the current <see cref="IEnumerable{T}"/> that satisfies the specified predicate;
    /// otherwise <see cref="Optional{T}.None"/> if no such element is found,
    /// or <see cref="Failure{T}"/> if the current <see cref="IEnumerable{T}"/> contains more than one matching element.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> from which to return a single satisfactory element.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>
    /// Returns a single element of the current <see cref="IEnumerable{T}"/> that satisfies the specified predicate;
    /// otherwise <see cref="Optional{T}.None"/> if no such element is found,
    /// or <see cref="Failure{T}"/> if the current <see cref="IEnumerable{T}"/> contains more than one matching element.
    /// </returns>
    public static Result<Optional<T>> SingleOrNone<T>(this IEnumerable<T> enumerable, Func<T, bool>? predicate = null) where T : notnull
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        try
        {
            return Optional<T>.Of(enumerable.SingleOrDefault(predicate ?? (_ => true)));
        }
        catch (Exception exception)
        {
            return Result<Optional<T>>.Failure(exception);
        }
    }

    /// <summary>
    /// Calculates the sum of the elements of the current <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> to sum.</param>
    /// <typeparam name="T">The underlying <see cref="INumberBase{T}"/> type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns the sum of the elements of the current <see cref="IEnumerable{T}"/>.</returns>
    public static T Sum<T>(this IEnumerable<T> enumerable) where T : INumberBase<T>
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        return enumerable.IsEmpty() ? T.Zero : enumerable.Aggregate((left, right) => left + right);
    }

    /// <summary>
    /// Calculates the sum of the elements of the current <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> to sum.</param>
    /// <param name="selector">The selector function which will be used to select each <see cref="INumberBase{T}"/> property from each element.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <typeparam name="TResult">The underlying <see cref="INumberBase{T}"/> type of each element to sum.</typeparam>
    /// <returns>Returns the sum of the elements of the current <see cref="IEnumerable{T}"/>.</returns>
    public static TResult SumBy<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector) where TResult : INumberBase<TResult>
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));
        RequireNotNull(selector, SelectorNullExceptionMessage, nameof(selector));

        return enumerable.Select(selector).Sum();
    }

    /// <summary>
    /// Filters the current <see cref="IEnumerable{T}"/> elements that do not satisfy the specified predicate condition.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <param name="predicate">The function to test each element for a condition.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="IEnumerable{T}"/> that contains elements that do not satisfy the condition.</returns>
    public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));
        RequireNotNull(predicate, PredicateNullExceptionMessage, nameof(predicate));

        // ReSharper disable once LoopCanBeConvertedToQuery, HeapView.ObjectAllocation.Possible
        foreach (T element in enumerable)
            if (!predicate(element))
                yield return element;
    }

    /// <summary>
    /// Filters the current <see cref="IEnumerable{T}"/> elements that are not <see langword="null"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="IEnumerable{T}"/> that contains elements that are not <see langword="null"/>.</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> enumerable) where T : class
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        return enumerable.Where(element => element is not null)!;
    }

    /// <summary>
    /// Filters the current <see cref="IEnumerable{T}"/> elements that are not <see langword="null"/>.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> on which to perform the operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="IEnumerable{T}"/> that contains elements that are not <see langword="null"/>.</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> enumerable) where T : struct
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        return enumerable.Where(element => element.HasValue).Select(element => element!.Value);
    }

    /// <summary>
    /// Formats the current <see cref="IEnumerable"/> as a collection string.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable"/> to format.</param>
    /// <returns>Returns a new <see cref="String"/> instance representing the current <see cref="IEnumerable"/>.</returns>
    public static string ToCollectionString(this IEnumerable enumerable)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        return enumerable.JoinToString().Wrap(CollectionOpenBracket, CollectionCloseBracket);
    }

    /// <summary>
    /// Formats the current <see cref="IEnumerable{T}"/> as a collection string.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> to format.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="String"/> instance representing the current <see cref="IEnumerable{T}"/>.</returns>
    public static string ToCollectionString<T>(this IEnumerable<T> enumerable)
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        return enumerable.JoinToString().Wrap(CollectionOpenBracket, CollectionCloseBracket);
    }

    /// <summary>
    /// Formats the current <see cref="IEnumerable{T}"/> as a collection string.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> to format.</param>
    /// <param name="format">The format which will be applied to each element.</param>
    /// <param name="provider">An object that provides culture-specific formatting information.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="String"/> instance representing the current <see cref="IEnumerable{T}"/>.</returns>
    // ReSharper disable once HeapView.ClosureAllocation
    public static string ToCollectionString<T>(this IEnumerable<T> enumerable, string format, IFormatProvider? provider = null) where T : IFormattable
    {
        // ReSharper disable PossibleMultipleEnumeration
        RequireNotNull(enumerable, EnumerableNullExceptionMessage, nameof(enumerable));

        // ReSharper disable once HeapView.DelegateAllocation
        return enumerable.Select(element => element.ToString(format, provider)).ToCollectionString();
    }
}
