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
using OnixLabs.Core.Collections.Generic;
using OnixLabs.Core.Text;

namespace OnixLabs.Core.Linq;

/// <summary>
/// Provides LINQ-like extension methods for <see cref="IEnumerable"/> and <see cref="IEnumerable{T}"/> instances.
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
    private const string SpecificationNullExceptionMessage = "Specification must not be null.";
    private const string ActionNullExceptionMessage = "Action must not be null.";

    /// <summary>
    /// Provides LINQ-like extension methods for <see cref="IEnumerable"/> instances.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable"/> instance.</param>
    extension(IEnumerable? enumerable)
    {
        /// <summary>
        /// Obtains a number that represents how many elements are in the current <see cref="IEnumerable"/>.
        /// </summary>
        /// <returns>Returns a number that represents how many elements are in the current <see cref="IEnumerable"/>.</returns>
        public int Count()
        {
            // ReSharper disable PossibleMultipleEnumeration
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);

            // ReSharper disable once GenericEnumeratorNotDisposed
            IEnumerator enumerator = enumerable.GetEnumerator();

            int count = 0;

            while (enumerator.MoveNext())
                checked
                {
                    ++count;
                }

            return count;
        }

        /// <summary>
        /// Performs the specified <see cref="Action{T}"/> for each element of the current <see cref="IEnumerable"/>.
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> to perform for each <see cref="IEnumerable"/> element.</param>
        public void ForEach(Action<object?> action)
        {
            if (enumerable is null)
                return;

            RequireNotNull(action, ActionNullExceptionMessage);

            // ReSharper disable once HeapView.ObjectAllocation.Possible
            foreach (object? element in enumerable)
                action(element);
        }

        /// <summary>
        /// Gets the content hash code of the elements of the current <see cref="IEnumerable"/>.
        /// </summary>
        /// <returns>Returns the computed content hash code of the current <see cref="IEnumerable"/>.</returns>
        public int GetContentHashCode()
        {
            if (enumerable is null)
                return 0;

            HashCode result = new();

            // ReSharper disable once HeapView.ObjectAllocation.Possible
            foreach (object? element in enumerable)
                result.Add(element);

            return result.ToHashCode();
        }

        /// <summary>
        /// Determines whether the current <see cref="IEnumerable"/> is empty.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable"/> is empty; otherwise, <see langword="false"/>.</returns>
        public bool IsEmpty() => enumerable.LongCount() is 0;

        /// <summary>
        /// Determines whether the current <see cref="IEnumerable"/> is not empty.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable"/> is not empty; otherwise, <see langword="false"/>.</returns>
        public bool IsNotEmpty() => !enumerable.IsEmpty();

        /// <summary>
        /// Determines whether the current <see cref="IEnumerable"/> contains a single element.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable"/> contains a single element; otherwise, <see langword="false"/>.</returns>
        public bool IsSingle() => enumerable.LongCount() is 1;

        /// <summary>
        /// Determines whether the current <see cref="IEnumerable"/> contains an even number of elements.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable"/> contains an even number of elements; otherwise, <see langword="false"/>.</returns>
        public bool IsCountEven() => (enumerable.LongCount() & 1) is 0;

        /// <summary>
        /// Determines whether the current <see cref="IEnumerable"/> contains an odd number of elements.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable"/> contains an odd number of elements; otherwise, <see langword="false"/>.</returns>
        public bool IsCountOdd() => !enumerable.IsCountEven();

        /// <summary>
        /// Joins the elements of the current <see cref="IEnumerable"/> into a new <see cref="String"/> instance.
        /// </summary>
        /// <param name="separator">The separator which will appear between joined elements.</param>
        /// <returns>Returns the elements of the current <see cref="IEnumerable"/>, joined into a new <see cref="String"/> instance.</returns>
        public string JoinToString(string separator = CollectionSeparator)
        {
            if (enumerable is null)
                return string.Empty;

            // ReSharper disable once HeapView.ObjectAllocation.Evident
            StringBuilder builder = new();

            // ReSharper disable once HeapView.ObjectAllocation.Possible
            foreach (object? element in enumerable)
                builder.Append(element.ToStringOrNull()).Append(separator);

            return builder.TrimEnd(separator).ToString();
        }

        /// <summary>
        /// Obtains a number that represents how many elements are in the current <see cref="IEnumerable"/>.
        /// </summary>
        /// <returns>Returns a number that represents how many elements are in the current <see cref="IEnumerable"/>.</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public long LongCount()
        {
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);

            // ReSharper disable once GenericEnumeratorNotDisposed
            IEnumerator enumerator = enumerable.GetEnumerator();

            long count = 0;

            while (enumerator.MoveNext())
                checked
                {
                    ++count;
                }

            return count;
        }

        /// <summary>
        /// Formats the current <see cref="IEnumerable"/> as a collection string.
        /// </summary>
        /// <returns>Returns a new <see cref="String"/> instance representing the current <see cref="IEnumerable"/>.</returns>
        public string ToCollectionString() => enumerable is null
            ? string.Empty
            : enumerable.JoinToString().Wrap(CollectionOpenBracket, CollectionCloseBracket);
    }

    /// <summary>
    /// Provides LINQ-like extension methods for <see cref="IEnumerable{T}"/> instances.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> instance.</param>
    extension<T>(IEnumerable<T>? enumerable)
    {
        /// <summary>
        /// Determines whether all elements of the current <see cref="IEnumerable{T}"/> are equal by a specified property.
        /// </summary>
        /// <param name="selector">The selector function which will be used to select the desired property from each element.</param>
        /// <param name="comparer">The equality comparer that will be used to compare objects of type <typeparamref name="TProperty"/>, or <see langword="null"/> to use the default equality comparer.</param>
        /// <typeparam name="TProperty">The underlying type of each selected <see cref="IEnumerable{T}"/> element.</typeparam>
        /// <returns>Returns <see langword="true"/> if all selected element properties are equal; otherwise <see langword="false"/>.</returns>
        public bool AllEqualBy<TProperty>(Func<T, TProperty> selector, IEqualityComparer<TProperty>? comparer = null)
        {
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);
            RequireNotNull(selector, SelectorNullExceptionMessage);

            using IEnumerator<T> enumerator = enumerable.GetEnumerator();

            // AllEqualBy of empty IEnumerable<T> is considered true.
            if (!enumerator.MoveNext())
                return true;

            TProperty firstValue = selector(enumerator.Current);
            IEqualityComparer<TProperty> nonNullComparer = comparer.GetOrDefault();

            while (enumerator.MoveNext())
                if (!nonNullComparer.Equals(firstValue, selector(enumerator.Current)))
                    return false;

            return true;
        }

        /// <summary>
        /// Determines whether any elements of the current <see cref="IEnumerable{T}"/> are equal by a specified property.
        /// </summary>
        /// <param name="selector">The selector function which will be used to select the desired property from each element.</param>
        /// <param name="comparer">The equality comparer that will be used to compare objects of type <typeparamref name="TProperty"/>, or <see langword="null"/> to use the default equality comparer.</param>
        /// <typeparam name="TProperty">The underlying type of each selected <see cref="IEnumerable{T}"/> element.</typeparam>
        /// <returns>Returns <see langword="true"/> if any selected element properties are equal; otherwise <see langword="false"/>.</returns>
        public bool AnyEqualBy<TProperty>(Func<T, TProperty> selector, IEqualityComparer<TProperty>? comparer = null)
        {
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);
            RequireNotNull(selector, SelectorNullExceptionMessage);

            using IEnumerator<T> enumerator = enumerable.GetEnumerator();

            // AnyEqualBy of empty IEnumerable<T> is considered false.
            if (!enumerator.MoveNext())
                return false;

            TProperty firstValue = selector(enumerator.Current);
            IEqualityComparer<TProperty> nonNullComparer = comparer.GetOrDefault();

            while (enumerator.MoveNext())
                if (nonNullComparer.Equals(firstValue, selector(enumerator.Current)))
                    return true;

            return false;
        }

        /// <summary>
        /// Obtains a number that represents how many elements in the current <see cref="IEnumerable{T}"/> do not satisfy the specified predicate condition.
        /// </summary>
        /// <param name="predicate">The function to test each element for a condition.</param>
        /// <returns>Returns a number that represents how many elements in the current <see cref="IEnumerable{T}"/> do not satisfy the specified predicate condition.</returns>
        public int CountNot(Func<T, bool> predicate)
        {
            // ReSharper disable PossibleMultipleEnumeration
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);
            RequireNotNull(predicate, PredicateNullExceptionMessage);

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
        /// Performs the specified <see cref="Action{T}"/> for each element of the current <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> to perform for each <see cref="IEnumerable{T}"/> element.</param>
        public void ForEach(Action<T> action)
        {
            if (enumerable is null)
                return;

            RequireNotNull(action, ActionNullExceptionMessage);

            // ReSharper disable once HeapView.ObjectAllocation.Possible
            foreach (T element in enumerable)
                action(element);
        }

        /// <summary>
        /// Gets the content hash code of the elements of the current <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <returns>Returns the computed content hash code of the current <see cref="IEnumerable{T}"/>.</returns>
        public int GetContentHashCode()
        {
            if (enumerable is null)
                return 0;

            HashCode result = new();

            // ReSharper disable once HeapView.ObjectAllocation.Possible
            foreach (T element in enumerable)
                result.Add(element);

            return result.ToHashCode();
        }

        /// <summary>
        /// Determines whether the current <see cref="IEnumerable{T}"/> is empty.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> is empty; otherwise, <see langword="false"/>.</returns>
        public bool IsEmpty() => enumerable.LongCount() is 0;

        /// <summary>
        /// Determines whether the current <see cref="IEnumerable{T}"/> is not empty.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> is not empty; otherwise, <see langword="false"/>.</returns>
        public bool IsNotEmpty() => !enumerable.IsEmpty();

        /// <summary>
        /// Determines whether the current <see cref="IEnumerable{T}"/> contains a single element.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> contains a single element; otherwise, <see langword="false"/>.</returns>
        public bool IsSingle() => enumerable.LongCount() is 1;

        /// <summary>
        /// Determines whether the current <see cref="IEnumerable{T}"/> contains an even number of elements.
        /// </summary>
        /// <param name="predicate">The function to test each element for a condition.</param>
        /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> contains an even number of elements; otherwise, <see langword="false"/>.</returns>
        public bool IsCountEven(Func<T, bool>? predicate = null)
        {
            // ReSharper disable PossibleMultipleEnumeration
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);

            return (enumerable.LongCount(predicate ?? (_ => true)) & 1) is 0;
        }

        /// <summary>
        /// Determines whether the current <see cref="IEnumerable{T}"/> contains an odd number of elements.
        /// </summary>
        /// <param name="predicate">The function to test each element for a condition.</param>
        /// <returns>Returns <see langword="true"/> if the <see cref="IEnumerable{T}"/> contains an odd number of elements; otherwise, <see langword="false"/>.</returns>
        public bool IsCountOdd(Func<T, bool>? predicate = null) => !enumerable.IsCountEven(predicate);

        /// <summary>
        /// Joins the elements of the current <see cref="IEnumerable{T}"/> into a new <see cref="String"/> instance.
        /// </summary>
        /// <param name="separator">The separator which will appear between joined elements.</param>
        /// <returns>Returns the elements of the current <see cref="IEnumerable{T}"/>, joined into a new <see cref="String"/> instance.</returns>
        public string JoinToString(string separator = CollectionSeparator)
        {
            if (enumerable is null)
                return string.Empty;

            // ReSharper disable once HeapView.ObjectAllocation.Evident
            StringBuilder builder = new();

            // ReSharper disable once HeapView.ObjectAllocation.Possible, HeapView.PossibleBoxingAllocation
            foreach (T element in enumerable)
                builder.Append(element.ToStringOrNull()).Append(separator);

            return builder.TrimEnd(separator).ToString();
        }

        /// <summary>
        /// Determines whether none of the elements of the current <see cref="IEnumerable{T}"/> satisfy the specified predicate condition.
        /// </summary>
        /// <param name="predicate">The function to test each element for a condition.</param>
        /// <returns>Returns <see langword="true"/> if none of the elements of the current <see cref="IEnumerable{T}"/> satisfy the specified predicate condition; otherwise, <see langword="false"/>.</returns>
        public bool None(Func<T, bool> predicate)
        {
            // ReSharper disable PossibleMultipleEnumeration
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);

            return !enumerable.Any(predicate);
        }

        /// <summary>Determines whether two sequences are <see langword="null"/>, or equal by comparing their elements by using a specified <see cref="T:IEqualityComparer{T}" />.</summary>
        /// <param name="other">An <see cref="T:IEnumerable{T}" /> to compare to the first sequence.</param>
        /// <param name="comparer">An <see cref="T:IEqualityComparer{T}" /> to use to compare elements.</param>
        /// <returns> Returns <see langword="true" /> if the two source sequences are <see langword="null"/>, or of equal length and their corresponding elements compare equal according to <paramref name="comparer" />; otherwise, <see langword="false" />.</returns>
        public bool SequenceEqualOrNull(IEnumerable<T>? other, IEqualityComparer<T>? comparer = null)
        {
            if (enumerable is null || other is null)
                return enumerable is null && other is null;

            return enumerable.SequenceEqual(other, comparer.GetOrDefault());
        }

        /// <summary>
        /// Calculates the sum of the elements of the current <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="selector">The selector function which will be used to select each <see cref="INumberBase{T}"/> property from each element.</param>
        /// <typeparam name="TResult">The underlying <see cref="INumberBase{T}"/> type of each element to sum.</typeparam>
        /// <returns>Returns the sum of the elements of the current <see cref="IEnumerable{T}"/>.</returns>
        public TResult SumBy<TResult>(Func<T, TResult> selector) where TResult : INumberBase<TResult>
        {
            // ReSharper disable PossibleMultipleEnumeration
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);
            RequireNotNull(selector, SelectorNullExceptionMessage);

            return enumerable.Select(selector).Sum();
        }

        /// <summary>
        /// Filters a sequence of values based on a specification.
        /// </summary>
        /// <param name="specification">The <see cref="Specification{T}"/> to filter by.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> that contains elements from the input sequence that satisfy the specification.</returns>
        public IEnumerable<T> Where(Specification<T> specification)
        {
            // ReSharper disable PossibleMultipleEnumeration
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);
            RequireNotNull(specification, SpecificationNullExceptionMessage);

            return enumerable.Where(specification.Criteria.Compile());
        }

        /// <summary>
        /// Filters a sequence of values based on a negated specification.
        /// </summary>
        /// <param name="specification">The <see cref="Specification{T}"/> to filter by.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> that contains elements from the input sequence that satisfy the negated specification.</returns>
        public IEnumerable<T> WhereNot(Specification<T> specification)
        {
            // ReSharper disable PossibleMultipleEnumeration
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);
            RequireNotNull(specification, SpecificationNullExceptionMessage);

            return enumerable.Where(specification.Not().Criteria.Compile());
        }

        /// <summary>
        /// Filters the current <see cref="IEnumerable{T}"/> elements that do not satisfy the specified predicate condition.
        /// </summary>
        /// <param name="predicate">The function to test each element for a condition.</param>
        /// <returns>Returns a new <see cref="IEnumerable{T}"/> that contains elements that do not satisfy the condition.</returns>
        public IEnumerable<T> WhereNot(Func<T, bool> predicate)
        {
            // ReSharper disable PossibleMultipleEnumeration
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);
            RequireNotNull(predicate, PredicateNullExceptionMessage);

            // ReSharper disable once LoopCanBeConvertedToQuery, HeapView.ObjectAllocation.Possible
            foreach (T element in enumerable)
                if (!predicate(element))
                    yield return element;
        }

        /// <summary>
        /// Formats the current <see cref="IEnumerable{T}"/> as a collection string.
        /// </summary>
        /// <returns>Returns a new <see cref="String"/> instance representing the current <see cref="IEnumerable{T}"/>.</returns>
        public string ToCollectionString() => enumerable is null
            ? string.Empty
            : enumerable.JoinToString().Wrap(CollectionOpenBracket, CollectionCloseBracket);
    }

    /// <summary>
    /// Provides LINQ-like extension methods for <see cref="IEnumerable{T}"/> instances whose elements are not null.
    /// </summary>
    /// <param name="enumerable">The current <see cref="IEnumerable{T}"/> instance.</param>
    extension<T>(IEnumerable<T>? enumerable) where T : notnull
    {
        /// <summary>
        /// Obtains the first element of the current <see cref="IEnumerable{T}"/> that satisfies the specified predicate;
        /// otherwise <see cref="Optional{T}.None"/> if no such element is found.
        /// </summary>
        /// <param name="predicate">The function to test each element for a condition.</param>
        /// <returns>
        /// Returns the first element of the current <see cref="IEnumerable{T}"/> that satisfies the specified predicate;
        /// otherwise <see cref="Optional{T}.None"/> if no such element is found.
        /// </returns>
        public Optional<T> FirstOrNone(Func<T, bool>? predicate = null)
        {
            // ReSharper disable PossibleMultipleEnumeration
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);

            return Optional<T>.Of(enumerable.FirstOrDefault(predicate ?? (_ => true)));
        }

        /// <summary>
        /// Obtains the last element of the current <see cref="IEnumerable{T}"/> that satisfies the specified predicate;
        /// otherwise <see cref="Optional{T}.None"/> if no such element is found.
        /// </summary>
        /// <param name="predicate">The function to test each element for a condition.</param>
        /// <returns>
        /// Returns the last element of the current <see cref="IEnumerable{T}"/> that satisfies the specified predicate;
        /// otherwise <see cref="Optional{T}.None"/> if no such element is found.
        /// </returns>
        public Optional<T> LastOrNone(Func<T, bool>? predicate = null)
        {
            // ReSharper disable PossibleMultipleEnumeration
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);

            return Optional<T>.Of(enumerable.LastOrDefault(predicate ?? (_ => true)));
        }

        /// <summary>
        /// Obtains a single element of the current <see cref="IEnumerable{T}"/> that satisfies the specified predicate;
        /// otherwise <see cref="Optional{T}.None"/> if no such element is found,
        /// or <see cref="Failure{T}"/> if the current <see cref="IEnumerable{T}"/> contains more than one matching element.
        /// </summary>
        /// <param name="predicate">The function to test each element for a condition.</param>
        /// <returns>
        /// Returns a single element of the current <see cref="IEnumerable{T}"/> that satisfies the specified predicate;
        /// otherwise <see cref="Optional{T}.None"/> if no such element is found,
        /// or <see cref="Failure{T}"/> if the current <see cref="IEnumerable{T}"/> contains more than one matching element.
        /// </returns>
        public Result<Optional<T>> SingleOrNone(Func<T, bool>? predicate = null)
        {
            // ReSharper disable PossibleMultipleEnumeration
            RequireNotNull(enumerable, EnumerableNullExceptionMessage);

            try
            {
                return Optional<T>.Of(enumerable.SingleOrDefault(predicate ?? (_ => true)));
            }
            catch (Exception exception)
            {
                return exception;
            }
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
        RequireNotNull(enumerable, EnumerableNullExceptionMessage);

        return enumerable.IsEmpty() ? T.Zero : enumerable.Aggregate((left, right) => left + right);
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
        RequireNotNull(enumerable, EnumerableNullExceptionMessage);

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
        RequireNotNull(enumerable, EnumerableNullExceptionMessage);

        return enumerable.Where(element => element.HasValue).Select(element => element!.Value);
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
        RequireNotNull(enumerable, EnumerableNullExceptionMessage);

        // ReSharper disable once HeapView.DelegateAllocation
        return enumerable.Select(element => element.ToString(format, provider)).ToCollectionString();
    }
}
