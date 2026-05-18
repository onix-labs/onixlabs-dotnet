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

public sealed class Int256FormatTests
{
    [Fact(DisplayName = "Int256.ToString without format should produce decimal representation")]
    public void Int256ToStringWithoutFormatShouldProduceDecimal()
    {
        Int256 value = (Int256)(-12345);
        Assert.Equal("-12345", value.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.ToString with G format should produce decimal representation")]
    public void Int256ToStringWithGFormatShouldProduceDecimal()
    {
        Int256 value = (Int256)(-100);
        Assert.Equal("-100", value.ToString("G", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.ToString with D format should produce decimal representation")]
    public void Int256ToStringWithDFormatShouldProduceDecimal()
    {
        Int256 value = (Int256)42;
        Assert.Equal("42", value.ToString("D", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.ToString of MinValue should produce the canonical text representation")]
    public void Int256ToStringOfMinValueShouldProduceCanonicalText()
    {
        string expected = "-57896044618658097711785492504343953926634992332820282019728792003956564819968";
        Assert.Equal(expected, Int256.MinValue.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.ToString of MaxValue should produce the canonical text representation")]
    public void Int256ToStringOfMaxValueShouldProduceCanonicalText()
    {
        string expected = "57896044618658097711785492504343953926634992332820282019728792003956564819967";
        Assert.Equal(expected, Int256.MaxValue.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.ToString of NegativeOne should produce '-1'")]
    public void Int256ToStringOfNegativeOneShouldProduceMinusOne()
    {
        Assert.Equal("-1", Int256.NegativeOne.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.ToString with N0 format should produce grouped representation")]
    public void Int256ToStringWithN0FormatShouldProduceGrouped()
    {
        Int256 value = (Int256)(-1234567);
        Assert.Equal("-1,234,567", value.ToString("N0", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.TryFormat into a sufficiently sized span should succeed")]
    public void Int256TryFormatIntoSufficientSpanShouldSucceed()
    {
        Int256 value = (Int256)(-12345);
        Span<char> buffer = stackalloc char[16];
        bool result = value.TryFormat(buffer, out int charsWritten, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.Equal("-12345", buffer.Slice(0, charsWritten).ToString());
    }

    [Fact(DisplayName = "Int256.TryFormat into a too-small span should fail and write zero characters")]
    public void Int256TryFormatIntoTooSmallSpanShouldFail()
    {
        Int256 value = (Int256)(-12345);
        Span<char> buffer = stackalloc char[2];
        bool result = value.TryFormat(buffer, out int charsWritten, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.Equal(0, charsWritten);
    }

    [Fact(DisplayName = "Int256.TryFormat into a UTF-8 span should write UTF-8 bytes")]
    public void Int256TryFormatIntoUtf8SpanShouldWriteBytes()
    {
        Int256 value = (Int256)(-1);
        Span<byte> buffer = stackalloc byte[16];
        bool result = value.TryFormat(buffer, out int bytesWritten, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.Equal(2, bytesWritten);
        Assert.Equal((byte)'-', buffer[0]);
        Assert.Equal((byte)'1', buffer[1]);
    }

    [Fact(DisplayName = "Int256.ToString of zero should produce '0'")]
    public void Int256ToStringOfZeroShouldProduceZero()
    {
        Assert.Equal("0", Int256.Zero.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.ToString with X format for positive value should produce hex without sign-disambiguation padding")]
    public void Int256ToStringWithXFormatForPositiveShouldProduceHex()
    {
        Int256 value = (Int256)0xCAFEBABEu;
        Assert.Equal("CAFEBABE", value.ToString("X", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.ToString with X format for NegativeOne should produce 64 'F' hex digits (two's complement)")]
    public void Int256ToStringWithXFormatForNegativeOneShouldProduceAllFs()
    {
        string actual = Int256.NegativeOne.ToString("X", CultureInfo.InvariantCulture);
        Assert.Equal(new string('F', 64), actual);
    }

    [Fact(DisplayName = "Int256.ToString with X format for MinValue should produce 80...0 (sign bit set, rest clear)")]
    public void Int256ToStringWithXFormatForMinValueShouldProduceSignBit()
    {
        string actual = Int256.MinValue.ToString("X", CultureInfo.InvariantCulture);
        Assert.Equal("8" + new string('0', 63), actual);
    }

    [Fact(DisplayName = "Int256.ToString with X format for MaxValue should produce 7FFF...F")]
    public void Int256ToStringWithXFormatForMaxValueShouldProduceSignClearAllOnes()
    {
        string actual = Int256.MaxValue.ToString("X", CultureInfo.InvariantCulture);
        Assert.Equal("7" + new string('F', 63), actual);
    }

    [Fact(DisplayName = "Int256.ToString with x format should produce lowercase hex")]
    public void Int256ToStringWithLowercaseXFormatShouldProduceLowercase()
    {
        Int256 value = (Int256)0xCAFE;
        Assert.Equal("cafe", value.ToString("x", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.ToString with X8 format should pad to at least 8 hex digits")]
    public void Int256ToStringWithX8FormatShouldPadToEightDigits()
    {
        Int256 value = (Int256)0xAB;
        Assert.Equal("000000AB", value.ToString("X8", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.ToString with R format for positive value should produce signed decimal")]
    public void Int256ToStringWithRFormatForPositiveShouldProduceSignedDecimal()
    {
        Int256 value = (Int256)1234567;
        Assert.Equal("1234567", value.ToString("R", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.ToString with R format for negative value should produce signed decimal")]
    public void Int256ToStringWithRFormatForNegativeShouldProduceSignedDecimal()
    {
        Int256 value = (Int256)(-1234567);
        Assert.Equal("-1234567", value.ToString("R", CultureInfo.InvariantCulture));
    }
}
