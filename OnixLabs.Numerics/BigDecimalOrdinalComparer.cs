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
/// Represents an ordinal comparer for comparing <see cref="BigDecimal"/> values.
/// </summary>
public sealed class BigDecimalOrdinalComparer : IComparer<BigDecimal>, IComparer
{
    /// <summary>
    /// Gets the default ordinal comparer for comparing <see cref="BigDecimal"/> values.
    /// </summary>
    public static readonly BigDecimalOrdinalComparer Default = new();

    /// <summary>
    /// Prevents a default instance of the <see cref="BigDecimalOrdinalComparer"/> class from being created.
    /// </summary>
    private BigDecimalOrdinalComparer()
    {
    }

    /// <summary>Compares two <see cref="BigDecimal"/> values and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
    /// <param name="x">The first <see cref="BigDecimal"/> value to compare.</param>
    /// <param name="y">The second <see cref="BigDecimal"/> value to compare.</param>
    /// <returns>Returns a signed integer that indicates the relative order of the <see cref="BigDecimal"/> values being compared.</returns>
    public int Compare(BigDecimal x, BigDecimal y)
    {
        int scale = BigDecimal.MaxScale(x, y);
        (BigInteger left, BigInteger right) = BigDecimal.NormalizeUnscaledValues(x, y);
        BigDecimal leftNormalized = new(left, scale);
        BigDecimal rightNormalized = new(right, scale);

        return leftNormalized.NumberInfo.UnscaledValue.CompareTo(rightNormalized.NumberInfo.UnscaledValue);
    }

    /// <summary>Compares two <see cref="BigDecimal"/> values and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
    /// <param name="x">The first <see cref="BigDecimal"/> value to compare.</param>
    /// <param name="y">The second <see cref="BigDecimal"/> value to compare.</param>
    /// <returns>Returns a signed integer that indicates the relative order of the <see cref="BigDecimal"/> values being compared.</returns>
    /// <exception cref="ArgumentException">If either <paramref name="x"/> or <paramref name="y"/> are not of type <see cref="BigDecimal"/>.</exception>
    public int Compare(object? x, object? y)
    {
        if (x is not BigDecimal left) throw new ArgumentException($"Argument must be of type {nameof(BigDecimal)}", nameof(x));
        if (y is not BigDecimal right) throw new ArgumentException($"Argument must be of type {nameof(BigDecimal)}", nameof(y));

        return Compare(left, right);
    }

    public bool IsEqual(BigDecimal left, BigDecimal right)
    {
        return Compare(left, right) is 0;
    }

    public bool IsGreaterThan(BigDecimal left, BigDecimal right)
    {
        return Compare(left, right) is 1;
    }

    public bool IsGreaterThanOrEqual(BigDecimal left, BigDecimal right)
    {
        return Compare(left, right) is 0 or 1;
    }

    public bool IsLessThan(BigDecimal left, BigDecimal right)
    {
        return Compare(left, right) is -1;
    }

    public bool IsLessThanOrEqual(BigDecimal left, BigDecimal right)
    {
        return Compare(left, right) is -1 or 0;
    }
}