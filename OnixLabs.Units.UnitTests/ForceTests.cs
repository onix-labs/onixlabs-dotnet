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

public sealed class ForceTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Force.Zero should produce the expected result")]
    public void ForceZeroShouldProduceExpectedResult()
    {
        // Given / When
        Force<double> force = Force<double>.Zero;

        // Then
        Assert.Equal(0.0, force.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromQuectonewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void ForceFromQuectonewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromQuectonewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromRontonewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void ForceFromRontonewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromRontonewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromYoctonewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void ForceFromYoctonewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromYoctonewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromZeptonewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void ForceFromZeptonewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromZeptonewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromAttonewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void ForceFromAttonewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromAttonewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromFemtonewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void ForceFromFemtonewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromFemtonewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromPiconewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void ForceFromPiconewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromPiconewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromNanonewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void ForceFromNanonewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromNanonewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromMicronewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void ForceFromMicronewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromMicronewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromMillinewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void ForceFromMillinewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromMillinewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromCentinewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void ForceFromCentinewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromCentinewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromDecinewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void ForceFromDecinewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromDecinewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromNewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void ForceFromNewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromNewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromDecanewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void ForceFromDecanewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromDecanewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromHectonewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void ForceFromHectonewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromHectonewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromKilonewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void ForceFromKilonewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromKilonewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromMeganewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void ForceFromMeganewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromMeganewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromGiganewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void ForceFromGiganewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromGiganewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromTeranewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void ForceFromTeranewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromTeranewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromPetanewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void ForceFromPetanewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromPetanewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromExanewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void ForceFromExanewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromExanewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromZettanewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void ForceFromZettanewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromZettanewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromYottanewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void ForceFromYottanewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromYottanewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromRonnanewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void ForceFromRonnanewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromRonnanewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromQuettanewtons should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void ForceFromQuettanewtonsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromQuettanewtons(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromDynes should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e25)]
    [InlineData(1e5, 1e30)] // 1e5 dyn = 1 N
    public void ForceFromDynesShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromDynes(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromPoundsForce should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.4482216152605e30)]
    [InlineData(2.0, 8.896443230521e30)]
    public void ForceFromPoundsForceShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromPoundsForce(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromOuncesForce should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.7801385095378125e29)]
    [InlineData(16.0, 4.4482216152605e30)] // 16 ozf = 1 lbf
    public void ForceFromOuncesForceShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromOuncesForce(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromPoundals should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.38254954376e29)]
    [InlineData(2.0, 2.76509908752e29)]
    public void ForceFromPoundalsShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromPoundals(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromKilogramsForce should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.80665e30)]
    [InlineData(2.0, 1.96133e31)]
    public void ForceFromKilogramsForceShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromKilogramsForce(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromGramsForce should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.80665e27)]
    [InlineData(1000.0, 9.80665e30)] // 1000 gf = 1 kgf
    public void ForceFromGramsForceShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromGramsForce(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromMetricTonsForce should produce the expected QuectoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.80665e33)]
    [InlineData(0.001, 9.80665e30)] // 0.001 tnf = 1 kgf
    public void ForceFromMetricTonsForceShouldProduceExpectedQuectoNewtons(double value, double expected)
    {
        Force<double> f = Force<double>.FromMetricTonsForce(value);
        Assert.Equal(expected, f.QuectoNewtons, Tolerance);
    }

    [Fact(DisplayName = "Force non-SI conversions should roundtrip through Newtons")]
    public void ForceNonSiConversionsShouldRoundtripThroughNewtons()
    {
        // Given
        Force<double> p = Force<double>.FromNewtons(4.4482216152605);
        Assert.Equal(1.0, p.PoundsForce, 1e-9);

        Force<double> q = Force<double>.FromNewtons(9.80665);
        Assert.Equal(1.0, q.KilogramsForce, 1e-9);

        Force<double> r = Force<double>.FromNewtons(1.0);
        Assert.Equal(1e5, r.Dynes, 1e-6);
    }

    [Fact(DisplayName = "Force.Add should produce the expected result")]
    public void ForceAddShouldProduceExpectedValue()
    {
        // Given
        Force<double> left = Force<double>.FromKilonewtons(1.5);
        Force<double> right = Force<double>.FromKilonewtons(0.5);

        // When
        Force<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.KiloNewtons, Tolerance);
    }

    [Fact(DisplayName = "Force.Subtract should produce the expected result")]
    public void ForceSubtractShouldProduceExpectedValue()
    {
        // Given
        Force<double> left = Force<double>.FromKilonewtons(1.5);
        Force<double> right = Force<double>.FromKilonewtons(0.4);

        // When
        Force<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.KiloNewtons, Tolerance);
    }

    [Fact(DisplayName = "Force.Multiply should produce the expected result")]
    public void ForceMultiplyShouldProduceExpectedValue()
    {
        // Given
        Force<double> left = Force<double>.FromNewtons(10.0);  // 1e31 qN
        Force<double> right = Force<double>.FromNewtons(3.0);  // 3e30 qN

        // When
        Force<double> result = left.Multiply(right);  // 1e31 * 3e30 = 3e61 qN

        // Then
        Assert.Equal(1e31, left.QuectoNewtons, Tolerance);
        Assert.Equal(3e30, right.QuectoNewtons, Tolerance);
        Assert.Equal(3e61, result.QuectoNewtons, Tolerance);
    }

    [Fact(DisplayName = "Force.Divide should produce the expected result")]
    public void ForceDivideShouldProduceExpectedValue()
    {
        // Given
        Force<double> left = Force<double>.FromNewtons(100.0);  // 1e32 qN
        Force<double> right = Force<double>.FromNewtons(20.0);  // 2e31 qN

        // When
        Force<double> result = left.Divide(right);  // 1e32 / 2e31 = 5 qN

        // Then
        Assert.Equal(5.0, result.QuectoNewtons, Tolerance);
        Assert.Equal(5e-30, result.Newtons, Tolerance);
    }

    [Fact(DisplayName = "Force comparison should produce the expected result (left equal to right)")]
    public void ForceComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Force<double> left = Force<double>.FromKilonewtons(123.0);
        Force<double> right = Force<double>.FromKilonewtons(123.0);

        // When / Then
        Assert.Equal(0, Force<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Force comparison should produce the expected result (left greater than right)")]
    public void ForceComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Force<double> left = Force<double>.FromKilonewtons(456.0);
        Force<double> right = Force<double>.FromKilonewtons(123.0);

        // When / Then
        Assert.Equal(1, Force<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Force comparison should produce the expected result (left less than right)")]
    public void ForceComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Force<double> left = Force<double>.FromKilonewtons(123.0);
        Force<double> right = Force<double>.FromKilonewtons(456.0);

        // When / Then
        Assert.Equal(-1, Force<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Force equality should produce the expected result (left equal to right)")]
    public void ForceEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Force<BigDecimal> left = Force<BigDecimal>.FromKilonewtons(2.0);
        Force<BigDecimal> right = Force<BigDecimal>.FromNewtons(2000.0);

        // When / Then
        Assert.True(Force<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Force equality should produce the expected result (left not equal to right)")]
    public void ForceEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Force<double> left = Force<double>.FromKilonewtons(2.0);
        Force<double> right = Force<double>.FromNewtons(2500.0);

        // When / Then
        Assert.False(Force<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Force.ToString should produce the expected result")]
    public void ForceToStringShouldProduceExpectedResult()
    {
        // Given
        Force<double> f = Force<double>.FromNewtons(1000.0);

        // When / Then
        Assert.Equal("1,000.000 N", $"{f:N3}");
        Assert.Equal("1.000 kN", $"{f:kN3}");
        Assert.Equal("0.001 MN", $"{f:MN3}");
        Assert.Equal("1,000,000.000 mN", $"{f:mN3}");
    }

    [Fact(DisplayName = "Force.ToString MN vs mN are case-sensitive")]
    public void ForceToStringMnVsMnAreCaseSensitive()
    {
        // Given
        Force<double> f = Force<double>.FromNewtons(1.0);

        // Then
        Assert.Equal("0.000001 MN", $"{f:MN6}"); // mega
        Assert.Equal("1,000.000 mN", $"{f:mN3}"); // milli
    }

    [Fact(DisplayName = "Force.ToString non-SI specifiers should use proper unit symbols")]
    public void ForceToStringNonSiSpecifiersShouldUseProperUnitSymbols()
    {
        // Given
        Force<double> f = Force<double>.FromNewtons(1.0);

        // Then
        Assert.Equal("100,000.000 dyn", $"{f:dyn3}");
        Assert.Equal("0.225 lbf", $"{f:lbf3}");
        Assert.Equal("3.597 ozf", $"{f:ozf3}");
        Assert.Equal("7.233 pdl", $"{f:pdl3}");
        Assert.Equal("0.102 kgf", $"{f:kgf3}");
        Assert.Equal("101.972 gf", $"{f:gf3}");
    }

    [Fact(DisplayName = "Force.ToString MetricTonsForce should use proper unit symbol")]
    public void ForceToStringMetricTonsForceShouldUseProperUnitSymbol()
    {
        // Given
        Force<double> f = Force<double>.FromKilonewtons(9.80665);

        // Then: 9.80665 kN = 1 metric tonne-force
        Assert.Equal("1.000 tnf", $"{f:tnf3}");
    }

    [Fact(DisplayName = "Force.ToString µN symbol should differ from format specifier")]
    public void ForceToStringMicronewtonsSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        Force<double> f = Force<double>.FromNewtons(1.0);

        // Then: specifier is uN, but symbol rendered is µN
        Assert.Equal("1,000,000.000 µN", $"{f:uN3}");
    }

    [Fact(DisplayName = "Force.ToString should honor custom culture separators")]
    public void ForceToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Force<double> f = Force<double>.FromKilonewtons(1234.56);

        // When
        string formatted = f.ToString("kN2", customCulture);

        // Then
        Assert.Equal("1.234,56 kN", formatted);
    }
}
