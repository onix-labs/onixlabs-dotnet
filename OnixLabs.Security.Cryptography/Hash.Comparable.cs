// Copyright 2020 ONIXLabs
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

using System.Numerics;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hash
{
    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
    /// other object.
    /// </summary>
    /// <param name="other">An object to compare with this instance.</param>
    /// <returns>Returns a value that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(Hash other)
    {
        BigInteger left = new(value);
        BigInteger right = new(other.value);

        return left.CompareTo(right);
    }

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
    /// other object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>Returns a value that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(object? obj) => this.CompareToObject(obj);

    /// <summary>
    /// Performs a greater than comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is greater than the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator >(Hash left, Hash right) => left.CompareTo(right) is 1;

    /// <summary>
    /// Performs a greater than or equal comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is greater than or equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator >=(Hash left, Hash right) => left.CompareTo(right) is 0 or 1;

    /// <summary>
    /// Performs a less than comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is less than the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator <(Hash left, Hash right) => left.CompareTo(right) is -1;

    /// <summary>
    /// Performs a less than or equal comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is less than or equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator <=(Hash left, Hash right) => left.CompareTo(right) is 0 or -1;
}
