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
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace OnixLabs.Numerics;

/// <summary>
/// Represents an ordinality comparer for comparing <see cref="NumberInfo"/> values.
/// </summary>
public sealed class NumberInfoOrdinalityComparer : IComparer<NumberInfo>, IComparer
{
    /// <summary>
    /// Gets the default ordinal comparer for comparing <see cref="NumberInfo"/> values.
    /// </summary>
    public static readonly NumberInfoOrdinalityComparer Default = new();

    /// <summary>
    /// Prevents a default instance of the <see cref="NumberInfoOrdinalityComparer"/> class from being created.
    /// </summary>
    private NumberInfoOrdinalityComparer()
    {
    }

    /// <summary>Compares two <see cref="NumberInfo"/> values and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
    /// <param name="x">The first <see cref="NumberInfo"/> value to compare.</param>
    /// <param name="y">The second <see cref="NumberInfo"/> value to compare.</param>
    /// <returns>Returns a signed integer that indicates the relative order of the <see cref="NumberInfo"/> values being compared.</returns>
    public int Compare(NumberInfo x, NumberInfo y)
    {
        BigInteger factor = BigInteger.Min(x.ScaleFactor, y.ScaleFactor);
        BigInteger xNormalized = x.UnscaledValue * y.ScaleFactor / factor;
        BigInteger yNormalized = y.UnscaledValue * x.ScaleFactor / factor;

        return xNormalized.CompareTo(yNormalized);
    }

    /// <summary>Compares two <see cref="NumberInfo"/> values and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
    /// <param name="x">The first <see cref="NumberInfo"/> value to compare.</param>
    /// <param name="y">The second <see cref="NumberInfo"/> value to compare.</param>
    /// <returns>Returns a signed integer that indicates the relative order of the <see cref="NumberInfo"/> values being compared.</returns>
    /// <exception cref="ArgumentException">If either <paramref name="x"/> or <paramref name="y"/> are not of type <see cref="NumberInfo"/>.</exception>
    public int Compare(object? x, object? y)
    {
        if (x is not NumberInfo xInfo) throw new ArgumentException($"Argument must be of type {nameof(NumberInfo)}", nameof(x));
        if (y is not NumberInfo yInfo) throw new ArgumentException($"Argument must be of type {nameof(NumberInfo)}", nameof(y));

        return Compare(xInfo, yInfo);
    }

    /// <summary>
    /// Determines whether the left-hand <see cref="NumberInfo"/> value is equal to the right-hand <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="left">The left-hand <see cref="NumberInfo"/> value to compare.</param>
    /// <param name="right">The left-hand <see cref="NumberInfo"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand <see cref="NumberInfo"/> value is equal to the right-hand <see cref="NumberInfo"/> value; otherwise, <see langword="false"/>.</returns>
    public bool IsEqual(NumberInfo left, NumberInfo right)
    {
        return Compare(left, right) is 0;
    }

    /// <summary>
    /// Determines whether the left-hand <see cref="NumberInfo"/> value is greater than the right-hand <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="left">The left-hand <see cref="NumberInfo"/> value to compare.</param>
    /// <param name="right">The left-hand <see cref="NumberInfo"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand <see cref="NumberInfo"/> value is greater than the right-hand <see cref="NumberInfo"/> value; otherwise, <see langword="false"/>.</returns>
    public bool IsGreaterThan(NumberInfo left, NumberInfo right)
    {
        return Compare(left, right) is 1;
    }

    /// <summary>
    /// Determines whether the left-hand <see cref="NumberInfo"/> value is greater than, or equal to the right-hand <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="left">The left-hand <see cref="NumberInfo"/> value to compare.</param>
    /// <param name="right">The left-hand <see cref="NumberInfo"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand <see cref="NumberInfo"/> value is greater than, or equal to the right-hand <see cref="NumberInfo"/> value; otherwise, <see langword="false"/>.</returns>
    public bool IsGreaterThanOrEqual(NumberInfo left, NumberInfo right)
    {
        return Compare(left, right) is 0 or 1;
    }

    /// <summary>
    /// Determines whether the left-hand <see cref="NumberInfo"/> value is less than the right-hand <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="left">The left-hand <see cref="NumberInfo"/> value to compare.</param>
    /// <param name="right">The left-hand <see cref="NumberInfo"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand <see cref="NumberInfo"/> value is less than the right-hand <see cref="NumberInfo"/> value; otherwise, <see langword="false"/>.</returns>
    public bool IsLessThan(NumberInfo left, NumberInfo right)
    {
        return Compare(left, right) is -1;
    }

    /// <summary>
    /// Determines whether the left-hand <see cref="NumberInfo"/> value is less than, or equal to the right-hand <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="left">The left-hand <see cref="NumberInfo"/> value to compare.</param>
    /// <param name="right">The left-hand <see cref="NumberInfo"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand <see cref="NumberInfo"/> value is less than, or equal to the right-hand <see cref="NumberInfo"/> value; otherwise, <see langword="false"/>.</returns>
    public bool IsLessThanOrEqual(NumberInfo left, NumberInfo right)
    {
        return Compare(left, right) is -1 or 0;
    }
}
