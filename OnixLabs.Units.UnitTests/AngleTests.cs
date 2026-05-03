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
using System.Globalization;
using OnixLabs.Numerics;

namespace OnixLabs.Units.UnitTests;

public sealed class AngleTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Angle.Zero should produce the expected result")]
    public void AngleZeroShouldProduceExpectedResult()
    {
        // Given / When
        Angle<double> angle = Angle<double>.Zero;

        // Then
        Assert.Equal(0.0, angle.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromQuectoradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void AngleFromQuectoradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromQuectoradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromRontoradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void AngleFromRontoradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromRontoradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromYoctoradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void AngleFromYoctoradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromYoctoradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromZeptoradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void AngleFromZeptoradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromZeptoradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromAttoradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void AngleFromAttoradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromAttoradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromFemtoradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void AngleFromFemtoradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromFemtoradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromPicoradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void AngleFromPicoradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromPicoradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromNanoradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void AngleFromNanoradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromNanoradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromMicroradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void AngleFromMicroradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromMicroradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromMilliradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void AngleFromMilliradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromMilliradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromCentiradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void AngleFromCentiradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromCentiradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromDeciradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void AngleFromDeciradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromDeciradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromRadians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void AngleFromRadiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromRadians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromDecaradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void AngleFromDecaradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromDecaradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromHectoradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void AngleFromHectoradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromHectoradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromKiloradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void AngleFromKiloradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromKiloradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromMegaradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void AngleFromMegaradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromMegaradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromGigaradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void AngleFromGigaradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromGigaradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromTeraradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void AngleFromTeraradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromTeraradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromPetaradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void AngleFromPetaradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromPetaradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromExaradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void AngleFromExaradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromExaradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromZettaradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void AngleFromZettaradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromZettaradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromYottaradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void AngleFromYottaradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromYottaradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromRonnaradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void AngleFromRonnaradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromRonnaradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromQuettaradians should produce the expected QuectoRadians")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void AngleFromQuettaradiansShouldProduceExpectedQuectoRadians(double value, double expected)
    {
        Angle<double> a = Angle<double>.FromQuettaradians(value);
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromDegrees should produce the expected QuectoRadians")]
    [InlineData(0.0)]
    [InlineData(180.0)]
    [InlineData(360.0)]
    public void AngleFromDegreesShouldProduceExpectedQuectoRadians(double value)
    {
        // Given: 1 deg = π/180 rad, so qrad = value × π/180 × 1e30
        double expected = value * (Math.PI / 180.0) * 1e30;

        // When
        Angle<double> a = Angle<double>.FromDegrees(value);

        // Then
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromArcminutes should produce the expected QuectoRadians")]
    [InlineData(0.0)]
    [InlineData(60.0)]    // = 1°
    [InlineData(21600.0)] // = 360°
    public void AngleFromArcminutesShouldProduceExpectedQuectoRadians(double value)
    {
        // Given: 1 arcmin = π/(180×60) rad
        double expected = value * (Math.PI / (180.0 * 60.0)) * 1e30;

        // When
        Angle<double> a = Angle<double>.FromArcminutes(value);

        // Then
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromArcseconds should produce the expected QuectoRadians")]
    [InlineData(0.0)]
    [InlineData(3600.0)]    // = 1°
    [InlineData(1296000.0)] // = 360°
    public void AngleFromArcsecondsShouldProduceExpectedQuectoRadians(double value)
    {
        // Given: 1 arcsec = π/(180×3600) rad
        double expected = value * (Math.PI / (180.0 * 3600.0)) * 1e30;

        // When
        Angle<double> a = Angle<double>.FromArcseconds(value);

        // Then
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromGradians should produce the expected QuectoRadians")]
    [InlineData(0.0)]
    [InlineData(200.0)] // = 180°
    [InlineData(400.0)] // = 360°
    public void AngleFromGradiansShouldProduceExpectedQuectoRadians(double value)
    {
        // Given: 1 gon = π/200 rad
        double expected = value * (Math.PI / 200.0) * 1e30;

        // When
        Angle<double> a = Angle<double>.FromGradians(value);

        // Then
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Theory(DisplayName = "Angle.FromTurns should produce the expected QuectoRadians")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(0.5)]
    public void AngleFromTurnsShouldProduceExpectedQuectoRadians(double value)
    {
        // Given: 1 tr = 2π rad
        double expected = value * (2.0 * Math.PI) * 1e30;

        // When
        Angle<double> a = Angle<double>.FromTurns(value);

        // Then
        Assert.Equal(expected, a.QuectoRadians, Tolerance);
    }

    [Fact(DisplayName = "Angle non-SI conversions should roundtrip through Radians")]
    public void AngleNonSiConversionsShouldRoundtripThroughRadians()
    {
        // Given: π rad = 180°
        Angle<double> halfTurn = Angle<double>.FromRadians(Math.PI);
        Assert.Equal(180.0, halfTurn.Degrees, 1e-9);

        // 2π rad = 1 turn
        Angle<double> fullTurn = Angle<double>.FromRadians(2.0 * Math.PI);
        Assert.Equal(1.0, fullTurn.Turns, 1e-9);

        // π/2 rad = 100 gon
        Angle<double> quarterTurn = Angle<double>.FromRadians(Math.PI / 2.0);
        Assert.Equal(100.0, quarterTurn.Gradians, 1e-9);

        // 1° = 60′ = 3600″
        Angle<double> oneDegree = Angle<double>.FromDegrees(1.0);
        Assert.Equal(60.0, oneDegree.Arcminutes, 1e-9);
        Assert.Equal(3600.0, oneDegree.Arcseconds, 1e-9);
    }

    [Fact(DisplayName = "Angle.Add should produce the expected result")]
    public void AngleAddShouldProduceExpectedValue()
    {
        // Given
        Angle<double> left = Angle<double>.FromDegrees(30.0);
        Angle<double> right = Angle<double>.FromDegrees(60.0);

        // When
        Angle<double> result = left.Add(right);

        // Then
        Assert.Equal(90.0, result.Degrees, 1e-9);
    }

    [Fact(DisplayName = "Angle.Subtract should produce the expected result")]
    public void AngleSubtractShouldProduceExpectedValue()
    {
        // Given
        Angle<double> left = Angle<double>.FromDegrees(180.0);
        Angle<double> right = Angle<double>.FromDegrees(45.0);

        // When
        Angle<double> result = left.Subtract(right);

        // Then
        Assert.Equal(135.0, result.Degrees, 1e-9);
    }

    [Fact(DisplayName = "Angle.Multiply should produce the expected result")]
    public void AngleMultiplyShouldProduceExpectedValue()
    {
        // Given
        Angle<double> left = Angle<double>.FromRadians(10.0);  // 1e31 qrad
        Angle<double> right = Angle<double>.FromRadians(3.0);  // 3e30 qrad

        // When
        Angle<double> result = left.Multiply(right);  // 1e31 * 3e30 = 3e61 qrad

        // Then
        Assert.Equal(1e31, left.QuectoRadians, Tolerance);
        Assert.Equal(3e30, right.QuectoRadians, Tolerance);
        Assert.Equal(3e61, result.QuectoRadians, Tolerance);
    }

    [Fact(DisplayName = "Angle.Divide should produce the expected result")]
    public void AngleDivideShouldProduceExpectedValue()
    {
        // Given
        Angle<double> left = Angle<double>.FromRadians(100.0);  // 1e32 qrad
        Angle<double> right = Angle<double>.FromRadians(20.0);  // 2e31 qrad

        // When
        Angle<double> result = left.Divide(right);  // 1e32 / 2e31 = 5 qrad

        // Then
        Assert.Equal(5.0, result.QuectoRadians, Tolerance);
        Assert.Equal(5e-30, result.Radians, Tolerance);
    }

    [Fact(DisplayName = "Angle comparison should produce the expected result (left equal to right)")]
    public void AngleComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Angle<double> left = Angle<double>.FromDegrees(123.0);
        Angle<double> right = Angle<double>.FromDegrees(123.0);

        // When / Then
        Assert.Equal(0, Angle<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Angle comparison should produce the expected result (left greater than right)")]
    public void AngleComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Angle<double> left = Angle<double>.FromDegrees(456.0);
        Angle<double> right = Angle<double>.FromDegrees(123.0);

        // When / Then
        Assert.Equal(1, Angle<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Angle comparison should produce the expected result (left less than right)")]
    public void AngleComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Angle<double> left = Angle<double>.FromDegrees(123.0);
        Angle<double> right = Angle<double>.FromDegrees(456.0);

        // When / Then
        Assert.Equal(-1, Angle<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Angle equality should produce the expected result (left equal to right)")]
    public void AngleEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given: 2 rad = 2000 mrad (exact 1:1000 ratio across SI scales)
        Angle<BigDecimal> left = Angle<BigDecimal>.FromRadians(2.0);
        Angle<BigDecimal> right = Angle<BigDecimal>.FromMilliradians(2000.0);

        // When / Then
        Assert.True(Angle<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Angle equality should produce the expected result (left not equal to right)")]
    public void AngleEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Angle<double> left = Angle<double>.FromDegrees(180.0);
        Angle<double> right = Angle<double>.FromDegrees(90.0);

        // When / Then
        Assert.False(Angle<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Angle.ToString should produce the expected result")]
    public void AngleToStringShouldProduceExpectedResult()
    {
        // Given: 1 rad
        Angle<double> a = Angle<double>.FromRadians(1.0);

        // When / Then
        Assert.Equal("1.000 rad", $"{a:rad3}");
        Assert.Equal("1,000.000 mrad", $"{a:mrad3}");
        Assert.Equal("0.001 krad", $"{a:krad3}");
    }

    [Fact(DisplayName = "Angle.ToString Mrad vs mrad are case-sensitive")]
    public void AngleToStringMradVsMradAreCaseSensitive()
    {
        // Given
        Angle<double> a = Angle<double>.FromRadians(1.0);

        // Then
        Assert.Equal("0.000001 Mrad", $"{a:Mrad6}"); // mega
        Assert.Equal("1,000.000 mrad", $"{a:mrad3}"); // milli
    }

    [Fact(DisplayName = "Angle.ToString Prad vs prad are case-sensitive")]
    public void AngleToStringPradVsPradAreCaseSensitive()
    {
        // Given
        Angle<double> a = Angle<double>.FromRadians(1.0);

        // Then
        Assert.Equal("0.000000000000001 Prad", $"{a:Prad15}"); // peta
        Assert.Equal("1,000,000,000,000.000 prad", $"{a:prad3}"); // pico
    }

    [Fact(DisplayName = "Angle.ToString degree symbol should be rendered correctly")]
    public void AngleToStringDegreeSymbolShouldBeRenderedCorrectly()
    {
        // Given
        Angle<double> a = Angle<double>.FromRadians(Math.PI);

        // Then: π rad = 180°
        Assert.Equal("180.000 °", $"{a:deg3}");
    }

    [Fact(DisplayName = "Angle.ToString arcminute and arcsecond symbols should be rendered correctly")]
    public void AngleToStringArcminuteAndArcsecondSymbolsShouldBeRenderedCorrectly()
    {
        // Given
        Angle<double> a = Angle<double>.FromDegrees(1.0);

        // Then: 1° = 60′ = 3600″
        Assert.Equal("60.000 ′", $"{a:arcmin3}");
        Assert.Equal("3,600.000 ″", $"{a:arcsec3}");
    }

    [Fact(DisplayName = "Angle.ToString gon and tr should be rendered correctly")]
    public void AngleToStringGonAndTrShouldBeRenderedCorrectly()
    {
        // Given: a quarter turn
        Angle<double> a = Angle<double>.FromRadians(Math.PI / 2.0);

        // Then
        Assert.Equal("100.000 gon", $"{a:gon3}");
        Assert.Equal("0.250 tr", $"{a:tr3}");
    }

    [Fact(DisplayName = "Angle.ToString µrad symbol should differ from format specifier")]
    public void AngleToStringMicroradiansSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        Angle<double> a = Angle<double>.FromRadians(1.0);

        // Then: specifier is urad, but symbol rendered is µrad
        Assert.Equal("1,000,000.000 µrad", $"{a:urad3}");
    }

    [Fact(DisplayName = "Angle.ToString should honor custom culture separators")]
    public void AngleToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Angle<double> a = Angle<double>.FromDegrees(1234.56);

        // When
        string formatted = a.ToString("deg2", customCulture);

        // Then
        Assert.Equal("1.234,56 °", formatted);
    }
}
