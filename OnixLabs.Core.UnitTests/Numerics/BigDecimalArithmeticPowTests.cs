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

using OnixLabs.Core.Numerics;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class BigDecimalArithmeticPowTests
{
    [Theory(DisplayName = "BigDecimal.Pow should produce the expected result")]
    [InlineData(0, 1, 0)]
    // [InlineData(1, 1, 1)]
    // [InlineData(2, 1, 2)]
    // [InlineData(10, 1, 10)]
    // [InlineData(10, 2, 100)]
    // [InlineData(10, 3, 1000)]
    // [InlineData(10, 4, 10000)]
    // [InlineData(123.456, 2, 15241.383936)]
    // [InlineData(256, 2, 65536)]
    // [InlineData(65536, 2, 4294967296)]
    // [InlineData(1, -1, 1)]
    // [InlineData(2, -1, 0.5)]
    // [InlineData(10, -1, 0.1)]
    // [InlineData(10, -2, 0.01)]
    // [InlineData(10, -3, 0.001)]
    // [InlineData(10, -4, 0.0001)]
    // [InlineData(256, -1, 0.00390625)]
    // [InlineData(65536, -1, 0.0000152587890625)]
    public void BigDecimalPowShouldProduceExpectedResult(double value, int exponent, double expected)
    {
        // When
        BigDecimal actual = BigDecimal.Pow(value, exponent);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.Pow should return the expected result")]
    [InlineData(10, 0, "1")]
    // [InlineData(10, 1, "10")]
    // [InlineData(10, 2, "100")]
    // [InlineData(10, 3, "1000")]
    // [InlineData(10, 4, "10000")]
    // [InlineData(10, 5, "100000")]
    // [InlineData(10, 6, "1000000")]
    // [InlineData(10, 7, "10000000")]
    // [InlineData(10, 8, "100000000")]
    // [InlineData(10, 9, "1000000000")]
    // [InlineData(10, 10, "10000000000")]
    // [InlineData(10, 20, "100000000000000000000")]
    // [InlineData(10, 30, "1000000000000000000000000000000")]
    // [InlineData(10, 40, "10000000000000000000000000000000000000000")]
    // [InlineData(10, 50, "100000000000000000000000000000000000000000000000000")]
    // [InlineData(10, 60, "1000000000000000000000000000000000000000000000000000000000000")]
    // [InlineData(10, 70, "10000000000000000000000000000000000000000000000000000000000000000000000")]
    // [InlineData(10, 80, "100000000000000000000000000000000000000000000000000000000000000000000000000000000")]
    // [InlineData(10, 90, "1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000")]
    // [InlineData(10, 100, "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000")]
    // [InlineData(10, -1, "0.1")]
    // [InlineData(10, -2, "0.01")]
    // [InlineData(10, -3, "0.001")]
    // [InlineData(10, -4, "0.0001")]
    // [InlineData(10, -5, "0.00001")]
    // [InlineData(10, -6, "0.000001")]
    // [InlineData(10, -7, "0.0000001")]
    // [InlineData(10, -8, "0.00000001")]
    // [InlineData(10, -9, "0.000000001")]
    // [InlineData(10, -10, "0.0000000001")]
    // [InlineData(10, -20, "0.00000000000000000001")]
    // [InlineData(10, -30, "0.000000000000000000000000000001")]
    // [InlineData(10, -40, "0.0000000000000000000000000000000000000001")]
    // [InlineData(10, -50, "0.00000000000000000000000000000000000000000000000001")]
    // [InlineData(10, -60, "0.000000000000000000000000000000000000000000000000000000000001")]
    // [InlineData(10, -70, "0.0000000000000000000000000000000000000000000000000000000000000000000001")]
    // [InlineData(10, -80, "0.00000000000000000000000000000000000000000000000000000000000000000000000000000001")]
    // [InlineData(10, -90, "0.000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001")]
    // [InlineData(10, -100, "0.0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001")]
    public void BigDecimalPowShouldReturnTheExpectedResult(int value, int exponent, string expected)
    {
        // Arrange / Act
        BigDecimal actual = BigDecimal.Pow(value, exponent);

        // Assert
        Assert.Equal(actual.ToString(), expected);
    }
}
