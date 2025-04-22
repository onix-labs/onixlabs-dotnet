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

using System;
using System.Globalization;
using System.Numerics;
using OnixLabs.Numerics.UnitTests.Data;
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class NumericExtensionsTests
{
    [Theory(DisplayName = "Decimal.GetUnscaledValue should produce the expected result (decimal values)")]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(-1, -1)]
    [InlineData(0.000000001, 1)]
    [InlineData(-0.000000001, -1)]
    [InlineData(123456789, 123456789)]
    [InlineData(12345678.9, 123456789)]
    [InlineData(1234567.89, 123456789)]
    [InlineData(123456.789, 123456789)]
    [InlineData(12345.6789, 123456789)]
    [InlineData(1234.56789, 123456789)]
    [InlineData(123.456789, 123456789)]
    [InlineData(12.3456789, 123456789)]
    [InlineData(1.23456789, 123456789)]
    [InlineData(-123456789, -123456789)]
    [InlineData(-12345678.9, -123456789)]
    [InlineData(-1234567.89, -123456789)]
    [InlineData(-123456.789, -123456789)]
    [InlineData(-12345.6789, -123456789)]
    [InlineData(-1234.56789, -123456789)]
    [InlineData(-123.456789, -123456789)]
    [InlineData(-12.3456789, -123456789)]
    [InlineData(-1.23456789, -123456789)]
    [InlineData(1.000000001, 1000000001)]
    public void DecimalGetUnscaledValueShouldProduceExpectedResultDecimal(decimal value, BigInteger expected)
    {
        // When
        BigInteger actual = value.GetUnscaledValue();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Decimal.GetUnscaledValue should produce the expected result (value and scale)")]
    [InlineData(123, 0, 123)]
    [InlineData(123, 1, 1230)]
    [InlineData(123, 2, 12300)]
    [InlineData(123, 3, 123000)]
    [InlineData(123, 4, 1230000)]
    [InlineData(123, 5, 12300000)]
    [InlineData(123, 6, 123000000)]
    [InlineData(123, 7, 1230000000)]
    [InlineData(123, 8, 12300000000)]
    [InlineData(123, 9, 123000000000)]
    [InlineData(123, 10, 1230000000000)]
    public void DecimalGetUnscaledValueShouldProduceExpectedResultValueAndScale(int value, int scale, BigInteger expected)
    {
        // Given
        decimal candidate = value.ToDecimal(scale, ScaleMode.Integral);

        // When
        BigInteger actual = candidate.GetUnscaledValue();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Decimal.SetScale should preserve or pad when scale is less or equal")]
    [InlineData("123.0", 2, "123.00")]
    [InlineData("123.00", 2, "123.00")]
    [InlineData("123.000", 2, "123.00")]
    [InlineData("0.0", 2, "0.00")]
    [InlineData("123.12", 2, "123.12")]
    public void DecimalSetScaleShouldPreserveOrPadWhenScaleIsLessOrEqual(string inputStr, int scale, string expectedStr)
    {
        // Given
        decimal input = decimal.Parse(inputStr);
        decimal expected = decimal.Parse(expectedStr);

        // When
        decimal result = input.SetScale(scale);

        // Then
        Assert.Equal(expected, result);
    }

    [Theory(DisplayName = "Decimal.SetScale should truncate when no precision loss")]
    [InlineData("123.1200", 2, "123.12")]
    [InlineData("0.1000", 1, "0.1")]
    [InlineData("999.0000", 3, "999.000")]
    public void DecimalSetScaleShouldTruncateWhenNoPrecisionLoss(string inputStr, int scale, string expectedStr)
    {
        // Given
        decimal input = decimal.Parse(inputStr);
        decimal expected = decimal.Parse(expectedStr);

        // When
        decimal result = input.SetScale(scale);

        // Then
        Assert.Equal(expected, result);
    }

    [Theory(DisplayName = "Decimal.SetScale should throw when truncation would lose precision")]
    [InlineData("123.456", 2)]
    [InlineData("1.001", 2)]
    [InlineData("0.123456789", 5)]
    public void DecimalSetScaleShouldThrowWhenTruncationWouldLosePrecision(string inputStr, int scale)
    {
        // Given
        decimal input = decimal.Parse(inputStr);

        // When / Then
        Assert.Throws<InvalidOperationException>(() => input.SetScale(scale));
    }

    [Theory(DisplayName = "Decimal.SetScale(rounding) should apply correct rounding")]
    [InlineData("123.456", 2, MidpointRounding.AwayFromZero, "123.46")]
    [InlineData("123.454", 2, MidpointRounding.AwayFromZero, "123.45")]
    [InlineData("123.455", 2, MidpointRounding.ToZero, "123.45")]
    [InlineData("123.455", 2, MidpointRounding.ToEven, "123.46")]
    [InlineData("0.125", 2, MidpointRounding.ToEven, "0.12")]
    [InlineData("0.135", 2, MidpointRounding.ToEven, "0.14")]
    public void DecimalSetScaleWithRoundingShouldApplyCorrectRounding(string inputStr, int scale, MidpointRounding mode, string expectedStr)
    {
        // Given
        decimal input = decimal.Parse(inputStr);
        decimal expected = decimal.Parse(expectedStr);

        // When
        decimal result = input.SetScale(scale, mode);

        // Then
        Assert.Equal(expected, result);
    }

    [Theory(DisplayName = "Decimal.SetScale(rounding) should truncate when no precision loss")]
    [InlineData("123.0000", 2, MidpointRounding.AwayFromZero, "123.00")]
    [InlineData("1.000", 1, MidpointRounding.ToEven, "1.0")]
    public void DecimalSetScaleWithRoundingShouldTruncateWhenNoPrecisionLoss(string inputStr, int scale, MidpointRounding mode, string expectedStr)
    {
        // Given
        decimal input = decimal.Parse(inputStr);
        decimal expected = decimal.Parse(expectedStr);

        // When
        decimal result = input.SetScale(scale, mode);

        // Then
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName = "Decimal.SetScale should throw when scale is negative")]
    public void DecimalSetScaleShouldThrowWhenScaleIsNegative()
    {
        // Given / When / Then
        Exception exception = Assert.Throws<ArgumentException>(() => 123.45m.SetScale(-1));
        Assert.Contains("Scale must be greater than, or equal to zero.", exception.Message);
    }

    [Fact(DisplayName = "Decimal.SetScale(rounding) should throw when scale is negative")]
    public void DecimalSetScaleWithRoundingShouldThrowWhenScaleIsNegative()
    {
        // Given / When / Then
        Exception exception = Assert.Throws<ArgumentException>(() => 123.45m.SetScale(-1, MidpointRounding.AwayFromZero));
        Assert.Contains("Scale must be greater than, or equal to zero.", exception.Message);
    }

    [Theory(DisplayName = "INumber<T>.IsBetween should produce the expected result")]
    [InlineData(0, 0, 0, true)]
    [InlineData(0, 0, 1, true)]
    [InlineData(0, -1, 1, true)]
    [InlineData(0, 1, 2, false)]
    public void NumberIsBetweenShouldProduceExpectedResult(int value, int minimum, int maximum, bool expected)
    {
        // When
        bool actual = value.IsBetween(minimum, maximum);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BinaryInteger<T>.ToBigInteger should produce the expected result")]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(-1, -1)]
    [InlineData(123456789, 123456789)]
    [InlineData(-123456789, -123456789)]
    public void BinaryIntegerToBigIntegerShouldProduceExpectedResult(int value, BigInteger expected)
    {
        // When
        BigInteger actual = value.ToBigInteger();

        // Then
        Assert.Equal(expected, actual);
    }

    [NumericsExtensionsToDecimalData]
    [Theory(DisplayName = "IBinaryInteger<T>.ToDecimal should produce the expected result")]
    public void BinaryIntegerToDecimalShouldProduceExpectedResult(Int128 value, int scale, ScaleMode mode, string expected)
    {
        // When
        decimal candidate = value.ToDecimal(scale, mode);
        string actual = candidate.ToString(CultureInfo.InvariantCulture);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IBinaryInteger<T>.ToDecimal should throw InvalidOperationException if a value is too large.")]
    public void BinaryIntegerToDecimalShouldThrowInvalidOperationExceptionIfAValueIsTooLarge()
    {
        // When
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => Int128.MaxValue.ToDecimal());

        // Then
        Assert.Equal($"Value is either too large or too small to convert to {nameof(Decimal)}.", exception.Message);
    }

    [Fact(DisplayName = "IBinaryInteger<T>.ToDecimal should throw InvalidOperationException if a value is too small.")]
    public void BinaryIntegerToDecimalShouldThrowInvalidOperationExceptionIfAValueIsTooSmall()
    {
        // When
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => Int128.MinValue.ToDecimal());

        // Then
        Assert.Equal($"Value is either too large or too small to convert to {nameof(Decimal)}.", exception.Message);
    }

    [Fact(DisplayName = "IBinaryInteger<T>.ToDecimal should throw ArgumentException if scale is less than zero.")]
    public void BinaryIntegerToDecimalShouldThrowArgumentExceptionIfScaleIsLessThanZero()
    {
        // When
        ArgumentException exception = Assert.Throws<ArgumentException>(() => 0.ToDecimal(-1));

        // Then
        Assert.Equal("Scale must be between 0 and 28.", exception.Message);
    }

    [Fact(DisplayName = "IBinaryInteger<T>.ToDecimal should throw ArgumentException if mode is not defined.")]
    public void BinaryIntegerToDecimalShouldThrowArgumentExceptionIfModeIsNotDefined()
    {
        // When
        ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => 0.ToDecimal(0, (ScaleMode)2));

        // Then
        Assert.Equal("Invalid ScaleMode enum value: 2. Valid values include: Fractional, Integral. (Parameter 'mode')", exception.Message);
    }

    [NumericsExtensionsToNumberInfoIntegerData]
    [Theory(DisplayName = "IBinaryInteger<T>.ToNumberInfo should produce the expected result")]
    public void BinaryIntegerToNumberInfoProduceExpectedResultIntegerValues
        (Int128 value, BigInteger unscaledValue, int scale, BigInteger significand, int exponent, int sign, int precision)
    {
        // When
        NumberInfo candidate = value.ToNumberInfo();

        // Then
        Assert.Equal(significand, candidate.Significand);
        Assert.Equal(exponent, candidate.Exponent);
        Assert.Equal(precision, candidate.Precision);
        Assert.Equal(sign, candidate.Sign);
        Assert.Equal(unscaledValue, candidate.UnscaledValue);
        Assert.Equal(scale, candidate.Scale);
    }

    [NumericsExtensionsToNumberInfoFloatData]
    [Theory(DisplayName = "Single.ToNumberInfo should produce the expected result")]
    public void SingleToNumberInfoShouldProduceExpectedResultFloatValues
        (float value, BigInteger unscaledValue, int scale, BigInteger significand, int exponent, int sign, int precision)
    {
        // When
        NumberInfo candidate = value.ToNumberInfo();

        // Then
        Assert.Equal(significand, candidate.Significand);
        Assert.Equal(exponent, candidate.Exponent);
        Assert.Equal(precision, candidate.Precision);
        Assert.Equal(sign, candidate.Sign);
        Assert.Equal(unscaledValue, candidate.UnscaledValue);
        Assert.Equal(scale, candidate.Scale);
    }

    [NumericsExtensionsToNumberInfoDoubleData]
    [Theory(DisplayName = "Double.ToNumberInfo should produce the expected result")]
    public void NumberInfoCreateShouldProduceExpectedResultDoubleValues
        (double value, BigInteger unscaledValue, int scale, BigInteger significand, int exponent, int sign, int precision)
    {
        // When
        NumberInfo candidate = value.ToNumberInfo();

        // Then
        Assert.Equal(significand, candidate.Significand);
        Assert.Equal(exponent, candidate.Exponent);
        Assert.Equal(precision, candidate.Precision);
        Assert.Equal(sign, candidate.Sign);
        Assert.Equal(unscaledValue, candidate.UnscaledValue);
        Assert.Equal(scale, candidate.Scale);
    }

    [NumericsExtensionsToNumberInfoDecimalData]
    [Theory(DisplayName = "Decimal.ToNumberInfo should produce the expected result")]
    public void NumberInfoCreateShouldProduceExpectedResultDecimalValues
        (decimal value, BigInteger unscaledValue, int scale, BigInteger significand, int exponent, int sign, int precision)
    {
        // When
        NumberInfo candidate = value.ToNumberInfo();

        // Then
        Assert.Equal(significand, candidate.Significand);
        Assert.Equal(exponent, candidate.Exponent);
        Assert.Equal(precision, candidate.Precision);
        Assert.Equal(sign, candidate.Sign);
        Assert.Equal(unscaledValue, candidate.UnscaledValue);
        Assert.Equal(scale, candidate.Scale);
    }
}
