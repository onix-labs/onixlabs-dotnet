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
using OnixLabs.Numerics.UnitTests.Data;

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalArithmeticRoundTests
{
    [BigDecimalArithmeticRoundData]
    [Theory(DisplayName = "BigDecimal.Round should produce the correct result")]
    public void BigDecimalRoundShouldProduceExpectedResult(decimal value, byte scale, MidpointRounding mode, Guid _)
    {
        // Given
        decimal expected = decimal.Round(value, scale, mode);

        // When
        BigDecimal actual = BigDecimal.Round(value, scale, mode);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.Round should round correctly just past a midpoint beyond ten fractional digits")]
    [InlineData("2.50000000001", 0, MidpointRounding.ToEven)]
    [InlineData("2.50000000001", 0, MidpointRounding.AwayFromZero)]
    [InlineData("-2.50000000001", 0, MidpointRounding.ToEven)]
    [InlineData("0.250000000001", 1, MidpointRounding.ToEven)]
    [InlineData("2.49999999999", 0, MidpointRounding.ToEven)]
    public void BigDecimalRoundShouldRoundCorrectlyJustPastAMidpoint(string value, int scale, MidpointRounding mode)
    {
        // Given
        decimal input = decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
        decimal expected = decimal.Round(input, scale, mode);

        // When
        BigDecimal actual = BigDecimal.Round(input, scale, mode);

        // Then
        Assert.Equal(expected, actual);
    }
}
