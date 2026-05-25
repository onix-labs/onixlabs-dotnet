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

public sealed class Ieee754ConverterFloatTests
{
    [Theory(DisplayName = "Float128 Binary-mode conversion should match the existing exact operator")]
    [InlineData(1.5)]
    [InlineData(0.1)]
    [InlineData(-123.456)]
    [InlineData(10000000000.0)]
    [InlineData(0.0000000001)]
    [InlineData(double.Epsilon)]
    public void Float128BinaryModeShouldMatchExactOperator(double value)
    {
        // Given
        Float128 subject = value;

        // When / Then
        Assert.Equal((BigDecimal)subject, new BigDecimal(subject, ConversionMode.Binary), BigDecimalEqualityComparer.Strict);
    }

    [Theory(DisplayName = "Float256 Binary-mode conversion should match the existing exact operator")]
    [InlineData(1.5)]
    [InlineData(0.1)]
    [InlineData(-123.456)]
    [InlineData(10000000000.0)]
    [InlineData(0.0000000001)]
    [InlineData(double.Epsilon)]
    public void Float256BinaryModeShouldMatchExactOperator(double value)
    {
        // Given
        Float256 subject = value;

        // When / Then
        Assert.Equal((BigDecimal)subject, new BigDecimal(subject, ConversionMode.Binary), BigDecimalEqualityComparer.Strict);
    }

    [Theory(DisplayName = "Float128 Decimal-mode conversion should round-trip back to the original value")]
    [InlineData(1.5)]
    [InlineData(0.1)]
    [InlineData(-123.456)]
    [InlineData(10000000000.0)]
    public void Float128DecimalModeShouldRoundTrip(double value)
    {
        // Given
        Float128 subject = value;

        // When
        BigDecimal converted = new(subject, ConversionMode.Decimal);

        // Then
        Assert.Equal(subject, (Float128)converted);
    }

    [Theory(DisplayName = "Float256 Decimal-mode conversion should round-trip back to the original value")]
    [InlineData(1.5)]
    [InlineData(0.1)]
    [InlineData(-123.456)]
    [InlineData(10000000000.0)]
    public void Float256DecimalModeShouldRoundTrip(double value)
    {
        // Given
        Float256 subject = value;

        // When
        BigDecimal converted = new(subject, ConversionMode.Decimal);

        // Then
        Assert.Equal(subject, (Float256)converted);
    }
}
