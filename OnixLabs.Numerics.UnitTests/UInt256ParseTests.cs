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

public sealed class UInt256ParseTests
{
    private const string MaxValueText = "115792089237316195423570985008687907853269984665640564039457584007913129639935";

    [Fact(DisplayName = "UInt256.Parse should parse zero from string")]
    public void UInt256ParseShouldParseZero()
    {
        Assert.Equal(UInt256.Zero, UInt256.Parse("0"));
    }

    [Fact(DisplayName = "UInt256.Parse should parse one from string")]
    public void UInt256ParseShouldParseOne()
    {
        Assert.Equal(UInt256.One, UInt256.Parse("1"));
    }

    [Fact(DisplayName = "UInt256.Parse should round-trip MaxValue text representation")]
    public void UInt256ParseShouldRoundTripMaxValueText()
    {
        UInt256 parsed = UInt256.Parse(MaxValueText);
        Assert.Equal(UInt256.MaxValue, parsed);
        Assert.Equal(MaxValueText, parsed.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt256.Parse with hex style should parse a hex string")]
    public void UInt256ParseWithHexStyleShouldParseHexString()
    {
        // BigInteger.Parse treats a hex string whose top nibble is >= 8 as a negative number, so we
        // prefix with '0' to ensure the parsed value is interpreted as positive.
        UInt256 parsed = UInt256.Parse("0ABCDEF", NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        Assert.Equal((UInt256)0xABCDEFu, parsed);
    }

    [Fact(DisplayName = "UInt256.Parse with invariant culture should parse plain integer")]
    public void UInt256ParseWithInvariantCultureShouldParsePlainInteger()
    {
        Assert.Equal((UInt256)42, UInt256.Parse("42", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt256.Parse of negative value should throw OverflowException")]
    public void UInt256ParseOfNegativeShouldThrow()
    {
        Assert.Throws<OverflowException>(() => UInt256.Parse("-1", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt256.Parse of malformed text should throw FormatException")]
    public void UInt256ParseOfMalformedTextShouldThrow()
    {
        Assert.Throws<FormatException>(() => UInt256.Parse("not-a-number", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "UInt256.TryParse should succeed for valid input")]
    public void UInt256TryParseShouldSucceedForValidInput()
    {
        Assert.True(UInt256.TryParse("12345", out UInt256 result));
        Assert.Equal((UInt256)12345, result);
    }

    [Fact(DisplayName = "UInt256.TryParse should fail for malformed input")]
    public void UInt256TryParseShouldFailForMalformedInput()
    {
        Assert.False(UInt256.TryParse("xyz", out UInt256 result));
        Assert.Equal(UInt256.Zero, result);
    }

    [Fact(DisplayName = "UInt256.TryParse should fail for value larger than MaxValue")]
    public void UInt256TryParseShouldFailForOverflowValue()
    {
        string overflow = "115792089237316195423570985008687907853269984665640564039457584007913129639936";
        Assert.False(UInt256.TryParse(overflow, out UInt256 result));
        Assert.Equal(UInt256.Zero, result);
    }

    [Fact(DisplayName = "UInt256.TryParse should fail for negative input")]
    public void UInt256TryParseShouldFailForNegativeInput()
    {
        Assert.False(UInt256.TryParse("-5", NumberStyles.Integer, CultureInfo.InvariantCulture, out UInt256 result));
        Assert.Equal(UInt256.Zero, result);
    }

    [Fact(DisplayName = "UInt256.TryParse with hex style should succeed")]
    public void UInt256TryParseWithHexStyleShouldSucceed()
    {
        // BigInteger.TryParse treats a leading top bit as a sign indicator; '0FF' ensures positive.
        Assert.True(UInt256.TryParse("0FF", NumberStyles.HexNumber, CultureInfo.InvariantCulture, out UInt256 result));
        Assert.Equal((UInt256)0xFF, result);
    }

    [Fact(DisplayName = "UInt256.TryParse from ReadOnlySpan should succeed")]
    public void UInt256TryParseFromSpanShouldSucceed()
    {
        ReadOnlySpan<char> span = "777".AsSpan();
        Assert.True(UInt256.TryParse(span, out UInt256 result));
        Assert.Equal((UInt256)777, result);
    }

    [Fact(DisplayName = "UInt256.TryParse from null string should fail and return zero")]
    public void UInt256TryParseFromNullStringShouldFail()
    {
        Assert.False(UInt256.TryParse((string?)null, out UInt256 result));
        Assert.Equal(UInt256.Zero, result);
    }
}
