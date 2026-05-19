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
/// Hand-curated regression cases for <see cref="Int256"/> division. Mirrors the unsigned Knuth
/// edge cases plus signed-only concerns: sign of remainder, MinValue/-1 overflow under checked
/// arithmetic, and mixed-sign quotient sign rules.
/// </summary>
public sealed class Int256KnuthDivisionEdgeCaseTests
{
    public static TheoryData<string, Int256, Int256> Cases() => new()
    {
        // Regression for the exact failing input the cross-validation suite found before the
        // UInt256.DivRemBy128 fix: dividend ≈ -5.4e76, divisor = UInt128.MaxValue = 2^128 - 1.
        // Int256 division returned 0 because UInt256.DivRemBy128 lost its working-accumulator top bit.
        { "regression for UInt256.DivRemBy128 shift-overflow (cross-validation discovery)",
          Int256.Parse("-54052701908063338004747292985430325837523135972101534980128614304616256933917"),
          Int256.Parse("340282366920938463463374607431768211455") },

        // Signed boundaries.
        { "dividend = MaxValue, divisor = 2", Int256.MaxValue, (Int256)2 },
        { "dividend = MinValue, divisor = 2", Int256.MinValue, (Int256)2 },
        { "dividend = MinValue + 1, divisor = -1 (no overflow)", Int256.MinValue + Int256.One, Int256.NegativeOne },

        // Sign-of-remainder cases — C# truncates toward zero so r has the sign of the dividend.
        { "negative / positive => negative quotient, negative remainder", (Int256)(-17), (Int256)5 },
        { "positive / negative => negative quotient, positive remainder", (Int256)17, (Int256)(-5) },
        { "negative / negative => positive quotient, negative remainder", (Int256)(-17), (Int256)(-5) },

        // Divisor close to UInt128.MaxValue (same shift-overflow concern as UInt256 path).
        { "divisor = 2^128 - 1 (positive form)",
          Int256.MaxValue / (Int256)2,
          new Int256(UInt128.Zero, UInt128.MaxValue) },

        // Power-of-two divisor at the half-word boundary.
        { "divisor = 2^128 exactly", Int256.MaxValue, Int256.One << 128 },

        // Knuth q̂ correction: top half of |dividend| equals top half of |divisor|.
        { "Knuth q̂: high words equal (positive)",
          new Int256(new UInt128(0x7FFF_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_0000UL), UInt128.Zero),
          new Int256(new UInt128(0x7FFF_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_0001UL), UInt128.One) }
    };

    [Theory(DisplayName = "Int256: Knuth-class division edge cases match BigInteger oracle (truncating toward zero)")]
    [MemberData(nameof(Cases))]
    public void DivisionMatchesOracle(string label, Int256 a, Int256 b)
    {
        _ = label;
        BigInteger expectedQuotient = (BigInteger)a / (BigInteger)b;
        BigInteger expectedRemainder = (BigInteger)a % (BigInteger)b;
        Assert.Equal(expectedQuotient, (BigInteger)(a / b));
        Assert.Equal(expectedRemainder, (BigInteger)(a % b));
    }

    [Fact(DisplayName = "Int256: checked MinValue / -1 throws OverflowException (signed-overflow boundary)")]
    public void CheckedMinValueDividedByNegativeOneThrows()
    {
        Assert.Throws<OverflowException>(() => checked(Int256.MinValue / Int256.NegativeOne));
    }
}
