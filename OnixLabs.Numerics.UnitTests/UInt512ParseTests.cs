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

public sealed class UInt512ParseTests
{
    private const string MaxValueString =
        "13407807929942597099574024998205846127479365820592393377723561443721764030073546976801874298166903427690031858186486050853753882811946569946433649006084095";

    private const string MaxValuePlusOneString =
        "13407807929942597099574024998205846127479365820592393377723561443721764030073546976801874298166903427690031858186486050853753882811946569946433649006084096";

    [Fact(DisplayName = "UInt512.Parse should accept Zero")]
    public void UInt512ParseShouldAcceptZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.Parse("0"));
    }

    [Fact(DisplayName = "UInt512.Parse should accept One")]
    public void UInt512ParseShouldAcceptOne()
    {
        Assert.Equal(UInt512.One, UInt512.Parse("1"));
    }

    [Fact(DisplayName = "UInt512.Parse should round-trip MaxValue")]
    public void UInt512ParseShouldRoundTripMaxValue()
    {
        UInt512 value = UInt512.Parse(MaxValueString);
        Assert.Equal(UInt512.MaxValue, value);
        Assert.Equal(MaxValueString, value.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt512.Parse of MaxValue plus one should throw OverflowException")]
    public void UInt512ParseOfMaxValuePlusOneShouldThrow()
    {
        Assert.Throws<OverflowException>(() => UInt512.Parse(MaxValuePlusOneString));
    }

    [Fact(DisplayName = "UInt512.Parse of negative value should throw OverflowException")]
    public void UInt512ParseOfNegativeShouldThrow()
    {
        Assert.Throws<OverflowException>(() => UInt512.Parse("-1"));
    }

    [Fact(DisplayName = "UInt512.Parse of empty string should throw FormatException")]
    public void UInt512ParseOfEmptyShouldThrow()
    {
        Assert.Throws<FormatException>(() => UInt512.Parse(""));
    }

    [Fact(DisplayName = "UInt512.Parse of invalid characters should throw FormatException")]
    public void UInt512ParseOfInvalidShouldThrow()
    {
        Assert.Throws<FormatException>(() => UInt512.Parse("hello"));
    }

    [Fact(DisplayName = "UInt512.Parse with hex style should parse hexadecimal strings when the leading hex digit is below 8")]
    public void UInt512ParseShouldAcceptHexStyle()
    {
        // BigInteger.Parse(HexNumber) treats a leading nibble >= 8 as the sign bit.
        UInt512 value = UInt512.Parse("0FF", NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        Assert.Equal((UInt512)255UL, value);
    }

    [Fact(DisplayName = "UInt512.Parse with hex style and a high leading nibble should be rejected as negative")]
    public void UInt512ParseHexWithHighLeadingNibbleShouldRejectAsNegative()
    {
        // "FF" with HexNumber style is interpreted by BigInteger as -1; UInt512 rejects this.
        Assert.Throws<OverflowException>(() => UInt512.Parse("FF", NumberStyles.HexNumber, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt512.TryParse should succeed for valid integer strings")]
    public void UInt512TryParseShouldSucceedForValid()
    {
        Assert.True(UInt512.TryParse("12345", out UInt512 result));
        Assert.Equal((UInt512)12345UL, result);
    }

    [Fact(DisplayName = "UInt512.TryParse should fail for invalid strings and return default")]
    public void UInt512TryParseShouldFailForInvalid()
    {
        Assert.False(UInt512.TryParse("garbage", out UInt512 result));
        Assert.Equal(UInt512.Zero, result);
    }

    [Fact(DisplayName = "UInt512.TryParse should fail for negative input")]
    public void UInt512TryParseShouldFailForNegative()
    {
        Assert.False(UInt512.TryParse("-1", out UInt512 result));
        Assert.Equal(UInt512.Zero, result);
    }

    [Fact(DisplayName = "UInt512.TryParse should fail for over-large input")]
    public void UInt512TryParseShouldFailForOverLarge()
    {
        Assert.False(UInt512.TryParse(MaxValuePlusOneString, out UInt512 result));
        Assert.Equal(UInt512.Zero, result);
    }

    [Fact(DisplayName = "UInt512.TryParse with null should fail")]
    public void UInt512TryParseWithNullShouldFail()
    {
        Assert.False(UInt512.TryParse((string?)null, out UInt512 result));
        Assert.Equal(UInt512.Zero, result);
    }

    [Fact(DisplayName = "UInt512.Parse with ReadOnlySpan<char> should accept large value")]
    public void UInt512ParseWithSpanShouldAcceptLargeValue()
    {
        ReadOnlySpan<char> input = "12345678901234567890123456789012345678901234567890".AsSpan();
        UInt512 value = UInt512.Parse(input);
        Assert.Equal(UInt512.Parse("12345678901234567890123456789012345678901234567890"), value);
    }

    [Fact(DisplayName = "UInt512.Parse should ignore leading whitespace when AllowLeadingWhite is set")]
    public void UInt512ParseShouldRespectNumberStyles()
    {
        UInt512 value = UInt512.Parse("   42", NumberStyles.Integer | NumberStyles.AllowLeadingWhite, CultureInfo.InvariantCulture);
        Assert.Equal((UInt512)42UL, value);
    }
}
