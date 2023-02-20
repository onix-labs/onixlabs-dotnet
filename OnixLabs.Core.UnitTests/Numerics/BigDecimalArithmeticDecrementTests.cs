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

public sealed class BigDecimalArithmeticDecrementTests
{
    [Theory(DisplayName = "BigDecimal.Decrement should return the expected result")]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    [InlineData(0, -1)]
    [InlineData(1.0001, 0.0001)]
    [InlineData(1.0000, 0.0000)]
    [InlineData(123.456, 122.456)]
    public void BigDecimalDecrementShouldReturnTheExpectedResult(decimal value, decimal expected)
    {
        // Arrange
        BigDecimal actual = value.ToBigDecimal();

        // Act
        actual = BigDecimal.Decrement(actual);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory(DisplayName = "BigDecimal.DecrementFraction should return the expected result")]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    [InlineData(0, -1)]
    [InlineData(1.0002, 1.0001)]
    [InlineData(1.9999, 1.9998)]
    [InlineData(123.456, 123.455)]
    public void BigDecimalDecrementFractionShouldReturnTheExpectedResult(decimal value, decimal expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal();

        // Act
        BigDecimal actual = BigDecimal.DecrementFraction(candidate);

        // Assert
        Assert.Equal(expected, actual);
    }
}
