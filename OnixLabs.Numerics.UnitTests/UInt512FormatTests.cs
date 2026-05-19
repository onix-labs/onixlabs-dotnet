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
using System.Text;

namespace OnixLabs.Numerics.UnitTests;

public sealed class UInt512FormatTests
{
    [Fact(DisplayName = "UInt512.ToString with no format should produce decimal representation")]
    public void UInt512ToStringNoFormatShouldProduceDecimal()
    {
        UInt512 value = (UInt512)12345UL;
        Assert.Equal("12345", value.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt512.ToString with 'D' format should produce decimal representation")]
    public void UInt512ToStringDShouldProduceDecimal()
    {
        UInt512 value = (UInt512)98765UL;
        Assert.Equal("98765", value.ToString("D", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt512.ToString with 'D10' format should pad with leading zeroes")]
    public void UInt512ToStringD10ShouldPadWithLeadingZeroes()
    {
        UInt512 value = (UInt512)123UL;
        Assert.Equal("0000000123", value.ToString("D10", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt512.ToString with 'X' format should produce hexadecimal representation without sign-disambiguation padding")]
    public void UInt512ToStringXShouldProduceHex()
    {
        UInt512 value = (UInt512)0xABCDEFUL;
        Assert.Equal("ABCDEF", value.ToString("X", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt512.ToString with 'x' format should produce lowercase hexadecimal representation")]
    public void UInt512ToStringLowercaseXShouldProduceLowercaseHex()
    {
        UInt512 value = (UInt512)0xABCDEFUL;
        Assert.Equal("abcdef", value.ToString("x", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt512.ToString with 'X16' format should pad to at least 16 hex digits")]
    public void UInt512ToStringX16ShouldPadToSixteenDigits()
    {
        UInt512 value = (UInt512)0xABCDUL;
        Assert.Equal("000000000000ABCD", value.ToString("X16", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt512.ToString with 'X' format for MaxValue should produce 128 hex digits")]
    public void UInt512ToStringXForMaxValueShouldProduce128HexDigits()
    {
        string actual = UInt512.MaxValue.ToString("X", CultureInfo.InvariantCulture);
        Assert.Equal(new string('F', 128), actual);
    }

    [Fact(DisplayName = "UInt512.ToString with 'R' format should produce the decimal representation")]
    public void UInt512ToStringRFormatShouldProduceDecimal()
    {
        UInt512 value = (UInt512)98765UL;
        Assert.Equal("98765", value.ToString("R", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt512.ToString with 'X' format for Zero should produce 0")]
    public void UInt512ToStringXForZeroShouldProduceZero()
    {
        Assert.Equal("0", UInt512.Zero.ToString("X", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt512.ToString of MaxValue should be the decimal expansion of 2^512 - 1")]
    public void UInt512ToStringMaxValueShouldProduce155DigitDecimal()
    {
        string actual = UInt512.MaxValue.ToString(null, CultureInfo.InvariantCulture);
        string expected = "13407807929942597099574024998205846127479365820592393377723561443721764030073546976801874298166903427690031858186486050853753882811946569946433649006084095";
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "UInt512.ToString with culture-specific separator should match BigInteger behavior")]
    public void UInt512ToStringCultureShouldMatchBigInteger()
    {
        UInt512 value = (UInt512)1234567UL;
        string actual = value.ToString("N0", CultureInfo.InvariantCulture);
        Assert.Equal("1,234,567", actual);
    }

    [Fact(DisplayName = "UInt512.TryFormat into a span should succeed when the destination is large enough")]
    public void UInt512TryFormatShouldSucceedWithLargeDestination()
    {
        UInt512 value = (UInt512)1234567UL;
        Span<char> buffer = stackalloc char[64];
        Assert.True(value.TryFormat(buffer, out int written, default, CultureInfo.InvariantCulture));
        Assert.Equal(7, written);
        Assert.Equal("1234567", buffer[..written].ToString());
    }

    [Fact(DisplayName = "UInt512.TryFormat should fail when the destination is too small")]
    public void UInt512TryFormatShouldFailWithSmallDestination()
    {
        UInt512 value = (UInt512)1234567UL;
        Span<char> buffer = stackalloc char[3];
        Assert.False(value.TryFormat(buffer, out int written, default, CultureInfo.InvariantCulture));
        Assert.Equal(0, written);
    }

    [Fact(DisplayName = "UInt512.TryFormat should respect format specifier")]
    public void UInt512TryFormatShouldRespectFormatSpecifier()
    {
        UInt512 value = (UInt512)0xABCDUL;
        Span<char> buffer = stackalloc char[16];
        Assert.True(value.TryFormat(buffer, out int written, "X", CultureInfo.InvariantCulture));
        Assert.Equal("ABCD", buffer[..written].ToString());
    }

    [Fact(DisplayName = "UInt512.TryFormat into a UTF-8 span should produce the expected bytes")]
    public void UInt512TryFormatUtf8ShouldProduceExpectedBytes()
    {
        UInt512 value = (UInt512)1234567UL;
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(value.TryFormat(buffer, out int written, default, CultureInfo.InvariantCulture));
        Assert.Equal(7, written);
        Assert.Equal("1234567", Encoding.UTF8.GetString(buffer[..written]));
    }

    [Fact(DisplayName = "UInt512.TryFormat UTF-8 should fail when destination is too small")]
    public void UInt512TryFormatUtf8ShouldFailWithSmallDestination()
    {
        UInt512 value = (UInt512)1234567UL;
        Span<byte> buffer = stackalloc byte[3];
        Assert.False(value.TryFormat(buffer, out int written, default, CultureInfo.InvariantCulture));
        Assert.Equal(0, written);
    }
}
