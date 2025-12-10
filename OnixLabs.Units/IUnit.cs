// Copyright 2020-2025 ONIXLabs
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

namespace OnixLabs.Units;

/// <summary>
/// Defines a unit of measurement.
/// </summary>
/// <typeparam name="TSelf">The underlying type of the unit of measurement.</typeparam>
public interface IUnit<TSelf> : IEquatable<TSelf>, IComparable<TSelf>, IComparable, ISpanFormattable where TSelf : struct
{
    /// <summary>
    /// Gets a zero <c>0</c> <typeparamref name="TSelf"/> value.
    /// </summary>
    static abstract TSelf Zero { get; }

    /// <summary>
    /// Computes the sum of the specified <typeparamref name="TSelf"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <typeparamref name="TSelf"/> values.</returns>
    static abstract TSelf Add(TSelf left, TSelf right);

    /// <summary>
    /// Computes the difference between the specified <typeparamref name="TSelf"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the specified <typeparamref name="TSelf"/> values.</returns>
    static abstract TSelf Subtract(TSelf left, TSelf right);

    /// <summary>
    /// Computes the product of the specified <typeparamref name="TSelf"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply.</param>
    /// <param name="right">The right-hand value multiply by.</param>
    /// <returns>Returns the product of the specified <typeparamref name="TSelf"/> values.</returns>
    static abstract TSelf Multiply(TSelf left, TSelf right);

    /// <summary>
    /// Computes the quotient of the specified <typeparamref name="TSelf"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <typeparamref name="TSelf"/> values.</returns>
    static abstract TSelf Divide(TSelf left, TSelf right);

    /// <summary>
    /// Determines whether the specified left-hand and right-hand <typeparamref name="TSelf"/> values are equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the specified left-hand and right-hand <typeparamref name="TSelf"/> values are equal; otherwise <see langword="false"/>.</returns>
    static abstract bool Equals(TSelf left, TSelf right);

    /// <summary>
    /// Compares the specified left-hand and right-hand <typeparamref name="TSelf"/> values and returns an integer
    /// that indicates whether the left-hand value is less than, equal to, or greater than the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns an integer value that indicates the relative order of the left-hand and right-hand values being compared.
    /// The return value is less than zero if <paramref name="left"/> is less than <paramref name="right"/>,
    /// zero if <paramref name="left"/> is equal to <paramref name="right"/>,
    /// or greater than zero if <paramref name="left"/> is greater than <paramref name="right"/>.
    /// </returns>
    static abstract int Compare(TSelf left, TSelf right);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null);
}
