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

public sealed class PressureTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Pressure.Zero should produce the expected result")]
    public void PressureZeroShouldProduceExpectedResult()
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.Zero;

        // Then
        Assert.Equal(0.0, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromQuectopascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void PressureFromQuectopascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromQuectopascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromRontopascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void PressureFromRontopascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromRontopascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromYoctopascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void PressureFromYoctopascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromYoctopascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromZeptopascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void PressureFromZeptopascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromZeptopascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromAttopascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void PressureFromAttopascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromAttopascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromFemtopascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void PressureFromFemtopascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromFemtopascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromPicopascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void PressureFromPicopascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromPicopascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromNanopascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void PressureFromNanopascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromNanopascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromMicropascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void PressureFromMicropascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromMicropascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromMillipascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void PressureFromMillipascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromMillipascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromCentipascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void PressureFromCentipascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromCentipascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromDecipascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void PressureFromDecipascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromDecipascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void PressureFromPascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromPascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromDecapascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void PressureFromDecapascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromDecapascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromHectopascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void PressureFromHectopascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromHectopascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromKilopascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void PressureFromKilopascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromKilopascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromMegapascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void PressureFromMegapascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromMegapascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromGigapascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void PressureFromGigapascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromGigapascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromTerapascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void PressureFromTerapascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromTerapascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromPetapascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void PressureFromPetapascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromPetapascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromExapascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void PressureFromExapascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromExapascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromZettapascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void PressureFromZettapascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromZettapascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromYottapascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void PressureFromYottapascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromYottapascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromRonnapascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void PressureFromRonnapascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromRonnapascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromQuettapascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void PressureFromQuettapascalsShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromQuettapascals(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromBar should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e35)] // 1 bar = 100,000 Pa
    [InlineData(2.5, 2.5e35)]
    public void PressureFromBarShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromBar(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromMillibar should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(1013.25, 1.01325e35)] // ≈ 1 atm
    public void PressureFromMillibarShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromMillibar(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromAtmospheres should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.01325e35)] // 1 atm = 101,325 Pa
    [InlineData(2.5, 2.533125e35)]
    public void PressureFromAtmospheresShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromAtmospheres(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromTorr should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(760.0, 1.01325e35)] // 760 Torr = 1 atm exactly
    [InlineData(1.0, 1.3332236842105263e32)]
    public void PressureFromTorrShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromTorr(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromMillimetersOfMercury should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.33322387415e32)]
    [InlineData(760.0, 1.013250144354e35)]
    public void PressureFromMillimetersOfMercuryShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromMillimetersOfMercury(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromInchesOfMercury should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.386389e33)]
    [InlineData(29.92, 1.013207e35)] // ≈ 1 atm
    public void PressureFromInchesOfMercuryShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromInchesOfMercury(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromPoundsPerSquareInch should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 6.894757293168361e33)]
    [InlineData(14.6959488, 1.01325e35)] // ≈ 1 atm
    public void PressureFromPoundsPerSquareInchShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromPoundsPerSquareInch(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromKilopoundsPerSquareInch should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 6.894757293168361e36)] // 1 ksi = 1000 psi
    [InlineData(2.5, 1.7236893232920903e37)]
    public void PressureFromKilopoundsPerSquareInchShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromKilopoundsPerSquareInch(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromTechnicalAtmospheres should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.80665e34)] // 1 at = 98,066.5 Pa
    [InlineData(2.0, 1.96133e35)]
    public void PressureFromTechnicalAtmospheresShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromTechnicalAtmospheres(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromBaryes should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)] // 1 Ba = 0.1 Pa
    [InlineData(10.0, 1e30)] // 10 Ba = 1 Pa
    public void PressureFromBaryesShouldProduceExpectedQuectoPascals(double value, double expected)
    {
        Pressure<double> p = Pressure<double>.FromBaryes(value);
        Assert.Equal(expected, p.QuectoPascals, Tolerance);
    }

    [Fact(DisplayName = "Pressure.Add should produce the expected result")]
    public void PressureAddShouldProduceExpectedValue()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromKilopascals(1.5);
        Pressure<double> right = Pressure<double>.FromKilopascals(0.5);

        // When
        Pressure<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.KiloPascals, Tolerance);
    }

    [Fact(DisplayName = "Pressure.Subtract should produce the expected result")]
    public void PressureSubtractShouldProduceExpectedValue()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromKilopascals(1.5);
        Pressure<double> right = Pressure<double>.FromKilopascals(0.4);

        // When
        Pressure<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.KiloPascals, Tolerance);
    }

    [Fact(DisplayName = "Pressure.Multiply should produce the expected result")]
    public void PressureMultiplyShouldProduceExpectedValue()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromPascals(10.0);  // 1e31 qPa
        Pressure<double> right = Pressure<double>.FromPascals(3.0);  // 3e30 qPa

        // When
        Pressure<double> result = left.Multiply(right);  // 1e31 * 3e30 = 3e61 qPa

        // Then
        Assert.Equal(1e31, left.QuectoPascals, Tolerance);
        Assert.Equal(3e30, right.QuectoPascals, Tolerance);
        Assert.Equal(3e61, result.QuectoPascals, Tolerance);
    }

    [Fact(DisplayName = "Pressure.Divide should produce the expected result")]
    public void PressureDivideShouldProduceExpectedValue()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromPascals(100.0);  // 1e32 qPa
        Pressure<double> right = Pressure<double>.FromPascals(20.0);  // 2e31 qPa

        // When
        Pressure<double> result = left.Divide(right);  // 1e32 / 2e31 = 5 qPa

        // Then
        Assert.Equal(5.0, result.QuectoPascals, Tolerance);
        Assert.Equal(5e-30, result.Pascals, Tolerance);
    }

    [Fact(DisplayName = "Pressure comparison should produce the expected result (left equal to right)")]
    public void PressureComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromKilopascals(123.0);
        Pressure<double> right = Pressure<double>.FromKilopascals(123.0);

        // When / Then
        Assert.Equal(0, Pressure<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Pressure comparison should produce the expected result (left greater than right)")]
    public void PressureComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromKilopascals(456.0);
        Pressure<double> right = Pressure<double>.FromKilopascals(123.0);

        // When / Then
        Assert.Equal(1, Pressure<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Pressure comparison should produce the expected result (left less than right)")]
    public void PressureComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromKilopascals(123.0);
        Pressure<double> right = Pressure<double>.FromKilopascals(456.0);

        // When / Then
        Assert.Equal(-1, Pressure<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Pressure equality should produce the expected result (left equal to right)")]
    public void PressureEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Pressure<BigDecimal> left = Pressure<BigDecimal>.FromKilopascals(2.0);
        Pressure<BigDecimal> right = Pressure<BigDecimal>.FromPascals(2000.0);

        // When / Then
        Assert.True(Pressure<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Pressure equality should produce the expected result (left not equal to right)")]
    public void PressureEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromKilopascals(2.0);
        Pressure<double> right = Pressure<double>.FromPascals(2500.0);

        // When / Then
        Assert.False(Pressure<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Pressure.ToString should produce the expected result")]
    public void PressureToStringShouldProduceExpectedResult()
    {
        // Given: 1 atm
        Pressure<double> p = Pressure<double>.FromAtmospheres(1.0);

        // When / Then
        Assert.Equal("101,325.000 Pa", $"{p:Pa3}");
        Assert.Equal("101.325 kPa", $"{p:kPa3}");
        Assert.Equal("0.101 MPa", $"{p:MPa3}");
        Assert.Equal("1,013.250 hPa", $"{p:hPa3}");
        Assert.Equal("1.013 bar", $"{p:bar3}");
        Assert.Equal("1,013.250 mbar", $"{p:mbar3}");
        Assert.Equal("1.000 atm", $"{p:atm3}");
        Assert.Equal("760.000 Torr", $"{p:Torr3}");
        Assert.Equal("14.696 psi", $"{p:psi3}");
    }

    [Fact(DisplayName = "Pressure.ToString MPa vs mPa are case-sensitive")]
    public void PressureToStringMpaVsMpaAreCaseSensitive()
    {
        // Given
        Pressure<double> p = Pressure<double>.FromPascals(1.0);

        // When / Then
        Assert.Equal("0.000001 MPa", $"{p:MPa6}"); // mega
        Assert.Equal("1,000.000 mPa", $"{p:mPa3}"); // milli
    }

    [Fact(DisplayName = "Pressure.ToString should honor custom culture separators")]
    public void PressureToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Pressure<double> p = Pressure<double>.FromKilopascals(1234.56);

        // When
        string formatted = p.ToString("kPa2", customCulture);

        // Then
        Assert.Equal("1.234,56 kPa", formatted);
    }
}
