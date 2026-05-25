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

public readonly partial struct Int256
{
    /// <summary>Compares two instances of <see cref="Int256"/> to determine whether their values are equal.</summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified instances are equal; otherwise, <see langword="false"/>.</returns>
    public static bool Equals(Int256 left, Int256 right) => left.UpperBits == right.UpperBits && left.LowerBits == right.LowerBits;

    /// <summary>Compares the current instance of <see cref="Int256"/> with the specified other instance of <see cref="Int256"/>.</summary>
    /// <param name="other">The other instance of <see cref="Int256"/> to compare with the current instance.</param>
    /// <returns>Returns <see langword="true"/> if the current instance is equal to the specified other instance; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Int256 other) => Equals(this, other);

    /// <summary>Checks for equality between this instance and another object.</summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to this instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => obj is Int256 other && Equals(other);

    /// <summary>Serves as the hash code function for this instance.</summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    /// <remarks>
    /// Hashes every byte of the 256-bit representation rather than combining the upper and lower
    /// <see cref="UInt128"/> hashes: <see cref="ulong.GetHashCode"/> XORs its two 32-bit halves, which
    /// causes pairs like <c>2^k</c> and <c>2^(k+32)</c> to collide.
    /// </remarks>
    public override int GetHashCode()
    {
        Span<byte> buffer = stackalloc byte[32];
        TryWriteLittleEndian(buffer, out _);
        HashCode hash = new();
        hash.AddBytes(buffer);
        return hash.ToHashCode();
    }

    /// <summary>Determines whether the two values are equal.</summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the values are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Int256 left, Int256 right) => Equals(left, right);

    /// <summary>Determines whether the two values are not equal.</summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the values are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Int256 left, Int256 right) => !Equals(left, right);
}
