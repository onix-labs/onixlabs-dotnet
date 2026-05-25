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

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float256Ieee754Tests
{
    [Fact(DisplayName = "Float256.Ieee754Remainder should round half to even")]
    public void Float256Ieee754RemainderShouldRoundHalfToEven()
    {
        Assert.Equal(Float256.Zero, Float256.Ieee754Remainder((Float256)6, (Float256)3));
        Assert.Equal((Float256)0.5, Float256.Ieee754Remainder((Float256)3.5, (Float256)3));

        // Regression: exact even when the quotient exceeds the 237-bit significand precision.
        // 2^240 mod 3 = 1 (< 1.5) -> 1; 2^241 mod 3 = 2 (> 1.5) -> 2 - 3 = -1.
        Assert.Equal(Float256.One, Float256.Ieee754Remainder(Float256.ScaleB(Float256.One, 240), (Float256)3));
        Assert.Equal((Float256)(-1), Float256.Ieee754Remainder(Float256.ScaleB(Float256.One, 241), (Float256)3));
    }

    [Fact(DisplayName = "Float256.Ieee754Remainder of NaN should return NaN")]
    public void Float256Ieee754RemainderOfNaNShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Ieee754Remainder(Float256.NaN, Float256.One)));
        Assert.True(Float256.IsNaN(Float256.Ieee754Remainder(Float256.One, Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Ieee754Remainder by zero should return NaN")]
    public void Float256Ieee754RemainderByZeroShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Ieee754Remainder(Float256.One, Float256.Zero)));
    }

    [Fact(DisplayName = "Float256.FusedMultiplyAdd should compute single-rounded product plus addend")]
    public void Float256FusedMultiplyAddShouldComputeSingleRoundedResult()
    {
        Float256 a = Float256.Two;
        Float256 b = (Float256)3;
        Float256 c = (Float256)4;
        Assert.Equal((Float256)10, Float256.FusedMultiplyAdd(a, b, c));
    }

    [Fact(DisplayName = "Float256.FusedMultiplyAdd with NaN operand should return NaN")]
    public void Float256FusedMultiplyAddWithNaNShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.FusedMultiplyAdd(Float256.NaN, Float256.One, Float256.One)));
        Assert.True(Float256.IsNaN(Float256.FusedMultiplyAdd(Float256.One, Float256.NaN, Float256.One)));
        Assert.True(Float256.IsNaN(Float256.FusedMultiplyAdd(Float256.One, Float256.One, Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.FusedMultiplyAdd of zero and infinity should return NaN")]
    public void Float256FusedMultiplyAddOfZeroTimesInfinityShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.FusedMultiplyAdd(Float256.Zero, Float256.PositiveInfinity, Float256.One)));
        Assert.True(Float256.IsNaN(Float256.FusedMultiplyAdd(Float256.PositiveInfinity, Float256.Zero, Float256.One)));
    }

    [Fact(DisplayName = "Float256.ReciprocalEstimate of two should approximately equal one half")]
    public void Float256ReciprocalEstimateOfTwoShouldApproximatelyEqualOneHalf()
    {
        Float256 result = Float256.ReciprocalEstimate(Float256.Two);
        Assert.Equal((Float256)0.5, result);
    }

    [Fact(DisplayName = "Float256.ReciprocalSqrtEstimate of four should approximately equal one half")]
    public void Float256ReciprocalSqrtEstimateOfFourShouldApproximatelyEqualOneHalf()
    {
        Float256 result = Float256.ReciprocalSqrtEstimate((Float256)4);
        Float256 difference = Float256.Abs(result - (Float256)0.5);
        Assert.True(difference < Float256.Parse("1E-65"));
    }

    [Fact(DisplayName = "Float256.IsPow2 should return true for powers of two")]
    public void Float256IsPow2ShouldReturnTrueForPowersOfTwo()
    {
        Assert.True(Float256.IsPow2(Float256.One));
        Assert.True(Float256.IsPow2(Float256.Two));
        Assert.True(Float256.IsPow2((Float256)1024));
        Assert.True(Float256.IsPow2((Float256)0.5));
        Assert.True(Float256.IsPow2(Float256.Epsilon));
    }

    [Fact(DisplayName = "Float256.IsPow2 should return false for non-powers of two")]
    public void Float256IsPow2ShouldReturnFalseForNonPowersOfTwo()
    {
        Assert.False(Float256.IsPow2(Float256.Zero));
        Assert.False(Float256.IsPow2(Float256.NegativeZero));
        Assert.False(Float256.IsPow2(Float256.NegativeOne));
        Assert.False(Float256.IsPow2((Float256)3));
        Assert.False(Float256.IsPow2((Float256)1.5));
        Assert.False(Float256.IsPow2(Float256.NaN));
        Assert.False(Float256.IsPow2(Float256.PositiveInfinity));
        Assert.False(Float256.IsPow2(Float256.NegativeInfinity));
    }

    [Fact(DisplayName = "Float256.AllBitsSet should have every bit of the raw representation set")]
    public void Float256AllBitsSetShouldEqualUInt128MaxValue()
    {
        Assert.Equal(System.UInt128.MaxValue, Float256.AllBitsSet.Bits.UpperBits);
        Assert.Equal(System.UInt128.MaxValue, Float256.AllBitsSet.Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256 bitwise AND should AND the raw bits")]
    public void Float256BitwiseAndShouldAndTheRawBits()
    {
        UInt128 leftHigh = new(0xFF00FF00_FF00FF00UL, 0xFF00FF00_FF00FF00UL);
        UInt128 leftLow = new(0xFF00FF00_FF00FF00UL, 0xFF00FF00_FF00FF00UL);
        UInt128 rightHigh = new(0xFFFF0000_FFFF0000UL, 0xFFFF0000_FFFF0000UL);
        UInt128 rightLow = new(0xFFFF0000_FFFF0000UL, 0xFFFF0000_FFFF0000UL);
        Float256 left = new(new UInt256(leftHigh, leftLow));
        Float256 right = new(new UInt256(rightHigh, rightLow));
        Float256 result = left & right;
        Assert.Equal(leftHigh & rightHigh, result.Bits.UpperBits);
        Assert.Equal(leftLow & rightLow, result.Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256 bitwise OR should OR the raw bits")]
    public void Float256BitwiseOrShouldOrTheRawBits()
    {
        UInt128 leftHigh = new(0x0000FFFF_0000FFFFUL, 0x0000FFFF_0000FFFFUL);
        UInt128 leftLow = new(0x0000FFFF_0000FFFFUL, 0x0000FFFF_0000FFFFUL);
        UInt128 rightHigh = new(0xFFFF0000_FFFF0000UL, 0xFFFF0000_FFFF0000UL);
        UInt128 rightLow = new(0xFFFF0000_FFFF0000UL, 0xFFFF0000_FFFF0000UL);
        Float256 left = new(new UInt256(leftHigh, leftLow));
        Float256 right = new(new UInt256(rightHigh, rightLow));
        Float256 result = left | right;
        Assert.Equal(leftHigh | rightHigh, result.Bits.UpperBits);
        Assert.Equal(leftLow | rightLow, result.Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256 bitwise XOR should XOR the raw bits")]
    public void Float256BitwiseXorShouldXorTheRawBits()
    {
        UInt128 leftHigh = new(0xAAAAAAAA_AAAAAAAAUL, 0xAAAAAAAA_AAAAAAAAUL);
        UInt128 leftLow = new(0xAAAAAAAA_AAAAAAAAUL, 0xAAAAAAAA_AAAAAAAAUL);
        UInt128 rightHigh = new(0x55555555_55555555UL, 0x55555555_55555555UL);
        UInt128 rightLow = new(0x55555555_55555555UL, 0x55555555_55555555UL);
        Float256 left = new(new UInt256(leftHigh, leftLow));
        Float256 right = new(new UInt256(rightHigh, rightLow));
        Float256 result = left ^ right;
        Assert.Equal(System.UInt128.MaxValue, result.Bits.UpperBits);
        Assert.Equal(System.UInt128.MaxValue, result.Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256 bitwise complement should invert the raw bits")]
    public void Float256BitwiseComplementShouldInvertTheRawBits()
    {
        Float256 value = Float256.Zero;
        Assert.Equal(System.UInt128.MaxValue, (~value).Bits.UpperBits);
        Assert.Equal(System.UInt128.MaxValue, (~value).Bits.LowerBits);
    }
}
