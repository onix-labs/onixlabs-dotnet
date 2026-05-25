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

public sealed class Int512ParseTests
{
    private const string MinValueString =
        "-6703903964971298549787012499102923063739682910296196688861780721860882015036773488400937149083451713845015929093243025426876941405973284973216824503042048";

    private const string MaxValueString =
        "6703903964971298549787012499102923063739682910296196688861780721860882015036773488400937149083451713845015929093243025426876941405973284973216824503042047";

    [Fact(DisplayName = "Int512.Parse should accept Zero")]
    public void Int512ParseShouldAcceptZero()
    {
        Assert.Equal(Int512.Zero, Int512.Parse("0"));
    }

    [Fact(DisplayName = "Int512.Parse should accept One")]
    public void Int512ParseShouldAcceptOne()
    {
        Assert.Equal(Int512.One, Int512.Parse("1"));
    }

    [Fact(DisplayName = "Int512.Parse should accept negative one")]
    public void Int512ParseShouldAcceptNegativeOne()
    {
        Assert.Equal(Int512.NegativeOne, Int512.Parse("-1"));
    }

    [Fact(DisplayName = "Int512.Parse should round-trip MinValue (-2^511)")]
    public void Int512ParseShouldRoundTripMinValue()
    {
        Int512 value = Int512.Parse(MinValueString);
        Assert.Equal(Int512.MinValue, value);
        Assert.Equal(MinValueString, value.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int512.Parse should round-trip MaxValue (2^511 - 1)")]
    public void Int512ParseShouldRoundTripMaxValue()
    {
        Int512 value = Int512.Parse(MaxValueString);
        Assert.Equal(Int512.MaxValue, value);
        Assert.Equal(MaxValueString, value.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int512.Parse of MaxValue + 1 should throw OverflowException")]
    public void Int512ParseOfMaxValuePlusOneShouldThrow()
    {
        string overflow = "6703903964971298549787012499102923063739682910296196688861780721860882015036773488400937149083451713845015929093243025426876941405973284973216824503042048";
        Assert.Throws<OverflowException>(() => Int512.Parse(overflow));
    }

    [Fact(DisplayName = "Int512.Parse of MinValue - 1 should throw OverflowException")]
    public void Int512ParseOfMinValueMinusOneShouldThrow()
    {
        string underflow = "-6703903964971298549787012499102923063739682910296196688861780721860882015036773488400937149083451713845015929093243025426876941405973284973216824503042049";
        Assert.Throws<OverflowException>(() => Int512.Parse(underflow));
    }

    [Fact(DisplayName = "Int512.Parse of empty string should throw FormatException")]
    public void Int512ParseOfEmptyShouldThrow()
    {
        Assert.Throws<FormatException>(() => Int512.Parse(""));
    }

    [Fact(DisplayName = "Int512.Parse of invalid characters should throw FormatException")]
    public void Int512ParseOfInvalidShouldThrow()
    {
        Assert.Throws<FormatException>(() => Int512.Parse("abcd"));
    }

    [Fact(DisplayName = "Int512.TryParse should succeed for valid signed strings")]
    public void Int512TryParseShouldSucceedForValid()
    {
        Assert.True(Int512.TryParse("-12345", out Int512 result));
        Assert.Equal((Int512)(-12345), result);
    }

    [Fact(DisplayName = "Int512.TryParse should fail for invalid strings")]
    public void Int512TryParseShouldFailForInvalid()
    {
        Assert.False(Int512.TryParse("garbage", out Int512 result));
        Assert.Equal(Int512.Zero, result);
    }

    [Fact(DisplayName = "Int512.TryParse should fail for over-large input")]
    public void Int512TryParseShouldFailForOverLarge()
    {
        Assert.False(Int512.TryParse("99999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999", out Int512 result));
        Assert.Equal(Int512.Zero, result);
    }

    [Fact(DisplayName = "Int512.TryParse with null should fail")]
    public void Int512TryParseWithNullShouldFail()
    {
        Assert.False(Int512.TryParse((string?)null, out Int512 result));
        Assert.Equal(Int512.Zero, result);
    }

    [Fact(DisplayName = "Int512.Parse with ReadOnlySpan<char> should accept negative value")]
    public void Int512ParseWithSpanShouldAcceptNegative()
    {
        ReadOnlySpan<char> input = "-99999999999".AsSpan();
        Int512 value = Int512.Parse(input);
        Assert.Equal((Int512)(-99999999999L), value);
    }

    [Fact(DisplayName = "Int512.Parse should respect AllowLeadingSign style for explicit positive")]
    public void Int512ParseShouldRespectAllowLeadingSign()
    {
        Int512 value = Int512.Parse("+42", NumberStyles.Integer | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
        Assert.Equal((Int512)42, value);
    }
}
