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

public sealed class UInt512GetWriteTests
{
    [Fact(DisplayName = "UInt512.GetByteCount should return 64")]
    public void UInt512GetByteCountShouldReturn64()
    {
        Assert.Equal(64, UInt512.MaxValue.GetByteCount());
        Assert.Equal(64, UInt512.Zero.GetByteCount());
    }

    [Fact(DisplayName = "UInt512.GetShortestBitLength of Zero should be 0")]
    public void UInt512GetShortestBitLengthOfZeroShouldBeZero()
    {
        Assert.Equal(0, UInt512.Zero.GetShortestBitLength());
    }

    [Fact(DisplayName = "UInt512.GetShortestBitLength of One should be 1")]
    public void UInt512GetShortestBitLengthOfOneShouldBeOne()
    {
        Assert.Equal(1, UInt512.One.GetShortestBitLength());
    }

    [Fact(DisplayName = "UInt512.GetShortestBitLength of MaxValue should be 512")]
    public void UInt512GetShortestBitLengthOfMaxValueShouldBe512()
    {
        Assert.Equal(512, UInt512.MaxValue.GetShortestBitLength());
    }

    [Theory(DisplayName = "UInt512.GetShortestBitLength of 2^n should be n+1")]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(63, 64)]
    [InlineData(64, 65)]
    [InlineData(127, 128)]
    [InlineData(128, 129)]
    [InlineData(255, 256)]
    [InlineData(256, 257)]
    [InlineData(384, 385)]
    [InlineData(511, 512)]
    public void UInt512GetShortestBitLengthOfPowerOfTwo(int exponent, int expected)
    {
        UInt512 value = UInt512.One << exponent;
        Assert.Equal(expected, value.GetShortestBitLength());
    }

    [Fact(DisplayName = "UInt512.TryWriteBigEndian should round-trip with TryReadBigEndian for MaxValue")]
    public void UInt512TryWriteBigEndianRoundTripsMaxValue()
    {
        UInt512 source = UInt512.MaxValue;
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(source.TryWriteBigEndian(buffer, out int written));
        Assert.Equal(64, written);
        Assert.True(UInt512.TryReadBigEndian(buffer, isUnsigned: true, out UInt512 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "UInt512.TryWriteBigEndian should round-trip a large parsed value")]
    public void UInt512TryWriteBigEndianRoundTripsLargeValue()
    {
        UInt512 source = UInt512.Parse("123456789012345678901234567890123456789012345678901234567890");
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(source.TryWriteBigEndian(buffer, out int written));
        Assert.Equal(64, written);
        Assert.True(UInt512.TryReadBigEndian(buffer, isUnsigned: true, out UInt512 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "UInt512.TryWriteLittleEndian should round-trip with TryReadLittleEndian for MaxValue")]
    public void UInt512TryWriteLittleEndianRoundTripsMaxValue()
    {
        UInt512 source = UInt512.MaxValue;
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(source.TryWriteLittleEndian(buffer, out int written));
        Assert.Equal(64, written);
        Assert.True(UInt512.TryReadLittleEndian(buffer, isUnsigned: true, out UInt512 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "UInt512.TryWriteBigEndian should fail when destination is too small")]
    public void UInt512TryWriteBigEndianTooSmallShouldFail()
    {
        UInt512 source = UInt512.One;
        Span<byte> small = stackalloc byte[32];
        Assert.False(source.TryWriteBigEndian(small, out int written));
        Assert.Equal(0, written);
    }

    [Fact(DisplayName = "UInt512.TryWriteLittleEndian should fail when destination is too small")]
    public void UInt512TryWriteLittleEndianTooSmallShouldFail()
    {
        UInt512 source = UInt512.One;
        Span<byte> small = stackalloc byte[63];
        Assert.False(source.TryWriteLittleEndian(small, out int written));
        Assert.Equal(0, written);
    }

    [Fact(DisplayName = "UInt512.TryWriteBigEndian of One should set the last byte only")]
    public void UInt512TryWriteBigEndianOneShouldSetLastByteOnly()
    {
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(UInt512.One.TryWriteBigEndian(buffer, out _));
        for (int i = 0; i < 63; i++) Assert.Equal((byte)0, buffer[i]);
        Assert.Equal((byte)1, buffer[63]);
    }

    [Fact(DisplayName = "UInt512.TryWriteLittleEndian of One should set the first byte only")]
    public void UInt512TryWriteLittleEndianOneShouldSetFirstByteOnly()
    {
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(UInt512.One.TryWriteLittleEndian(buffer, out _));
        Assert.Equal((byte)1, buffer[0]);
        for (int i = 1; i < 64; i++) Assert.Equal((byte)0, buffer[i]);
    }

    [Fact(DisplayName = "UInt512.TryReadBigEndian with empty source should return Zero")]
    public void UInt512TryReadBigEndianEmptyShouldReturnZero()
    {
        Assert.True(UInt512.TryReadBigEndian(ReadOnlySpan<byte>.Empty, isUnsigned: true, out UInt512 value));
        Assert.Equal(UInt512.Zero, value);
    }

    [Fact(DisplayName = "UInt512.TryReadLittleEndian with empty source should return Zero")]
    public void UInt512TryReadLittleEndianEmptyShouldReturnZero()
    {
        Assert.True(UInt512.TryReadLittleEndian(ReadOnlySpan<byte>.Empty, isUnsigned: true, out UInt512 value));
        Assert.Equal(UInt512.Zero, value);
    }

    [Fact(DisplayName = "UInt512.TryReadBigEndian with oversize source containing leading zeros should succeed")]
    public void UInt512TryReadBigEndianOversizeWithLeadingZerosShouldSucceed()
    {
        Span<byte> oversize = stackalloc byte[80];
        oversize.Clear();
        oversize[79] = 0x42;
        Assert.True(UInt512.TryReadBigEndian(oversize, isUnsigned: true, out UInt512 value));
        Assert.Equal((UInt512)0x42UL, value);
    }

    [Fact(DisplayName = "UInt512.TryReadBigEndian with oversize source containing significant high bytes should fail")]
    public void UInt512TryReadBigEndianOversizeWithHighBytesShouldFail()
    {
        Span<byte> oversize = stackalloc byte[80];
        oversize.Clear();
        oversize[0] = 0x01;
        Assert.False(UInt512.TryReadBigEndian(oversize, isUnsigned: true, out UInt512 value));
        Assert.Equal(UInt512.Zero, value);
    }

    [Fact(DisplayName = "UInt512.ReadBigEndian should match TryReadBigEndian for in-range source")]
    public void UInt512ReadBigEndianShouldMatchTryReadBigEndian()
    {
        Span<byte> buffer = stackalloc byte[64];
        buffer.Clear();
        buffer[63] = 0xFE;
        UInt512 read = UInt512.ReadBigEndian(buffer, isUnsigned: true);
        Assert.Equal((UInt512)0xFEUL, read);
    }

    [Fact(DisplayName = "UInt512.ReadBigEndian should throw when source is too large for the type")]
    public void UInt512ReadBigEndianTooLargeShouldThrow()
    {
        Span<byte> oversize = stackalloc byte[80];
        oversize.Clear();
        oversize[0] = 0x01;
        byte[] copy = oversize.ToArray();
        Assert.Throws<OverflowException>(() => UInt512.ReadBigEndian(copy, isUnsigned: true));
    }

    [Fact(DisplayName = "UInt512 big-endian writes should produce the network byte order of BigInteger")]
    public void UInt512BigEndianMatchesBigIntegerOrder()
    {
        UInt512 source = UInt512.Parse("123456789012345678901234567890");
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(source.TryWriteBigEndian(buffer, out _));

        BigInteger big = (BigInteger)source;
        byte[] bigBytes = big.ToByteArray(isUnsigned: true, isBigEndian: true);
        // Pad the BigInteger representation to 64 bytes to compare.
        byte[] padded = new byte[64];
        Array.Copy(bigBytes, 0, padded, 64 - bigBytes.Length, bigBytes.Length);
        Assert.Equal(padded, buffer.ToArray());
    }
}
