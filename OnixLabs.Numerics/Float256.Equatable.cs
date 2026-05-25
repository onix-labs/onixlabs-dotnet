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

public readonly partial struct Float256
{
    /// <summary>
    /// Compares two instances of <see cref="Float256"/> to determine whether their values are equal, using the equality semantics of <see cref="IEquatable{T}"/>.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified instances are equal; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// Negative zero is considered equal to positive zero, and any NaN value is considered equal to any other NaN value.
    /// </remarks>
    public static bool Equals(Float256 left, Float256 right)
    {
        if (IsNaNBits(left.Bits)) return IsNaNBits(right.Bits);
        if (IsZeroBits(left.Bits) && IsZeroBits(right.Bits)) return true;
        return left.Bits == right.Bits;
    }

    /// <summary>
    /// Compares the current instance of <see cref="Float256"/> with the specified other instance of <see cref="Float256"/>.
    /// </summary>
    /// <param name="other">The other instance of <see cref="Float256"/> to compare with the current instance.</param>
    /// <returns>Returns <see langword="true"/> if the current instance is equal to the specified other instance; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Float256 other) => Equals(this, other);

    /// <summary>
    /// Checks for equality between this instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to this instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => obj is Float256 other && Equals(other);

    /// <summary>
    /// Serves as the hash code function for this instance.
    /// </summary>
    /// <returns>Returns a hash code for this instance.</returns>
    public override int GetHashCode()
    {
        if (IsNaNBits(Bits)) return NaN.Bits.GetHashCode();
        if (IsZeroBits(Bits)) return UInt256.Zero.GetHashCode();
        return Bits.GetHashCode();
    }

    /// <summary>
    /// Compares two instances of <see cref="Float256"/> to determine whether their values are equal, using the IEEE 754 ordering predicates.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified instances are equal under IEEE 754 semantics; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Float256 left, Float256 right)
    {
        if (IsNaNBits(left.Bits) || IsNaNBits(right.Bits)) return false;
        if (IsZeroBits(left.Bits) && IsZeroBits(right.Bits)) return true;
        return left.Bits == right.Bits;
    }

    /// <summary>
    /// Compares two instances of <see cref="Float256"/> to determine whether their values are not equal, using the IEEE 754 ordering predicates.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified instances are not equal under IEEE 754 semantics; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Float256 left, Float256 right) => !(left == right);
}
