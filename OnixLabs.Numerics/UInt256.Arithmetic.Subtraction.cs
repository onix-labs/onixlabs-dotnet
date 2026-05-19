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
using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct UInt256
{
    /// <summary>Computes the difference of two <see cref="UInt256"/> values, wrapping on underflow.</summary>
    /// <param name="left">The minuend.</param>
    /// <param name="right">The subtrahend.</param>
    /// <returns>Returns the wrapping difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static UInt256 operator -(UInt256 left, UInt256 right)
    {
        UInt128 newLower = left.LowerBits - right.LowerBits;
        UInt128 borrow = left.LowerBits < right.LowerBits ? UInt128.One : UInt128.Zero;
        UInt128 newUpper = left.UpperBits - right.UpperBits - borrow;
        return new UInt256(newUpper, newLower);
    }

    /// <summary>Computes the difference of two <see cref="UInt256"/> values, throwing on underflow.</summary>
    /// <param name="left">The minuend.</param>
    /// <param name="right">The subtrahend.</param>
    /// <returns>Returns the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the difference would underflow below zero.</exception>
    public static UInt256 operator checked -(UInt256 left, UInt256 right)
    {
        if (left < right) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        return left - right;
    }
}
