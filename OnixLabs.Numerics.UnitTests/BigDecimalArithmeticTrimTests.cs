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

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalArithmeticTrimTests
{
    [BigDecimalArithmeticTrimData]
    [Theory(DisplayName = "BigDecimal.TrimTrailingZeros should produce the expected result")]
    public void BigDecimalTrimTrailingZerosShouldProduceExpectedResult(decimal value, decimal expected)
    {
        // When
        BigDecimal actual = value.ToBigDecimal().TrimTrailingZeros();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.TrimTrailingZeros should not trim zeros belonging to the integral part")]
    [InlineData("10", "10")]
    [InlineData("100", "100")]
    [InlineData("10.0", "10")]
    [InlineData("120.00", "120")]
    [InlineData("0", "0")]
    public void BigDecimalTrimTrailingZerosShouldNotTrimIntegralZeros(string value, string expected)
    {
        // Given
        BigDecimal input = BigDecimal.Parse(value, CultureInfo.InvariantCulture);
        BigDecimal expectedResult = BigDecimal.Parse(expected, CultureInfo.InvariantCulture);

        // When
        BigDecimal actual = input.TrimTrailingZeros();

        // Then
        Assert.Equal(expectedResult, actual, BigDecimalEqualityComparer.Strict);
    }
}
