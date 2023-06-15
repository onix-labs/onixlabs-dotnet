// Copyright 2020-2023 ONIXLabs
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
using System.Numerics;
using OnixLabs.Core.Numerics;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class NumericExtensionTests
{
    [Theory(DisplayName = "Decimal.GetUnscaledValue should produce the expected result from zero")]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(-1, -1)]
    [InlineData(0.1, 1)]
    [InlineData(-0.1, -1)]
    [InlineData(1.01, 101)]
    [InlineData(-1.01, -101)]
    [InlineData(100.000123, 100000123)]
    [InlineData(-100.000123, -100000123)]
    [InlineData(987654321.123456, 987654321123456)]
    [InlineData(-987654321.123456, -987654321123456)]
    public void DecimalGetUnscaledValueShouldProduceExpectedResultFromZero(decimal value, long expected)
    {
        // When
        BigInteger unscaledValue = value.GetUnscaledValue();

        // Then
        Assert.Equal(expected, unscaledValue);
    }

    [Fact(DisplayName = "Decimal.GetUnscaledValue should produce the expected result from decimal.MaxValue")]
    public void DecimalGetUnscaledValueShouldProduceExpectedResultFromMaxValue()
    {
        // Given
        const decimal value = decimal.MaxValue;

        // When
        BigInteger unscaledValue = value.GetUnscaledValue();

        // Then
        Assert.Equal(decimal.MaxValue.ToString(CultureInfo.InvariantCulture), unscaledValue.ToString());
    }

    [Fact(DisplayName = "Decimal.GetUnscaledValue should produce the expected result from decimal.MinValue")]
    public void DecimalGetUnscaledValueShouldProduceExpectedResultFromMinValue()
    {
        // Given
        const decimal value = decimal.MinValue;

        // When
        BigInteger unscaledValue = value.GetUnscaledValue();

        // Then
        Assert.Equal(decimal.MinValue.ToString(CultureInfo.InvariantCulture), unscaledValue.ToString());
    }
}
