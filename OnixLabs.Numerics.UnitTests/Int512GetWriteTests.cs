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

public sealed class Int512GetWriteTests
{
    [Fact(DisplayName = "Int512.GetByteCount should always return 64")]
    public void Int512GetByteCountShouldReturn64()
    {
        Assert.Equal(64, Int512.MaxValue.GetByteCount());
        Assert.Equal(64, Int512.MinValue.GetByteCount());
        Assert.Equal(64, Int512.Zero.GetByteCount());
    }

    [Fact(DisplayName = "Int512.GetShortestBitLength of Zero should be 0")]
    public void Int512GetShortestBitLengthOfZeroShouldBeZero()
    {
        Assert.Equal(0, Int512.Zero.GetShortestBitLength());
    }

    [Fact(DisplayName = "Int512.GetShortestBitLength of One should be 2 (1 bit for value, 1 for sign)")]
    public void Int512GetShortestBitLengthOfOneShouldBe2()
    {
        // For two's-complement, +1 needs at least 2 bits: a sign bit (0) and the value bit (1).
        Assert.Equal(2, Int512.One.GetShortestBitLength());
    }

    [Fact(DisplayName = "Int512.GetShortestBitLength of NegativeOne should be 1 (just the sign bit)")]
    public void Int512GetShortestBitLengthOfNegativeOneShouldBe1()
    {
        Assert.Equal(1, Int512.NegativeOne.GetShortestBitLength());
    }

    [Fact(DisplayName = "Int512.GetShortestBitLength of MaxValue should be 512")]
    public void Int512GetShortestBitLengthOfMaxValueShouldBe512()
    {
        Assert.Equal(512, Int512.MaxValue.GetShortestBitLength());
    }

    [Fact(DisplayName = "Int512.GetShortestBitLength of MinValue should be 512")]
    public void Int512GetShortestBitLengthOfMinValueShouldBe512()
    {
        Assert.Equal(512, Int512.MinValue.GetShortestBitLength());
    }

    [Fact(DisplayName = "Int512.TryWriteBigEndian should round-trip a large negative value")]
    public void Int512TryWriteBigEndianRoundTripsNegativeValue()
    {
        Int512 source = (Int512)(-(BigInteger.Pow(2, 300) + BigInteger.One));
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(source.TryWriteBigEndian(buffer, out int written));
        Assert.Equal(64, written);
        Assert.True(Int512.TryReadBigEndian(buffer, isUnsigned: false, out Int512 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "Int512.TryWriteBigEndian of NegativeOne should produce all 0xFF bytes")]
    public void Int512TryWriteBigEndianNegativeOneShouldProduceAllFF()
    {
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(Int512.NegativeOne.TryWriteBigEndian(buffer, out _));
        for (int i = 0; i < 64; i++) Assert.Equal((byte)0xFF, buffer[i]);
    }

    [Fact(DisplayName = "Int512.TryWriteBigEndian of MinValue should produce 0x80 followed by zeros")]
    public void Int512TryWriteBigEndianMinValueShouldProduce0x80AndZeros()
    {
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(Int512.MinValue.TryWriteBigEndian(buffer, out _));
        Assert.Equal((byte)0x80, buffer[0]);
        for (int i = 1; i < 64; i++) Assert.Equal((byte)0x00, buffer[i]);
    }

    [Fact(DisplayName = "Int512.TryWriteBigEndian of MaxValue should produce 0x7F followed by 0xFF bytes")]
    public void Int512TryWriteBigEndianMaxValueShouldProduce0x7FAndFFs()
    {
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(Int512.MaxValue.TryWriteBigEndian(buffer, out _));
        Assert.Equal((byte)0x7F, buffer[0]);
        for (int i = 1; i < 64; i++) Assert.Equal((byte)0xFF, buffer[i]);
    }

    [Fact(DisplayName = "Int512.TryWriteLittleEndian should round-trip a large negative value")]
    public void Int512TryWriteLittleEndianRoundTripsNegativeValue()
    {
        Int512 source = (Int512)(-(BigInteger.Pow(2, 400)));
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(source.TryWriteLittleEndian(buffer, out int written));
        Assert.Equal(64, written);
        Assert.True(Int512.TryReadLittleEndian(buffer, isUnsigned: false, out Int512 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "Int512.TryWriteBigEndian should fail when destination is too small")]
    public void Int512TryWriteBigEndianTooSmallShouldFail()
    {
        Int512 source = Int512.NegativeOne;
        Span<byte> small = stackalloc byte[32];
        Assert.False(source.TryWriteBigEndian(small, out int written));
        Assert.Equal(0, written);
    }

    [Fact(DisplayName = "Int512.TryReadBigEndian with sign-extended oversize source should succeed")]
    public void Int512TryReadBigEndianOversizeSignExtendedShouldSucceed()
    {
        Span<byte> buffer = stackalloc byte[80];
        buffer.Fill(0xFF);
        Assert.True(Int512.TryReadBigEndian(buffer, isUnsigned: false, out Int512 value));
        Assert.Equal(Int512.NegativeOne, value);
    }

    [Fact(DisplayName = "Int512.TryReadBigEndian with non-sign-extended oversize source should fail")]
    public void Int512TryReadBigEndianOversizeIncoherentShouldFail()
    {
        Span<byte> buffer = stackalloc byte[80];
        buffer.Clear();
        buffer[0] = 0xFF;
        // High byte set to 0xFF but the rest is zero — inconsistent sign extension for negative
        Assert.False(Int512.TryReadBigEndian(buffer, isUnsigned: false, out Int512 _));
    }

    [Fact(DisplayName = "Int512.TryReadLittleEndian with empty source should produce Zero")]
    public void Int512TryReadLittleEndianEmptyShouldBeZero()
    {
        Assert.True(Int512.TryReadLittleEndian(ReadOnlySpan<byte>.Empty, isUnsigned: false, out Int512 value));
        Assert.Equal(Int512.Zero, value);
    }

    [Fact(DisplayName = "Int512.TryReadBigEndian as unsigned should not treat high-bit-set values as negative")]
    public void Int512TryReadBigEndianUnsignedShouldNotTreatHighBitAsSign()
    {
        Span<byte> buffer = stackalloc byte[64];
        buffer.Fill(0xFF);
        Assert.True(Int512.TryReadBigEndian(buffer, isUnsigned: true, out Int512 value));
        // The all-FF bit pattern read as signed-from-unsigned yields the same bit pattern (NegativeOne).
        Assert.Equal(Int512.NegativeOne, value);
    }
}
