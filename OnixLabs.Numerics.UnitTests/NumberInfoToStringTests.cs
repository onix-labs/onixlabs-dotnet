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

public sealed class NumberInfoToStringTests
{
    [NumberFormatData]
    [Theory(DisplayName = "NumberInfo.ToString should produce the expected result (General)")]
    public void NumberInfoToStringShouldProduceExpectedResultGeneral(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = value.ToString("G", culture);
        NumberInfo candidate = value.ToNumberInfo();

        // When
        string actual = candidate.ToString("G", culture);

        // Then
        Assert.Equal(expected, actual);
    }

    [NumberFormatData]
    [Theory(DisplayName = "NumberInfo.ToString should produce the expected result (Currency)")]
    public void NumberInfoToStringShouldProduceExpectedResultCurrency(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = value.ToString("C", culture);
        NumberInfo candidate = value.ToNumberInfo();

        // When
        string actual = candidate.ToString("C", culture);

        // Then
        Assert.Equal(expected, actual);
    }

    [NumberFormatData]
    [Theory(DisplayName = "NumberInfo.ToString should produce the expected result (Fixed)")]
    public void NumberInfoToStringShouldProduceExpectedResultFixed(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = value.ToString("F", culture);
        NumberInfo candidate = value.ToNumberInfo();

        // When
        string actual = candidate.ToString("F", culture);

        // Then
        Assert.Equal(expected, actual);
    }

    [NumberFormatData]
    [Theory(DisplayName = "NumberInfo.ToString should produce the expected result (Number)")]
    public void NumberInfoToStringShouldProduceExpectedResultNumber(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = value.ToString("N", culture);
        NumberInfo candidate = value.ToNumberInfo();

        // When
        string actual = candidate.ToString("N", culture);

        // Then
        Assert.Equal(expected, actual);
    }

    [NumberFormatData]
    [Theory(DisplayName = "NumberInfo.ToString should produce the expected result (Percent)")]
    public void NumberInfoToStringShouldProduceExpectedResultPercent(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = value.ToString("P", culture);
        NumberInfo candidate = value.ToNumberInfo();

        // When
        string actual = candidate.ToString("P", culture);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "NumberInfo.ToString with precision suffix should match decimal.ToString")]
    [InlineData(50, "N3")]
    [InlineData(50, "N0")]
    [InlineData(50, "N2")]
    [InlineData(1234.56, "N")]
    [InlineData(1234.56, "N0")]
    [InlineData(1234.56, "N2")]
    [InlineData(1234.56, "N4")]
    [InlineData(0.5, "F1")]
    [InlineData(0.5, "F3")]
    [InlineData(0.005, "F2")]
    [InlineData(-12.345, "F2")]
    [InlineData(-12.345, "N2")]
    [InlineData(0, "N3")]
    [InlineData(0, "F0")]
    public void NumberInfoToStringWithPrecisionSuffixShouldMatchDecimalToString(decimal value, string format)
    {
        // Given
        string expected = value.ToString(format, CultureInfo.InvariantCulture);
        NumberInfo candidate = value.ToNumberInfo();

        // When
        string actual = candidate.ToString(format, CultureInfo.InvariantCulture);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "NumberInfo.ToString with precision suffix should honor culture separators")]
    [InlineData(1234.56, "N2", "de-DE", "1.234,56")]
    [InlineData(1234.56, "N2", "en-US", "1,234.56")]
    [InlineData(50, "N3", "en-US", "50.000")]
    [InlineData(50, "N3", "de-DE", "50,000")]
    public void NumberInfoToStringWithPrecisionSuffixShouldHonorCultureSeparators(decimal value, string format, string cultureName, string expected)
    {
        // Given
        CultureInfo culture = CultureInfo.GetCultureInfo(cultureName);
        NumberInfo candidate = value.ToNumberInfo();

        // When
        string actual = candidate.ToString(format, culture);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "NumberInfo.ToString with invalid precision suffix should echo the format string")]
    [InlineData("Nxx")]
    [InlineData("F-1")]
    [InlineData("Cabc")]
    public void NumberInfoToStringWithInvalidPrecisionSuffixShouldEchoFormatString(string format)
    {
        // Given
        NumberInfo candidate = 1.23m.ToNumberInfo();

        // When
        string actual = candidate.ToString(format, CultureInfo.InvariantCulture);

        // Then
        Assert.Equal(format, actual);
    }
}
