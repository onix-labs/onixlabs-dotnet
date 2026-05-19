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
/// Hand-curated regression cases for <see cref="UInt256"/> division targeting Knuth Algorithm D
/// pitfalls (TAOCP Vol 2, §4.3.1): divisors close to a word boundary, quotient-estimate correction
/// paths, leading-all-ones divisors. Each case is labelled with the edge it pins so a future
/// generator change cannot lose the coverage silently.
/// </summary>
public sealed class UInt256KnuthDivisionEdgeCaseTests
{
    public static TheoryData<string, UInt256, UInt256> Cases() => new()
    {
        // Divisor = MaxValue (all bits set across both halves) — q̂ correction path.
        { "divisor = MaxValue, dividend = MaxValue", UInt256.MaxValue, UInt256.MaxValue },
        { "divisor = MaxValue, dividend = MaxValue - 1", UInt256.MaxValue - UInt256.One, UInt256.MaxValue },

        // Divisor = UInt128.MaxValue (= 2^128 - 1) — regression for the UInt256.DivRemBy128 shift-overflow
        // bug surfaced by the cross-validation suite. Working accumulator's top bit was lost on every
        // shift, silently zeroing the quotient when divisor was close to 2^128.
        { "divisor = 2^128 - 1, mid-range dividend (regression for DivRemBy128 shift-overflow)",
          new UInt256(new UInt128(0x1234_5678_9ABC_DEF0UL, 0xFEDC_BA98_7654_3210UL),
                      new UInt128(0x1111_2222_3333_4444UL, 0x5555_6666_7777_8888UL)),
          new UInt256(UInt128.Zero, UInt128.MaxValue) },

        // Divisor with top bit set, all other bits zero — boundary of 256-bit space.
        { "divisor = 2^255, dividend = MaxValue", UInt256.MaxValue, UInt256.One << 255 },

        // Divisor = 2^128 (one beyond UInt128.MaxValue) — exercises the "divisor straddles word boundary" path.
        { "divisor = 2^128 exactly, large dividend", UInt256.MaxValue, UInt256.One << 128 },

        // Dividend == divisor and dividend == divisor + 1 — trivial-quotient boundaries.
        { "dividend == divisor (Q=1, R=0)", UInt256.MaxValue, UInt256.MaxValue },
        { "dividend == divisor + 1 (Q=1, R=1)", UInt256.MaxValue, UInt256.MaxValue - UInt256.One },
        { "dividend == divisor - 1 (Q=0, R=dividend)", UInt256.MaxValue - UInt256.One, UInt256.MaxValue },

        // Knuth's q̂ overestimate path: top digit of dividend equals top digit of divisor.
        // For the bit-by-bit fallback this exercises the long-subtract correction.
        { "Knuth q̂: high words equal",
          new UInt256(new UInt128(0xFFFF_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_0000UL), UInt128.Zero),
          new UInt256(new UInt128(0xFFFF_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_0001UL), UInt128.One) },

        // Single-word divisor (LowerBits only, UpperBits = 0) but not a power of two — exercises
        // the 128-bit fast path inside DivRem.
        { "divisor fits in low 64 bits", UInt256.MaxValue, (UInt256)0x9E37_79B9_7F4A_7C15UL },

        // Divisor = 1 — identity.
        { "divisor = 1 (Q == dividend)", UInt256.MaxValue, UInt256.One }
    };

    [Theory(DisplayName = "UInt256: Knuth-class division edge cases match BigInteger oracle")]
    [MemberData(nameof(Cases))]
    public void DivisionMatchesOracle(string label, UInt256 a, UInt256 b)
    {
        _ = label;
        BigInteger expectedQuotient = (BigInteger)a / (BigInteger)b;
        BigInteger expectedRemainder = (BigInteger)a % (BigInteger)b;
        Assert.Equal(expectedQuotient, (BigInteger)(a / b));
        Assert.Equal(expectedRemainder, (BigInteger)(a % b));
    }
}
