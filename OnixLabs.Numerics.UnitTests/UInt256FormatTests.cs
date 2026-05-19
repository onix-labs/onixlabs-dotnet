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
using System.Globalization;

namespace OnixLabs.Numerics.UnitTests;

public sealed class UInt256FormatTests
{
    [Fact(DisplayName = "UInt256.ToString without format should produce the decimal representation")]
    public void UInt256ToStringWithoutFormatShouldProduceDecimal()
    {
        UInt256 value = (UInt256)1234567890UL;
        Assert.Equal("1234567890", value.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt256.ToString with G format should produce the decimal representation")]
    public void UInt256ToStringWithGFormatShouldProduceDecimal()
    {
        UInt256 value = (UInt256)100;
        Assert.Equal("100", value.ToString("G", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt256.ToString with D format should produce the decimal representation")]
    public void UInt256ToStringWithDFormatShouldProduceDecimal()
    {
        UInt256 value = (UInt256)42;
        Assert.Equal("42", value.ToString("D", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt256.ToString with X format should produce the hex representation without sign-disambiguation padding")]
    public void UInt256ToStringWithXFormatShouldProduceHex()
    {
        UInt256 value = (UInt256)0xCAFEBABEu;
        Assert.Equal("CAFEBABE", value.ToString("X", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt256.ToString with x format should produce the lowercase hex representation")]
    public void UInt256ToStringWithLowercaseXFormatShouldProduceLowercaseHex()
    {
        UInt256 value = (UInt256)0xCAFEBABEu;
        Assert.Equal("cafebabe", value.ToString("x", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt256.ToString with X8 format should pad to at least 8 hex digits")]
    public void UInt256ToStringWithX8FormatShouldPadToEightDigits()
    {
        UInt256 value = (UInt256)0xABu;
        Assert.Equal("000000AB", value.ToString("X8", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt256.ToString with X format for MaxValue should produce 64 hex digits")]
    public void UInt256ToStringWithXFormatForMaxValueShouldProduce64HexDigits()
    {
        string actual = UInt256.MaxValue.ToString("X", CultureInfo.InvariantCulture);
        Assert.Equal(new string('F', 64), actual);
    }

    [Fact(DisplayName = "UInt256.ToString with R format should produce the decimal representation")]
    public void UInt256ToStringWithRFormatShouldProduceDecimal()
    {
        UInt256 value = (UInt256)1234567UL;
        Assert.Equal("1234567", value.ToString("R", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt256.ToString with N0 format should produce grouped representation")]
    public void UInt256ToStringWithN0FormatShouldProduceGrouped()
    {
        UInt256 value = (UInt256)1234567UL;
        Assert.Equal("1,234,567", value.ToString("N0", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt256.ToString of MaxValue should produce the canonical 78-digit decimal")]
    public void UInt256ToStringOfMaxValueShouldProduce78DigitDecimal()
    {
        string expected = "115792089237316195423570985008687907853269984665640564039457584007913129639935";
        Assert.Equal(expected, UInt256.MaxValue.ToString(null, CultureInfo.InvariantCulture));
        Assert.Equal(78, expected.Length);
    }

    [Fact(DisplayName = "UInt256.TryFormat into a sufficiently sized span should succeed")]
    public void UInt256TryFormatIntoSufficientSpanShouldSucceed()
    {
        UInt256 value = (UInt256)12345UL;
        Span<char> buffer = stackalloc char[16];
        bool result = value.TryFormat(buffer, out int charsWritten, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.Equal(5, charsWritten);
        Assert.Equal("12345", buffer.Slice(0, charsWritten).ToString());
    }

    [Fact(DisplayName = "UInt256.TryFormat into a too-small span should fail and write zero characters")]
    public void UInt256TryFormatIntoTooSmallSpanShouldFail()
    {
        UInt256 value = (UInt256)12345UL;
        Span<char> buffer = stackalloc char[2];
        bool result = value.TryFormat(buffer, out int charsWritten, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.Equal(0, charsWritten);
    }

    [Fact(DisplayName = "UInt256.TryFormat with X format into a span should write hex characters without sign-disambiguation padding")]
    public void UInt256TryFormatWithXFormatShouldWriteHexCharacters()
    {
        UInt256 value = (UInt256)0xABCD;
        Span<char> buffer = stackalloc char[16];
        bool result = value.TryFormat(buffer, out int charsWritten, "X".AsSpan(), CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.Equal("ABCD", buffer.Slice(0, charsWritten).ToString());
    }

    [Fact(DisplayName = "UInt256.TryFormat into a UTF-8 span should write UTF-8 bytes")]
    public void UInt256TryFormatIntoUtf8SpanShouldWriteBytes()
    {
        UInt256 value = (UInt256)42;
        Span<byte> buffer = stackalloc byte[16];
        bool result = value.TryFormat(buffer, out int bytesWritten, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.Equal(2, bytesWritten);
        Assert.Equal((byte)'4', buffer[0]);
        Assert.Equal((byte)'2', buffer[1]);
    }

    [Fact(DisplayName = "UInt256.ToString of zero should produce the literal '0'")]
    public void UInt256ToStringOfZeroShouldProduceZero()
    {
        Assert.Equal("0", UInt256.Zero.ToString(null, CultureInfo.InvariantCulture));
    }
}
