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

namespace OnixLabs.Numerics;

public readonly partial struct NumberInfo : IEquatable<NumberInfo>
{
    /// <summary>
    /// Compares two instances of <see cref="NumberInfo"/> to determine whether their values are equal.
    /// This method implements the <see cref="NumberInfoEqualityComparer.Strict"/> comparer.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns true if the two specified instances are equal; otherwise, false.</returns>
    public static bool Equals(NumberInfo left, NumberInfo right)
    {
        return Equals(left, right, NumberInfoEqualityComparer.Strict);
    }

    /// <summary>
    /// Compares two instances of <see cref="NumberInfo"/> to determine whether their values are equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <param name="comparer">The equality comparer to use to determine equality.</param>
    /// <returns>Returns true if the two specified instances are equal; otherwise, false.</returns>
    public static bool Equals(NumberInfo left, NumberInfo right, NumberInfoEqualityComparer comparer)
    {
        return comparer.Equals(left, right);
    }

    /// <summary>
    /// Compares the current instance of <see cref="NumberInfo"/> with the specified other instance of <see cref="NumberInfo"/>.
    /// This method implements the <see cref="NumberInfoEqualityComparer.Strict"/> comparer.
    /// </summary>
    /// <param name="other">The other instance of <see cref="NumberInfo"/> to compare with the current instance.</param>
    /// <returns>Returns true if the current instance is equal to the specified other instance; otherwise, false.</returns>
    public bool Equals(NumberInfo other)
    {
        return Equals(this, other);
    }

    /// <summary>
    /// Compares the current instance of <see cref="NumberInfo"/> with the specified other instance of <see cref="NumberInfo"/>.
    /// </summary>
    /// <param name="other">The other instance of <see cref="NumberInfo"/> to compare with the current instance.</param>
    /// <param name="comparer">The equality comparer to use to determine equality.</param>
    /// <returns>Returns true if the current instance is equal to the specified other instance; otherwise, false.</returns>
    public bool Equals(NumberInfo other, NumberInfoEqualityComparer comparer)
    {
        return Equals(this, other, comparer);
    }

    /// <summary>
    /// Checks for equality between this instance and another object.
    /// This method implements the <see cref="NumberInfoEqualityComparer.Strict"/> comparer.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns true if the object is equal to this instance; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is NumberInfo other && Equals(other);
    }

    /// <summary>
    /// Checks for equality between this instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <param name="comparer">The equality comparer to use to determine equality.</param>
    /// <returns>Returns true if the object is equal to this instance; otherwise, false.</returns>
    public bool Equals(object? obj, NumberInfoEqualityComparer comparer)
    {
        return obj is NumberInfo other && Equals(other, comparer);
    }

    /// <summary>
    /// Serves as a hash code function for this instance.
    /// This method implements the <see cref="NumberInfoEqualityComparer.Strict"/> comparer.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return NumberInfoEqualityComparer.Strict.GetHashCode(this);
    }

    /// <summary>
    /// Compares two instances of <see cref="NumberInfo"/> to determine whether their values are equal.
    /// This method implements the <see cref="NumberInfoEqualityComparer.Semantic"/> comparer.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns true if the two specified instances are equal; otherwise, false.</returns>
    public static bool operator ==(NumberInfo left, NumberInfo right)
    {
        return Equals(left, right, NumberInfoEqualityComparer.Semantic);
    }

    /// <summary>
    /// Compares two instances of <see cref="NumberInfo"/> to determine whether their values are not equal.
    /// This method implements the <see cref="NumberInfoEqualityComparer.Semantic"/> comparer.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns true if the two specified instances are not equal; otherwise, false.</returns>
    public static bool operator !=(NumberInfo left, NumberInfo right)
    {
        return Equals(left, right, NumberInfoEqualityComparer.Semantic);
    }
}
