// Copyright 2020 ONIXLabs
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

using System;

namespace OnixLabs.Numerics.UnitTests;

public sealed class GenericMathPowTests
{
    [Theory(DisplayName = "GenericMath.Pow should produce the expected result (Int32)")]
    [InlineData(2, 0, 1)]
    [InlineData(2, 1, 2)]
    [InlineData(2, 10, 1024)]
    [InlineData(3, 4, 81)]
    [InlineData(5, 6, 15625)]
    [InlineData(10, 9, 1000000000)]
    public void GenericMathPowShouldProduceExpectedResultInt32(int value, int exponent, int expected)
    {
        // Given / When
        int result = GenericMath.Pow(value, exponent);

        // Then
        Assert.Equal(expected, result);
    }

    [Theory(DisplayName = "GenericMath.Pow should produce the expected result (Int64)")]
    [InlineData(2L, 40, 1099511627776L)]
    [InlineData(1024L, 5, 1125899906842624L)]
    [InlineData(1000L, 5, 1000000000000000L)]
    public void GenericMathPowShouldProduceExpectedResultInt64(long value, int exponent, long expected)
    {
        // Given / When
        long result = GenericMath.Pow(value, exponent);

        // Then
        Assert.Equal(expected, result);
    }

    [Theory(DisplayName = "GenericMath.Pow should produce the expected result (Double)")]
    [InlineData(0.0, 0, 1.0)]
    [InlineData(1.5, 4, 5.0625)]
    [InlineData(2.0, 10, 1024.0)]
    [InlineData(1024.0, 3, 1073741824.0)]
    public void GenericMathPowShouldProduceExpectedResultDouble(double value, int exponent, double expected)
    {
        // Given / When
        double result = GenericMath.Pow(value, exponent);

        // Then
        Assert.Equal(expected, result, precision: 10);
    }

    [Theory(DisplayName = "GenericMath.Pow should produce the expected result (Decimal)")]
    [InlineData("2", 10, "1024")]
    [InlineData("1024", 5, "1125899906842624")]
    [InlineData("1000", 6, "1000000000000000000")]
    public void GenericMathPowShouldProduceExpectedResultDecimal(string valueStr, int exponent, string expectedStr)
    {
        // Given
        decimal value = decimal.Parse(valueStr);
        decimal expected = decimal.Parse(expectedStr);

        // When
        decimal result = GenericMath.Pow(value, exponent);

        // Then
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName = "GenericMath.Pow should return one when the exponent is zero")]
    public void GenericMathPowShouldReturnOneWhenExponentIsZero()
    {
        // Given / When
        int result = GenericMath.Pow(42, 0);

        // Then
        Assert.Equal(1, result);
    }

    [Fact(DisplayName = "GenericMath.Pow should throw ArgumentException when the exponent is negative")]
    public void GenericMathPowShouldThrowArgumentExceptionWhenExponentNegative()
    {
        // Given / When
        Exception exception = Assert.Throws<ArgumentException>(() => GenericMath.Pow(2, -1));

        // Then
        Assert.Contains("Exponent must be greater", exception.Message);
    }
}
