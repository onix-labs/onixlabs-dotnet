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

namespace OnixLabs.Numerics.UnitTests.CrossValidation;

/// <summary>
/// Hand-curated regression cases for <see cref="Int512"/> division. Mirrors the unsigned cases
/// plus signed concerns: sign of remainder, MinValue/-1 overflow under checked arithmetic.
/// </summary>
public sealed class Int512KnuthDivisionEdgeCaseTests
{
    public static TheoryData<string, Int512, Int512> Cases() => new()
    {
        // Signed boundaries.
        { "dividend = MaxValue, divisor = 2", Int512.MaxValue, (Int512)2 },
        { "dividend = MinValue, divisor = 2", Int512.MinValue, (Int512)2 },
        { "dividend = MinValue + 1, divisor = -1 (no overflow)", Int512.MinValue + Int512.One, Int512.NegativeOne },

        // Sign-of-remainder rules — C# truncates toward zero.
        { "negative / positive => negative Q, negative R", (Int512)(-17), (Int512)5 },
        { "positive / negative => negative Q, positive R", (Int512)17, (Int512)(-5) },
        { "negative / negative => positive Q, negative R", (Int512)(-17), (Int512)(-5) },

        // Divisor close to UInt256.MaxValue — analog of the regression band.
        { "divisor = 2^256 - 1 (positive form)",
          Int512.MaxValue / (Int512)2,
          new Int512(UInt256.Zero, UInt256.MaxValue) },

        // Word-boundary divisor.
        { "divisor = 2^256 exactly", Int512.MaxValue, Int512.One << 256 },

        // Knuth q̂ correction at the 512-bit scale.
        { "Knuth q̂: high words equal (positive)",
          new Int512(new UInt256(new UInt128(0x7FFF_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL), UInt128.MaxValue - UInt128.One), UInt256.Zero),
          new Int512(new UInt256(new UInt128(0x7FFF_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL), UInt128.MaxValue - UInt128.One), UInt256.One) }
    };

    [Theory(DisplayName = "Int512: Knuth-class division edge cases match BigInteger oracle (truncating toward zero)")]
    [MemberData(nameof(Cases))]
    public void DivisionMatchesOracle(string label, Int512 a, Int512 b)
    {
        _ = label;
        BigInteger expectedQuotient = (BigInteger)a / (BigInteger)b;
        BigInteger expectedRemainder = (BigInteger)a % (BigInteger)b;
        Assert.Equal(expectedQuotient, (BigInteger)(a / b));
        Assert.Equal(expectedRemainder, (BigInteger)(a % b));
    }

    [Fact(DisplayName = "Int512: checked MinValue / -1 throws OverflowException")]
    public void CheckedMinValueDividedByNegativeOneThrows()
    {
        Assert.Throws<OverflowException>(() => checked(Int512.MinValue / Int512.NegativeOne));
    }
}
