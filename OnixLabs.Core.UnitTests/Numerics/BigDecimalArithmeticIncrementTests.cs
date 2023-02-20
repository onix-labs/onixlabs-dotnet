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

using OnixLabs.Core.Numerics;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class BigDecimalArithmeticIncrementTests
{
    [Theory(DisplayName = "BigDecimal.Increment should return the expected result")]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(-1, 0)]
    [InlineData(1.0001, 2.0001)]
    [InlineData(123.456, 124.456)]
    public void BigDecimalIncrementShouldReturnTheExpectedResult(decimal value, decimal expected)
    {
        // Arrange
        BigDecimal actual = value.ToBigDecimal();

        // Act
        actual = BigDecimal.Increment(actual);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory(DisplayName = "BigDecimal.IncrementFraction should return the expected result")]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(-1, 0)]
    [InlineData(1.0001, 1.0002)]
    [InlineData(123.456, 123.457)]
    public void BigDecimalIncrementFractionShouldReturnTheExpectedResult(decimal value, decimal expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal();

        // Act
        BigDecimal actual = BigDecimal.IncrementFraction(candidate);

        // Assert
        Assert.Equal(expected, actual);
    }
}
