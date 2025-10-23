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

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalComparableMinMaxTests
{
    [Theory(DisplayName = "BigDecimal.Min should produce the expected result")]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 0)]
    [InlineData(1, 1, 1)]
    [InlineData(1.1, 0.1, 0.1)]
    [InlineData(1.1, 1.2, 1.1)]
    [InlineData(0.01, 0.02, 0.01)]
    [InlineData(-0.01, -0.02, -0.02)]
    public void BigDecimalMinShouldProduceExpectedResult(double left, double right, double expected)
    {
        // When
        BigDecimal actual = BigDecimal.Min(left, right);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.Max should produce the expected result")]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(1, 1, 1)]
    [InlineData(1.1, 0.1, 1.1)]
    [InlineData(1.1, 1.2, 1.2)]
    [InlineData(0.01, 0.02, 0.02)]
    [InlineData(-0.01, -0.02, -0.01)]
    public void BigDecimalMaxShouldProduceExpectedResult(double left, double right, double expected)
    {
        // When
        BigDecimal actual = BigDecimal.Max(left, right);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.MinMax should produce the expected result")]
    [InlineData(0, 0, 0, 0)]
    [InlineData(1, 0, 0, 1)]
    [InlineData(1, 1, 1, 1)]
    [InlineData(1.1, 0.1, 0.1, 1.1)]
    [InlineData(1.1, 1.2, 1.1, 1.2)]
    [InlineData(0.01, 0.02, 0.01, 0.02)]
    [InlineData(-0.01, -0.02, -0.02, -0.01)]
    public void BigDecimalMinMaxShouldProduceExpectedResult(double left, double right, double expectedMin, double expectedMax)
    {
        // When
        (BigDecimal actualMin, BigDecimal actualMax) = BigDecimal.MinMax(left, right);

        // Then
        Assert.Equal(expectedMin, actualMin);
        Assert.Equal(expectedMax, actualMax);
    }

    [Theory(DisplayName = "BigDecimal.MinMagnitude should produce expected result")]
    [InlineData(0, 0, 0)]
    [InlineData(0, 1, 0)]
    [InlineData(0, -1, 0)]
    [InlineData(-1, 1, -1)]
    [InlineData(1, 2, 1)]
    [InlineData(1, -2, 1)]
    [InlineData(-1, 2, -1)]
    [InlineData(-1, -2, -1)]
    [InlineData(123.456, 456.789, 123.456)]
    [InlineData(-123.456, 456.789, -123.456)]
    [InlineData(123.456, -456.789, 123.456)]
    [InlineData(-123.456, -456.789, -123.456)]
    [InlineData(0.1, 0.01, 0.01)]
    [InlineData(0.1, -0.01, -0.01)]
    [InlineData(-0.1, 0.01, 0.01)]
    [InlineData(-0.1, -0.01, -0.01)]
    public void BigDecimalMinMagnitudeShouldProduceExpectedResult(double left, double right, double expected)
    {
        // When
        BigDecimal actual = BigDecimal.MinMagnitude(left, right);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.MaxMagnitude should produce expected result")]
    [InlineData(0, 0, 0)]
    [InlineData(0, 1, 1)]
    [InlineData(0, -1, -1)]
    [InlineData(-1, 1, 1)]
    [InlineData(1, 2, 2)]
    [InlineData(1, -2, -2)]
    [InlineData(-1, 2, 2)]
    [InlineData(-1, -2, -2)]
    [InlineData(123.456, 456.789, 456.789)]
    [InlineData(-123.456, 456.789, 456.789)]
    [InlineData(123.456, -456.789, -456.789)]
    [InlineData(-123.456, -456.789, -456.789)]
    [InlineData(0.1, 0.01, 0.1)]
    [InlineData(0.1, -0.01, 0.1)]
    [InlineData(-0.1, 0.01, -0.1)]
    [InlineData(-0.1, -0.01, -0.1)]
    public void BigDecimalMaxMagnitudeShouldProduceExpectedResult(double left, double right, double expected)
    {
        // When
        BigDecimal actual = BigDecimal.MaxMagnitude(left, right);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.MinScale should produce the expected result")]
    [InlineData(0, 0, 0)]
    [InlineData(0.1, 0, 0)]
    [InlineData(0.01, 0.1, 1)]
    [InlineData(0.001, 0.01, 2)]
    public void BigDecimalMinScaleShouldProduceExpectedResult(double left, double right, double expected)
    {
        // When
        BigDecimal actual = BigDecimal.MinScale(left, right);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.MaxScale should produce the expected result")]
    [InlineData(0, 0, 0)]
    [InlineData(0.1, 0, 1)]
    [InlineData(0.01, 0.1, 2)]
    [InlineData(0.001, 0.01, 3)]
    public void BigDecimalMaxScaleShouldProduceExpectedResult(double left, double right, double expected)
    {
        // When
        BigDecimal actual = BigDecimal.MaxScale(left, right);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.MinScaleMaxScale should produce the expected result")]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0.1, 0, 0, 1)]
    [InlineData(0.01, 0.1, 1, 2)]
    [InlineData(0.001, 0.01, 2, 3)]
    public void BigDecimalMinScaleMaxScaleShouldProduceExpectedResult(double left, double right, double expectedMin, double expectedMax)
    {
        // When
        (BigDecimal actualMin, BigDecimal actualMax) = BigDecimal.MinMaxScale(left, right);

        // Then
        Assert.Equal(expectedMin, actualMin);
        Assert.Equal(expectedMax, actualMax);
    }
}
