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

public sealed class Int256ParseTests
{
    private const string MinValueText = "-57896044618658097711785492504343953926634992332820282019728792003956564819968";
    private const string MaxValueText = "57896044618658097711785492504343953926634992332820282019728792003956564819967";

    [Fact(DisplayName = "Int256.Parse should parse zero")]
    public void Int256ParseShouldParseZero()
    {
        Assert.Equal(Int256.Zero, Int256.Parse("0", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.Parse should parse negative one")]
    public void Int256ParseShouldParseNegativeOne()
    {
        Assert.Equal(Int256.NegativeOne, Int256.Parse("-1", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.Parse should round-trip MinValue")]
    public void Int256ParseShouldRoundTripMinValue()
    {
        Int256 parsed = Int256.Parse(MinValueText, CultureInfo.InvariantCulture);
        Assert.Equal(Int256.MinValue, parsed);
        Assert.Equal(MinValueText, parsed.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.Parse should round-trip MaxValue")]
    public void Int256ParseShouldRoundTripMaxValue()
    {
        Int256 parsed = Int256.Parse(MaxValueText, CultureInfo.InvariantCulture);
        Assert.Equal(Int256.MaxValue, parsed);
        Assert.Equal(MaxValueText, parsed.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.Parse should parse a large positive value")]
    public void Int256ParseShouldParseLargePositive()
    {
        Int256 parsed = Int256.Parse("123456789012345678901234567890", CultureInfo.InvariantCulture);
        Assert.Equal(Int256.Parse("123456789012345678901234567890", CultureInfo.InvariantCulture), parsed);
    }

    [Fact(DisplayName = "Int256.Parse of one below MinValue should throw OverflowException")]
    public void Int256ParseOfBelowMinValueShouldThrow()
    {
        string underflow = "-57896044618658097711785492504343953926634992332820282019728792003956564819969";
        Assert.Throws<OverflowException>(() => Int256.Parse(underflow, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.Parse of one above MaxValue should throw OverflowException")]
    public void Int256ParseOfAboveMaxValueShouldThrow()
    {
        string overflow = "57896044618658097711785492504343953926634992332820282019728792003956564819968";
        Assert.Throws<OverflowException>(() => Int256.Parse(overflow, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.Parse of malformed text should throw FormatException")]
    public void Int256ParseOfMalformedTextShouldThrow()
    {
        Assert.Throws<FormatException>(() => Int256.Parse("not-a-number", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int256.TryParse should succeed for valid negative input")]
    public void Int256TryParseShouldSucceedForValidNegativeInput()
    {
        Assert.True(Int256.TryParse("-9999", out Int256 result));
        Assert.Equal((Int256)(-9999), result);
    }

    [Fact(DisplayName = "Int256.TryParse should fail for malformed input")]
    public void Int256TryParseShouldFailForMalformedInput()
    {
        Assert.False(Int256.TryParse("xyz", out Int256 result));
        Assert.Equal(Int256.Zero, result);
    }

    [Fact(DisplayName = "Int256.TryParse should fail for value out of range")]
    public void Int256TryParseShouldFailForOutOfRangeValue()
    {
        string overflow = "57896044618658097711785492504343953926634992332820282019728792003956564819968";
        Assert.False(Int256.TryParse(overflow, out Int256 result));
        Assert.Equal(Int256.Zero, result);
    }

    [Fact(DisplayName = "Int256.TryParse from span should succeed")]
    public void Int256TryParseFromSpanShouldSucceed()
    {
        Assert.True(Int256.TryParse("-12345".AsSpan(), out Int256 result));
        Assert.Equal((Int256)(-12345), result);
    }

    [Fact(DisplayName = "Int256.TryParse from null string should fail and return zero")]
    public void Int256TryParseFromNullStringShouldFail()
    {
        Assert.False(Int256.TryParse((string?)null, out Int256 result));
        Assert.Equal(Int256.Zero, result);
    }

    [Fact(DisplayName = "Int256.Parse should accept invariant culture format")]
    public void Int256ParseShouldAcceptInvariantCultureFormat()
    {
        Assert.Equal((Int256)42, Int256.Parse("42", CultureInfo.InvariantCulture));
        Assert.Equal((Int256)(-42), Int256.Parse("-42", CultureInfo.InvariantCulture));
    }
}
