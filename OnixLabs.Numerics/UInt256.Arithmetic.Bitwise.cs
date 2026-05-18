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

public readonly partial struct UInt256
{
    /// <summary>Computes the bitwise AND of two <see cref="UInt256"/> values.</summary>
    /// <param name="left">The first operand.</param>
    /// <param name="right">The second operand.</param>
    /// <returns>Returns the bitwise AND of the operands.</returns>
    public static UInt256 operator &(UInt256 left, UInt256 right) => new(left.upper & right.upper, left.lower & right.lower);

    /// <summary>Computes the bitwise OR of two <see cref="UInt256"/> values.</summary>
    /// <param name="left">The first operand.</param>
    /// <param name="right">The second operand.</param>
    /// <returns>Returns the bitwise OR of the operands.</returns>
    public static UInt256 operator |(UInt256 left, UInt256 right) => new(left.upper | right.upper, left.lower | right.lower);

    /// <summary>Computes the bitwise XOR of two <see cref="UInt256"/> values.</summary>
    /// <param name="left">The first operand.</param>
    /// <param name="right">The second operand.</param>
    /// <returns>Returns the bitwise XOR of the operands.</returns>
    public static UInt256 operator ^(UInt256 left, UInt256 right) => new(left.upper ^ right.upper, left.lower ^ right.lower);

    /// <summary>Computes the bitwise complement of a <see cref="UInt256"/> value.</summary>
    /// <param name="value">The operand.</param>
    /// <returns>Returns the bitwise complement of <paramref name="value"/>.</returns>
    public static UInt256 operator ~(UInt256 value) => new(~value.upper, ~value.lower);
}
