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

public sealed class CapacitanceTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Capacitance.Zero should produce the expected result")]
    public void CapacitanceZeroShouldProduceExpectedResult()
    {
        // Given / When
        Capacitance<double> capacitance = Capacitance<double>.Zero;

        // Then
        Assert.Equal(0.0, capacitance.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromQuectofarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void CapacitanceFromQuectofaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromQuectofarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromRontofarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void CapacitanceFromRontofaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromRontofarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromYoctofarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void CapacitanceFromYoctofaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromYoctofarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromZeptofarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void CapacitanceFromZeptofaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromZeptofarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromAttofarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void CapacitanceFromAttofaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromAttofarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromFemtofarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void CapacitanceFromFemtofaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromFemtofarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromPicofarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void CapacitanceFromPicofaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromPicofarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromNanofarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void CapacitanceFromNanofaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromNanofarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromMicrofarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void CapacitanceFromMicrofaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromMicrofarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromMillifarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void CapacitanceFromMillifaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromMillifarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromCentifarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void CapacitanceFromCentifaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromCentifarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromDecifarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void CapacitanceFromDecifaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromDecifarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromFarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void CapacitanceFromFaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromFarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromDecafarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void CapacitanceFromDecafaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromDecafarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromHectofarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void CapacitanceFromHectofaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromHectofarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromKilofarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void CapacitanceFromKilofaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromKilofarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromMegafarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void CapacitanceFromMegafaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromMegafarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromGigafarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void CapacitanceFromGigafaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromGigafarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromTerafarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void CapacitanceFromTerafaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromTerafarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromPetafarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void CapacitanceFromPetafaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromPetafarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromExafarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void CapacitanceFromExafaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromExafarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromZettafarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void CapacitanceFromZettafaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromZettafarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromYottafarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void CapacitanceFromYottafaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromYottafarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromRonnafarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void CapacitanceFromRonnafaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromRonnafarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Theory(DisplayName = "Capacitance.FromQuettafarads should produce the expected QuectoFarads")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void CapacitanceFromQuettafaradsShouldProduceExpectedQuectoFarads(double value, double expected)
    {
        Capacitance<double> c = Capacitance<double>.FromQuettafarads(value);
        Assert.Equal(expected, c.QuectoFarads, Tolerance);
    }

    [Fact(DisplayName = "Capacitance.Add should produce the expected result")]
    public void CapacitanceAddShouldProduceExpectedValue()
    {
        // Given
        Capacitance<double> left = Capacitance<double>.FromMicrofarads(1.5);
        Capacitance<double> right = Capacitance<double>.FromMicrofarads(0.5);

        // When
        Capacitance<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.MicroFarads, Tolerance);
    }

    [Fact(DisplayName = "Capacitance.Subtract should produce the expected result")]
    public void CapacitanceSubtractShouldProduceExpectedValue()
    {
        // Given
        Capacitance<double> left = Capacitance<double>.FromMicrofarads(1.5);
        Capacitance<double> right = Capacitance<double>.FromMicrofarads(0.4);

        // When
        Capacitance<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.MicroFarads, Tolerance);
    }

    [Fact(DisplayName = "Capacitance.Multiply should produce the expected result")]
    public void CapacitanceMultiplyShouldProduceExpectedValue()
    {
        // Given
        Capacitance<double> left = Capacitance<double>.FromFarads(10.0);  // 1e31 qF
        Capacitance<double> right = Capacitance<double>.FromFarads(3.0);  // 3e30 qF

        // When
        Capacitance<double> result = left.Multiply(right);  // 1e31 * 3e30 = 3e61 qF

        // Then
        Assert.Equal(1e31, left.QuectoFarads, Tolerance);
        Assert.Equal(3e30, right.QuectoFarads, Tolerance);
        Assert.Equal(3e61, result.QuectoFarads, Tolerance);
    }

    [Fact(DisplayName = "Capacitance.Divide should produce the expected result")]
    public void CapacitanceDivideShouldProduceExpectedValue()
    {
        // Given
        Capacitance<double> left = Capacitance<double>.FromFarads(100.0);  // 1e32 qF
        Capacitance<double> right = Capacitance<double>.FromFarads(20.0);  // 2e31 qF

        // When
        Capacitance<double> result = left.Divide(right);  // 1e32 / 2e31 = 5 qF

        // Then
        Assert.Equal(5.0, result.QuectoFarads, Tolerance);
        Assert.Equal(5e-30, result.Farads, Tolerance);
    }

    [Fact(DisplayName = "Capacitance comparison should produce the expected result (left equal to right)")]
    public void CapacitanceComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Capacitance<double> left = Capacitance<double>.FromMicrofarads(123.0);
        Capacitance<double> right = Capacitance<double>.FromMicrofarads(123.0);

        // When / Then
        Assert.Equal(0, Capacitance<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Capacitance comparison should produce the expected result (left greater than right)")]
    public void CapacitanceComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Capacitance<double> left = Capacitance<double>.FromMicrofarads(456.0);
        Capacitance<double> right = Capacitance<double>.FromMicrofarads(123.0);

        // When / Then
        Assert.Equal(1, Capacitance<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Capacitance comparison should produce the expected result (left less than right)")]
    public void CapacitanceComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Capacitance<double> left = Capacitance<double>.FromMicrofarads(123.0);
        Capacitance<double> right = Capacitance<double>.FromMicrofarads(456.0);

        // When / Then
        Assert.Equal(-1, Capacitance<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Capacitance equality should produce the expected result (left equal to right)")]
    public void CapacitanceEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given: 2 µF = 2000 nF
        Capacitance<BigDecimal> left = Capacitance<BigDecimal>.FromMicrofarads(2.0);
        Capacitance<BigDecimal> right = Capacitance<BigDecimal>.FromNanofarads(2000.0);

        // When / Then
        Assert.True(Capacitance<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Capacitance equality should produce the expected result (left not equal to right)")]
    public void CapacitanceEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Capacitance<double> left = Capacitance<double>.FromMicrofarads(2.0);
        Capacitance<double> right = Capacitance<double>.FromNanofarads(2500.0);

        // When / Then
        Assert.False(Capacitance<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Capacitance.ToString should produce the expected result")]
    public void CapacitanceToStringShouldProduceExpectedResult()
    {
        // Given: 1 F
        Capacitance<double> c = Capacitance<double>.FromFarads(1.0);

        // When / Then
        Assert.Equal("1.000 F", $"{c:F3}");
        Assert.Equal("1,000.000 mF", $"{c:mF3}");
        Assert.Equal("1,000,000.000 µF", $"{c:uF3}");
        Assert.Equal("1,000,000,000.000 nF", $"{c:nF3}");
        Assert.Equal("1,000,000,000,000.000 pF", $"{c:pF3}");
    }

    [Fact(DisplayName = "Capacitance.ToString MF vs mF are case-sensitive")]
    public void CapacitanceToStringMfVsMfAreCaseSensitive()
    {
        // Given
        Capacitance<double> c = Capacitance<double>.FromFarads(1.0);

        // Then
        Assert.Equal("0.000001 MF", $"{c:MF6}"); // mega
        Assert.Equal("1,000.000 mF", $"{c:mF3}"); // milli
    }

    [Fact(DisplayName = "Capacitance.ToString PF vs pF are case-sensitive")]
    public void CapacitanceToStringPfVsPfAreCaseSensitive()
    {
        // Given
        Capacitance<double> c = Capacitance<double>.FromFarads(1.0);

        // Then
        Assert.Equal("0.000000000000001 PF", $"{c:PF15}"); // peta
        Assert.Equal("1,000,000,000,000.000 pF", $"{c:pF3}"); // pico
    }

    [Fact(DisplayName = "Capacitance.ToString µF symbol should differ from format specifier")]
    public void CapacitanceToStringMicrofaradsSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        Capacitance<double> c = Capacitance<double>.FromFarads(1.0);

        // Then: specifier is uF, but symbol rendered is µF
        Assert.Equal("1,000,000.000 µF", $"{c:uF3}");
    }

    [Fact(DisplayName = "Capacitance.ToString should honor custom culture separators")]
    public void CapacitanceToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Capacitance<double> c = Capacitance<double>.FromMicrofarads(1234.56);

        // When
        string formatted = c.ToString("uF2", customCulture);

        // Then
        Assert.Equal("1.234,56 µF", formatted);
    }
}
