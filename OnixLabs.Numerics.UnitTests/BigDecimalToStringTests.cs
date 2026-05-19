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
using OnixLabs.Numerics.UnitTests.Data;

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalToStringTests
{
    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.ToString should produce the expected result (Currency)")]
    public void BigDecimalToStringShouldProduceExpectedResultCurrency(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = value.ToString("C", culture);
        BigDecimal candidate = value;

        // When
        string actual = candidate.ToString("C", culture);

        // Then
        Assert.Equal(expected, actual);
    }

    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.ToString should produce the expected result (Fixed)")]
    public void BigDecimalToStringShouldProduceExpectedResultFixed(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = value.ToString("F", culture);
        BigDecimal candidate = value;

        // When
        string actual = candidate.ToString("F", culture);

        // Then
        Assert.Equal(expected, actual);
    }

    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.ToString should produce the expected result (General)")]
    public void BigDecimalToStringShouldProduceExpectedResultGeneral(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = value.ToString("G", culture);
        BigDecimal candidate = value;

        // When
        string actual = candidate.ToString("G", culture);

        // Then
        Assert.Equal(expected, actual);
    }

    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.ToString should produce the expected result (Number)")]
    public void BigDecimalToStringShouldProduceExpectedResultNumber(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = value.ToString("N", culture);
        BigDecimal candidate = value;

        // When
        string actual = candidate.ToString("N", culture);

        // Then
        Assert.Equal(expected, actual);
    }

    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.ToString should produce the expected result (Percent)")]
    public void BigDecimalToStringShouldProduceExpectedResultPercent(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = value.ToString("P", culture);
        BigDecimal candidate = value;

        // When
        string actual = candidate.ToString("P", culture);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "BigDecimal.ToString with R format should produce the round-trip representation")]
    public void BigDecimalToStringWithRFormatShouldProduceRoundTrip()
    {
        BigDecimal value = BigDecimal.Parse("3.14159265358979323846", CultureInfo.InvariantCulture);
        Assert.Equal("3.14159265358979323846", value.ToString("R", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "BigDecimal.ToString with R format for integer-valued BigDecimal should produce decimal digits")]
    public void BigDecimalToStringWithRFormatForIntegerShouldProduceDecimalDigits()
    {
        BigDecimal value = BigDecimal.Parse("1234567", CultureInfo.InvariantCulture);
        Assert.Equal("1234567", value.ToString("R", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "BigDecimal.ToString with X format for integer-valued BigDecimal should produce hex digits")]
    public void BigDecimalToStringWithXFormatForIntegerShouldProduceHex()
    {
        BigDecimal value = BigDecimal.Parse("255", CultureInfo.InvariantCulture);
        Assert.Equal("FF", value.ToString("X", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "BigDecimal.ToString with X4 format for integer-valued BigDecimal should pad to four hex digits")]
    public void BigDecimalToStringWithX4FormatForIntegerShouldPad()
    {
        BigDecimal value = BigDecimal.Parse("255", CultureInfo.InvariantCulture);
        Assert.Equal("00FF", value.ToString("X4", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "BigDecimal.ToString with X format for fractional BigDecimal should echo the format unchanged")]
    public void BigDecimalToStringWithXFormatForFractionalShouldEchoFormat()
    {
        BigDecimal value = BigDecimal.Parse("3.14", CultureInfo.InvariantCulture);
        Assert.Equal("X", value.ToString("X", CultureInfo.InvariantCulture));
    }
}
