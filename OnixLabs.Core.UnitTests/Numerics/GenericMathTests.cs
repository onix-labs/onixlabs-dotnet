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
using OnixLabs.Core.UnitTests.MockData;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class GenericMathTests
{
    [Theory(DisplayName = "GenericMath.GreatestCommonDenominator should return the expected result")]
    [InlineData(1, 1, 1)]
    [InlineData(1, 2, 1)]
    [InlineData(2, 2, 2)]
    [InlineData(3, 2, 1)]
    [InlineData(4, 2, 2)]
    [InlineData(5, 2, 1)]
    [InlineData(6, 2, 2)]
    [InlineData(7, 2, 1)]
    [InlineData(8, 2, 2)]
    [InlineData(9, 2, 1)]
    [InlineData(10, 2, 2)]
    [InlineData(1, 3, 1)]
    [InlineData(2, 3, 1)]
    [InlineData(3, 3, 3)]
    [InlineData(4, 3, 1)]
    [InlineData(5, 3, 1)]
    [InlineData(6, 3, 3)]
    [InlineData(7, 3, 1)]
    [InlineData(8, 3, 1)]
    [InlineData(9, 3, 3)]
    [InlineData(10, 3, 1)]
    [InlineData(1, 4, 1)]
    [InlineData(2, 4, 2)]
    [InlineData(3, 4, 1)]
    [InlineData(4, 4, 4)]
    [InlineData(5, 4, 1)]
    [InlineData(6, 4, 2)]
    [InlineData(7, 4, 1)]
    [InlineData(8, 4, 4)]
    [InlineData(9, 4, 1)]
    [InlineData(10, 4, 2)]
    [InlineData(1, 5, 1)]
    [InlineData(2, 5, 1)]
    [InlineData(3, 5, 1)]
    [InlineData(4, 5, 1)]
    [InlineData(5, 5, 5)]
    [InlineData(6, 5, 1)]
    [InlineData(7, 5, 1)]
    [InlineData(8, 5, 1)]
    [InlineData(9, 5, 1)]
    [InlineData(10, 5, 5)]
    [InlineData(670930735, 389486873, 1)]
    [InlineData(327501994, 121852120, 2)]
    [InlineData(684081228, 592345407, 3)]
    [InlineData(1205199004, 1513582076, 4)]
    [InlineData(693591160, 1145648635, 5)]
    [InlineData(245882724, 1706154162, 6)]
    [InlineData(437137057, 967165430, 7)]
    [InlineData(1612649570, 1248893510, 10)]
    [InlineData(1638548483, 443553770, 11)]
    [InlineData(454194108, 1523064108, 12)]
    [InlineData(527831902, 1993452045, 13)]
    [InlineData(1516290678, 376733686, 14)]
    [InlineData(1622007810, 912667575, 15)]
    [InlineData(46617008, 522140272, 16)]
    [InlineData(1355702009, 546733107, 17)]
    [InlineData(1810645964, 748781203, 19)]
    [InlineData(653306514, 1638876519, 21)]
    [InlineData(835443650, 252411925, 25)]
    [InlineData(1436268018, 1006287800, 34)]
    [InlineData(1991515674, 2119273103, 43)]
    [InlineData(2112210950, 1417199800, 50)]
    [InlineData(1782339468, 620086038, 78)]
    [InlineData(532513204, 1702056212, 92)]
    [InlineData(834467353, 63608588, 101)]
    [InlineData(2071779437, 391607594, 133)]
    [InlineData(611989812, 976660160, 196)]
    [InlineData(1567337427, 1516628853, 333)]
    public void GenericMathGreatestCommonDenominatorShouldReturnTheExpectedResult(int left, int right, int expected)
    {
        // Arrange / Act
        int actual = GenericMath.GreatestCommonDenominator(left, right);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "GenericMath.DivRem should return the expected result using Int32")]
    [GenericMathDivRemData(1000)]
    public void GenericMathDivRemShouldReturnTheExpectedResultUsingInt32(
        int dividend,
        int divisor,
        int expectedQuotient,
        int expectedRemainder)
    {
        // Arrange / Act
        (int actualQuotient, int actualRemainder) = GenericMath.DivRem(dividend, divisor);

        // Assert
        Assert.Equal(expectedQuotient, actualQuotient);
        Assert.Equal(expectedRemainder, actualRemainder);
    }

    [Theory(DisplayName = "GenericMath.DivRem should return the expected result using Double")]
    [GenericMathDivRemData(1000)]
    public void GenericMathDivRemShouldReturnTheExpectedResultUsingDouble(
        double dividend,
        double divisor,
        double expectedQuotient,
        double expectedRemainder)
    {
        // Arrange / Act
        (double actualQuotient, double actualRemainder) = GenericMath.DivRem(dividend, divisor);

        // Assert
        Assert.Equal(expectedQuotient, actualQuotient);
        Assert.Equal(expectedRemainder, actualRemainder);
    }

    [Theory(DisplayName = "GenericMath.IntegerLength should return the expected result")]
    [GenericMathIntegerLengthData(1000)]
    public void GenericMathIntegerLengthShouldReturnTheExpectedResultUsingDouble(int value, int expected)
    {
        // Arrange / Act
        int actual = GenericMath.IntegerLength(value);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "GenericMath.Pow should return the expected result for Int32")]
    [GenericMathPowData(1, 10, 1, 9)]
    public void GenericMathPowShouldReturnTheExpectedResultForInt32(int value, int exponent, int expected)
    {
        // Arrange / Act
        int actual = GenericMath.Pow(value, exponent);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "GenericMath.Pow should return the expected result for Double")]
    [GenericMathPowData(-10, 10, -10, 10)]
    // TODO : Investigate precision issues and increase number of tests.
    public void GenericMathPowShouldReturnTheExpectedResultForDouble(double value, int exponent, double expected)
    {
        // Arrange / Act
        double actual = GenericMath.Pow(value, exponent);

        // Assert
        Assert.Equal(expected, actual);
    }
}
