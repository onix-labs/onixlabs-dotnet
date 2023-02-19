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
        BigDecimal leftCandidate = new(left);
        BigDecimal rightCandidate = new(right);

        // Act
        (BigDecimal leftBalanced, BigDecimal rightBalanced) = BigDecimal.Balance(leftCandidate, rightCandidate);

        // Assert
        Assert.Equal(leftBalanced.Scale, rightBalanced.Scale);
    }

    [Theory(DisplayName = "BigDecimal.MinMax should return the expected result")]
    [InlineData(sbyte.MinValue, sbyte.MaxValue)]
    [InlineData(byte.MinValue, byte.MaxValue)]
    [InlineData(short.MinValue, short.MaxValue)]
    [InlineData(ushort.MinValue, ushort.MaxValue)]
    [InlineData(int.MinValue, int.MaxValue)]
    [InlineData(uint.MinValue, uint.MaxValue)]
    [InlineData(long.MinValue, long.MaxValue)]
    [InlineData(ulong.MinValue, ulong.MaxValue)]
    public void BigDecimalMinMaxShouldReturnTheExpectedResult(decimal min, decimal max)
    {
        // Arrange / Act
        (BigDecimal minCandidate, BigDecimal maxCandidate) = BigDecimal.MinMax(min.ToBigDecimal(), max.ToBigDecimal());

        // Assert
        Assert.True(minCandidate <= maxCandidate);
    }

    [Theory(DisplayName = "BigDecimal.Min should return the expected result")]
    [InlineData(sbyte.MinValue, sbyte.MaxValue)]
    [InlineData(byte.MinValue, byte.MaxValue)]
    [InlineData(short.MinValue, short.MaxValue)]
    [InlineData(ushort.MinValue, ushort.MaxValue)]
    [InlineData(int.MinValue, int.MaxValue)]
    [InlineData(uint.MinValue, uint.MaxValue)]
    [InlineData(long.MinValue, long.MaxValue)]
    [InlineData(ulong.MinValue, ulong.MaxValue)]
    public void BigDecimalMinShouldReturnTheExpectedResult(decimal min, decimal max)
    {
        // Arrange / Act
        BigDecimal candidate = BigDecimal.Min(min.ToBigDecimal(), max.ToBigDecimal());

        // Assert
        Assert.Equal(candidate, min);
    }

    [Theory(DisplayName = "BigDecimal.Max should return the expected result")]
    [InlineData(sbyte.MinValue, sbyte.MaxValue)]
    [InlineData(byte.MinValue, byte.MaxValue)]
    [InlineData(short.MinValue, short.MaxValue)]
    [InlineData(ushort.MinValue, ushort.MaxValue)]
    [InlineData(int.MinValue, int.MaxValue)]
    [InlineData(uint.MinValue, uint.MaxValue)]
    [InlineData(long.MinValue, long.MaxValue)]
    [InlineData(ulong.MinValue, ulong.MaxValue)]
    public void BigDecimalMaxShouldReturnTheExpectedResult(decimal min, decimal max)
    {
        // Arrange / Act
        BigDecimal candidate = BigDecimal.Max(min.ToBigDecimal(), max.ToBigDecimal());

        // Assert
        Assert.Equal(candidate, max);
    }

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
        BigDecimal actual = BigDecimal.Negate(candidate);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.Truncate should return the expected result")]
    [InlineData(1.0, 1)]
    [InlineData(1.01, 1)]
    [InlineData(123.456, 123)]
    [InlineData(1000.01, 1000)]
    public void BigDecimalTruncateShouldReturnTheExpectedResult(decimal value, decimal expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal();

        // Act
        BigDecimal actual = BigDecimal.Truncate(candidate);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.SetScale should return the expected result")]
    [InlineData(1, 0, 1)]
    [InlineData(1, 1, 0.1)]
    [InlineData(1, 2, 0.01)]
    [InlineData(1, 3, 0.001)]
    [InlineData(1, 4, 0.0001)]
    [InlineData(1, 5, 0.00001)]
    [InlineData(1, 6, 0.000001)]
    [InlineData(1, 7, 0.0000001)]
    [InlineData(1, 8, 0.00000001)]
    [InlineData(1, 9, 0.000000001)]
    [InlineData(0.1, 0, 1)]
    [InlineData(0.01, 0, 1)]
    [InlineData(0.001, 0, 1)]
    [InlineData(0.0001, 0, 1)]
    [InlineData(0.00001, 0, 1)]
    [InlineData(0.000001, 0, 1)]
    [InlineData(0.0000001, 0, 1)]
    [InlineData(0.00000001, 0, 1)]
    [InlineData(0.000000001, 0, 1)]
    public void BigDecimalSetScaleShouldReturnTheExpectedResult(decimal value, int scale, decimal expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal();

        // Act
        BigDecimal actual = candidate.SetScale(scale);

        // Assert
        Assert.Equal(expected, actual);
    }
}
