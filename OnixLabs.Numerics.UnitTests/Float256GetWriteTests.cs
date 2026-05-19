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

public sealed class Float256GetWriteTests
{
    [Fact(DisplayName = "Float256.GetExponentByteCount should return 4 for binary256 layout")]
    public void Float256GetExponentByteCountShouldReturnFour()
    {
        Assert.Equal(sizeof(int), Float256.One.GetExponentByteCount());
    }

    [Fact(DisplayName = "Float256.GetSignificandBitLength should return the 237-bit precision")]
    public void Float256GetSignificandBitLengthShouldReturnPrecision()
    {
        Assert.Equal(237, Float256.One.GetSignificandBitLength());
    }

    [Fact(DisplayName = "Float256.GetSignificandByteCount should return the 32-byte storage size")]
    public void Float256GetSignificandByteCountShouldReturnThirtyTwo()
    {
        Assert.Equal(32, Float256.One.GetSignificandByteCount());
    }

    [Fact(DisplayName = "Float256.GetExponentShortestBitLength for 1.0 should be 0")]
    public void Float256GetExponentShortestBitLengthForOneShouldBeZero()
    {
        Assert.Equal(0, Float256.One.GetExponentShortestBitLength());
    }

    [Fact(DisplayName = "Float256.GetExponentShortestBitLength for 2.0 should be 1")]
    public void Float256GetExponentShortestBitLengthForTwoShouldBeOne()
    {
        Assert.Equal(1, Float256.Two.GetExponentShortestBitLength());
    }

    [Fact(DisplayName = "Float256.TryWriteExponentBigEndian should write the unbiased exponent")]
    public void Float256TryWriteExponentBigEndianShouldWriteCorrectBytes()
    {
        // Float256.Two has unbiased exponent of 1, written big-endian as 0x00 0x00 0x00 0x01.
        Span<byte> buffer = stackalloc byte[4];
        Assert.True(Float256.Two.TryWriteExponentBigEndian(buffer, out int written));
        Assert.Equal(4, written);
        Assert.Equal(0, buffer[0]);
        Assert.Equal(0, buffer[1]);
        Assert.Equal(0, buffer[2]);
        Assert.Equal(1, buffer[3]);
    }

    [Fact(DisplayName = "Float256.TryWriteExponentLittleEndian should write the unbiased exponent")]
    public void Float256TryWriteExponentLittleEndianShouldWriteCorrectBytes()
    {
        // Float256.Two has unbiased exponent of 1, written little-endian as 0x01 0x00 0x00 0x00.
        Span<byte> buffer = stackalloc byte[4];
        Assert.True(Float256.Two.TryWriteExponentLittleEndian(buffer, out int written));
        Assert.Equal(4, written);
        Assert.Equal(1, buffer[0]);
        Assert.Equal(0, buffer[1]);
        Assert.Equal(0, buffer[2]);
        Assert.Equal(0, buffer[3]);
    }

    [Fact(DisplayName = "Float256.TryWriteExponent should fail when destination is too small")]
    public void Float256TryWriteExponentShouldFailWhenBufferTooSmall()
    {
        Span<byte> buffer = stackalloc byte[3];
        Assert.False(Float256.Two.TryWriteExponentBigEndian(buffer, out int written));
        Assert.Equal(0, written);
    }

    [Fact(DisplayName = "Float256.TryWriteSignificandBigEndian should write the 32-byte significand")]
    public void Float256TryWriteSignificandBigEndianShouldWriteCorrectBytes()
    {
        // Float256.One has the implicit leading bit at significand bit 236.
        // In a 32-byte (256-bit) big-endian buffer, bit 236 sits in byte 32 - 1 - (236 / 8) = 2,
        // within that byte at position (236 % 8) = 4, so the byte value is 0x10.
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(Float256.One.TryWriteSignificandBigEndian(buffer, out int written));
        Assert.Equal(32, written);

        Assert.Equal(0x00, buffer[0]);
        Assert.Equal(0x00, buffer[1]);
        Assert.Equal(0x10, buffer[2]);
        for (int i = 3; i < 32; i++) Assert.Equal(0x00, buffer[i]);
    }

    [Fact(DisplayName = "Float256.TryWriteSignificandLittleEndian should write the 32-byte significand in reverse byte order")]
    public void Float256TryWriteSignificandLittleEndianShouldWriteCorrectBytes()
    {
        // Little-endian writes the LSB first; bit 236 lives in byte 236 / 8 = 29,
        // within that byte at position (236 % 8) = 4, so the byte value is 0x10.
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(Float256.One.TryWriteSignificandLittleEndian(buffer, out int written));
        Assert.Equal(32, written);

        for (int i = 0; i < 29; i++) Assert.Equal(0x00, buffer[i]);
        Assert.Equal(0x10, buffer[29]);
        Assert.Equal(0x00, buffer[30]);
        Assert.Equal(0x00, buffer[31]);
    }
}
