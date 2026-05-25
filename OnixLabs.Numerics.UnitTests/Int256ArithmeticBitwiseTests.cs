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

public sealed class Int256ArithmeticBitwiseTests
{
    [Fact(DisplayName = "Int256 bitwise AND with NegativeOne should return the value")]
    public void Int256BitwiseAndWithNegativeOneShouldReturnValue()
    {
        Int256 value = (Int256)0xFEED_BEEF;
        Assert.Equal(value, value & Int256.NegativeOne);
    }

    [Fact(DisplayName = "Int256 bitwise OR with zero should return the value")]
    public void Int256BitwiseOrWithZeroShouldReturnValue()
    {
        Int256 value = (Int256)(-12345);
        Assert.Equal(value, value | Int256.Zero);
    }

    [Fact(DisplayName = "Int256 bitwise XOR of value with itself should be zero")]
    public void Int256BitwiseXorOfValueWithItselfShouldBeZero()
    {
        Int256 value = (Int256)0x123456;
        Assert.Equal(Int256.Zero, value ^ value);
    }

    [Fact(DisplayName = "Int256 bitwise NOT of zero should equal NegativeOne (all bits set)")]
    public void Int256BitwiseNotOfZeroShouldEqualNegativeOne()
    {
        Assert.Equal(Int256.NegativeOne, ~Int256.Zero);
    }

    [Fact(DisplayName = "Int256 bitwise NOT of NegativeOne should equal zero")]
    public void Int256BitwiseNotOfNegativeOneShouldEqualZero()
    {
        Assert.Equal(Int256.Zero, ~Int256.NegativeOne);
    }

    [Fact(DisplayName = "Int256 bitwise NOT applied twice should return the value")]
    public void Int256BitwiseNotTwiceShouldReturnValue()
    {
        Int256 value = (Int256)(-9999);
        Assert.Equal(value, ~~value);
    }

    [Fact(DisplayName = "Int256 bitwise NOT of MaxValue should equal MinValue")]
    public void Int256BitwiseNotOfMaxValueShouldEqualMinValue()
    {
        Assert.Equal(Int256.MinValue, ~Int256.MaxValue);
        Assert.Equal(Int256.MaxValue, ~Int256.MinValue);
    }

    [Fact(DisplayName = "Int256 bitwise operations should AND each half independently")]
    public void Int256BitwiseAndShouldAndEachHalfIndependently()
    {
        Int256 left = new(UInt128.MaxValue, UInt128.Zero);
        Int256 right = new(UInt128.Zero, UInt128.MaxValue);
        Assert.Equal(Int256.Zero, left & right);
    }

    [Fact(DisplayName = "Int256 bitwise OR of complementary halves should equal NegativeOne")]
    public void Int256BitwiseOrOfComplementaryHalvesShouldEqualNegativeOne()
    {
        Int256 left = new(UInt128.MaxValue, UInt128.Zero);
        Int256 right = new(UInt128.Zero, UInt128.MaxValue);
        Assert.Equal(Int256.NegativeOne, left | right);
    }

    [Fact(DisplayName = "Int256 bitwise XOR of complementary halves should equal NegativeOne")]
    public void Int256BitwiseXorOfComplementaryHalvesShouldEqualNegativeOne()
    {
        Int256 left = new(UInt128.MaxValue, UInt128.Zero);
        Int256 right = new(UInt128.Zero, UInt128.MaxValue);
        Assert.Equal(Int256.NegativeOne, left ^ right);
    }

    [Fact(DisplayName = "Int256 bitwise operations should be commutative")]
    public void Int256BitwiseOperationsShouldBeCommutative()
    {
        Int256 a = (Int256)0xDEADBEEF;
        Int256 b = (Int256)0xCAFEBABE;
        Assert.Equal(a & b, b & a);
        Assert.Equal(a | b, b | a);
        Assert.Equal(a ^ b, b ^ a);
    }
}
