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

public sealed class Int256ConstantTests
{
    [Fact(DisplayName = "Int256.Zero should equal default")]
    public void Int256ZeroShouldEqualDefault()
    {
        Assert.Equal(default, Int256.Zero);
        Assert.Equal(BigInteger.Zero, (BigInteger)Int256.Zero);
    }

    [Fact(DisplayName = "Int256.One bit pattern should have only the lowest bit set")]
    public void Int256OneBitPatternShouldHaveOnlyLowestBitSet()
    {
        Assert.Equal(UInt128.Zero, Int256.One.UpperBits);
        Assert.Equal(UInt128.One, Int256.One.LowerBits);
        Assert.Equal(BigInteger.One, (BigInteger)Int256.One);
    }

    [Fact(DisplayName = "Int256.NegativeOne should have all bits set (two's-complement representation)")]
    public void Int256NegativeOneShouldHaveAllBitsSet()
    {
        Assert.Equal(UInt128.MaxValue, Int256.NegativeOne.UpperBits);
        Assert.Equal(UInt128.MaxValue, Int256.NegativeOne.LowerBits);
        Assert.Equal(BigInteger.MinusOne, (BigInteger)Int256.NegativeOne);
    }

    [Fact(DisplayName = "Int256.MinValue should equal negative 2 to the 255")]
    public void Int256MinValueShouldEqualNegativeTwoTo255()
    {
        BigInteger expected = -(BigInteger.One << 255);
        Assert.Equal(expected, (BigInteger)Int256.MinValue);
        // Sign bit set, all other bits zero.
        Assert.Equal(UInt128.One << 127, Int256.MinValue.UpperBits);
        Assert.Equal(UInt128.Zero, Int256.MinValue.LowerBits);
    }

    [Fact(DisplayName = "Int256.MaxValue should equal 2 to the 255 minus one")]
    public void Int256MaxValueShouldEqual2To255MinusOne()
    {
        BigInteger expected = (BigInteger.One << 255) - BigInteger.One;
        Assert.Equal(expected, (BigInteger)Int256.MaxValue);
        // Sign bit clear, all other bits set.
        Assert.Equal(UInt128.MaxValue >> 1, Int256.MaxValue.UpperBits);
        Assert.Equal(UInt128.MaxValue, Int256.MaxValue.LowerBits);
    }

    [Fact(DisplayName = "Int256.AllBitsSet should equal Int256.NegativeOne")]
    public void Int256AllBitsSetShouldEqualNegativeOne()
    {
        Assert.Equal(Int256.NegativeOne, Int256.AllBitsSet);
    }

    [Fact(DisplayName = "Int256.MinValue plus MaxValue should equal negative one")]
    public void Int256MinValuePlusMaxValueShouldEqualNegativeOne()
    {
        Assert.Equal(Int256.NegativeOne, Int256.MinValue + Int256.MaxValue);
    }

    [Fact(DisplayName = "Int256.MaxValue minus MinValue should wrap to negative one (two's-complement)")]
    public void Int256MaxValueMinusMinValueShouldWrapToNegativeOne()
    {
        // MaxValue - MinValue = (2^255 - 1) - (-2^255) = 2^256 - 1, which wraps to NegativeOne.
        Assert.Equal(Int256.NegativeOne, Int256.MaxValue - Int256.MinValue);
    }

    [Fact(DisplayName = "Int256 constants should preserve identity across reads")]
    public void Int256ConstantsShouldPreserveIdentityAcrossReads()
    {
        Assert.True(Int256.Zero == Int256.Zero);
        Assert.True(Int256.One == Int256.One);
        Assert.True(Int256.NegativeOne == Int256.NegativeOne);
        Assert.True(Int256.MinValue == Int256.MinValue);
        Assert.True(Int256.MaxValue == Int256.MaxValue);
    }
}
