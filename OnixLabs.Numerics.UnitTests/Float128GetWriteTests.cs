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

public sealed class Float128GetWriteTests
{
    [Fact(DisplayName = "Float128.GetExponentByteCount should return 2 for binary128 layout")]
    public void Float128GetExponentByteCountShouldReturnTwo()
    {
        Assert.Equal(sizeof(short), Float128.One.GetExponentByteCount());
    }

    [Fact(DisplayName = "Float128.GetSignificandBitLength should return the 113-bit precision")]
    public void Float128GetSignificandBitLengthShouldReturnPrecision()
    {
        Assert.Equal(113, Float128.One.GetSignificandBitLength());
    }

    [Fact(DisplayName = "Float128.GetSignificandByteCount should return the 16-byte storage size")]
    public void Float128GetSignificandByteCountShouldReturnSixteen()
    {
        Assert.Equal(16, Float128.One.GetSignificandByteCount());
    }

    [Fact(DisplayName = "Float128.GetExponentShortestBitLength for 1.0 should be 0")]
    public void Float128GetExponentShortestBitLengthForOneShouldBeZero()
    {
        Assert.Equal(0, Float128.One.GetExponentShortestBitLength());
    }

    [Fact(DisplayName = "Float128.GetExponentShortestBitLength for 2.0 should be 1")]
    public void Float128GetExponentShortestBitLengthForTwoShouldBeOne()
    {
        Assert.Equal(1, Float128.Two.GetExponentShortestBitLength());
    }

    [Fact(DisplayName = "Float128.TryWriteExponentBigEndian should write the unbiased exponent")]
    public void Float128TryWriteExponentBigEndianShouldWriteCorrectBytes()
    {
        Span<byte> buffer = stackalloc byte[2];
        Assert.True(Float128.Two.TryWriteExponentBigEndian(buffer, out int written));
        Assert.Equal(2, written);
        Assert.Equal(0, buffer[0]);
        Assert.Equal(1, buffer[1]);
    }

    [Fact(DisplayName = "Float128.TryWriteExponentLittleEndian should write the unbiased exponent")]
    public void Float128TryWriteExponentLittleEndianShouldWriteCorrectBytes()
    {
        Span<byte> buffer = stackalloc byte[2];
        Assert.True(Float128.Two.TryWriteExponentLittleEndian(buffer, out int written));
        Assert.Equal(2, written);
        Assert.Equal(1, buffer[0]);
        Assert.Equal(0, buffer[1]);
    }

    [Fact(DisplayName = "Float128.TryWriteExponent should fail when destination is too small")]
    public void Float128TryWriteExponentShouldFailWhenBufferTooSmall()
    {
        Span<byte> buffer = stackalloc byte[1];
        Assert.False(Float128.Two.TryWriteExponentBigEndian(buffer, out int written));
        Assert.Equal(0, written);
    }

    [Fact(DisplayName = "Float128.TryWriteSignificandBigEndian should write the 16-byte significand")]
    public void Float128TryWriteSignificandBigEndianShouldWriteCorrectBytes()
    {
        // Float128.One has biased exponent 16383 (non-zero), so the written significand includes the
        // implicit leading bit, giving 0x0001_0000_..._0000 (bit 112 set). Big-endian writes the MSB
        // first, so byte 1 contains the implicit bit and all other bytes are zero.
        Span<byte> buffer = stackalloc byte[16];
        Assert.True(Float128.One.TryWriteSignificandBigEndian(buffer, out int written));
        Assert.Equal(16, written);

        Assert.Equal(0x00, buffer[0]);
        Assert.Equal(0x01, buffer[1]);
        for (int i = 2; i < 16; i++) Assert.Equal(0x00, buffer[i]);
    }

    [Fact(DisplayName = "Float128.TryWriteSignificandLittleEndian should write the 16-byte significand in reverse byte order")]
    public void Float128TryWriteSignificandLittleEndianShouldWriteCorrectBytes()
    {
        // Little-endian writes the LSB first; bit 112 lives in byte 14 (bytes 14..15 cover bits 112..127).
        Span<byte> buffer = stackalloc byte[16];
        Assert.True(Float128.One.TryWriteSignificandLittleEndian(buffer, out int written));
        Assert.Equal(16, written);

        for (int i = 0; i < 14; i++) Assert.Equal(0x00, buffer[i]);
        Assert.Equal(0x01, buffer[14]);
        Assert.Equal(0x00, buffer[15]);
    }
}
