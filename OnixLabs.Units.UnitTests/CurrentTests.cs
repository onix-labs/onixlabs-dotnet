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

using System.Globalization;
using OnixLabs.Numerics;

namespace OnixLabs.Units.UnitTests;

public sealed class CurrentTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Current.Zero should produce the expected result")]
    public void CurrentZeroShouldProduceExpectedResult()
    {
        // Given / When
        Current<double> current = Current<double>.Zero;

        // Then
        Assert.Equal(0.0, current.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromQuectoamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void CurrentFromQuectoamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromQuectoamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromRontoamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void CurrentFromRontoamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromRontoamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromYoctoamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void CurrentFromYoctoamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromYoctoamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromZeptoamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void CurrentFromZeptoamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromZeptoamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromAttoamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void CurrentFromAttoamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromAttoamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromFemtoamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void CurrentFromFemtoamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromFemtoamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromPicoamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void CurrentFromPicoamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromPicoamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromNanoamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void CurrentFromNanoamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromNanoamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromMicroamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void CurrentFromMicroamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromMicroamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromMilliamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void CurrentFromMilliamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromMilliamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromCentiamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void CurrentFromCentiamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromCentiamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromDeciamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void CurrentFromDeciamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromDeciamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromAmperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void CurrentFromAmperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromAmperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromDecaamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void CurrentFromDecaamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromDecaamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromHectoamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void CurrentFromHectoamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromHectoamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromKiloamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void CurrentFromKiloamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromKiloamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromMegaamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void CurrentFromMegaamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromMegaamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromGigaamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void CurrentFromGigaamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromGigaamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromTeraamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void CurrentFromTeraamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromTeraamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromPetaamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void CurrentFromPetaamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromPetaamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromExaamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void CurrentFromExaamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromExaamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromZettaamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void CurrentFromZettaamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromZettaamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromYottaamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void CurrentFromYottaamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromYottaamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromRonnaamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void CurrentFromRonnaamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromRonnaamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Theory(DisplayName = "Current.FromQuettaamperes should produce the expected QuectoAmperes")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void CurrentFromQuettaamperesShouldProduceExpectedQuectoAmperes(double value, double expected)
    {
        Current<double> c = Current<double>.FromQuettaamperes(value);
        Assert.Equal(expected, c.QuectoAmperes, Tolerance);
    }

    [Fact(DisplayName = "Current.Add should produce the expected result")]
    public void CurrentAddShouldProduceExpectedValue()
    {
        // Given
        Current<double> left = Current<double>.FromAmperes(1.5);
        Current<double> right = Current<double>.FromAmperes(0.5);

        // When
        Current<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.Amperes, Tolerance);
    }

    [Fact(DisplayName = "Current.Subtract should produce the expected result")]
    public void CurrentSubtractShouldProduceExpectedValue()
    {
        // Given
        Current<double> left = Current<double>.FromAmperes(1.5);
        Current<double> right = Current<double>.FromAmperes(0.4);

        // When
        Current<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.Amperes, Tolerance);
    }

    [Fact(DisplayName = "Current.Multiply should produce the expected result")]
    public void CurrentMultiplyShouldProduceExpectedValue()
    {
        // Given
        Current<double> left = Current<double>.FromAmperes(10.0);  // 1e31 qA
        Current<double> right = Current<double>.FromAmperes(3.0);  // 3e30 qA

        // When
        Current<double> result = left.Multiply(right);  // 1e31 * 3e30 = 3e61 qA

        // Then
        Assert.Equal(1e31, left.QuectoAmperes, Tolerance);
        Assert.Equal(3e30, right.QuectoAmperes, Tolerance);
        Assert.Equal(3e61, result.QuectoAmperes, Tolerance);
    }

    [Fact(DisplayName = "Current.Divide should produce the expected result")]
    public void CurrentDivideShouldProduceExpectedValue()
    {
        // Given
        Current<double> left = Current<double>.FromAmperes(100.0);  // 1e32 qA
        Current<double> right = Current<double>.FromAmperes(20.0);  // 2e31 qA

        // When
        Current<double> result = left.Divide(right);  // 1e32 / 2e31 = 5 qA

        // Then
        Assert.Equal(5.0, result.QuectoAmperes, Tolerance);
        Assert.Equal(5e-30, result.Amperes, Tolerance);
    }

    [Fact(DisplayName = "Current comparison should produce the expected result (left equal to right)")]
    public void CurrentComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Current<double> left = Current<double>.FromAmperes(123.0);
        Current<double> right = Current<double>.FromAmperes(123.0);

        // When / Then
        Assert.Equal(0, Current<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Current comparison should produce the expected result (left greater than right)")]
    public void CurrentComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Current<double> left = Current<double>.FromAmperes(456.0);
        Current<double> right = Current<double>.FromAmperes(123.0);

        // When / Then
        Assert.Equal(1, Current<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Current comparison should produce the expected result (left less than right)")]
    public void CurrentComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Current<double> left = Current<double>.FromAmperes(123.0);
        Current<double> right = Current<double>.FromAmperes(456.0);

        // When / Then
        Assert.Equal(-1, Current<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Current equality should produce the expected result (left equal to right)")]
    public void CurrentEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given: 2 A = 2000 mA
        Current<BigDecimal> left = Current<BigDecimal>.FromAmperes(2.0);
        Current<BigDecimal> right = Current<BigDecimal>.FromMilliamperes(2000.0);

        // When / Then
        Assert.True(Current<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Current equality should produce the expected result (left not equal to right)")]
    public void CurrentEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Current<double> left = Current<double>.FromAmperes(2.0);
        Current<double> right = Current<double>.FromMilliamperes(2500.0);

        // When / Then
        Assert.False(Current<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Current.ToString should produce the expected result")]
    public void CurrentToStringShouldProduceExpectedResult()
    {
        // Given
        Current<double> c = Current<double>.FromAmperes(1000.0);

        // When / Then
        Assert.Equal("1,000.000 A", $"{c:A3}");
        Assert.Equal("1.000 kA", $"{c:kA3}");
        Assert.Equal("0.001 MA", $"{c:MA3}");
        Assert.Equal("1,000,000.000 mA", $"{c:mA3}");
    }

    [Fact(DisplayName = "Current.ToString MA vs mA are case-sensitive")]
    public void CurrentToStringMaVsMaAreCaseSensitive()
    {
        // Given
        Current<double> c = Current<double>.FromAmperes(1.0);

        // Then
        Assert.Equal("0.000001 MA", $"{c:MA6}"); // mega
        Assert.Equal("1,000.000 mA", $"{c:mA3}"); // milli
    }

    [Fact(DisplayName = "Current.ToString PA vs pA are case-sensitive")]
    public void CurrentToStringPaVsPaAreCaseSensitive()
    {
        // Given
        Current<double> c = Current<double>.FromAmperes(1.0);

        // Then
        Assert.Equal("0.000000000000001 PA", $"{c:PA15}"); // peta
        Assert.Equal("1,000,000,000,000.000 pA", $"{c:pA3}"); // pico
    }

    [Fact(DisplayName = "Current.ToString µA symbol should differ from format specifier")]
    public void CurrentToStringMicroamperesSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        Current<double> c = Current<double>.FromAmperes(1.0);

        // Then: specifier is uA, but symbol rendered is µA
        Assert.Equal("1,000,000.000 µA", $"{c:uA3}");
    }

    [Fact(DisplayName = "Current.ToString should honor custom culture separators")]
    public void CurrentToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Current<double> c = Current<double>.FromAmperes(1234.56);

        // When
        string formatted = c.ToString("A2", customCulture);

        // Then
        Assert.Equal("1.234,56 A", formatted);
    }
}
