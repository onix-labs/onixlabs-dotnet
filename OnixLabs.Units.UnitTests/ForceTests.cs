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
        Assert.Equal(0.0, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromYoctoNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void ForceFromYoctoNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromYoctoNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromZeptoNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void ForceFromZeptoNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromZeptoNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromAttoNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void ForceFromAttoNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromAttoNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromFemtoNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void ForceFromFemtoNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromFemtoNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromPicoNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void ForceFromPicoNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromPicoNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromNanoNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void ForceFromNanoNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromNanoNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromMicroNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void ForceFromMicroNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromMicroNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromMilliNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void ForceFromMilliNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromMilliNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void ForceFromNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromKiloNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void ForceFromKiloNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromKiloNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromMegaNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void ForceFromMegaNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromMegaNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromGigaNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void ForceFromGigaNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromGigaNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromTeraNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void ForceFromTeraNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromTeraNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromPetaNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void ForceFromPetaNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromPetaNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromExaNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void ForceFromExaNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromExaNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromZettaNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void ForceFromZettaNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromZettaNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromYottaNewtons should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void ForceFromYottaNewtonsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromYottaNewtons(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromDynes should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e19)]
    [InlineData(2.5, 2.5e19)]
    public void ForceFromDynesShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromDynes(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromKilogramForce should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.80665e24)]
    [InlineData(2.5, 2.4516625e25)]
    public void ForceFromKilogramForceShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromKilogramForce(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromGramForce should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.80665e21)]
    [InlineData(2.5, 2.4516625e22)]
    public void ForceFromGramForceShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromGramForce(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromTonneForce should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.80665e27)]
    [InlineData(2.5, 2.4516625e28)]
    public void ForceFromTonneForceShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromTonneForce(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromPoundForce should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.4482216152605e24)]
    [InlineData(2.5, 1.112055403815125e25)]
    public void ForceFromPoundForceShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromPoundForce(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromOunceForce should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.780138850953781e23)]
    [InlineData(2.5, 6.950347127384452e23)]
    public void ForceFromOunceForceShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromOunceForce(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromPoundals should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.38255e23)]
    [InlineData(2.5, 3.456375e23)]
    public void ForceFromPoundalsShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromPoundals(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromShortTonForce should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 8.896443230521e27)]
    [InlineData(2.5, 2.22411080763025e28)]
    public void ForceFromShortTonForceShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromShortTonForce(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Theory(DisplayName = "Force.FromLongTonForce should produce the expected YoctoNewtons")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.964016418183519e27)]
    [InlineData(2.5, 2.4910041045458797e28)]
    public void ForceFromLongTonForceShouldProduceExpectedYoctoNewtons(double value, double expectedYoctoNewtons)
    {
        // Given / When
        Force<double> force = Force<double>.FromLongTonForce(value);

        // Then
        Assert.Equal(expectedYoctoNewtons, force.YoctoNewtons, Tolerance);
    }

    [Fact(DisplayName = "Force.Add should produce the expected result")]
    public void ForceAddShouldProduceExpectedValue()
    {
        // Given
        Force<double> left = Force<double>.FromNewtons(1500.0);
        Force<double> right = Force<double>.FromNewtons(500.0);

        // When
        Force<double> result = left.Add(right);

        // Then
        Assert.Equal(2000.0, result.Newtons, Tolerance);
    }

    [Fact(DisplayName = "Force.Subtract should produce the expected result")]
    public void ForceSubtractShouldProduceExpectedValue()
    {
        // Given
        Force<double> left = Force<double>.FromNewtons(1500.0);
        Force<double> right = Force<double>.FromNewtons(400.0);

        // When
        Force<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1100.0, result.Newtons, Tolerance);
    }

    [Fact(DisplayName = "Force.Multiply should produce the expected result")]
    public void ForceMultiplyShouldProduceExpectedValue()
    {
        // Given
        Force<double> left = Force<double>.FromNewtons(10.0); // 1e25 yN
        Force<double> right = Force<double>.FromNewtons(3.0); // 3e24 yN

        // When
        Force<double> result = left.Multiply(right); // 1e25 * 3e24 = 3e49 yN

        // Then
        Assert.Equal(1e25, left.YoctoNewtons, Tolerance);
        Assert.Equal(3e24, right.YoctoNewtons, Tolerance);
        Assert.Equal(3e49, result.YoctoNewtons, Tolerance);
        Assert.Equal(3e25, result.Newtons, Tolerance);
    }

    [Fact(DisplayName = "Force.Divide should produce the expected result")]
    public void ForceDivideShouldProduceExpectedValue()
    {
        // Given
        Force<double> left = Force<double>.FromNewtons(100.0); // 1e26 yN
        Force<double> right = Force<double>.FromNewtons(20.0); // 2e25 yN

        // When
        Force<double> result = left.Divide(right); // 1e26 / 2e25 = 5 yN

        // Then
        Assert.Equal(5.0, result.YoctoNewtons, Tolerance);
        Assert.Equal(5e-24, result.Newtons, Tolerance);
    }

    [Fact(DisplayName = "Force comparison should produce the expected result (left equal to right)")]
    public void ForceComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Force<double> left = Force<double>.FromNewtons(1234.0);
        Force<double> right = Force<double>.FromNewtons(1234.0);

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
        Force<double> left = Force<double>.FromNewtons(4567.0);
        Force<double> right = Force<double>.FromNewtons(1234.0);

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
        Force<double> left = Force<double>.FromNewtons(1234.0);
        Force<double> right = Force<double>.FromNewtons(4567.0);

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
        Force<BigDecimal> left = Force<BigDecimal>.FromKiloNewtons(2.0);
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
        Force<double> left = Force<double>.FromKiloNewtons(2.0);
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
        Force<double> force = Force<double>.FromNewtons(1000.0);

        // When / Then
        Assert.Equal("1,000.000 N", $"{force:N3}");
        Assert.Equal("1.000 kN", $"{force:kN3}");
        Assert.Equal("0.001 MN", $"{force:MN3}");
        Assert.Equal("100,000,000.000 dyn", $"{force:dyn3}");
        Assert.Equal("101.972 kgf", $"{force:kgf3}");
        Assert.Equal("101,971.621 gf", $"{force:gf3}");
        Assert.Equal("224.809 lbf", $"{force:lbf3}");
        Assert.Equal("3,596.943 ozf", $"{force:ozf3}");
    }

    [Fact(DisplayName = "Force.ToString should honor custom culture separators")]
    public void ForceToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Force<double> force = Force<double>.FromNewtons(1234.56);

        // When
        string formatted = force.ToString("N2", customCulture);

        // Then
        // German uses '.' for thousands and ',' for decimals.
        Assert.Equal("1.234,56 N", formatted);
    }
}
