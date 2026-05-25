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

public sealed class Int256GetWriteTests
{
    [Fact(DisplayName = "Int256.GetByteCount should always return 32")]
    public void Int256GetByteCountShouldAlwaysReturn32()
    {
        Assert.Equal(32, Int256.Zero.GetByteCount());
        Assert.Equal(32, Int256.NegativeOne.GetByteCount());
        Assert.Equal(32, Int256.MinValue.GetByteCount());
    }

    [Fact(DisplayName = "Int256.TryWriteBigEndian of NegativeOne should produce all 0xFF bytes (sign-extended)")]
    public void Int256TryWriteBigEndianOfNegativeOneShouldProduceAllFFBytes()
    {
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(Int256.NegativeOne.TryWriteBigEndian(buffer, out int written));
        Assert.Equal(32, written);
        for (int i = 0; i < 32; i++) Assert.Equal((byte)0xFF, buffer[i]);
    }

    [Fact(DisplayName = "Int256.TryWriteLittleEndian of NegativeOne should produce all 0xFF bytes (sign-extended)")]
    public void Int256TryWriteLittleEndianOfNegativeOneShouldProduceAllFFBytes()
    {
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(Int256.NegativeOne.TryWriteLittleEndian(buffer, out int written));
        Assert.Equal(32, written);
        for (int i = 0; i < 32; i++) Assert.Equal((byte)0xFF, buffer[i]);
    }

    [Fact(DisplayName = "Int256.TryWriteBigEndian of MinValue should set only the leading sign bit")]
    public void Int256TryWriteBigEndianOfMinValueShouldSetLeadingSignBit()
    {
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(Int256.MinValue.TryWriteBigEndian(buffer, out int written));
        Assert.Equal(32, written);
        Assert.Equal((byte)0x80, buffer[0]);
        for (int i = 1; i < 32; i++) Assert.Equal((byte)0x00, buffer[i]);
    }

    [Fact(DisplayName = "Int256.TryWriteLittleEndian of MinValue should set the trailing sign bit")]
    public void Int256TryWriteLittleEndianOfMinValueShouldSetTrailingSignBit()
    {
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(Int256.MinValue.TryWriteLittleEndian(buffer, out int written));
        Assert.Equal(32, written);
        for (int i = 0; i < 31; i++) Assert.Equal((byte)0x00, buffer[i]);
        Assert.Equal((byte)0x80, buffer[31]);
    }

    [Fact(DisplayName = "Int256.TryWriteBigEndian into too-small span should fail")]
    public void Int256TryWriteBigEndianTooSmallSpanShouldFail()
    {
        Span<byte> buffer = stackalloc byte[16];
        Assert.False(Int256.NegativeOne.TryWriteBigEndian(buffer, out int written));
        Assert.Equal(0, written);
    }

