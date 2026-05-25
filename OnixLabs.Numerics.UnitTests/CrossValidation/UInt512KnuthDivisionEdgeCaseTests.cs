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

using System.Numerics;

namespace OnixLabs.Numerics.UnitTests.CrossValidation;

/// <summary>
/// Hand-curated regression cases for <see cref="UInt512"/> division targeting Knuth Algorithm D
/// pitfalls. Complements the DivRemBy256-focused regression cases in
/// <c>UInt512ArithmeticDivisionTests</c> by exercising the general <c>operator /</c> and
/// <c>operator %</c> entry points.
/// </summary>
public sealed class UInt512KnuthDivisionEdgeCaseTests
{
    public static TheoryData<string, UInt512, UInt512> Cases() => new()
    {
        // Divisor = MaxValue across both halves.
        { "divisor = MaxValue, dividend = MaxValue", UInt512.MaxValue, UInt512.MaxValue },
        { "divisor = MaxValue, dividend = MaxValue - 1", UInt512.MaxValue - UInt512.One, UInt512.MaxValue },

        // Divisor = UInt256.MaxValue (= 2^256 - 1) — analog of the UInt256 shift-overflow case in
        // the larger type. UInt512.DivRemBy256 was patched in commit 9e88719 for exactly this band.
        { "divisor = 2^256 - 1 (regression band for DivRemBy256 shift-overflow)",
          UInt512.MaxValue - UInt512.One,
          new UInt512(UInt256.Zero, UInt256.MaxValue) },

        // Divisor with top bit set, all other bits zero.
        { "divisor = 2^511 (sign-bit-only mass)", UInt512.MaxValue, UInt512.One << 511 },

        // Divisor straddles the word boundary.
        { "divisor = 2^256 exactly", UInt512.MaxValue, UInt512.One << 256 },

        // Trivial-quotient boundaries.
        { "dividend == divisor (Q=1, R=0)", UInt512.MaxValue, UInt512.MaxValue },
        { "dividend == divisor + 1 (Q=1, R=1)", UInt512.MaxValue, UInt512.MaxValue - UInt512.One },
        { "dividend == divisor - 1 (Q=0, R=dividend)", UInt512.MaxValue - UInt512.One, UInt512.MaxValue },

        // Knuth's q̂ overestimate case: top 256-bit word of dividend equals top word of divisor.
        { "Knuth q̂: high words equal",
          new UInt512(UInt256.MaxValue - (UInt256)16U, UInt256.Zero),
          new UInt512(UInt256.MaxValue - (UInt256)16U, UInt256.One) },

        // Single-word divisor that's prime-like (no special structure).
        { "divisor fits in low 64 bits", UInt512.MaxValue, (UInt512)0x9E37_79B9_7F4A_7C15UL },

        // Divisor = 1 (identity).
        { "divisor = 1 (Q == dividend)", UInt512.MaxValue, UInt512.One }
    };

    [Theory(DisplayName = "UInt512: Knuth-class division edge cases match BigInteger oracle")]
    [MemberData(nameof(Cases))]
    public void DivisionMatchesOracle(string label, UInt512 a, UInt512 b)
    {
        _ = label;
        BigInteger expectedQuotient = (BigInteger)a / (BigInteger)b;
        BigInteger expectedRemainder = (BigInteger)a % (BigInteger)b;
        Assert.Equal(expectedQuotient, (BigInteger)(a / b));
        Assert.Equal(expectedRemainder, (BigInteger)(a % b));
    }
}
