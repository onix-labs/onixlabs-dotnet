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
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with the current object.</param>
    /// <returns>Returns true if the current object is equal to the other parameter; otherwise, false.</returns>
    public bool Equals(NumberInfo other)
    {
        return other.Scale == Scale
               && other.Exponent == Exponent
               && other.Precision == Precision
               && other.Sign == Sign
               && other.UnscaledValue == UnscaledValue
               && other.Significand == Significand;
    }

    /// <summary>
    /// Checks for equality between the current instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns true if the object is equal to the current instance; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is NumberInfo other && Equals(other);
    }

    /// <summary>
    /// Serves as a hash code function for the current instance.
    /// </summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(UnscaledValue, Scale, Significand, Exponent, Precision, Sign);
    }

    /// <summary>
    /// Performs an equality check between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns true if the instances are equal; otherwise, false.</returns>
    public static bool operator ==(NumberInfo left, NumberInfo right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Performs an inequality check between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns true if the instances are not equal; otherwise, false.</returns>
    public static bool operator !=(NumberInfo left, NumberInfo right)
    {
        return !Equals(left, right);
    }
}
