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

public readonly partial struct Int512
{
    /// <summary>Computes the remainder of two <see cref="Int512"/> values, following C# integer modulus rules (sign of dividend).</summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns the remainder.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="right"/> is zero.</exception>
    public static Int512 operator %(Int512 left, Int512 right)
    {
        if (IsZero(right)) throw new DivideByZeroException();
        if (left == MinValue && right == NegativeOne) return Zero;

        (_, Int512 remainder) = DivRem(left, right);
        return remainder;
    }
}
