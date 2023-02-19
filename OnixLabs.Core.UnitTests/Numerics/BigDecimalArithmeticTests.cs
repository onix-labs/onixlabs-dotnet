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

public sealed class BigDecimalArithmeticTests
{
    [Theory(DisplayName = "BigDecimal.Abs should return the expected result")]
    [InlineData(-0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-3)]
    [InlineData(-4)]
    [InlineData(-5)]
    [InlineData(-6)]
    [InlineData(-7)]
    [InlineData(-8)]
    [InlineData(-9)]
    [InlineData(-10)]
    [InlineData(-20)]
    [InlineData(-50)]
    [InlineData(-100)]
    [InlineData(-200)]
    [InlineData(-500)]
    [InlineData(-1000)]
    [InlineData(-123456789)]
    [InlineData(-12345678.9)]
    [InlineData(-1234567.89)]
    [InlineData(-123456.789)]
    [InlineData(-12345.6789)]
    [InlineData(-1234.56789)]
    [InlineData(-123.456789)]
    [InlineData(-12.3456789)]
    [InlineData(-1.23456789)]
    public void BigDecimalAbsShouldReturnTheExpectedResult(decimal value)
    {
        // Arrange
        BigDecimal expected = new(decimal.Abs(value));
        BigDecimal candidate = new(value);

        // Act
        BigDecimal actual = BigDecimal.Abs(candidate);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.Balance should return the expected result")]
    [InlineData(123456789, 1.23456789)]
    [InlineData(12345678.9, 12.3456789)]
    [InlineData(1234567.89, 123.456789)]
    [InlineData(123456.789, 1234.56789)]
    [InlineData(12345.6789, 12345.6789)]
    [InlineData(1234.56789, 123456.789)]
    [InlineData(123.456789, 1234567.89)]
    [InlineData(12.3456789, 12345678.9)]
    [InlineData(1.23456789, 123456789)]
    public void BigDecimalBalanceShouldReturnTheExpectedResult(decimal left, decimal right)
    {
        // Arrange
        BigDecimal leftValue = new(left);
        BigDecimal rightValue = new(right);

        // Act
        (BigDecimal leftBalanced, BigDecimal rightBalanced) = BigDecimal.Balance(leftValue, rightValue);

        // Assert
        Assert.Equal(leftBalanced.Scale, rightBalanced.Scale);
    }
}
