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

namespace OnixLabs.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Compares two <see cref="BigDecimal"/> values and returns an integer that indicates
    /// whether the left-hand value is less than, equal to, or greater than the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns a value that indicates the relative order of the objects being compared.</returns>
    public static int Compare(BigDecimal left, BigDecimal right)
    {
        return BigDecimalOrdinalComparer.Default.Compare(left, right);
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
        return BigDecimalOrdinalComparer.Default.Compare(this, obj);
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
        return Compare(this, other);
    }

    /// <summary>
    /// Determines whether the left-hand value is greater than the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns True if the left-hand operand is greater than right-hand operand; otherwise, false.</returns>
    public static bool operator >(BigDecimal left, BigDecimal right)
    {
        return BigDecimalOrdinalComparer.Default.IsGreaterThan(left, right);
    }

    /// <summary>
    /// Determines whether the left-hand value is greater than or equal to the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns true if the left-hand operand is greater than or equal to the right-hand operand; otherwise, false.</returns>
    public static bool operator >=(BigDecimal left, BigDecimal right)
    {
        return BigDecimalOrdinalComparer.Default.IsGreaterThanOrEqual(left, right);
    }

    /// <summary>
    /// Determines whether the left-hand value is less than right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns true if the left-hand operand is less than the right-hand operand; otherwise, false.</returns>
    public static bool operator <(BigDecimal left, BigDecimal right)
    {
        return BigDecimalOrdinalComparer.Default.IsLessThan(left, right);
    }

    /// <summary>
    /// Determines whether the left-hand value is less than or equal to the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns true if the left-hand operand is less than or equal to the right-hand operand; otherwise, false.</returns>
    public static bool operator <=(BigDecimal left, BigDecimal right)
    {
        return BigDecimalOrdinalComparer.Default.IsLessThanOrEqual(left, right);
    }
}
