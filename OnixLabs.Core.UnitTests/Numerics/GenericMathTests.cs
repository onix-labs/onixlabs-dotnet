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
using OnixLabs.Core.UnitTests.Data.Generators;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class GenericMathTests
{
    [Theory(DisplayName = "GenericMath.DivRem should produce the expected result using Int32")]
    [GenericMathDivRemDataGenerator(100)]
    public void GenericMathDivRemShouldProduceExpectedResultUsingInt32(
        int dividend,
        int divisor,
        int expectedQuotient,
        int expectedRemainder)
    {
        // When
        (int actualQuotient, int actualRemainder) = GenericMath.DivRem(dividend, divisor);

        // Then
        Assert.Equal(expectedQuotient, actualQuotient);
        Assert.Equal(expectedRemainder, actualRemainder);
    }

    [Theory(DisplayName = "GenericMath.DivRem should produce the expected result using Double")]
    [GenericMathDivRemDataGenerator(100)]
    public void GenericMathDivRemShouldProduceExpectedResultUsingDouble(
        double dividend,
        double divisor,
        double expectedQuotient,
        double expectedRemainder)
    {
        // When
        (double actualQuotient, double actualRemainder) = GenericMath.DivRem(dividend, divisor);

        // Then
        Assert.Equal(expectedQuotient, actualQuotient);
        Assert.Equal(expectedRemainder, actualRemainder);
    }

    [Theory(DisplayName = "GenericMath.GreatestCommonDenominator should produce the expected result")]
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
    public void GenericMathGreatestCommonDenominatorShouldProduceExpectedResult(int left, int right, int expected)
    {
        // When
        int actual = GenericMath.GreatestCommonDenominator(left, right);

        // Then
        Assert.Equal(expected, actual);
    }

    // TODO : REMOVE!
    // [Theory(DisplayName = "GenericMath.GetScientificExponent should produce the expected result")]
    // [InlineData(double.NaN, 0)]
    // [InlineData(double.PositiveInfinity, 0)]
    // [InlineData(double.NegativeInfinity, 0)]
    // [InlineData(double.NegativeZero, 0)]
    // [InlineData(double.MinValue, 308)]
    // [InlineData(double.MaxValue, 308)]
    // [InlineData(0, 0)]
    // [InlineData(1, 0)]
    // [InlineData(2, 0)]
    // [InlineData(3, 0)]
    // [InlineData(4, 0)]
    // [InlineData(5, 0)]
    // [InlineData(6, 0)]
    // [InlineData(7, 0)]
    // [InlineData(8, 0)]
    // [InlineData(9, 0)]
    // [InlineData(10, 1)]
    // [InlineData(1e+1, 1)]
    // [InlineData(1e+2, 2)]
    // [InlineData(1e+10, 10)]
    // [InlineData(1e+100, 100)]
    // [InlineData(1e-1, -1)]
    // [InlineData(1e-2, -2)]
    // [InlineData(1e-10, -10)]
    // [InlineData(1e-100, -100)]
    // public void GenericMathGetScientificExponentShouldProduceExpectedResult(double value, int expected)
    // {
    //     // When
    //     int actual = GenericMath.GetScientificExponent(value);
    //
    //     // Then
    //     Assert.Equal(expected, actual);
    // }


    [Theory(DisplayName = "GenericMath.IntegerLength should produce the expected result")]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(3, 1)]
    [InlineData(4, 1)]
    [InlineData(5, 1)]
    [InlineData(6, 1)]
    [InlineData(7, 1)]
    [InlineData(8, 1)]
    [InlineData(9, 1)]
    [InlineData(10, 2)]
    [InlineData(11, 2)]
    [InlineData(12, 2)]
    [InlineData(13, 2)]
    [InlineData(14, 2)]
    [InlineData(15, 2)]
    [InlineData(16, 2)]
    [InlineData(17, 2)]
    [InlineData(18, 2)]
    [InlineData(19, 2)]
    [InlineData(20, 2)]
    [InlineData(30, 2)]
    [InlineData(40, 2)]
    [InlineData(50, 2)]
    [InlineData(60, 2)]
    [InlineData(70, 2)]
    [InlineData(80, 2)]
    [InlineData(90, 2)]
    [InlineData(100, 3)]
    [InlineData(1000, 4)]
    [InlineData(10000, 5)]
    [InlineData(100000, 6)]
    [InlineData(1000000, 7)]
    [InlineData(10000000, 8)]
    [InlineData(100000000, 9)]
    [InlineData(1000000000, 10)]
    [InlineData(10000000000, 11)]
    [InlineData(100000000000, 12)]
    [InlineData(1000000000000, 13)]
    [InlineData(10000000000000, 14)]
    [InlineData(100000000000000, 15)]
    [InlineData(1000000000000000, 16)]
    [InlineData(10000000000000000, 17)]
    [InlineData(100000000000000000, 18)]
    [InlineData(1000000000000000000, 19)]
    [InlineData(10000000000000000000, 20)]
    [GenericMathIntegerLengthDataGenerator(100)]
    public void GenericMathIntegerLengthShouldProduceExpectedResultUsingDouble(ulong value, int expected)
    {
        // When
        int actual = GenericMath.IntegerLength(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "GenericMath.Pow should produce the expected result for Int32")]
    [GenericMathPowDataGenerator(1, 10, 1, 9)]
    public void GenericMathPowShouldProduceExpectedResultForInt32(int value, int exponent, int expected)
    {
        // When
        int actual = GenericMath.Pow(value, exponent);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "GenericMath.Pow should produce the expected result for Double")]
    [GenericMathPowDataGenerator(-10, 10, -10, 10)]
    // TODO : Investigate precision issues and increase number of tests.
    public void GenericMathPowShouldProduceExpectedResultForDouble(double value, int exponent, double expected)
    {
        // When
        double actual = GenericMath.Pow(value, exponent);

        // Then
        Assert.Equal(expected, actual);
    }
}
