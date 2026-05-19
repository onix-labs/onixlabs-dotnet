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

public sealed class Float128Ieee754Tests
{
    [Fact(DisplayName = "Float128.Ieee754Remainder should round half to even")]
    public void Float128Ieee754RemainderShouldRoundHalfToEven()
    {
        Assert.Equal(Float128.Zero, Float128.Ieee754Remainder((Float128)6, (Float128)3));
        Assert.Equal((Float128)0.5, Float128.Ieee754Remainder((Float128)3.5, (Float128)3));
    }

    [Fact(DisplayName = "Float128.Ieee754Remainder of NaN should return NaN")]
    public void Float128Ieee754RemainderOfNaNShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Ieee754Remainder(Float128.NaN, Float128.One)));
        Assert.True(Float128.IsNaN(Float128.Ieee754Remainder(Float128.One, Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.Ieee754Remainder by zero should return NaN")]
    public void Float128Ieee754RemainderByZeroShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Ieee754Remainder(Float128.One, Float128.Zero)));
    }

    [Fact(DisplayName = "Float128.FusedMultiplyAdd should compute single-rounded product plus addend")]
    public void Float128FusedMultiplyAddShouldComputeSingleRoundedResult()
    {
        Float128 a = Float128.Two;
        Float128 b = (Float128)3;
        Float128 c = (Float128)4;
        Assert.Equal((Float128)10, Float128.FusedMultiplyAdd(a, b, c));
    }

    [Fact(DisplayName = "Float128.FusedMultiplyAdd with NaN operand should return NaN")]
    public void Float128FusedMultiplyAddWithNaNShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.FusedMultiplyAdd(Float128.NaN, Float128.One, Float128.One)));
        Assert.True(Float128.IsNaN(Float128.FusedMultiplyAdd(Float128.One, Float128.NaN, Float128.One)));
        Assert.True(Float128.IsNaN(Float128.FusedMultiplyAdd(Float128.One, Float128.One, Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.FusedMultiplyAdd of zero and infinity should return NaN")]
    public void Float128FusedMultiplyAddOfZeroTimesInfinityShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.FusedMultiplyAdd(Float128.Zero, Float128.PositiveInfinity, Float128.One)));
        Assert.True(Float128.IsNaN(Float128.FusedMultiplyAdd(Float128.PositiveInfinity, Float128.Zero, Float128.One)));
    }

    [Fact(DisplayName = "Float128.ReciprocalEstimate of two should approximately equal one half")]
    public void Float128ReciprocalEstimateOfTwoShouldApproximatelyEqualOneHalf()
    {
        Float128 result = Float128.ReciprocalEstimate(Float128.Two);
        Assert.Equal((Float128)0.5, result);
    }

    [Fact(DisplayName = "Float128.ReciprocalSqrtEstimate of four should approximately equal one half")]
    public void Float128ReciprocalSqrtEstimateOfFourShouldApproximatelyEqualOneHalf()
    {
        Float128 result = Float128.ReciprocalSqrtEstimate((Float128)4);
        Float128 difference = Float128.Abs(result - (Float128)0.5);
        Assert.True(difference < Float128.Parse("1E-30"));
    }

    [Fact(DisplayName = "Float128.IsPow2 should return true for powers of two")]
    public void Float128IsPow2ShouldReturnTrueForPowersOfTwo()
    {
        Assert.True(Float128.IsPow2(Float128.One));
        Assert.True(Float128.IsPow2(Float128.Two));
        Assert.True(Float128.IsPow2((Float128)1024));
        Assert.True(Float128.IsPow2((Float128)0.5));
        Assert.True(Float128.IsPow2(Float128.Epsilon));
    }

    [Fact(DisplayName = "Float128.IsPow2 should return false for non-powers of two")]
    public void Float128IsPow2ShouldReturnFalseForNonPowersOfTwo()
    {
        Assert.False(Float128.IsPow2(Float128.Zero));
        Assert.False(Float128.IsPow2(Float128.NegativeZero));
        Assert.False(Float128.IsPow2(Float128.NegativeOne));
        Assert.False(Float128.IsPow2((Float128)3));
        Assert.False(Float128.IsPow2((Float128)1.5));
        Assert.False(Float128.IsPow2(Float128.NaN));
        Assert.False(Float128.IsPow2(Float128.PositiveInfinity));
        Assert.False(Float128.IsPow2(Float128.NegativeInfinity));
    }

    [Fact(DisplayName = "Float128.AllBitsSet should have every bit of the raw representation set")]
    public void Float128AllBitsSetShouldEqualUInt128MaxValue()
    {
        Assert.Equal(System.UInt128.MaxValue, Float128.AllBitsSet.Bits);
    }

    [Fact(DisplayName = "Float128 bitwise AND should AND the raw bits")]
    public void Float128BitwiseAndShouldAndTheRawBits()
    {
        Float128 left = new(new System.UInt128(0xFF00FF00_FF00FF00UL, 0xFF00FF00_FF00FF00UL));
        Float128 right = new(new System.UInt128(0xFFFF0000_FFFF0000UL, 0xFFFF0000_FFFF0000UL));
        Float128 result = left & right;
        Assert.Equal(left.Bits & right.Bits, result.Bits);
    }

    [Fact(DisplayName = "Float128 bitwise OR should OR the raw bits")]
    public void Float128BitwiseOrShouldOrTheRawBits()
    {
        Float128 left = new(new System.UInt128(0x0000FFFF_0000FFFFUL, 0x0000FFFF_0000FFFFUL));
        Float128 right = new(new System.UInt128(0xFFFF0000_FFFF0000UL, 0xFFFF0000_FFFF0000UL));
        Float128 result = left | right;
        Assert.Equal(left.Bits | right.Bits, result.Bits);
    }

    [Fact(DisplayName = "Float128 bitwise XOR should XOR the raw bits")]
    public void Float128BitwiseXorShouldXorTheRawBits()
    {
        Float128 left = new(new System.UInt128(0xAAAAAAAA_AAAAAAAAUL, 0xAAAAAAAA_AAAAAAAAUL));
        Float128 right = new(new System.UInt128(0x55555555_55555555UL, 0x55555555_55555555UL));
        Float128 result = left ^ right;
        Assert.Equal(System.UInt128.MaxValue, result.Bits);
    }

    [Fact(DisplayName = "Float128 bitwise complement should invert the raw bits")]
    public void Float128BitwiseComplementShouldInvertTheRawBits()
    {
        Float128 value = Float128.Zero;
        Assert.Equal(System.UInt128.MaxValue, (~value).Bits);
    }
}
