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

public sealed class VoltageTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Voltage.Zero should produce the expected result")]
    public void VoltageZeroShouldProduceExpectedResult()
    {
        // Given / When
        Voltage<double> voltage = Voltage<double>.Zero;

        // Then
        Assert.Equal(0.0, voltage.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromQuectovolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void VoltageFromQuectovoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromQuectovolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromRontovolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void VoltageFromRontovoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromRontovolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromYoctovolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void VoltageFromYoctovoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromYoctovolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromZeptovolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void VoltageFromZeptovoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromZeptovolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromAttovolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void VoltageFromAttovoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromAttovolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromFemtovolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void VoltageFromFemtovoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromFemtovolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromPicovolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void VoltageFromPicovoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromPicovolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromNanovolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void VoltageFromNanovoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromNanovolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromMicrovolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void VoltageFromMicrovoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromMicrovolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromMillivolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void VoltageFromMillivoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromMillivolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromCentivolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void VoltageFromCentivoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromCentivolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromDecivolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void VoltageFromDecivoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromDecivolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromVolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void VoltageFromVoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromVolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromDecavolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void VoltageFromDecavoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromDecavolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromHectovolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void VoltageFromHectovoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromHectovolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromKilovolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void VoltageFromKilovoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromKilovolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromMegavolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void VoltageFromMegavoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromMegavolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromGigavolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void VoltageFromGigavoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromGigavolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromTeravolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void VoltageFromTeravoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromTeravolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromPetavolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void VoltageFromPetavoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromPetavolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromExavolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void VoltageFromExavoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromExavolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromZettavolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void VoltageFromZettavoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromZettavolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromYottavolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void VoltageFromYottavoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromYottavolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromRonnavolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void VoltageFromRonnavoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromRonnavolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Theory(DisplayName = "Voltage.FromQuettavolts should produce the expected QuectoVolts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void VoltageFromQuettavoltsShouldProduceExpectedQuectoVolts(double value, double expected)
    {
        Voltage<double> v = Voltage<double>.FromQuettavolts(value);
        Assert.Equal(expected, v.QuectoVolts, Tolerance);
    }

    [Fact(DisplayName = "Voltage.Add should produce the expected result")]
    public void VoltageAddShouldProduceExpectedValue()
    {
        // Given
        Voltage<double> left = Voltage<double>.FromVolts(1.5);
        Voltage<double> right = Voltage<double>.FromVolts(0.5);

        // When
        Voltage<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.Volts, Tolerance);
    }

    [Fact(DisplayName = "Voltage.Subtract should produce the expected result")]
    public void VoltageSubtractShouldProduceExpectedValue()
    {
        // Given
        Voltage<double> left = Voltage<double>.FromVolts(1.5);
        Voltage<double> right = Voltage<double>.FromVolts(0.4);

        // When
        Voltage<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.Volts, Tolerance);
    }

    [Fact(DisplayName = "Voltage.Multiply should produce the expected result")]
    public void VoltageMultiplyShouldProduceExpectedValue()
    {
        // Given
        Voltage<double> left = Voltage<double>.FromVolts(10.0);  // 1e31 qV
        Voltage<double> right = Voltage<double>.FromVolts(3.0);  // 3e30 qV

        // When
        Voltage<double> result = left.Multiply(right);  // 1e31 * 3e30 = 3e61 qV

        // Then
        Assert.Equal(1e31, left.QuectoVolts, Tolerance);
        Assert.Equal(3e30, right.QuectoVolts, Tolerance);
        Assert.Equal(3e61, result.QuectoVolts, Tolerance);
    }

    [Fact(DisplayName = "Voltage.Divide should produce the expected result")]
    public void VoltageDivideShouldProduceExpectedValue()
    {
        // Given
        Voltage<double> left = Voltage<double>.FromVolts(100.0);  // 1e32 qV
        Voltage<double> right = Voltage<double>.FromVolts(20.0);  // 2e31 qV

        // When
        Voltage<double> result = left.Divide(right);  // 1e32 / 2e31 = 5 qV

        // Then
        Assert.Equal(5.0, result.QuectoVolts, Tolerance);
        Assert.Equal(5e-30, result.Volts, Tolerance);
    }

    [Fact(DisplayName = "Voltage comparison should produce the expected result (left equal to right)")]
    public void VoltageComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Voltage<double> left = Voltage<double>.FromVolts(123.0);
        Voltage<double> right = Voltage<double>.FromVolts(123.0);

        // When / Then
        Assert.Equal(0, Voltage<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Voltage comparison should produce the expected result (left greater than right)")]
    public void VoltageComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Voltage<double> left = Voltage<double>.FromVolts(456.0);
        Voltage<double> right = Voltage<double>.FromVolts(123.0);

        // When / Then
        Assert.Equal(1, Voltage<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Voltage comparison should produce the expected result (left less than right)")]
    public void VoltageComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Voltage<double> left = Voltage<double>.FromVolts(123.0);
        Voltage<double> right = Voltage<double>.FromVolts(456.0);

        // When / Then
        Assert.Equal(-1, Voltage<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Voltage equality should produce the expected result (left equal to right)")]
    public void VoltageEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given: 2 V = 2000 mV
        Voltage<BigDecimal> left = Voltage<BigDecimal>.FromVolts(2.0);
        Voltage<BigDecimal> right = Voltage<BigDecimal>.FromMillivolts(2000.0);

        // When / Then
        Assert.True(Voltage<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Voltage equality should produce the expected result (left not equal to right)")]
    public void VoltageEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Voltage<double> left = Voltage<double>.FromVolts(2.0);
        Voltage<double> right = Voltage<double>.FromMillivolts(2500.0);

        // When / Then
        Assert.False(Voltage<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Voltage.ToString should produce the expected result")]
    public void VoltageToStringShouldProduceExpectedResult()
    {
        // Given
        Voltage<double> v = Voltage<double>.FromVolts(1000.0);

        // When / Then
        Assert.Equal("1,000.000 V", $"{v:V3}");
        Assert.Equal("1.000 kV", $"{v:kV3}");
        Assert.Equal("0.001 MV", $"{v:MV3}");
        Assert.Equal("1,000,000.000 mV", $"{v:mV3}");
    }

    [Fact(DisplayName = "Voltage.ToString MV vs mV are case-sensitive")]
    public void VoltageToStringMvVsMvAreCaseSensitive()
    {
        // Given
        Voltage<double> v = Voltage<double>.FromVolts(1.0);

        // Then
        Assert.Equal("0.000001 MV", $"{v:MV6}"); // mega
        Assert.Equal("1,000.000 mV", $"{v:mV3}"); // milli
    }

    [Fact(DisplayName = "Voltage.ToString PV vs pV are case-sensitive")]
    public void VoltageToStringPvVsPvAreCaseSensitive()
    {
        // Given
        Voltage<double> v = Voltage<double>.FromVolts(1.0);

        // Then
        Assert.Equal("0.000000000000001 PV", $"{v:PV15}"); // peta
        Assert.Equal("1,000,000,000,000.000 pV", $"{v:pV3}"); // pico
    }

    [Fact(DisplayName = "Voltage.ToString µV symbol should differ from format specifier")]
    public void VoltageToStringMicrovoltsSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        Voltage<double> v = Voltage<double>.FromVolts(1.0);

        // Then: specifier is uV, but symbol rendered is µV
        Assert.Equal("1,000,000.000 µV", $"{v:uV3}");
    }

    [Fact(DisplayName = "Voltage.ToString should honor custom culture separators")]
    public void VoltageToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Voltage<double> v = Voltage<double>.FromVolts(1234.56);

        // When
        string formatted = v.ToString("V2", customCulture);

        // Then
        Assert.Equal("1.234,56 V", formatted);
    }
}
