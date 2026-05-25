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

namespace OnixLabs.Numerics.UnitTests;

public sealed class UInt512ArithmeticBitwiseTests
{
    [Fact(DisplayName = "UInt512 bitwise AND of a value with zero should produce zero")]
    public void UInt512AndWithZeroShouldProduceZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.MaxValue & UInt512.Zero);
        Assert.Equal(UInt512.Zero, UInt512.Zero & UInt512.MaxValue);
    }

    [Fact(DisplayName = "UInt512 bitwise AND of a value with MaxValue should be the identity")]
    public void UInt512AndWithMaxValueShouldBeIdentity()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(value, value & UInt512.MaxValue);
    }

    [Fact(DisplayName = "UInt512 bitwise AND should AND each half separately")]
    public void UInt512AndShouldAndEachHalf()
    {
        UInt512 left = new(UInt256.MaxValue, UInt256.MaxValue);
        UInt512 right = new(UInt256.Zero, UInt256.MaxValue);
        UInt512 result = left & right;
        Assert.Equal(right, result);
    }

    [Fact(DisplayName = "UInt512 bitwise OR of a value with zero should be the identity")]
    public void UInt512OrWithZeroShouldBeIdentity()
    {
        UInt512 value = UInt512.Parse("987654321098765432109876543210");
        Assert.Equal(value, value | UInt512.Zero);
        Assert.Equal(value, UInt512.Zero | value);
    }

    [Fact(DisplayName = "UInt512 bitwise OR with MaxValue should yield MaxValue")]
    public void UInt512OrWithMaxValueShouldYieldMaxValue()
    {
        UInt512 value = UInt512.Parse("12345");
        Assert.Equal(UInt512.MaxValue, value | UInt512.MaxValue);
    }

    [Fact(DisplayName = "UInt512 bitwise OR should OR each half separately")]
    public void UInt512OrShouldOrEachHalf()
    {
        UInt512 left = new(UInt256.MaxValue, UInt256.Zero);
        UInt512 right = new(UInt256.Zero, UInt256.MaxValue);
        UInt512 result = left | right;
        Assert.Equal(UInt512.MaxValue, result);
    }

    [Fact(DisplayName = "UInt512 bitwise XOR with itself should produce zero")]
    public void UInt512XorWithItselfShouldProduceZero()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(UInt512.Zero, value ^ value);
    }

    [Fact(DisplayName = "UInt512 bitwise XOR with zero should be the identity")]
    public void UInt512XorWithZeroShouldBeIdentity()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(value, value ^ UInt512.Zero);
    }

    [Fact(DisplayName = "UInt512 bitwise XOR with MaxValue should be bitwise complement")]
    public void UInt512XorWithMaxValueShouldBeComplement()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(~value, value ^ UInt512.MaxValue);
    }

    [Fact(DisplayName = "UInt512 bitwise XOR should match BigInteger XOR")]
    public void UInt512XorShouldMatchBigInteger()
    {
        UInt512 left = UInt512.Parse("123456789012345678901234567890");
        UInt512 right = UInt512.Parse("987654321098765432109876543210");
        UInt512 result = left ^ right;
        BigInteger expected = (BigInteger)left ^ (BigInteger)right;
        Assert.Equal(expected, (BigInteger)result);
    }

    [Fact(DisplayName = "UInt512 bitwise complement of Zero should be MaxValue")]
    public void UInt512ComplementOfZeroShouldBeMaxValue()
    {
        Assert.Equal(UInt512.MaxValue, ~UInt512.Zero);
    }

    [Fact(DisplayName = "UInt512 bitwise complement of MaxValue should be Zero")]
    public void UInt512ComplementOfMaxValueShouldBeZero()
    {
        Assert.Equal(UInt512.Zero, ~UInt512.MaxValue);
    }

    [Fact(DisplayName = "UInt512 bitwise complement should toggle every bit")]
    public void UInt512ComplementShouldToggleEveryBit()
    {
        UInt512 value = new(UInt256.MaxValue, UInt256.Zero);
        UInt512 complement = ~value;
        Assert.Equal(new UInt512(UInt256.Zero, UInt256.MaxValue), complement);
    }

    [Fact(DisplayName = "UInt512 double complement should restore the original value")]
    public void UInt512DoubleComplementShouldBeIdentity()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(value, ~~value);
    }

    [Fact(DisplayName = "UInt512 AND with mask isolates the desired bit range")]
    public void UInt512AndWithMaskShouldIsolateBits()
    {
        UInt512 value = UInt512.MaxValue;
        UInt512 mask = (UInt512.One << 128) - UInt512.One;
        UInt512 result = value & mask;
        Assert.Equal(mask, result);
    }
}
