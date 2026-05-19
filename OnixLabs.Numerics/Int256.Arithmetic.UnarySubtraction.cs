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
    /// <summary>Computes the two's-complement negation of the specified <see cref="Int256"/> value.</summary>
    /// <param name="value">The value to negate.</param>
    /// <returns>Returns the negation; <see cref="MinValue"/> negated is itself due to two's-complement asymmetry.</returns>
    public static Int256 operator -(Int256 value) => Zero - value;

    /// <summary>Computes the negation of the specified <see cref="Int256"/> value, throwing on overflow.</summary>
    /// <param name="value">The value to negate.</param>
    /// <returns>Returns the negation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> equals <see cref="MinValue"/>.</exception>
    public static Int256 operator checked -(Int256 value)
    {
        if (value == MinValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int256)}.");
        return -value;
    }
}
