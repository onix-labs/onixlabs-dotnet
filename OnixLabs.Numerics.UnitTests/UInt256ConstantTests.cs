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

public sealed class UInt256ConstantTests
{
    [Fact(DisplayName = "UInt256.Zero should equal default")]
    public void UInt256ZeroShouldEqualDefault()
    {
        Assert.Equal(default, UInt256.Zero);
        Assert.Equal((BigInteger)0, (BigInteger)UInt256.Zero);
    }

    [Fact(DisplayName = "UInt256.One should equal BigInteger one")]
    public void UInt256OneShouldEqualBigIntegerOne()
    {
        Assert.Equal(BigInteger.One, (BigInteger)UInt256.One);
        Assert.NotEqual(UInt256.Zero, UInt256.One);
    }

    [Fact(DisplayName = "UInt256.MinValue should equal UInt256.Zero")]
    public void UInt256MinValueShouldEqualZero()
    {
        Assert.Equal(UInt256.Zero, UInt256.MinValue);
        Assert.Equal(BigInteger.Zero, (BigInteger)UInt256.MinValue);
    }

    [Fact(DisplayName = "UInt256.MaxValue should equal 2^256 minus one")]
    public void UInt256MaxValueShouldEqualTwoTo256MinusOne()
    {
        BigInteger expected = (BigInteger.One << 256) - BigInteger.One;
        Assert.Equal(expected, (BigInteger)UInt256.MaxValue);
        Assert.Equal(UInt128.MaxValue, UInt256.MaxValue.Upper);
        Assert.Equal(UInt128.MaxValue, UInt256.MaxValue.Lower);
    }

    [Fact(DisplayName = "UInt256.AllBitsSet should equal UInt256.MaxValue and have every bit one")]
    public void UInt256AllBitsSetShouldEqualMaxValueAndHaveEveryBitOne()
    {
        Assert.Equal(UInt256.MaxValue, UInt256.AllBitsSet);
        UInt256 popCount = UInt256.PopCount(UInt256.AllBitsSet);
        Assert.Equal((UInt256)256, popCount);
    }

    [Fact(DisplayName = "UInt256 constants should retain identity across reads")]
    public void UInt256ConstantsShouldRetainIdentityAcrossReads()
    {
        Assert.True(UInt256.Zero == UInt256.Zero);
        Assert.True(UInt256.One == UInt256.One);
        Assert.True(UInt256.MaxValue == UInt256.MaxValue);
        Assert.True(UInt256.AllBitsSet == UInt256.AllBitsSet);
    }

    [Fact(DisplayName = "UInt256.MaxValue plus one should wrap to zero")]
    public void UInt256MaxValuePlusOneShouldWrapToZero()
    {
        Assert.Equal(UInt256.Zero, UInt256.MaxValue + UInt256.One);
    }

    [Fact(DisplayName = "UInt256.Zero bit pattern should be all zero halves")]
    public void UInt256ZeroBitPatternShouldBeAllZeroHalves()
    {
        Assert.Equal(UInt128.Zero, UInt256.Zero.Upper);
        Assert.Equal(UInt128.Zero, UInt256.Zero.Lower);
    }

    [Fact(DisplayName = "UInt256.One bit pattern should have only the lowest bit set")]
    public void UInt256OneBitPatternShouldHaveOnlyTheLowestBitSet()
    {
        Assert.Equal(UInt128.Zero, UInt256.One.Upper);
        Assert.Equal(UInt128.One, UInt256.One.Lower);
        Assert.Equal((UInt256)1, UInt256.PopCount(UInt256.One));
    }
}
