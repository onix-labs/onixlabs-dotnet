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

using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalArithmeticIncrementTests
{
    [Theory(DisplayName = "BigDecimal.Increment should produce the expected result.")]
    [InlineData(-1, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(10, 11)]
    [InlineData(1000, 1001)]
    [InlineData(-1000, -999)]
    public void BigDecimalIncrementShouldProduceExpectedResult(double value, double expected)
    {
        // When
        BigDecimal actual = BigDecimal.Increment(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.IncrementFraction should produce the expected result.")]
    [InlineData(-1.2, -1.1)]
    [InlineData(0.2, 0.3)]
    [InlineData(1.02, 1.03)]
    [InlineData(10.998, 10.999)]
    [InlineData(1000.02, 1000.03)]
    [InlineData(-1000.02, -1000.01)]
    public void BigDecimalIncrementFractionShouldProduceExpectedResult(double value, double expected)
    {
        // When
        BigDecimal actual = BigDecimal.IncrementFraction(value);

        // Then
        Assert.Equal(expected, actual);
    }
}
