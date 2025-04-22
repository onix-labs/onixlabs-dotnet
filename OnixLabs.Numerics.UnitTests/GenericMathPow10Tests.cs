// Copyright 2020-2025 ONIXLabs
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
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class GenericMathPow10Tests
{
    [Theory(DisplayName = "GenericMath.Pow10 should produce the expected result (Int32)")]
    [InlineData(0, 1)]
    [InlineData(1, 10)]
    [InlineData(2, 100)]
    [InlineData(3, 1000)]
    [InlineData(4, 10000)]
    [InlineData(5, 100000)]
    [InlineData(6, 1000000)]
    [InlineData(9, 1000000000)]
    public void GenericMathPow10ShouldProduceExpectedResultInt32(int exponent, int expected)
    {
        // Given / When
        int result = GenericMath.Pow10<int>(exponent);

        // Then
        Assert.Equal(expected, result);
    }

    [Theory(DisplayName = "GenericMath.Pow10 should produce the expected result (Int64)")]
    [InlineData(0, 1L)]
    [InlineData(1, 10L)]
    [InlineData(2, 100L)]
    [InlineData(10, 10000000000L)]
    [InlineData(15, 1000000000000000L)]
    public void GenericMathPow10ShouldProduceExpectedResultInt64(int exponent, long expected)
    {
        // Given / When
        long result = GenericMath.Pow10<long>(exponent);

        // Then
        Assert.Equal(expected, result);
    }

    [Theory(DisplayName = "GenericMath.Pow10 should produce the expected result (Double)")]
    [InlineData(0, 1.0)]
    [InlineData(3, 1000.0)]
    [InlineData(6, 1e6)]
    [InlineData(9, 1e9)]
    [InlineData(15, 1e15)]
    public void GenericMathPow10ShouldProduceExpectedResultDouble(int exponent, double expected)
    {
        // Given / When
        double result = GenericMath.Pow10<double>(exponent);

        // Then
        Assert.Equal(expected, result, precision: 10);
    }

    [Theory(DisplayName = "GenericMath.Pow10 should produce the expected result (Decimal)")]
    [InlineData(0, "1")]
    [InlineData(1, "10")]
    [InlineData(5, "100000")]
    [InlineData(10, "10000000000")]
    public void GenericMathPow10ShouldProduceExpectedResultDecimal(int exponent, string expectedStr)
    {
        // Given / When
        decimal expected = decimal.Parse(expectedStr);
        decimal result = GenericMath.Pow10<decimal>(exponent);

        // Then
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName = "GenericMath.Pow10 should throw ArgumentException when the exponent is negative.")]
    public void GenericMathPow10ShouldThrowArgumentExceptionWhenExponentNegative()
    {
        // Given / When
        Exception exception = Assert.Throws<ArgumentException>(() => GenericMath.Pow10<int>(-1));

        // Then
        Assert.Contains("Exponent must be greater", exception.Message);
    }
}
