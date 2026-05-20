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

    [Theory(DisplayName = "SolidAngle.FromMillisteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e27")]
    [InlineData("2.5", "2.5e27")]
    public void SolidAngleFromMillisteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromMillisteradians(Float128.Parse(value));
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

    [Theory(DisplayName = "SolidAngle.FromKilosteradians should produce the expected QuectoSteradians")]
    [InlineData("0", "0")]
    [InlineData("1", "1e33")]
    [InlineData("2.5", "2.5e33")]
    public void SolidAngleFromKilosteradiansShouldProduceExpectedQuectoSteradians(string value, string expected)
    {
        SolidAngle<Float128> a = SolidAngle<Float128>.FromKilosteradians(Float128.Parse(value));
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
}
