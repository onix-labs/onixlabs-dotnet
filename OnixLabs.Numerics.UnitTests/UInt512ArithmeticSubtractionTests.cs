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

public sealed class UInt512ArithmeticSubtractionTests
{
    [Fact(DisplayName = "UInt512 subtraction of identical values should produce zero")]
    public void UInt512SubtractionOfIdenticalShouldBeZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.MaxValue - UInt512.MaxValue);
        Assert.Equal(UInt512.Zero, UInt512.One - UInt512.One);
    }

    [Fact(DisplayName = "UInt512 subtraction of zero should be the identity")]
    public void UInt512SubtractionOfZeroShouldBeIdentity()
    {
        UInt512 value = UInt512.Parse("9999999999999999999999999999999");
        Assert.Equal(value, value - UInt512.Zero);
    }

    [Fact(DisplayName = "UInt512 subtraction should borrow from the upper half on underflow")]
    public void UInt512SubtractionShouldBorrowFromUpper()
    {
        UInt512 a = new(UInt256.One, UInt256.Zero);
        UInt512 b = new(UInt256.Zero, UInt256.One);
        UInt512 diff = a - b;
        Assert.Equal(UInt256.Zero, diff.Upper);
        Assert.Equal(UInt256.MaxValue, diff.Lower);
    }

    [Fact(DisplayName = "UInt512 subtraction should not borrow when lower is sufficient")]
    public void UInt512SubtractionShouldNotBorrowWhenLowerSufficient()
    {
        UInt512 a = new((UInt256)5UL, (UInt256)100UL);
        UInt512 b = new((UInt256)1UL, (UInt256)50UL);
        UInt512 diff = a - b;
        Assert.Equal((UInt256)4UL, diff.Upper);
        Assert.Equal((UInt256)50UL, diff.Lower);
    }

    [Fact(DisplayName = "UInt512 unchecked subtraction should wrap on underflow")]
    public void UInt512SubtractionShouldWrapOnUnderflow()
    {
        Assert.Equal(UInt512.MaxValue, UInt512.Zero - UInt512.One);
    }

    [Fact(DisplayName = "UInt512 subtraction of MaxValue from Zero should produce One")]
    public void UInt512SubtractionMaxValueFromZeroShouldProduceOne()
    {
        Assert.Equal(UInt512.One, UInt512.Zero - UInt512.MaxValue);
    }

    [Fact(DisplayName = "UInt512 subtraction should match BigInteger for large operands")]
    public void UInt512SubtractionShouldMatchBigInteger()
    {
        UInt512 a = UInt512.Parse("987654321098765432109876543210987654321098765432109876543210");
        UInt512 b = UInt512.Parse("123456789012345678901234567890123456789012345678901234567890");
        UInt512 diff = a - b;
        BigInteger mask = (BigInteger.One << 512) - BigInteger.One;
        BigInteger expected = ((BigInteger)a - (BigInteger)b) & mask;
        Assert.Equal(expected, (BigInteger)diff);
    }

    [Fact(DisplayName = "UInt512 checked subtraction should succeed when no underflow")]
    public void UInt512CheckedSubtractionShouldSucceedWhenNoUnderflow()
    {
        UInt512 a = (UInt512)1000UL;
        UInt512 b = (UInt512)300UL;
        Assert.Equal((UInt512)700UL, checked(a - b));
    }

    [Fact(DisplayName = "UInt512 checked subtraction should throw on underflow")]
    public void UInt512CheckedSubtractionShouldThrowOnUnderflow()
    {
        Assert.Throws<OverflowException>(() => checked(UInt512.Zero - UInt512.One));
        Assert.Throws<OverflowException>(() => checked((UInt512)1UL - (UInt512)2UL));
    }

    [Fact(DisplayName = "UInt512 checked subtraction across the half-boundary should throw on underflow")]
    public void UInt512CheckedSubtractionAcrossHalfBoundaryShouldThrowOnUnderflow()
    {
        UInt512 a = new(UInt256.Zero, UInt256.MaxValue);
        UInt512 b = new(UInt256.One, UInt256.Zero);
        Assert.Throws<OverflowException>(() => checked(a - b));
    }
}
