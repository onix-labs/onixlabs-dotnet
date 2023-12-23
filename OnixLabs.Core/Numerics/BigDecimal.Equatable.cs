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

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Compares two instances of <see cref="BigDecimal"/> to determine whether their values are equal.
    /// This method implements strict equality, meaning that both the <see cref="UnscaledValue"/> and <see cref="Scale"/> will be compared.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns true if the two specified instances are equal; otherwise, false.</returns>
    public static bool Equals(BigDecimal left, BigDecimal right)
    {
        return BigDecimalEqualityComparer.Strict.Equals(left, right);
    }

    /// <summary>
    /// Compares two instances of <see cref="BigDecimal"/> to determine whether their values are equal.
    /// This method implements value equality, meaning that <see cref="Scale"/> is effectively ignored.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns true if the two specified instances are equal; otherwise, false.</returns>
    public static bool ValueEquals(BigDecimal left, BigDecimal right)
    {
        return BigDecimalEqualityComparer.Semantic.Equals(left, right);
    }

    /// <summary>
    /// Compares two instances of <see cref="BigDecimal"/> to determine whether their values are equal.
    /// This method implements value equality, meaning that <see cref="Scale"/> is effectively ignored.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns true if the two specified instances are equal; otherwise, false.</returns>
    public static bool operator ==(BigDecimal left, BigDecimal right)
    {
        return ValueEquals(left, right);
    }

    /// <summary>
    /// Compares two instances of <see cref="BigDecimal"/> to determine whether their values are not equal.
    /// This method implements value equality, meaning that <see cref="Scale"/> is effectively ignored.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns true if the two specified instances are not equal; otherwise, false.</returns>
    public static bool operator !=(BigDecimal left, BigDecimal right)
    {
        return !ValueEquals(left, right);
    }

    /// <summary>
    /// Compares the current instance of <see cref="BigDecimal"/> with the specified other instance of <see cref="BigDecimal"/>.
    /// This method implements strict equality, meaning that both the <see cref="UnscaledValue"/> and <see cref="Scale"/> will be compared.
    /// </summary>
    /// <param name="other">The other instance of <see cref="BigDecimal"/> to compare with the current instance.</param>
    /// <returns>Returns true if the current instance is equal to the specified other instance; otherwise, false.</returns>
    public bool Equals(BigDecimal other)
    {
        return Equals(this, other);
    }

    /// <summary>
    /// Checks for equality between this instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>true if the object is equal to this instance; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is BigDecimal other && Equals(other);
    }

    /// <summary>
    /// Compares the current instance of <see cref="BigDecimal"/> with the specified other instance of <see cref="BigDecimal"/>.
    /// This method implements value equality, meaning that <see cref="Scale"/> is effectively ignored.
    /// </summary>
    /// <param name="other">The other instance of <see cref="BigDecimal"/> to compare with the current instance.</param>
    /// <returns>Returns true if the current instance is equal to the specified other instance; otherwise, false.</returns>
    public bool ValueEquals(BigDecimal other)
    {
        return ValueEquals(this, other);
    }

    /// <summary>
    /// Serves as a hash code function for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(UnscaledValue, Scale);
    }
}
