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

using System.Globalization;
using OnixLabs.Numerics.UnitTests.Data;
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class NumberInfoParseTests
{
    [NumberInfoFormatterData]
    [Theory(DisplayName = "NumberInfo.Parse should produce the expected result (Currency)")]
    public void NumberInfoParseShouldProduceExpectedResultCurrency(decimal value, CultureInfo culture)
    {
        // Given
        string formatted = value.ToString("C", culture);
        NumberInfo expected = decimal.Parse(formatted, NumberStyles.Currency, culture).ToNumberInfo();

        // When
        NumberInfo actual = NumberInfo.Parse(formatted, NumberStyles.Currency, culture);

        // Then
        Assert.Equal(expected, actual, NumberInfoEqualityComparer.Semantic);
    }

    [NumberInfoFormatterData]
    [Theory(DisplayName = "NumberInfo.Parse should produce the expected result (Decimal, Integer)")]
    public void NumberInfoParseShouldProduceExpectedResultDecimal(decimal value, CultureInfo culture)
    {
        // Given
        string formatted = Int128.CreateTruncating(value).ToString("D", culture);
        NumberInfo expected = Int128.Parse(formatted, NumberStyles.Integer, culture).ToNumberInfo();

        // When
        NumberInfo actual = NumberInfo.Parse(formatted, NumberStyles.Integer, culture);

        // Then
        Assert.Equal(expected, actual, NumberInfoEqualityComparer.Semantic);
    }

    // TODO : Re-add when NumberInfo is implemented.
    // [NumberInfoFormatterData]
    // [Theory(DisplayName = "NumberInfo.Parse should produce the expected result (Decimal)")]
    // public void NumberInfoParseShouldProduceExpectedResultDecimal(decimal expected, CultureInfo culture)
    // {
    //     // Given
    //     string formatted = expected.ToNumberInfo().ToString("D", culture);
    //
    //     // When
    //     NumberInfo actual = NumberInfo.Parse(formatted, NumberStyles.Number, culture);
    //
    //     // Then
    //     Assert.Equal(expected, actual, NumberInfoEqualityComparer.Semantic);
    // }

    [NumberInfoFormatterData]
    [Theory(DisplayName = "NumberInfo.Parse should produce the expected result (Fixed)")]
    public void NumberInfoParseShouldProduceExpectedResultFixed(decimal value, CultureInfo culture)
    {
        // Given
        string formatted = value.ToString("F", culture);
        NumberInfo expected = decimal.Parse(formatted, NumberStyles.Float, culture).ToNumberInfo();

        // When
        NumberInfo actual = NumberInfo.Parse(formatted, NumberStyles.Float, culture);

        // Then
        Assert.Equal(expected, actual, NumberInfoEqualityComparer.Semantic);
    }

    [NumberInfoFormatterData]
    [Theory(DisplayName = "NumberInfo.Parse should produce the expected result (General)")]
    public void NumberInfoParseShouldProduceExpectedResultGeneral(decimal value, CultureInfo culture)
    {
        // Given
        string formatted = value.ToString("G", culture);
        NumberInfo expected = decimal.Parse(formatted, NumberStyles.Any, culture).ToNumberInfo();

        // When
        NumberInfo actual = NumberInfo.Parse(formatted, NumberStyles.Any, culture);

        // Then
        Assert.Equal(expected, actual, NumberInfoEqualityComparer.Semantic);
    }

    [NumberInfoFormatterData]
    [Theory(DisplayName = "NumberInfo.Parse should produce the expected result (Number)")]
    public void NumberInfoParseShouldProduceExpectedResultNumber(decimal value, CultureInfo culture)
    {
        // Given
        string formatted = value.ToString("N", culture);
        NumberInfo expected = decimal.Parse(formatted, NumberStyles.Number, culture).ToNumberInfo();

        // When
        NumberInfo actual = NumberInfo.Parse(formatted, NumberStyles.Number, culture);

        // Then
        Assert.Equal(expected, actual, NumberInfoEqualityComparer.Semantic);
    }
}
