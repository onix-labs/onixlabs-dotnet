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

public sealed class BigDecimalFormatTests
{
    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.Format should produce the expected result (Currency)")]
    public void BigDecimalFormatShouldProduceExpectedResultCurrency(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = $"{value:C}";
        BigDecimal candidate = value;

        // When
        string actual = $"{candidate:C}";

        // Then
        Assert.Equal(expected, actual);
    }

    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.Format should produce the expected result (Fixed)")]
    public void BigDecimalFormatShouldProduceExpectedResultFixed(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = $"{value:F}";
        BigDecimal candidate = value;

        // When
        string actual = $"{candidate:F}";

        // Then
        Assert.Equal(expected, actual);
    }

    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.Format should produce the expected result (General)")]
    public void BigDecimalFormatShouldProduceExpectedResultGeneral(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = $"{value:G}";
        BigDecimal candidate = value;

        // When
        string actual = $"{candidate:G}";

        // Then
        Assert.Equal(expected, actual);
    }

    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.Format should produce the expected result (Number)")]
    public void BigDecimalFormatShouldProduceExpectedResultNumber(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = $"{value:N}";
        BigDecimal candidate = value;

        // When
        string actual = $"{candidate:N}";

        // Then
        Assert.Equal(expected, actual);
    }

    [NumberFormatData]
    [Theory(DisplayName = "BigDecimal.Format should produce the expected result (Percent)")]
    public void BigDecimalFormatShouldProduceExpectedResultPercent(decimal value, CultureInfo culture, Guid _)
    {
        // Given
        string expected = $"{value:P}";
        BigDecimal candidate = value;

        // When
        string actual = $"{candidate:P}";

        // Then
        Assert.Equal(expected, actual);
    }
}
