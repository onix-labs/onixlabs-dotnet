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
using System.ComponentModel;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for <see cref="IComparable{T}"/> instances.
/// </summary>
// ReSharper disable ConvertToExtensionBlock
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ComparableExtensions
{
    /// <summary>
    /// Implements the non-generic <see cref="IComparable.CompareTo(object)"/> contract on top of a generic
    /// <see cref="IComparable{T}"/> implementation, where the implementing type differs from the comparable
    /// parameter <typeparamref name="T"/>. This is the F-bounded / CRTP path — for example,
    /// <c>Enumeration&lt;T&gt; : IComparable&lt;T&gt;</c> where <c>T : Enumeration&lt;T&gt;</c>.
    /// </summary>
    /// <param name="left">The current <see cref="IComparable{T}"/> instance.</param>
    /// <param name="right">The object to compare against. May be <see langword="null"/>.</param>
    /// <typeparam name="T">The comparable parameter — not necessarily the runtime type of <paramref name="left"/>.</typeparam>
    /// <returns>
    /// Returns a positive value when <paramref name="right"/> is <see langword="null"/>; otherwise returns the
    /// signed comparison result from <see cref="IComparable{T}.CompareTo(T)"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="right"/> is not <see langword="null"/> and is not of type <typeparamref name="T"/>.
    /// </exception>
    public static int CompareToObject<T>(this IComparable<T> left, object? right) => right switch
    {
        null => 1,
        T other => left.CompareTo(other),
        _ => throw new ArgumentException($"Object must be of type {typeof(T).FullName}", nameof(right))
    };

    /// <summary>
    /// Implements the non-generic <see cref="IComparable.CompareTo(object)"/> contract on top of a generic
    /// <see cref="IComparable{T}"/> implementation, where the implementing type is itself <typeparamref name="T"/>.
    /// This is the self-comparable path — for example, <c>Hash : IComparable&lt;Hash&gt;</c>
    /// It avoids boxing when <typeparamref name="T"/> is a value type.
    /// </summary>
    /// <param name="right">The object to compare against. May be <see langword="null"/>.</param>
    /// <param name="left">The current <typeparamref name="T"/> instance.</param>
    /// <typeparam name="T">The implementing type, which is also the comparable parameter.</typeparam>
    /// <returns>
    /// Returns a positive value when <paramref name="right"/> is <see langword="null"/>; otherwise returns the
    /// signed comparison result from <see cref="IComparable{T}.CompareTo(T)"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="right"/> is not <see langword="null"/> and is not of type <typeparamref name="T"/>.
    /// </exception>
    public static int CompareToObject<T>(this T left, object? right) where T : IComparable<T> => right switch
    {
        null => 1,
        T other => left.CompareTo(other),
        _ => throw new ArgumentException($"Object must be of type {typeof(T).FullName}", nameof(right))
    };

    /// <summary>
    /// Determines whether the current <see cref="IComparable{T}"/> value falls within range,
    /// inclusive of the specified minimum and maximum values.
    /// </summary>
    /// <param name="minimum">The inclusive minimum value.</param>
    /// <param name="maximum">The inclusive maximum value.</param>
    /// <param name="left">The current <typeparamref name="T"/> instance.</param>
    /// <typeparam name="T">The implementing type, which is also the comparable parameter.</typeparam>
    /// <returns>
    /// Returns <see langword="true"/> if the current <see cref="IComparable{T}"/> value falls within range,
    /// inclusive of the specified minimum and maximum values; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsWithinRangeInclusive<T>(this T left, T minimum, T maximum) where T : IComparable<T> =>
        left.CompareTo(minimum) >= 0 && left.CompareTo(maximum) <= 0;

    /// <summary>
    /// Determines whether the current <see cref="IComparable{T}"/> value falls within range,
    /// exclusive of the specified minimum and maximum values.
    /// </summary>
    /// <param name="minimum">The exclusive minimum value.</param>
    /// <param name="maximum">The exclusive maximum value.</param>
    /// <param name="left">The current <typeparamref name="T"/> instance.</param>
    /// <typeparam name="T">The implementing type, which is also the comparable parameter.</typeparam>
    /// <returns>
    /// Returns <see langword="true"/> if the current <see cref="IComparable{T}"/> value falls within range,
    /// exclusive of the specified minimum and maximum values; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsWithinRangeExclusive<T>(this T left, T minimum, T maximum) where T : IComparable<T> =>
        left.CompareTo(minimum) > 0 && left.CompareTo(maximum) < 0;
}
