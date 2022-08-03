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

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal : IComparable, IComparable<BigDecimal>
{
    /// <summary>
    /// Performs a greater than comparison check between two instances of <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>True if a is greater than b, otherwise False.</returns>
    public static bool operator >(BigDecimal left, BigDecimal right)
    {
        return Compare(left, right) is 1;
    }

    /// <summary>
    /// Performs a greater than or equal to comparison check between two instances of <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>True if a is greater than or equal to b, otherwise False.</returns>
    public static bool operator >=(BigDecimal left, BigDecimal right)
    {
        return Compare(left, right) is 1 or 0;
    }

    /// <summary>
    /// Performs a less than comparison check between two instances of <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>True if a is less than b, otherwise False.</returns>
    public static bool operator <(BigDecimal left, BigDecimal right)
    {
        return Compare(left, right) is -1;
    }

    /// <summary>
    /// Performs a less than or equal to comparison check between two instances of <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>True if a is less than or equal to b, otherwise False.</returns>
    public static bool operator <=(BigDecimal left, BigDecimal right)
    {
        return Compare(left, right) is -1 or 0;
    }

    /// <summary>
    /// Performs a greater than comparison check between an instance of <see cref="BigDecimal"/> and <see cref="decimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>True if a is greater than b, otherwise False.</returns>
    public static bool operator >(BigDecimal left, decimal right)
    {
        return Compare(left, right.ToBigDecimal()) is 1;
    }

    /// <summary>
    /// Performs a greater than or equal to comparison check between an instance of <see cref="BigDecimal"/> and <see cref="decimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>True if a is greater than or equal to b, otherwise False.</returns>
    public static bool operator >=(BigDecimal left, decimal right)
    {
        return Compare(left, right.ToBigDecimal()) is 1 or 0;
    }

    /// <summary>
    /// Performs a less than comparison check between an instance of <see cref="BigDecimal"/> and <see cref="decimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>True if a is less than b, otherwise False.</returns>
    public static bool operator <(BigDecimal left, decimal right)
    {
        return Compare(left, right.ToBigDecimal()) is -1;
    }

    /// <summary>
    /// Performs a less than or equal to comparison check between an instance of <see cref="BigDecimal"/> and <see cref="decimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>True if a is less than or equal to b, otherwise False.</returns>
    public static bool operator <=(BigDecimal left, decimal right)
    {
        return Compare(left, right.ToBigDecimal()) is -1 or 0;
    }

    /// <summary>
    /// Performs a greater than comparison check between an instance of <see cref="decimal"/> and <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>True if a is greater than b, otherwise False.</returns>
    public static bool operator >(decimal left, BigDecimal right)
    {
        return Compare(left.ToBigDecimal(), right) is 1;
    }

    /// <summary>
    /// Performs a greater than or equal to comparison check between an instance of <see cref="decimal"/> and <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>True if a is greater than or equal to b, otherwise False.</returns>
    public static bool operator >=(decimal left, BigDecimal right)
    {
        return Compare(left.ToBigDecimal(), right) is 1 or 0;
    }

    /// <summary>
    /// Performs a less than comparison check between an instance of <see cref="decimal"/> and <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>True if a is less than b, otherwise False.</returns>
    public static bool operator <(decimal left, BigDecimal right)
    {
        return Compare(left.ToBigDecimal(), right) is -1;
    }

    /// <summary>
    /// Performs a less than or equal to comparison check between an instance of <see cref="decimal"/> and <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>True if a is less than or equal to b, otherwise False.</returns>
    public static bool operator <=(decimal left, BigDecimal right)
    {
        return Compare(left.ToBigDecimal(), right) is -1 or 0;
    }

    /// <summary>
    /// Compares two <see cref="BigDecimal"/> values and returns an integer that indicates
    /// whether the first value is less than, equal to, or greater than the second value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>Returns a value that indicates the relative order of the objects being compared.</returns>
    public static int Compare(BigDecimal left, BigDecimal right)
    {
        return left.CompareTo(right);
    }

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
    /// other object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>Returns a value that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(object? obj)
    {
        return this.CompareObject(obj);
    }

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
    /// other object.
    /// </summary>
    /// <param name="other">An object to compare with this instance.</param>
    /// <returns>Returns a value that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(BigDecimal other)
    {
        (BigDecimal left, BigDecimal right) = Balance(this, other);
        return left.UnscaledValue.CompareTo(right.UnscaledValue);
    }
}
