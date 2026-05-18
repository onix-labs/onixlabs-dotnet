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

public sealed class UInt512ArithmeticAdditionTests
{
    [Fact(DisplayName = "UInt512 addition of two zeros should produce zero")]
    public void UInt512AdditionOfTwoZerosShouldBeZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.Zero + UInt512.Zero);
    }

    [Fact(DisplayName = "UInt512 addition with zero should be the identity")]
    public void UInt512AdditionWithZeroShouldBeIdentity()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(value, value + UInt512.Zero);
        Assert.Equal(value, UInt512.Zero + value);
    }

    [Fact(DisplayName = "UInt512 addition should carry into the upper half when lower overflows")]
    public void UInt512AdditionShouldCarryIntoUpper()
    {
        UInt512 a = new(UInt256.Zero, UInt256.MaxValue);
        UInt512 b = new(UInt256.Zero, UInt256.One);
        UInt512 sum = a + b;
        Assert.Equal(UInt256.One, sum.Upper);
        Assert.Equal(UInt256.Zero, sum.Lower);
    }

    [Fact(DisplayName = "UInt512 addition should not carry when lower does not overflow")]
    public void UInt512AdditionShouldNotCarryWhenNoOverflow()
    {
        UInt512 a = new(UInt256.Zero, (UInt256)100UL);
        UInt512 b = new(UInt256.Zero, (UInt256)200UL);
        UInt512 sum = a + b;
        Assert.Equal(UInt256.Zero, sum.Upper);
        Assert.Equal((UInt256)300UL, sum.Lower);
    }

    [Fact(DisplayName = "UInt512 addition should wrap at MaxValue")]
    public void UInt512AdditionShouldWrapAtMaxValue()
    {
        Assert.Equal(UInt512.Zero, UInt512.MaxValue + UInt512.One);
    }

    [Fact(DisplayName = "UInt512 addition of MaxValue and MaxValue should wrap to MaxValue minus One")]
    public void UInt512AdditionMaxPlusMaxShouldWrap()
    {
        UInt512 expected = UInt512.MaxValue - UInt512.One;
        Assert.Equal(expected, UInt512.MaxValue + UInt512.MaxValue);
    }

    [Fact(DisplayName = "UInt512 addition should match BigInteger for large operands")]
    public void UInt512AdditionShouldMatchBigInteger()
    {
        UInt512 a = UInt512.Parse("123456789012345678901234567890123456789012345678901234567890");
        UInt512 b = UInt512.Parse("987654321098765432109876543210987654321098765432109876543210");
        UInt512 sum = a + b;
        BigInteger mask = (BigInteger.One << 512) - BigInteger.One;
        BigInteger expected = ((BigInteger)a + (BigInteger)b) & mask;
        Assert.Equal(expected, (BigInteger)sum);
    }

    [Fact(DisplayName = "UInt512 checked addition of zeros should succeed")]
    public void UInt512CheckedAdditionOfZerosShouldSucceed()
    {
        Assert.Equal(UInt512.Zero, checked(UInt512.Zero + UInt512.Zero));
    }

    [Fact(DisplayName = "UInt512 checked addition with carry but no overflow should succeed")]
    public void UInt512CheckedAdditionWithCarryNoOverflowShouldSucceed()
    {
        UInt512 a = new(UInt256.Zero, UInt256.MaxValue);
        UInt512 b = new(UInt256.Zero, UInt256.One);
        UInt512 sum = checked(a + b);
        Assert.Equal(new UInt512(UInt256.One, UInt256.Zero), sum);
    }

    [Fact(DisplayName = "UInt512 checked addition should throw on overflow of MaxValue plus One")]
    public void UInt512CheckedAdditionShouldThrowOnOverflow()
    {
        Assert.Throws<OverflowException>(() => checked(UInt512.MaxValue + UInt512.One));
    }

    [Fact(DisplayName = "UInt512 checked addition should throw when the upper carry overflows")]
    public void UInt512CheckedAdditionShouldThrowWhenUpperCarryOverflows()
    {
        UInt512 a = new(UInt256.MaxValue, UInt256.MaxValue);
        UInt512 b = new(UInt256.Zero, UInt256.One);
        Assert.Throws<OverflowException>(() => checked(a + b));
    }

    [Fact(DisplayName = "UInt512 checked addition should throw when only upper halves overflow")]
    public void UInt512CheckedAdditionShouldThrowWhenUpperHalvesOverflow()
    {
        UInt512 a = new(UInt256.MaxValue, UInt256.Zero);
        UInt512 b = new(UInt256.One, UInt256.Zero);
        Assert.Throws<OverflowException>(() => checked(a + b));
    }

    [Fact(DisplayName = "UInt512 unary plus should return value unchanged")]
    public void UInt512UnaryPlusShouldReturnSameValue()
    {
        UInt512 value = UInt512.MaxValue;
        Assert.Equal(value, +value);
    }
}
