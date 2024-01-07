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

public sealed class BigDecimalEquatableValueTests
{
    [Theory(DisplayName = "BigDecimal operator == should return the expected result")]
    [InlineData(0, 0, true)]
    [InlineData(1, 1, true)]
    [InlineData(1, 0, false)]
    [InlineData(0, 1, false)]
    [InlineData(1.1, 1.1, true)]
    [InlineData(1.1, 0.0, false)]
    [InlineData(1.0, 0.1, false)]
    [InlineData(0.0, 0.1, false)]
    [InlineData(0.1, 1.0, false)]
    [InlineData(123.45, 123.45, true)]
    [InlineData(123.45, 45.678, false)]
    [InlineData(12.345, 45.678, false)]
    [InlineData(0.1, 0.01, false)]
    [InlineData(0.01, 0.001, false)]
    [InlineData(0.001, 0.0001, false)]
    [InlineData(0.1, 0.1, true)]
    [InlineData(0.01, 0.01, true)]
    [InlineData(0.001, 0.001, true)]
    [InlineData(0.01, 0.1, false)]
    [InlineData(0.001, 0.01, false)]
    [InlineData(0.0001, 0.001, false)]
    public void BigDecimalEqualsShouldReturnTheExpectedResult(double left, double right, bool expected)
    {
        // Given
        BigDecimal leftBigDecimal = left;
        BigDecimal rightBigDecimal = right;

        // When
        bool actualBigDecimalBigDecimal = leftBigDecimal == rightBigDecimal;
        bool actualBigDecimalDecimal = leftBigDecimal == right;
        bool actualDecimalBigDecimal = left == rightBigDecimal;

        // Then
        Assert.Equal(expected, actualBigDecimalBigDecimal);
        Assert.Equal(expected, actualBigDecimalDecimal);
        Assert.Equal(expected, actualDecimalBigDecimal);
    }

    [Theory(DisplayName = "BigDecimal operator != should return the expected result")]
    [InlineData(0, 0, false)]
    [InlineData(1, 1, false)]
    [InlineData(1, 0, true)]
    [InlineData(0, 1, true)]
    [InlineData(1.1, 1.1, false)]
    [InlineData(1.1, 0.0, true)]
    [InlineData(1.0, 0.1, true)]
    [InlineData(0.0, 0.1, true)]
    [InlineData(0.1, 1.0, true)]
    [InlineData(123.45, 123.45, false)]
    [InlineData(123.45, 45.678, true)]
    [InlineData(12.345, 45.678, true)]
    [InlineData(0.1, 0.01, true)]
    [InlineData(0.01, 0.001, true)]
    [InlineData(0.001, 0.0001, true)]
    [InlineData(0.1, 0.1, false)]
    [InlineData(0.01, 0.01, false)]
    [InlineData(0.001, 0.001, false)]
    [InlineData(0.01, 0.1, true)]
    [InlineData(0.001, 0.01, true)]
    [InlineData(0.0001, 0.001, true)]
    public void BigDecimalNotEqualsShouldReturnTheExpectedResult(double left, double right, bool expected)
    {
        // Given
        BigDecimal leftBigDecimal = left;
        BigDecimal rightBigDecimal = right;

        // When
        bool actualBigDecimalBigDecimal = leftBigDecimal != rightBigDecimal;
        bool actualBigDecimalDecimal = leftBigDecimal != right;
        bool actualDecimalBigDecimal = left != rightBigDecimal;

        // Then
        Assert.Equal(expected, actualBigDecimalBigDecimal);
        Assert.Equal(expected, actualBigDecimalDecimal);
        Assert.Equal(expected, actualDecimalBigDecimal);
    }
}