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

public readonly partial struct UInt512
{
    /// <summary>Computes the difference of two <see cref="UInt512"/> values, wrapping on underflow.</summary>
    /// <param name="left">The minuend.</param>
    /// <param name="right">The subtrahend.</param>
    /// <returns>Returns the wrapping difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static UInt512 operator -(UInt512 left, UInt512 right)
    {
        UInt256 newLower = left.Lower - right.Lower;
        UInt256 borrow = left.Lower < right.Lower ? UInt256.One : UInt256.Zero;
        UInt256 newUpper = left.Upper - right.Upper - borrow;
        return new UInt512(newUpper, newLower);
    }

    /// <summary>Computes the difference of two <see cref="UInt512"/> values, throwing on underflow.</summary>
    /// <param name="left">The minuend.</param>
    /// <param name="right">The subtrahend.</param>
    /// <returns>Returns the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the difference would underflow below zero.</exception>
    public static UInt512 operator checked -(UInt512 left, UInt512 right)
    {
        if (left < right) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt512)}.");
        return left - right;
    }
}
