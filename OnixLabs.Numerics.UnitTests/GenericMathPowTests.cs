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

using OnixLabs.Numerics.UnitTests.Data;
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class GenericMathPowTests
{
    [Fact(DisplayName = "GenericMath.Pow should throw DivideByZeroException when the value is zero (0) and the exponent is negative one (-1")]
    public void GenericMathPowShouldThrowDivideByZeroExceptionWhenValueIsZeroAndExponentIsNegativeOne()
    {
        // Given
        const string expected = "Attempted to divide by zero.";

        // When
        Exception exception = Assert.Throws<DivideByZeroException>(() => GenericMath.Pow(0, -1));
        string actual = exception.Message;

        // Then
        Assert.Equal(expected, actual);
    }

    [GenericMathPowData(0)]
    [Theory(DisplayName = "GenericMath.Pow should return one (1) when the exponent is zero")]
    public void GenericMathPowShouldReturnOneWhenTheExponentIsZero(double value, int exponent, double expected, double tolerance)
    {
        // When
        double actual = GenericMath.Pow(value, exponent);

        // Then
        Assert.Equal(1, actual, tolerance);
        Assert.Equal(expected, actual, tolerance);
    }

    [GenericMathPowData(1)]
    [Theory(DisplayName = "GenericMath.Pow should return the value when the exponent is positive 1")]
    public void GenericMathPowShouldReturnValueWhenTheExponentIsPositiveOne(double value, int exponent, double expected, double tolerance)
    {
        // When
        double actual = GenericMath.Pow(value, exponent);

        // Then
        Assert.Equal(value, actual, tolerance);
        Assert.Equal(expected, actual, tolerance);
    }

    [GenericMathPowData(2, tolerance: 1e-10)]
    [GenericMathPowData(3, tolerance: 1e-9)]
    [GenericMathPowData(4, tolerance: 1e-7)]
    [GenericMathPowData(5, tolerance: 1e-5)]
    [GenericMathPowData(-1, zero: false)]
    [GenericMathPowData(-2, zero: false, tolerance: 1e-10)]
    [GenericMathPowData(-3, zero: false, tolerance: 1e-10)]
    [GenericMathPowData(-4, zero: false, tolerance: 1e-10)]
    [GenericMathPowData(-5, zero: false, tolerance: 1e-10)]
    [Theory(DisplayName = "GenericMath.Pow should produce the expected result")]
    public void GenericMathPowShouldProduceExpectedResult(double value, int exponent, double expected, double tolerance)
    {
        // When
        double actual = GenericMath.Pow(value, exponent);

        // Then
        Assert.Equal(expected, actual, tolerance);
    }
}
