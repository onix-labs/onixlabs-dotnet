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

public sealed class BigDecimalArithmeticUnarySubtractionTests
{
    [Theory(DisplayName = "BigDecimal.Negate should return the expected result")]
    [InlineData(1, -1)]
    [InlineData(-1, 1)]
    [InlineData(1.0001, -1.0001)]
    [InlineData(-1.0001, 1.0001)]
    [InlineData(123.456789, -123.456789)]
    [InlineData(-123.456789, 123.456789)]
    public void BigDecimalNegateShouldReturnTheExpectedResult(decimal value, decimal expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal();

        // Act
        BigDecimal actual = -candidate;

        // Assert
        Assert.Equal(expected, actual);
    }
}
