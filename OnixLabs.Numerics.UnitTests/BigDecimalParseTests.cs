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

public sealed class BigDecimalParseTests
{
    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.Parse should produce the expected result (Currency)")]
    public void BigDecimalParseShouldProduceExpectedResultCurrency(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string formatted = value.ToString("C", culture);
        decimal expected = decimal.Parse(formatted, NumberStyles.Currency, culture);

        // When
        BigDecimal actual = BigDecimal.Parse(formatted, NumberStyles.Currency, culture);

        // Then
        Assert.Equal(expected, actual, BigDecimalEqualityComparer.Semantic);
    }

    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.Parse should produce the expected result (Decimal)")]
    public void BigDecimalParseShouldProduceExpectedResultDecimal(decimal expected, CultureInfo culture, Guid _)
    {
        // Given
        string formatted = expected.ToBigDecimal().ToString("D", culture);

        // When
        BigDecimal actual = BigDecimal.Parse(formatted, NumberStyles.Number, culture);

        // Then
        Assert.Equal(expected, actual, BigDecimalEqualityComparer.Semantic);
    }

    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.Parse should produce the expected result (Fixed)")]
    public void BigDecimalParseShouldProduceExpectedResultFixed(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string formatted = value.ToString("F", culture);
        decimal expected = decimal.Parse(formatted, NumberStyles.Float, culture);

        // When
        BigDecimal actual = BigDecimal.Parse(formatted, NumberStyles.Float, culture);

        // Then
        Assert.Equal(expected, actual, BigDecimalEqualityComparer.Semantic);
    }

    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.Parse should produce the expected result (General)")]
    public void BigDecimalParseShouldProduceExpectedResultGeneral(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string formatted = value.ToString("G", culture);
        decimal expected = decimal.Parse(formatted, NumberStyles.Any, culture);

        // When
        BigDecimal actual = BigDecimal.Parse(formatted, NumberStyles.Any, culture);

        // Then
        Assert.Equal(expected, actual, BigDecimalEqualityComparer.Semantic);
    }

    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.Parse should produce the expected result (Number)")]
    public void BigDecimalParseShouldProduceExpectedResultNumber(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string formatted = value.ToString("G", culture);
        decimal expected = decimal.Parse(formatted, NumberStyles.Number, culture);

        // When
        BigDecimal actual = BigDecimal.Parse(formatted, NumberStyles.Number, culture);

        // Then
        Assert.Equal(expected, actual, BigDecimalEqualityComparer.Semantic);
    }
}
