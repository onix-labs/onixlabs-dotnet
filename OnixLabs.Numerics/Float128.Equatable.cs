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

public readonly partial struct Float128
{
    /// <summary>
    /// Compares two instances of <see cref="Float128"/> to determine whether their values are equal,
    /// using the equality semantics of <see cref="IEquatable{T}"/>.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified instances are equal; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// Negative zero is considered equal to positive zero, and any NaN value is considered equal to any other NaN value.
    /// This matches the contract of <see cref="double.Equals(double)"/>, which is appropriate for collection equality.
    /// </remarks>
    public static bool Equals(Float128 left, Float128 right)
    {
        if (IsNaNBits(left.RawBits)) return IsNaNBits(right.RawBits);
        if (IsZeroBits(left.RawBits) && IsZeroBits(right.RawBits)) return true;
        return left.RawBits == right.RawBits;
    }

    /// <summary>
    /// Compares the current instance of <see cref="Float128"/> with the specified other instance of <see cref="Float128"/>,
    /// using the equality semantics of <see cref="IEquatable{T}"/>.
    /// </summary>
    /// <param name="other">The other instance of <see cref="Float128"/> to compare with the current instance.</param>
    /// <returns>Returns <see langword="true"/> if the current instance is equal to the specified other instance; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Float128 other) => Equals(this, other);

    /// <summary>
    /// Checks for equality between this instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to this instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => obj is Float128 other && Equals(other);

    /// <summary>
    /// Serves as the hash code function for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    /// <remarks>
    /// The hash code collapses positive and negative zero to a single value, and collapses any NaN bit pattern
    /// to the canonical quiet NaN, so values that compare equal via <see cref="Equals(Float128)"/> share a hash code.
    /// </remarks>
    public override int GetHashCode()
    {
        if (IsNaNBits(RawBits)) return NaN.RawBits.GetHashCode();
        if (IsZeroBits(RawBits)) return UInt128.Zero.GetHashCode();
        return RawBits.GetHashCode();
    }

    /// <summary>
    /// Compares two instances of <see cref="Float128"/> to determine whether their values are equal,
    /// using the IEEE 754 ordering predicates.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified instances are equal under IEEE 754 semantics; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// Under IEEE 754 semantics, NaN is never equal to any value (including itself), but negative zero compares
    /// equal to positive zero. This matches the behaviour of <see cref="double.op_Equality(double, double)"/>.
    /// </remarks>
    public static bool operator ==(Float128 left, Float128 right)
    {
        if (IsNaNBits(left.RawBits) || IsNaNBits(right.RawBits)) return false;
        if (IsZeroBits(left.RawBits) && IsZeroBits(right.RawBits)) return true;
        return left.RawBits == right.RawBits;
    }

    /// <summary>
    /// Compares two instances of <see cref="Float128"/> to determine whether their values are not equal,
    /// using the IEEE 754 ordering predicates.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified instances are not equal under IEEE 754 semantics; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Float128 left, Float128 right) => !(left == right);
}
