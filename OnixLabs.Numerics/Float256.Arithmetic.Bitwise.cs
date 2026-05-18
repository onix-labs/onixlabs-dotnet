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

namespace OnixLabs.Numerics;

public readonly partial struct Float256
{
    /// <summary>Computes the bitwise AND of the IEEE 754 binary256 representations.</summary>
    /// <param name="left">The first operand.</param>
    /// <param name="right">The second operand.</param>
    /// <returns>Returns the <see cref="Float256"/> value whose raw bits are <c>left.RawBits &amp; right.RawBits</c>.</returns>
    public static Float256 operator &(Float256 left, Float256 right) => new(left.Bits & right.Bits);

    /// <summary>Computes the bitwise OR of the IEEE 754 binary256 representations.</summary>
    /// <param name="left">The first operand.</param>
    /// <param name="right">The second operand.</param>
    /// <returns>Returns the <see cref="Float256"/> value whose raw bits are <c>left.RawBits | right.RawBits</c>.</returns>
    public static Float256 operator |(Float256 left, Float256 right) => new(left.Bits | right.Bits);

    /// <summary>Computes the bitwise XOR of the IEEE 754 binary256 representations.</summary>
    /// <param name="left">The first operand.</param>
    /// <param name="right">The second operand.</param>
    /// <returns>Returns the <see cref="Float256"/> value whose raw bits are <c>left.RawBits ^ right.RawBits</c>.</returns>
    public static Float256 operator ^(Float256 left, Float256 right) => new(left.Bits ^ right.Bits);

    /// <summary>Computes the bitwise complement of the IEEE 754 binary256 representation.</summary>
    /// <param name="value">The operand.</param>
    /// <returns>Returns the <see cref="Float256"/> value whose raw bits are <c>~value.RawBits</c>.</returns>
    public static Float256 operator ~(Float256 value) => new(~value.Bits);
}
