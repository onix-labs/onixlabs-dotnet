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

public sealed class AngleTests
{
    [Fact(DisplayName = "Angle.Zero should produce the expected result")]
    public void AngleZeroShouldProduceExpectedResult()
    {
        // Given / When
        Angle<Float128> angle = Angle<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, angle.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromQuectoradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1")]
    [InlineData("2.5", "2.5")]
    public void AngleFromQuectoradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromQuectoradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromRontoradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e3")]
    [InlineData("2.5", "2.5e3")]
    public void AngleFromRontoradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromRontoradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromYoctoradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e6")]
    [InlineData("2.5", "2.5e6")]
    public void AngleFromYoctoradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromYoctoradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromZeptoradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e9")]
    [InlineData("2.5", "2.5e9")]
    public void AngleFromZeptoradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromZeptoradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromAttoradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e12")]
    [InlineData("2.5", "2.5e12")]
    public void AngleFromAttoradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromAttoradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromFemtoradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e15")]
    [InlineData("2.5", "2.5e15")]
    public void AngleFromFemtoradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromFemtoradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromPicoradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e18")]
    [InlineData("2.5", "2.5e18")]
    public void AngleFromPicoradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromPicoradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromNanoradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e21")]
    [InlineData("2.5", "2.5e21")]
    public void AngleFromNanoradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromNanoradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromMicroradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e24")]
    [InlineData("2.5", "2.5e24")]
    public void AngleFromMicroradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromMicroradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromMilliradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e27")]
    [InlineData("2.5", "2.5e27")]
    public void AngleFromMilliradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromMilliradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromCentiradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e28")]
    [InlineData("2.5", "2.5e28")]
    public void AngleFromCentiradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromCentiradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromDeciradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e29")]
    [InlineData("2.5", "2.5e29")]
    public void AngleFromDeciradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromDeciradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromRadians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e30")]
    [InlineData("2.5", "2.5e30")]
    public void AngleFromRadiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromRadians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromDecaradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e31")]
    [InlineData("2.5", "2.5e31")]
    public void AngleFromDecaradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromDecaradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromHectoradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e32")]
    [InlineData("2.5", "2.5e32")]
    public void AngleFromHectoradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromHectoradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromKiloradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e33")]
    [InlineData("2.5", "2.5e33")]
    public void AngleFromKiloradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromKiloradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromMegaradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e36")]
    [InlineData("2.5", "2.5e36")]
    public void AngleFromMegaradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromMegaradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromGigaradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e39")]
    [InlineData("2.5", "2.5e39")]
    public void AngleFromGigaradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromGigaradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromTeraradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e42")]
    [InlineData("2.5", "2.5e42")]
    public void AngleFromTeraradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromTeraradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromPetaradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e45")]
    [InlineData("2.5", "2.5e45")]
    public void AngleFromPetaradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromPetaradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromExaradians should produce the expected QuectoRadians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e48")]
    [InlineData("2.5", "2.5e48")]
    public void AngleFromExaradiansShouldProduceExpectedQuectoRadians(string value, string expected)
    {
        Angle<Float128> a = Angle<Float128>.FromExaradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromZettaradians should produce the expected QuectoRadians")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void AngleFromZettaradiansShouldProduceExpectedQuectoRadians(string value)
    {
        // Above Float128's 10^48 exact-power-of-10 range, Parse("X.Ye51") and value × Pow10(51) can
        // diverge in the LSB. Compute expected via the same chain as the unit to keep strict equality.
        Float128 input = Float128.Parse(value);
        Float128 expected = input * GenericMath.Pow10<Float128>(51);

        Angle<Float128> a = Angle<Float128>.FromZettaradians(input);

        Assert.Equal(expected, a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromYottaradians should produce the expected QuectoRadians")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void AngleFromYottaradiansShouldProduceExpectedQuectoRadians(string value)
    {
        Float128 input = Float128.Parse(value);
        Float128 expected = input * GenericMath.Pow10<Float128>(54);

        Angle<Float128> a = Angle<Float128>.FromYottaradians(input);

        Assert.Equal(expected, a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromRonnaradians should produce the expected QuectoRadians")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void AngleFromRonnaradiansShouldProduceExpectedQuectoRadians(string value)
    {
        Float128 input = Float128.Parse(value);
        Float128 expected = input * GenericMath.Pow10<Float128>(57);

        Angle<Float128> a = Angle<Float128>.FromRonnaradians(input);

        Assert.Equal(expected, a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromQuettaradians should produce the expected QuectoRadians")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void AngleFromQuettaradiansShouldProduceExpectedQuectoRadians(string value)
    {
        Float128 input = Float128.Parse(value);
        Float128 expected = input * GenericMath.Pow10<Float128>(60);

        Angle<Float128> a = Angle<Float128>.FromQuettaradians(input);

        Assert.Equal(expected, a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromDegrees should produce the expected QuectoRadians")]
    [InlineData("0")]
    [InlineData("180")]
    [InlineData("360")]
    public void AngleFromDegreesShouldProduceExpectedQuectoRadians(string value)
    {
        // Compute expected at Float128 precision via the same chain as the unit: π × 10^30 / 180.
        // T.Pi at Float128 carries 40+ digits, vastly above double's 15-17 digits.
        Float128 input = Float128.Parse(value);
        Float128 expected = input * (Float128.Pi * GenericMath.Pow10<Float128>(30) / (Float128)180);

        Angle<Float128> a = Angle<Float128>.FromDegrees(input);

        Assert.Equal(expected, a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromArcminutes should produce the expected QuectoRadians")]
    [InlineData("0")]
    [InlineData("60")]    // = 1°
    [InlineData("21600")] // = 360°
    public void AngleFromArcminutesShouldProduceExpectedQuectoRadians(string value)
    {
        // 1 arcmin = π/(180×60) rad
        Float128 input = Float128.Parse(value);
        Float128 expected = input * (Float128.Pi * GenericMath.Pow10<Float128>(30) / (Float128)10800);

        Angle<Float128> a = Angle<Float128>.FromArcminutes(input);

        Assert.Equal(expected, a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromArcseconds should produce the expected QuectoRadians")]
    [InlineData("0")]
    [InlineData("3600")]    // = 1°
    [InlineData("1296000")] // = 360°
    public void AngleFromArcsecondsShouldProduceExpectedQuectoRadians(string value)
    {
        // 1 arcsec = π/(180×3600) rad
        Float128 input = Float128.Parse(value);
        Float128 expected = input * (Float128.Pi * GenericMath.Pow10<Float128>(30) / (Float128)648000);

        Angle<Float128> a = Angle<Float128>.FromArcseconds(input);

        Assert.Equal(expected, a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromGradians should produce the expected QuectoRadians")]
    [InlineData("0")]
    [InlineData("200")] // = 180°
    [InlineData("400")] // = 360°
    public void AngleFromGradiansShouldProduceExpectedQuectoRadians(string value)
    {
        // 1 gon = π/200 rad
        Float128 input = Float128.Parse(value);
        Float128 expected = input * (Float128.Pi * GenericMath.Pow10<Float128>(30) / (Float128)200);

        Angle<Float128> a = Angle<Float128>.FromGradians(input);

        Assert.Equal(expected, a.QuectoRadians);
    }

    [Theory(DisplayName = "Angle.FromTurns should produce the expected QuectoRadians")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("0.5")]
    public void AngleFromTurnsShouldProduceExpectedQuectoRadians(string value)
    {
        // 1 tr = 2π rad
        Float128 input = Float128.Parse(value);
        Float128 expected = input * (Float128.Pi * GenericMath.Pow10<Float128>(30) * (Float128)2);

        Angle<Float128> a = Angle<Float128>.FromTurns(input);

        Assert.Equal(expected, a.QuectoRadians);
    }

    // Round-trips through π-based factors do one division to build the canonical and another to
    // read it back. Each rounds at T's ULP; π is irrational so it doesn't divide out cleanly,
    // leaving an ~1 ULP residual. A Float128 ULP at magnitude 3600 is ~7e-31; this tolerance
    // accepts ~14 ULPs there (and proportionally tighter at smaller magnitudes), still ~25 orders
    // of magnitude tighter than the old `double` test's 1e-9.
    private const string PiRoundtripTolerance = "1e-30";

    [Fact(DisplayName = "Angle non-SI conversions should roundtrip through Radians")]
    public void AngleNonSiConversionsShouldRoundtripThroughRadians()
    {
        Float128 tolerance = Float128.Parse(PiRoundtripTolerance);

        // Given: π rad = 180°
        Angle<Float128> halfTurn = Angle<Float128>.FromRadians(Float128.Pi);
        AssertNearlyEqual((Float128)180, halfTurn.Degrees, tolerance);

        // 2π rad = 1 turn
        Angle<Float128> fullTurn = Angle<Float128>.FromRadians(Float128.Pi * (Float128)2);
        AssertNearlyEqual((Float128)1, fullTurn.Turns, tolerance);

        // π/2 rad = 100 gon
        Angle<Float128> quarterTurn = Angle<Float128>.FromRadians(Float128.Pi / (Float128)2);
        AssertNearlyEqual((Float128)100, quarterTurn.Gradians, tolerance);

        // 1° = 60′ = 3600″
        Angle<Float128> oneDegree = Angle<Float128>.FromDegrees((Float128)1);
        AssertNearlyEqual((Float128)60, oneDegree.Arcminutes, tolerance);
        AssertNearlyEqual((Float128)3600, oneDegree.Arcseconds, tolerance);
    }

    private static void AssertNearlyEqual(Float128 expected, Float128 actual, Float128 tolerance)
    {
        Float128 diff = expected > actual ? expected - actual : actual - expected;
        Assert.True(diff <= tolerance, $"Expected {expected}, got {actual} (diff {diff} exceeds tolerance {tolerance})");
    }

    [Fact(DisplayName = "Angle.Add should produce the expected result")]
    public void AngleAddShouldProduceExpectedValue()
    {
        // Given
        Angle<Float128> left = Angle<Float128>.FromDegrees((Float128)30);
        Angle<Float128> right = Angle<Float128>.FromDegrees((Float128)60);

        // When
        Angle<Float128> result = left.Add(right);

        // Then
        Assert.Equal((Float128)90, result.Degrees);
    }

    [Fact(DisplayName = "Angle.Subtract should produce the expected result")]
    public void AngleSubtractShouldProduceExpectedValue()
    {
        // Given
        Angle<Float128> left = Angle<Float128>.FromDegrees((Float128)180);
        Angle<Float128> right = Angle<Float128>.FromDegrees((Float128)45);

        // When
        Angle<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal((Float128)135, result.Degrees);
    }

    [Fact(DisplayName = "Angle.Multiply should produce the expected result")]
    public void AngleMultiplyShouldProduceExpectedValue()
    {
        // Given
        Angle<Float128> left = Angle<Float128>.FromRadians((Float128)10);  // 1e31 qrad
        Angle<Float128> right = Angle<Float128>.FromRadians((Float128)3);  // 3e30 qrad

        // When
        Angle<Float128> result = left.Multiply(right);  // 1e31 × 3e30 = 3e61 qrad

        // Then — 3e61 exceeds Float128's 10^48 exact range, so compute expected via the same chain.
        Assert.Equal((Float128)10 * GenericMath.Pow10<Float128>(30), left.QuectoRadians);
        Assert.Equal((Float128)3 * GenericMath.Pow10<Float128>(30), right.QuectoRadians);
        Float128 expected = ((Float128)10 * GenericMath.Pow10<Float128>(30)) * ((Float128)3 * GenericMath.Pow10<Float128>(30));
        Assert.Equal(expected, result.QuectoRadians);
    }

    [Fact(DisplayName = "Angle.Divide should produce the expected result")]
    public void AngleDivideShouldProduceExpectedValue()
    {
        // Given
        Angle<Float128> left = Angle<Float128>.FromRadians((Float128)100);  // 1e32 qrad
        Angle<Float128> right = Angle<Float128>.FromRadians((Float128)20);  // 2e31 qrad

        // When
        Angle<Float128> result = left.Divide(right);  // 1e32 / 2e31 = 5 qrad

        // Then
        Assert.Equal((Float128)5, result.QuectoRadians);
    }

    [Fact(DisplayName = "Angle comparison should produce the expected result (left equal to right)")]
    public void AngleComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Angle<Float128> left = Angle<Float128>.FromDegrees((Float128)123);
        Angle<Float128> right = Angle<Float128>.FromDegrees((Float128)123);

        // When / Then
        Assert.Equal(0, Angle<Float128>.Compare(left, right));
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
        Angle<Float128> left = Angle<Float128>.FromDegrees((Float128)456);
        Angle<Float128> right = Angle<Float128>.FromDegrees((Float128)123);

        // When / Then
        Assert.Equal(1, Angle<Float128>.Compare(left, right));
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
        Angle<Float128> left = Angle<Float128>.FromDegrees((Float128)123);
        Angle<Float128> right = Angle<Float128>.FromDegrees((Float128)456);

        // When / Then
        Assert.Equal(-1, Angle<Float128>.Compare(left, right));
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
        // Given: 2 rad and 2000 mrad reduce to the same canonical at Float128.
        Angle<Float128> left = Angle<Float128>.FromRadians((Float128)2);
        Angle<Float128> right = Angle<Float128>.FromMilliradians((Float128)2000);

        // When / Then
        Assert.True(Angle<Float128>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Angle equality should produce the expected result (left not equal to right)")]
    public void AngleEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Angle<Float128> left = Angle<Float128>.FromDegrees((Float128)180);
        Angle<Float128> right = Angle<Float128>.FromDegrees((Float128)90);

        // When / Then
        Assert.False(Angle<Float128>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Angle.ToString should produce the expected result")]
    public void AngleToStringShouldProduceExpectedResult()
    {
        // Given: 1 rad
        Angle<Float128> a = Angle<Float128>.FromRadians((Float128)1);

        // When / Then
        Assert.Equal("1.000 rad", $"{a:rad3}");
        Assert.Equal("1,000.000 mrad", $"{a:mrad3}");
        Assert.Equal("0.001 krad", $"{a:krad3}");
    }

    [Fact(DisplayName = "Angle.ToString Mrad vs mrad are case-sensitive")]
    public void AngleToStringMradVsMradAreCaseSensitive()
    {
        // Given
        Angle<Float128> a = Angle<Float128>.FromRadians((Float128)1);

        // Then
        Assert.Equal("0.000001 Mrad", $"{a:Mrad6}"); // mega
        Assert.Equal("1,000.000 mrad", $"{a:mrad3}"); // milli
    }

    [Fact(DisplayName = "Angle.ToString Prad vs prad are case-sensitive")]
    public void AngleToStringPradVsPradAreCaseSensitive()
    {
        // Given
        Angle<Float128> a = Angle<Float128>.FromRadians((Float128)1);

        // Then
        Assert.Equal("0.000000000000001 Prad", $"{a:Prad15}"); // peta
        Assert.Equal("1,000,000,000,000.000 prad", $"{a:prad3}"); // pico
    }

    [Fact(DisplayName = "Angle.ToString degree symbol should be rendered correctly")]
    public void AngleToStringDegreeSymbolShouldBeRenderedCorrectly()
    {
        // Given
        Angle<Float128> a = Angle<Float128>.FromRadians(Float128.Pi);

        // Then: π rad = 180°
        Assert.Equal("180.000 °", $"{a:deg3}");
    }

    [Fact(DisplayName = "Angle.ToString arcminute and arcsecond symbols should be rendered correctly")]
    public void AngleToStringArcminuteAndArcsecondSymbolsShouldBeRenderedCorrectly()
    {
        // Given
        Angle<Float128> a = Angle<Float128>.FromDegrees((Float128)1);

        // Then: 1° = 60′ = 3600″
        Assert.Equal("60.000 ′", $"{a:arcmin3}");
        Assert.Equal("3,600.000 ″", $"{a:arcsec3}");
    }

    [Fact(DisplayName = "Angle.ToString gon and tr should be rendered correctly")]
    public void AngleToStringGonAndTrShouldBeRenderedCorrectly()
    {
        // Given: a quarter turn
        Angle<Float128> a = Angle<Float128>.FromRadians(Float128.Pi / (Float128)2);

        // Then
        Assert.Equal("100.000 gon", $"{a:gon3}");
        Assert.Equal("0.250 tr", $"{a:tr3}");
    }

    [Fact(DisplayName = "Angle.ToString µrad symbol should differ from format specifier")]
    public void AngleToStringMicroradiansSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        Angle<Float128> a = Angle<Float128>.FromRadians((Float128)1);

        // Then: specifier is urad, but symbol rendered is µrad
        Assert.Equal("1,000,000.000 µrad", $"{a:urad3}");
    }

    [Fact(DisplayName = "Angle.ToString should honor custom culture separators")]
    public void AngleToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Angle<Float128> a = Angle<Float128>.FromDegrees(Float128.Parse("1234.56"));

        // When
        string formatted = a.ToString("deg2", customCulture);

        // Then
        Assert.Equal("1.234,56 °", formatted);
    }
}
