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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using OnixLabs.Numerics.UnitTests.Data;

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

    [Fact(DisplayName = "BigDecimal.Parse should match BigInteger.Parse of the digits for every length and decimal point position around the 18-digit chunks")]
    public void BigDecimalParseShouldMatchBigIntegerParseOfTheDigitsAroundTheChunkBoundaries()
    {
        // Given
        Random random = new(Seed: 18);
        List<string> failures = [];
        List<int> lengths = [..Enumerable.Range(1, 60), 1001];

        // When
        foreach (int length in lengths)
        {
            string digits = new(Enumerable.Range(0, length).Select(_ => (char)('0' + random.Next(10))).ToArray());

            foreach (int fractionDigits in new[] { 0, 1, length / 2, length - 1, length }.Distinct())
            {
                string text = fractionDigits is 0 ? digits : $"{digits[..^fractionDigits]}.{digits[^fractionDigits..]}";

                if (text.StartsWith('.')) text = "0" + text;

                BigDecimal expected = new(BigInteger.Parse(digits, CultureInfo.InvariantCulture), fractionDigits);
                BigDecimal actual = BigDecimal.Parse(text, CultureInfo.InvariantCulture);

                if (actual.UnscaledValue != expected.UnscaledValue || actual.Scale != expected.Scale)
                    failures.Add($"'{text}': expected {expected.UnscaledValue}e-{expected.Scale} but was {actual.UnscaledValue}e-{actual.Scale}");
            }
        }

        // Then
        Assert.Empty(failures);
    }

    [Fact(DisplayName = "BigDecimal.Parse should ignore thousands separators that fall inside an 18-digit chunk")]
    public void BigDecimalParseShouldIgnoreThousandsSeparatorsThatFallInsideAChunk()
    {
        // Given
        const string text = "123,456,789,012,345,678,901,234,567,890.123456789012345678901";
        BigInteger expectedUnscaled = BigInteger.Parse("123456789012345678901234567890123456789012345678901", CultureInfo.InvariantCulture);

        // When
        BigDecimal actual = BigDecimal.Parse(text, CultureInfo.InvariantCulture);

        // Then
        Assert.Equal(expectedUnscaled, actual.UnscaledValue);
        Assert.Equal(21, actual.Scale);
    }
}
