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

public sealed class SolidAngleTests
{
    [Fact(DisplayName = "SolidAngle.Zero should produce the expected result")]
    public void SolidAngleZeroShouldProduceExpectedResult()
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.Zero;
        Assert.Equal(Float128.Zero, a.QuectoSteradians);
    }

    // -- SI-prefix decimal scaling round-trips: pure 10^k conversions, so exact equality holds. --

    [Theory(DisplayName = "SolidAngle.FromQuectosteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1")]
    [InlineData("2.5", "2.5")]
    public void SolidAngleFromQuectosteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromQuectosteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromRontosteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e3")]
    [InlineData("2.5", "2.5e3")]
    public void SolidAngleFromRontosteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromRontosteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromYoctosteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e6")]
    [InlineData("2.5", "2.5e6")]
    public void SolidAngleFromYoctosteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromYoctosteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromZeptosteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e9")]
    [InlineData("2.5", "2.5e9")]
    public void SolidAngleFromZeptosteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromZeptosteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromAttosteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e12")]
    [InlineData("2.5", "2.5e12")]
    public void SolidAngleFromAttosteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromAttosteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromFemtosteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e15")]
    [InlineData("2.5", "2.5e15")]
    public void SolidAngleFromFemtosteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromFemtosteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromPicosteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e18")]
    [InlineData("2.5", "2.5e18")]
    public void SolidAngleFromPicosteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromPicosteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromNanosteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e21")]
    [InlineData("2.5", "2.5e21")]
    public void SolidAngleFromNanosteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromNanosteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromMicrosteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e24")]
    [InlineData("2.5", "2.5e24")]
    public void SolidAngleFromMicrosteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromMicrosteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromMillisteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e27")]
    [InlineData("2.5", "2.5e27")]
    public void SolidAngleFromMillisteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromMillisteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromCentisteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e28")]
    [InlineData("2.5", "2.5e28")]
    public void SolidAngleFromCentisteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromCentisteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromDecisteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e29")]
    [InlineData("2.5", "2.5e29")]
    public void SolidAngleFromDecisteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromDecisteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromSteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e30")]
    [InlineData("2.5", "2.5e30")]
    public void SolidAngleFromSteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromSteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromDecasteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e31")]
    [InlineData("2.5", "2.5e31")]
    public void SolidAngleFromDecasteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromDecasteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromHectosteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e32")]
    [InlineData("2.5", "2.5e32")]
    public void SolidAngleFromHectosteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromHectosteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromKilosteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e33")]
    [InlineData("2.5", "2.5e33")]
    public void SolidAngleFromKilosteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromKilosteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromMegasteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e36")]
    [InlineData("2.5", "2.5e36")]
    public void SolidAngleFromMegasteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromMegasteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromGigasteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e39")]
    [InlineData("2.5", "2.5e39")]
    public void SolidAngleFromGigasteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromGigasteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromTerasteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e42")]
    [InlineData("2.5", "2.5e42")]
    public void SolidAngleFromTerasteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromTerasteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromPetasteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e45")]
    [InlineData("2.5", "2.5e45")]
    public void SolidAngleFromPetasteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromPetasteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromExasteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e48")]
    [InlineData("2.5", "2.5e48")]
    public void SolidAngleFromExasteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromExasteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromZettasteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e51")]
    [InlineData("2.5", "2.5e51")]
    public void SolidAngleFromZettasteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromZettasteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromYottasteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e54")]
    [InlineData("2.5", "2.5e54")]
    public void SolidAngleFromYottasteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromYottasteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromRonnasteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e57")]
    [InlineData("2.5", "2.5e57")]
    public void SolidAngleFromRonnasteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromRonnasteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromQuettasteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e60")]
    [InlineData("2.5", "2.5e60")]
    public void SolidAngleFromQuettasteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromQuettasteradians(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoSteradians);
    }

    // -- π-based conversions: compute expected via the same chain to keep strict equality. --

    [Theory(DisplayName = "SolidAngle.FromSquareDegrees should produce the expected QuectoSteradians")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("180")]
    public void SolidAngleFromSquareDegreesShouldProduceExpectedQuectoSteradians(string value)
    {
        // 1 deg² = (π/180)² sr = π² × 10^30 / 32400 qsr
        Float128 input = Float128.Parse(value);
        Float128 expected = input * (Float128.Pi * Float128.Pi * UnitMath.Pow10<Float128>(30) / (Float128)32400);

        SolidAngle<Float128> a = SolidAngle<Float128>.FromSquareDegrees(input);

        Assert.Equal(expected, a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromSquareArcminutes should produce the expected QuectoSteradians")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("3600")] // = 1 deg²
    public void SolidAngleFromSquareArcminutesShouldProduceExpectedQuectoSteradians(string value)
    {
        // 1 arcmin² = (π/10800)² sr = π² × 10^30 / 116,640,000 qsr
        Float128 input = Float128.Parse(value);
        Float128 expected = input * (Float128.Pi * Float128.Pi * UnitMath.Pow10<Float128>(30) / (Float128)116640000L);

        SolidAngle<Float128> a = SolidAngle<Float128>.FromSquareArcminutes(input);

        Assert.Equal(expected, a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromSquareArcseconds should produce the expected QuectoSteradians")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("12960000")] // = 1 deg²
    public void SolidAngleFromSquareArcsecondsShouldProduceExpectedQuectoSteradians(string value)
    {
        // 1 arcsec² = (π/648000)² sr = π² × 10^30 / 419,904,000,000 qsr
        Float128 input = Float128.Parse(value);
        Float128 expected = input * (Float128.Pi * Float128.Pi * UnitMath.Pow10<Float128>(30) / (Float128)419904000000L);

        SolidAngle<Float128> a = SolidAngle<Float128>.FromSquareArcseconds(input);

        Assert.Equal(expected, a.QuectoSteradians);
    }

    [Theory(DisplayName = "SolidAngle.FromSpats should produce the expected QuectoSteradians")]
    [InlineData("0")]
    [InlineData("1")] // = 4π sr (the whole sphere)
    [InlineData("0.5")] // = 2π sr (a hemisphere)
    public void SolidAngleFromSpatsShouldProduceExpectedQuectoSteradians(string value)
    {
        // 1 spat = 4π sr = 4π × 10^30 qsr
        Float128 input = Float128.Parse(value);
        Float128 expected = input * (Float128.Pi * UnitMath.Pow10<Float128>(30) * (Float128)4);

        SolidAngle<Float128> a = SolidAngle<Float128>.FromSpats(input);

        Assert.Equal(expected, a.QuectoSteradians);
    }

    // -- Real-world π-based scale identities, with tolerance for π-irrationality round-trip ULPs. --

    // One spat = 4π sr; the whole sphere = 4π sr; 1 sr = (180/π)² deg² ≈ 3282.806... deg².
    // Round-trip through two π-divisions leaves an ~1 ULP residual at Float128 (~7e-31 at magnitude 3282).
    private const string PiRoundtripTolerance = "1e-29";

    [Fact(DisplayName = "SolidAngle scale identities should round-trip through Steradians within tolerance")]
    public void SolidAngleScaleIdentitiesShouldRoundtripThroughSteradians()
    {
        Float128 tolerance = Float128.Parse(PiRoundtripTolerance);

        // Full sphere: 1 spat == 4π sr
        SolidAngle<Float128> sphere = SolidAngle<Float128>.FromSpats((Float128)1);
        AssertNearlyEqual(Float128.Pi * (Float128)4, sphere.Steradians, tolerance);

        // 1 sr ≈ (180/π)² deg²
        SolidAngle<Float128> oneSr = SolidAngle<Float128>.FromSteradians((Float128)1);
        Float128 expectedSqDeg = ((Float128)180 / Float128.Pi) * ((Float128)180 / Float128.Pi);
        AssertNearlyEqual(expectedSqDeg, oneSr.SquareDegrees, tolerance);

        // 1 deg² == 3600 arcmin² == 12,960,000 arcsec² (exact ratios — no π involved here)
        SolidAngle<Float128> oneSqDeg = SolidAngle<Float128>.FromSquareDegrees((Float128)1);
        AssertNearlyEqual((Float128)3600, oneSqDeg.SquareArcminutes, tolerance);
        AssertNearlyEqual((Float128)12960000, oneSqDeg.SquareArcseconds, tolerance);
    }

    private static void AssertNearlyEqual(Float128 expected, Float128 actual, Float128 tolerance)
    {
        Float128 diff = expected > actual ? expected - actual : actual - expected;
        Assert.True(diff <= tolerance, $"Expected {expected}, got {actual} (diff {diff} exceeds tolerance {tolerance})");
    }

    // -- Arithmetic, comparison, equality. --

    [Fact(DisplayName = "SolidAngle.Add should produce the expected result")]
    public void SolidAngleAddShouldProduceExpectedValue()
    {
        SolidAngle<Float128> left = SolidAngle<Float128>.FromSteradians((Float128)3);
        SolidAngle<Float128> right = SolidAngle<Float128>.FromSteradians((Float128)4);

        SolidAngle<Float128> result = left.Add(right);

        Assert.Equal((Float128)7, result.Steradians);
    }

    [Fact(DisplayName = "SolidAngle.Subtract should produce the expected result")]
    public void SolidAngleSubtractShouldProduceExpectedValue()
    {
        SolidAngle<Float128> left = SolidAngle<Float128>.FromSteradians((Float128)10);
        SolidAngle<Float128> right = SolidAngle<Float128>.FromSteradians((Float128)3);

        SolidAngle<Float128> result = left.Subtract(right);

        Assert.Equal((Float128)7, result.Steradians);
    }

    [Fact(DisplayName = "SolidAngle comparison should produce the expected result (left equal to right)")]
    public void SolidAngleComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        SolidAngle<Float128> left = SolidAngle<Float128>.FromSteradians((Float128)5);
        SolidAngle<Float128> right = SolidAngle<Float128>.FromSteradians((Float128)5);

        Assert.Equal(0, SolidAngle<Float128>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "SolidAngle comparison should produce the expected result (left greater than right)")]
    public void SolidAngleComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        SolidAngle<Float128> left = SolidAngle<Float128>.FromSteradians((Float128)10);
        SolidAngle<Float128> right = SolidAngle<Float128>.FromSteradians((Float128)5);

        Assert.Equal(1, SolidAngle<Float128>.Compare(left, right));
        Assert.True(left > right);
        Assert.False(left < right);
    }

    [Fact(DisplayName = "SolidAngle comparison should produce the expected result (left less than right)")]
    public void SolidAngleComparisonShouldProduceExpectedLeftLessThanRight()
    {
        SolidAngle<Float128> left = SolidAngle<Float128>.FromSteradians((Float128)5);
        SolidAngle<Float128> right = SolidAngle<Float128>.FromSteradians((Float128)10);

        Assert.Equal(-1, SolidAngle<Float128>.Compare(left, right));
        Assert.True(left < right);
        Assert.False(left > right);
    }

    [Fact(DisplayName = "SolidAngle equality should produce the expected result (left equal to right via different scales)")]
    public void SolidAngleEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // 2 sr and 2000 msr reduce to the same canonical.
        SolidAngle<Float128> left = SolidAngle<Float128>.FromSteradians((Float128)2);
        SolidAngle<Float128> right = SolidAngle<Float128>.FromMillisteradians((Float128)2000);

        Assert.True(SolidAngle<Float128>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "SolidAngle equality should produce the expected result (left not equal to right)")]
    public void SolidAngleEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        SolidAngle<Float128> left = SolidAngle<Float128>.FromSteradians((Float128)1);
        SolidAngle<Float128> right = SolidAngle<Float128>.FromSteradians((Float128)2);

        Assert.False(SolidAngle<Float128>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    // -- Formatting. --

    [Fact(DisplayName = "SolidAngle.ToString should produce the expected result for steradian-family scales")]
    public void SolidAngleToStringShouldProduceExpectedResult()
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromSteradians((Float128)1);

        Assert.Equal("1.000 sr", a.ToString("sr3", CultureInfo.InvariantCulture));
        Assert.Equal("1,000.000 msr", a.ToString("msr3", CultureInfo.InvariantCulture));
        Assert.Equal("0.001 ksr", a.ToString("ksr3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "SolidAngle.ToString Mrad-style case-sensitivity (Msr vs msr)")]
    public void SolidAngleToStringMsrVsMsrAreCaseSensitive()
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromSteradians((Float128)1);

        Assert.Equal("0.000001 Msr", a.ToString("Msr6", CultureInfo.InvariantCulture)); // mega
        Assert.Equal("1,000.000 msr", a.ToString("msr3", CultureInfo.InvariantCulture)); // milli
    }

    [Fact(DisplayName = "SolidAngle.ToString Psr vs psr are case-sensitive")]
    public void SolidAngleToStringPsrVsPsrAreCaseSensitive()
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromSteradians((Float128)1);

        Assert.Equal("0.000000000000001 Psr", $"{a:Psr15}"); // peta
        Assert.Equal("1,000,000,000,000.000 psr", $"{a:psr3}"); // pico
    }

    [Fact(DisplayName = "SolidAngle.ToString µsr symbol should differ from format specifier")]
    public void SolidAngleToStringMicrosteradiansSymbolShouldDifferFromFormatSpecifier()
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromSteradians((Float128)1);

        // specifier is "usr", but the rendered symbol is "µsr".
        Assert.Equal("1,000,000.000 µsr", a.ToString("usr3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "SolidAngle.ToString sqdeg symbol renders as deg²")]
    public void SolidAngleToStringSqdegSymbolRendersAsDeg2()
    {
        // 1 sr = (180/π)² deg² ≈ 3282.806...
        SolidAngle<Float128> a = SolidAngle<Float128>.FromSteradians((Float128)1);
        string formatted = a.ToString("sqdeg2", CultureInfo.InvariantCulture);

        // Just verify that the symbol came out right; the number is checked elsewhere.
        Assert.EndsWith(" deg²", formatted);
    }

    [Fact(DisplayName = "SolidAngle.ToString spats should render full sphere correctly")]
    public void SolidAngleToStringSpatsShouldRenderFullSphereCorrectly()
    {
        // A full sphere is 4π sr = 1 spat exactly.
        SolidAngle<Float128> sphere = SolidAngle<Float128>.FromSpats((Float128)1);

        Assert.Equal("1.000 sp", sphere.ToString("sp3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "SolidAngle.ToString should honor custom culture separators")]
    public void SolidAngleToStringShouldHonorCustomCulture()
    {
        CultureInfo german = new("de-DE");
        SolidAngle<Float128> a = SolidAngle<Float128>.FromSteradians(Float128.Parse("1234.56"));

        string formatted = a.ToString("sr2", german);

        Assert.Equal("1.234,56 sr", formatted);
    }

    [Theory(DisplayName = "SolidAngle.ValueOf should return the value at the matching scale")]
    [InlineData("qsr")]
    [InlineData("rsr")]
    [InlineData("ysr")]
    [InlineData("zsr")]
    [InlineData("asr")]
    [InlineData("fsr")]
    [InlineData("psr")]
    [InlineData("nsr")]
    [InlineData("usr")]
    [InlineData("msr")]
    [InlineData("csr")]
    [InlineData("dsr")]
    [InlineData("sr")]
    [InlineData("dasr")]
    [InlineData("hsr")]
    [InlineData("ksr")]
    [InlineData("Msr")]
    [InlineData("Gsr")]
    [InlineData("Tsr")]
    [InlineData("Psr")]
    [InlineData("Esr")]
    [InlineData("Zsr")]
    [InlineData("Ysr")]
    [InlineData("Rsr")]
    [InlineData("Qsr")]
    [InlineData("sqdeg")]
    [InlineData("sqarcmin")]
    [InlineData("sqarcsec")]
    [InlineData("sp")]
    public void SolidAngleValueOfShouldReturnValueAtMatchingScale(string specifier)
    {
        // Given
        SolidAngle<Float128> a = SolidAngle<Float128>.FromSteradians(Float128.Parse("1234.567"));

        // When
        Float128 expected = specifier switch
        {
            "qsr" => a.QuectoSteradians,
            "rsr" => a.RontoSteradians,
            "ysr" => a.YoctoSteradians,
            "zsr" => a.ZeptoSteradians,
            "asr" => a.AttoSteradians,
            "fsr" => a.FemtoSteradians,
            "psr" => a.PicoSteradians,
            "nsr" => a.NanoSteradians,
            "usr" => a.MicroSteradians,
            "msr" => a.MilliSteradians,
            "csr" => a.CentiSteradians,
            "dsr" => a.DeciSteradians,
            "sr" => a.Steradians,
            "dasr" => a.DecaSteradians,
            "hsr" => a.HectoSteradians,
            "ksr" => a.KiloSteradians,
            "Msr" => a.MegaSteradians,
            "Gsr" => a.GigaSteradians,
            "Tsr" => a.TeraSteradians,
            "Psr" => a.PetaSteradians,
            "Esr" => a.ExaSteradians,
            "Zsr" => a.ZettaSteradians,
            "Ysr" => a.YottaSteradians,
            "Rsr" => a.RonnaSteradians,
            "Qsr" => a.QuettaSteradians,
            "sqdeg" => a.SquareDegrees,
            "sqarcmin" => a.SquareArcminutes,
            "sqarcsec" => a.SquareArcseconds,
            "sp" => a.Spats,
            _ => throw new InvalidOperationException($"Unhandled specifier: {specifier}")
        };

        // Then
        Assert.Equal(expected, a.ValueOf(specifier));
    }

    [Fact(DisplayName = "SolidAngle.ValueOf should throw on invalid specifier")]
    public void SolidAngleValueOfShouldThrowOnInvalidSpecifier()
    {
        // Given
        SolidAngle<Float128> a = SolidAngle<Float128>.FromSteradians(Float128.Parse("1"));

        // Then
        Assert.Throws<ArgumentException>(() => a.ValueOf("xx"));
    }
}
