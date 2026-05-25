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

public sealed class Int512ArithmeticBitwiseTests
{
    [Fact(DisplayName = "Int512 bitwise AND with Zero should produce Zero")]
    public void Int512AndWithZeroShouldProduceZero()
    {
        Assert.Equal(Int512.Zero, Int512.NegativeOne & Int512.Zero);
        Assert.Equal(Int512.Zero, Int512.MaxValue & Int512.Zero);
    }

    [Fact(DisplayName = "Int512 bitwise AND with NegativeOne (all ones) should be the identity")]
    public void Int512AndWithNegativeOneShouldBeIdentity()
    {
        Int512 value = Int512.Parse("-123456789012345678901234567890");
        Assert.Equal(value, value & Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 bitwise OR with Zero should be the identity")]
    public void Int512OrWithZeroShouldBeIdentity()
    {
        Int512 value = Int512.Parse("12345");
        Assert.Equal(value, value | Int512.Zero);
        Assert.Equal(value, Int512.Zero | value);
    }

    [Fact(DisplayName = "Int512 bitwise OR with NegativeOne should yield NegativeOne")]
    public void Int512OrWithNegativeOneShouldYieldNegativeOne()
    {
        Int512 value = (Int512)12345;
        Assert.Equal(Int512.NegativeOne, value | Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 bitwise XOR with itself should produce Zero")]
    public void Int512XorWithItselfShouldProduceZero()
    {
        Int512 value = Int512.MinValue;
        Assert.Equal(Int512.Zero, value ^ value);
    }

    [Fact(DisplayName = "Int512 bitwise XOR with NegativeOne should be bitwise complement")]
    public void Int512XorWithNegativeOneShouldBeComplement()
    {
        Int512 value = Int512.Parse("12345");
        Assert.Equal(~value, value ^ Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 bitwise complement of Zero should be NegativeOne")]
    public void Int512ComplementOfZeroShouldBeNegativeOne()
    {
        Assert.Equal(Int512.NegativeOne, ~Int512.Zero);
    }

    [Fact(DisplayName = "Int512 bitwise complement of NegativeOne should be Zero")]
    public void Int512ComplementOfNegativeOneShouldBeZero()
    {
        Assert.Equal(Int512.Zero, ~Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 complement of MaxValue should be MinValue")]
    public void Int512ComplementOfMaxValueShouldBeMinValue()
    {
        Assert.Equal(Int512.MinValue, ~Int512.MaxValue);
    }

    [Fact(DisplayName = "Int512 double complement should be identity")]
    public void Int512DoubleComplementShouldBeIdentity()
    {
        Int512 value = Int512.Parse("-987654321098765432109876543210");
        Assert.Equal(value, ~~value);
    }

    [Fact(DisplayName = "Int512 AND with sign-bit-only mask should isolate the sign")]
    public void Int512AndWithSignBitMaskShouldIsolateSign()
    {
        Int512 negative = (Int512)(-1);
        Int512 signBit = new(UInt256.One << 255, UInt256.Zero);
        Int512 result = negative & signBit;
        Assert.Equal(signBit, result);
    }
}