    [Fact(DisplayName = "Int256.TryReadBigEndian of negative value should round-trip")]
    public void Int256TryReadBigEndianOfNegativeValueShouldRoundTrip()
    {
        Int256 source = Int256.Parse("-12345678901234567890123456789");
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(source.TryWriteBigEndian(buffer, out _));
        Assert.True(Int256.TryReadBigEndian(buffer, false, out Int256 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "Int256.TryReadLittleEndian of negative value should round-trip")]
    public void Int256TryReadLittleEndianOfNegativeValueShouldRoundTrip()
    {
        Int256 source = (Int256)(-BigInteger.Pow(2, 200));
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(source.TryWriteLittleEndian(buffer, out _));
        Assert.True(Int256.TryReadLittleEndian(buffer, false, out Int256 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "Int256.TryReadBigEndian of longer signed buffer with proper sign extension should succeed")]
    public void Int256TryReadBigEndianOfLongerSignedBufferWithProperSignExtensionShouldSucceed()
    {
        Span<byte> buffer = stackalloc byte[40];
        // First 8 bytes are 0xFF as sign extension; remaining 32 bytes spell out NegativeOne.
        for (int i = 0; i < 40; i++) buffer[i] = 0xFF;
        Assert.True(Int256.TryReadBigEndian(buffer, false, out Int256 value));
        Assert.Equal(Int256.NegativeOne, value);
    }

    [Fact(DisplayName = "Int256.TryReadBigEndian of longer signed buffer with non-sign-extension overflow bytes should fail")]
    public void Int256TryReadBigEndianOfLongerSignedBufferWithNonSignExtensionOverflowBytesShouldFail()
    {
        Span<byte> buffer = stackalloc byte[40];
        // Leading byte (sign indicator) is 0x00 -> positive, so overflow bytes [0..7] must all be 0x00.
        // Insert a 0x42 in the overflow region to break that contract.
        buffer[1] = 0x42;
        // Remaining bytes [8..39] are zero, so the 32-byte value itself would be zero.
        Assert.False(Int256.TryReadBigEndian(buffer, false, out _));
    }

    [Fact(DisplayName = "Int256.TryReadBigEndian as unsigned of all-0xFF should produce value with positive interpretation")]
    public void Int256TryReadBigEndianAsUnsignedShouldNotApplySignExtension()
    {
        // 33 bytes: leading 0x00, then 32 bytes of 0xFF — this is positive UInt256.MaxValue.
        Span<byte> buffer = stackalloc byte[33];
        buffer[0] = 0x00;
        for (int i = 1; i < 33; i++) buffer[i] = 0xFF;
        Assert.True(Int256.TryReadBigEndian(buffer, true, out Int256 value));
        // The result reinterprets UInt256.MaxValue's bits, which is NegativeOne as signed.
        Assert.Equal(Int256.NegativeOne, value);
    }

    [Fact(DisplayName = "Int256.TryReadBigEndian from empty span should yield zero")]
    public void Int256TryReadBigEndianFromEmptySpanShouldYieldZero()
    {
        Assert.True(Int256.TryReadBigEndian(ReadOnlySpan<byte>.Empty, false, out Int256 value));
        Assert.Equal(Int256.Zero, value);
    }

    [Fact(DisplayName = "Int256.TryWriteBigEndian and TryWriteLittleEndian should produce reversed buffers for non-zero values")]
    public void Int256TryWriteBigEndianAndLittleEndianShouldProduceReversedBuffers()
    {
        Int256 value = Int256.Parse("-12345678901234567890123456789");
        Span<byte> beBuffer = stackalloc byte[32];
        Span<byte> leBuffer = stackalloc byte[32];

        Assert.True(value.TryWriteBigEndian(beBuffer, out _));
        Assert.True(value.TryWriteLittleEndian(leBuffer, out _));

        for (int i = 0; i < 32; i++) Assert.Equal(beBuffer[i], leBuffer[31 - i]);
    }

    [Theory(DisplayName = "Int256.GetShortestBitLength should match the Int128 sibling for in-range values")]
    [InlineData(0L)]
    [InlineData(1L)]
    [InlineData(-1L)]
    [InlineData(-2L)]
    [InlineData(255L)]
    [InlineData(-128L)]
    [InlineData(long.MaxValue)]
    [InlineData(long.MinValue)]
    public void Int256GetShortestBitLengthShouldMatchInt128Sibling(long value)
    {
        // The honest oracle is the BCL IBinaryInteger sibling Int128 (NOT BigInteger.GetBitLength,
        // which differs by one for negative values).
        int expected = ShortestBitLength((Int128)value);
        Assert.Equal(expected, ((Int256)value).GetShortestBitLength());
    }

    private static int ShortestBitLength<T>(T value) where T : IBinaryInteger<T> => value.GetShortestBitLength();

    [Fact(DisplayName = "Int256.GetShortestBitLength of constants should match independently-known bit lengths")]
    public void Int256GetShortestBitLengthOfConstantsShouldMatchKnownValues()
    {
        // MaxValue = 2^255 - 1: 255 value bits, no sign bit. MinValue = -2^255: needs the full 256 bits.
        Assert.Equal(0, Int256.Zero.GetShortestBitLength());
        Assert.Equal(1, Int256.One.GetShortestBitLength());
        Assert.Equal(1, Int256.NegativeOne.GetShortestBitLength());
        Assert.Equal(255, Int256.MaxValue.GetShortestBitLength());
        Assert.Equal(256, Int256.MinValue.GetShortestBitLength());
    }

    [Fact(DisplayName = "Int256.GetShortestBitLength of a large positive value should match Int128 sibling shifted into the upper limb")]
    public void Int256GetShortestBitLengthOfUpperLimbValueShouldMatchSibling()
    {
        // 2^200 + 1 occupies the upper limb: bit length is 201 (highest set bit is index 200, no sign bit needed).
        Int256 value = (Int256)((BigInteger.One << 200) + BigInteger.One);
        Assert.Equal(201, value.GetShortestBitLength());
    }

    [Fact(DisplayName = "Int256.TryWriteBigEndian of a positive value should match BigInteger big-endian bytes")]
    public void Int256TryWriteBigEndianShouldMatchBigIntegerBytes()
    {
        Int256 value = (Int256)(BigInteger.Pow(2, 200) + 12345);
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(value.TryWriteBigEndian(buffer, out _));

        BigInteger big = (BigInteger)value;
        byte[] bigBytes = big.ToByteArray(isUnsigned: false, isBigEndian: true);
        byte[] expected = new byte[32];
        Array.Copy(bigBytes, 0, expected, 32 - bigBytes.Length, bigBytes.Length);
        Assert.Equal(expected, buffer.ToArray());
    }
}
