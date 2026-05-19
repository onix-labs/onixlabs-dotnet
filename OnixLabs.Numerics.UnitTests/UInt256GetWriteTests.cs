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

public sealed class UInt256GetWriteTests
{
    [Fact(DisplayName = "UInt256.GetByteCount should always return 32")]
    public void UInt256GetByteCountShouldAlwaysReturn32()
    {
        Assert.Equal(32, UInt256.Zero.GetByteCount());
        Assert.Equal(32, UInt256.One.GetByteCount());
        Assert.Equal(32, UInt256.MaxValue.GetByteCount());
    }

    [Theory(DisplayName = "UInt256.GetShortestBitLength of (One << shift) should equal shift plus one")]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(63, 64)]
    [InlineData(64, 65)]
    [InlineData(127, 128)]
    [InlineData(128, 129)]
    [InlineData(192, 193)]
    [InlineData(255, 256)]
    public void UInt256GetShortestBitLengthOfOneShiftedShouldEqualShiftPlusOne(int shift, int expected)
    {
        UInt256 value = UInt256.One << shift;
        Assert.Equal(expected, value.GetShortestBitLength());
    }

    [Fact(DisplayName = "UInt256.GetShortestBitLength of zero should be zero")]
    public void UInt256GetShortestBitLengthOfZeroShouldBeZero()
    {
        Assert.Equal(0, UInt256.Zero.GetShortestBitLength());
    }

    [Fact(DisplayName = "UInt256.TryWriteBigEndian into a too-small span should fail")]
    public void UInt256TryWriteBigEndianTooSmallSpanShouldFail()
    {
        Span<byte> buffer = stackalloc byte[16];
        Assert.False(UInt256.MaxValue.TryWriteBigEndian(buffer, out int bytesWritten));
        Assert.Equal(0, bytesWritten);
    }

    [Fact(DisplayName = "UInt256.TryWriteLittleEndian into a too-small span should fail")]
    public void UInt256TryWriteLittleEndianTooSmallSpanShouldFail()
    {
        Span<byte> buffer = stackalloc byte[16];
        Assert.False(UInt256.MaxValue.TryWriteLittleEndian(buffer, out int bytesWritten));
        Assert.Equal(0, bytesWritten);
    }

    [Fact(DisplayName = "UInt256.TryWriteBigEndian of One should place the trailing 0x01 byte at the end")]
    public void UInt256TryWriteBigEndianOfOneShouldPlaceTrailingOne()
    {
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(UInt256.One.TryWriteBigEndian(buffer, out int written));
        Assert.Equal(32, written);
        for (int i = 0; i < 31; i++) Assert.Equal(0, buffer[i]);
        Assert.Equal(1, buffer[31]);
    }

    [Fact(DisplayName = "UInt256.TryWriteLittleEndian of One should place the leading 0x01 byte at the start")]
    public void UInt256TryWriteLittleEndianOfOneShouldPlaceLeadingOne()
    {
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(UInt256.One.TryWriteLittleEndian(buffer, out int written));
        Assert.Equal(32, written);
        Assert.Equal(1, buffer[0]);
        for (int i = 1; i < 32; i++) Assert.Equal(0, buffer[i]);
    }

    [Fact(DisplayName = "UInt256.TryWriteBigEndian and TryWriteLittleEndian should produce reversed buffers")]
    public void UInt256TryWriteBigEndianAndLittleEndianShouldProduceReversedBuffers()
    {
        UInt256 value = UInt256.Parse("123456789012345678901234567890");
        Span<byte> beBuffer = stackalloc byte[32];
        Span<byte> leBuffer = stackalloc byte[32];

        Assert.True(value.TryWriteBigEndian(beBuffer, out _));
        Assert.True(value.TryWriteLittleEndian(leBuffer, out _));

        for (int i = 0; i < 32; i++) Assert.Equal(beBuffer[i], leBuffer[31 - i]);
    }

    [Fact(DisplayName = "UInt256.TryReadBigEndian from a longer buffer with leading zeros should succeed")]
    public void UInt256TryReadBigEndianFromLongerBufferWithLeadingZerosShouldSucceed()
    {
        Span<byte> buffer = stackalloc byte[40];
        // Last byte holds the value 0x42.
        buffer[39] = 0x42;
        Assert.True(UInt256.TryReadBigEndian(buffer, true, out UInt256 value));
        Assert.Equal((UInt256)0x42, value);
    }

    [Fact(DisplayName = "UInt256.TryReadBigEndian from a longer buffer with overflow bytes should fail")]
    public void UInt256TryReadBigEndianFromLongerBufferWithOverflowShouldFail()
    {
        Span<byte> buffer = stackalloc byte[40];
        // Leading non-zero byte should cause overflow detection.
        buffer[0] = 0x01;
        buffer[39] = 0x00;
        Assert.False(UInt256.TryReadBigEndian(buffer, true, out _));
    }

    [Fact(DisplayName = "UInt256.TryReadLittleEndian from a longer buffer with trailing zeros should succeed")]
    public void UInt256TryReadLittleEndianFromLongerBufferWithTrailingZerosShouldSucceed()
    {
        Span<byte> buffer = stackalloc byte[40];
        buffer[0] = 0x42;
        Assert.True(UInt256.TryReadLittleEndian(buffer, true, out UInt256 value));
        Assert.Equal((UInt256)0x42, value);
    }

    [Fact(DisplayName = "UInt256.TryReadBigEndian from an empty span should yield zero")]
    public void UInt256TryReadBigEndianFromEmptySpanShouldYieldZero()
    {
        Assert.True(UInt256.TryReadBigEndian(ReadOnlySpan<byte>.Empty, true, out UInt256 value));
        Assert.Equal(UInt256.Zero, value);
    }

    [Fact(DisplayName = "UInt256.TryReadBigEndian round-trip of MaxValue should match")]
    public void UInt256TryReadBigEndianRoundTripOfMaxValueShouldMatch()
    {
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(UInt256.MaxValue.TryWriteBigEndian(buffer, out _));
        Assert.True(UInt256.TryReadBigEndian(buffer, true, out UInt256 read));
        Assert.Equal(UInt256.MaxValue, read);
    }
}
