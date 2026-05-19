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

public sealed class DensityTests
{
    [Fact(DisplayName = "Density should preserve its underlying Mass and Volume components")]
    public void DensityShouldPreserveUnderlyingComponents()
    {
        // Given
        Mass<Float128> mass = Mass<Float128>.FromKilograms((Float128)1000);
        Volume<Float128> volume = Volume<Float128>.FromCubicMeters((Float128)1);

        // When
        Density<Float128> density = new(mass, volume);

        // Then
        Assert.Equal(mass, density.Left);
        Assert.Equal(volume, density.Right);
    }

    [Fact(DisplayName = "Density.Zero should produce zero magnitude (avoids 0/0 NaN)")]
    public void DensityZeroShouldProduceZeroMagnitude()
    {
        // Given / When — Zero stores Mass.Zero over Volume.FromCubicMeters(1), so the magnitude is
        // a genuine 0 and not NaN from a 0/0 ratio.
        Density<Float128> zero = Density<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, zero.Magnitude);
        Assert.True(zero.Equals(zero));
    }

    [Fact(DisplayName = "Density.Add should produce the expected result")]
    public void DensityAddShouldProduceExpectedValue()
    {
        // Given — 1000 kg/m³ + 500 kg/m³ = 1500 kg/m³
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)500), Volume<Float128>.FromCubicMeters((Float128)1));

        // When
        Density<Float128> result = Density<Float128>.Add(left, right);

        // Then — compare against the canonical-equivalent 1500 kg/m³ density.
        Density<Float128> expected = new(Mass<Float128>.FromKilograms((Float128)1500), Volume<Float128>.FromCubicMeters((Float128)1));
        Assert.Equal(expected.Magnitude, result.Magnitude);
    }

    [Fact(DisplayName = "Density.Add should reduce magnitudes across decompositions")]
    public void DensityAddShouldReduceMagnitudes()
    {
        // Given — (2000 kg / 2 m³) = 1000 kg/m³, plus (500 kg / 1 m³) = 500 kg/m³, total = 1500 kg/m³.
        Density<Float128> oneThousand = new(Mass<Float128>.FromKilograms((Float128)2000), Volume<Float128>.FromCubicMeters((Float128)2));
        Density<Float128> fiveHundred = new(Mass<Float128>.FromKilograms((Float128)500), Volume<Float128>.FromCubicMeters((Float128)1));

        // When
        Density<Float128> result = Density<Float128>.Add(oneThousand, fiveHundred);

        // Then
        Density<Float128> expected = new(Mass<Float128>.FromKilograms((Float128)1500), Volume<Float128>.FromCubicMeters((Float128)1));
        Assert.Equal(expected.Magnitude, result.Magnitude);
    }

    [Fact(DisplayName = "Density.Add should reduce across mixed mass/volume units")]
    public void DensityAddShouldReduceAcrossMixedUnits()
    {
        // Given — 1 t/m³ = 1000 kg/m³ exactly at Float128 (Mass.FromTonnes(1) = Pow10(36) qg and
        // Mass.FromKilograms(1000) = 1000 × Pow10(33) = Pow10(36) qg both reduce to the same
        // canonical), plus 500 kg/m³ = 1500 kg/m³ total. Replaces the original BigDecimal-based
        // test with Float128 strict equality.
        Density<Float128> left = new(Mass<Float128>.FromTonnes((Float128)1), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)500), Volume<Float128>.FromCubicMeters((Float128)1));

        // When
        Density<Float128> result = Density<Float128>.Add(left, right);

        // Then
        Density<Float128> expected = new(Mass<Float128>.FromKilograms((Float128)1500), Volume<Float128>.FromCubicMeters((Float128)1));
        Assert.Equal(expected.Magnitude, result.Magnitude);
    }

    [Fact(DisplayName = "Density.Add with Zero should return an equal-magnitude density")]
    public void DensityAddWithZeroShouldReturnSameMagnitude()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // When
        Density<Float128> result = Density<Float128>.Add(density, Density<Float128>.Zero);

        // Then
        Assert.Equal(density, result);
    }

    [Fact(DisplayName = "Density.Subtract should produce the expected result")]
    public void DensitySubtractShouldProduceExpectedValue()
    {
        // Given — 1500 kg/m³ - 500 kg/m³ = 1000 kg/m³
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)1500), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)500), Volume<Float128>.FromCubicMeters((Float128)1));

        // When
        Density<Float128> result = Density<Float128>.Subtract(left, right);

        // Then
        Density<Float128> expected = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));
        AssertMagnitudeNearlyEqual(expected.Magnitude, result.Magnitude);
    }

    [Fact(DisplayName = "Density.Subtract should reduce magnitudes across decompositions")]
    public void DensitySubtractShouldReduceMagnitudes()
    {
        // Given — (3000 kg / 2 m³) = 1500 kg/m³, minus (500 kg / 1 m³) = 500 kg/m³, result = 1000 kg/m³.
        Density<Float128> fifteenHundred = new(Mass<Float128>.FromKilograms((Float128)3000), Volume<Float128>.FromCubicMeters((Float128)2));
        Density<Float128> fiveHundred = new(Mass<Float128>.FromKilograms((Float128)500), Volume<Float128>.FromCubicMeters((Float128)1));

        // When
        Density<Float128> result = Density<Float128>.Subtract(fifteenHundred, fiveHundred);

        // Then
        Density<Float128> expected = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));
        AssertMagnitudeNearlyEqual(expected.Magnitude, result.Magnitude);
    }

    [Fact(DisplayName = "Density.Subtract should produce a negative result when left is less than right")]
    public void DensitySubtractShouldProduceNegativeWhenLeftLessThanRight()
    {
        // Given — 500 kg/m³ - 1500 kg/m³ = -1000 kg/m³
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)500), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)1500), Volume<Float128>.FromCubicMeters((Float128)1));

        // When
        Density<Float128> result = Density<Float128>.Subtract(left, right);

        // Then
        Density<Float128> expectedPositive = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));
        AssertMagnitudeNearlyEqual(-expectedPositive.Magnitude, result.Magnitude);
    }

    [Fact(DisplayName = "Density + operator should produce the expected result")]
    public void DensityAddOperatorShouldProduceExpectedValue()
    {
        // Given
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)500), Volume<Float128>.FromCubicMeters((Float128)1));

        // When
        Density<Float128> result = left + right;

        // Then
        Density<Float128> expected = new(Mass<Float128>.FromKilograms((Float128)1500), Volume<Float128>.FromCubicMeters((Float128)1));
        Assert.Equal(expected.Magnitude, result.Magnitude);
    }

    [Fact(DisplayName = "Density - operator should produce the expected result")]
    public void DensitySubtractOperatorShouldProduceExpectedValue()
    {
        // Given
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)1500), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)500), Volume<Float128>.FromCubicMeters((Float128)1));

        // When
        Density<Float128> result = left - right;

        // Then
        Density<Float128> expected = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));
        AssertMagnitudeNearlyEqual(expected.Magnitude, result.Magnitude);
    }

    [Fact(DisplayName = "Density instance Add should produce the expected result")]
    public void DensityInstanceAddShouldProduceExpectedValue()
    {
        // Given
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)500), Volume<Float128>.FromCubicMeters((Float128)1));

        // When
        Density<Float128> result = left.Add(right);

        // Then
        Density<Float128> expected = new(Mass<Float128>.FromKilograms((Float128)1500), Volume<Float128>.FromCubicMeters((Float128)1));
        Assert.Equal(expected.Magnitude, result.Magnitude);
    }

    [Fact(DisplayName = "Density instance Subtract should produce the expected result")]
    public void DensityInstanceSubtractShouldProduceExpectedValue()
    {
        // Given
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)1500), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)500), Volume<Float128>.FromCubicMeters((Float128)1));

        // When
        Density<Float128> result = left.Subtract(right);

        // Then
        Density<Float128> expected = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));
        AssertMagnitudeNearlyEqual(expected.Magnitude, result.Magnitude);
    }

    [Fact(DisplayName = "Density Add and Subtract should agree across static / operator / instance forms")]
    public void DensityAddAndSubtractShouldAgreeAcrossForms()
    {
        // Given
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)1500), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)500), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Equal(Density<Float128>.Add(left, right), left + right);
        Assert.Equal(Density<Float128>.Add(left, right), left.Add(right));
        Assert.Equal(Density<Float128>.Subtract(left, right), left - right);
        Assert.Equal(Density<Float128>.Subtract(left, right), left.Subtract(right));
    }

    [Fact(DisplayName = "Density equality should be by magnitude (identical components)")]
    public void DensityEqualityShouldBeByMagnitudeIdenticalComponents()
    {
        // Given
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Density equality should be by magnitude (proportional components)")]
    public void DensityEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // Given — 1000 kg/m³ in two different decompositions
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)2000), Volume<Float128>.FromCubicMeters((Float128)2));

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Density equality should be by magnitude (cross-unit conversion)")]
    public void DensityEqualityShouldBeByMagnitudeCrossUnit()
    {
        // Given — 1 t/m³ vs 1000 kg/m³ reduce to the same Float128 canonical because both Mass
        // conversion factors are pure integer powers of ten exactly representable in Float128's
        // 113-bit mantissa (Pow10(36) directly vs 1000 × Pow10(33) = Pow10(36)). Replaces the
        // original BigDecimal-based test with Float128 strict equality.
        Density<Float128> left = new(Mass<Float128>.FromTonnes((Float128)1), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Density inequality should hold for different magnitudes")]
    public void DensityInequalityShouldHoldForDifferentMagnitudes()
    {
        // Given
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)2));

        // Then
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
    }

    [Fact(DisplayName = "Density.Equals(null) should return false")]
    public void DensityEqualsNullShouldReturnFalse()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.False(density.Equals(null));
        Assert.False(density.Equals((object?)null));
    }

    [Fact(DisplayName = "Density.Equals with different type should return false")]
    public void DensityEqualsWithDifferentTypeShouldReturnFalse()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.False(density.Equals("not a density"));
    }

    [Fact(DisplayName = "Density.CompareTo should produce the expected result (left equal to right)")]
    public void DensityCompareToShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)2000), Volume<Float128>.FromCubicMeters((Float128)2));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Density.CompareTo should produce the expected result (left greater than right)")]
    public void DensityCompareToShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)2000), Volume<Float128>.FromCubicMeters((Float128)1));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Density.CompareTo should produce the expected result (left less than right)")]
    public void DensityCompareToShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Density<Float128> left = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)2));
        Density<Float128> right = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Density.CompareTo(null) should return 1")]
    public void DensityCompareToNullShouldReturnOne()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Equal(1, density.CompareTo(null));
        Assert.Equal(1, density.CompareTo((object?)null));
    }

    [Fact(DisplayName = "Density.CompareTo with incompatible type should throw")]
    public void DensityCompareToWithIncompatibleTypeShouldThrow()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Throws<ArgumentException>(() => density.CompareTo("not a density"));
    }

    [Fact(DisplayName = "Density.ToString with kg/cum:3 should produce the expected result")]
    public void DensityToStringWithKgPerCubicMeterScale3ShouldProduceExpectedResult()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Equal("1,000.000 kg/cum", density.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString should reduce magnitude (2000 kg / 2 m³ equals 1000 kg/m³)")]
    public void DensityToStringShouldReduceMagnitude()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)2000), Volume<Float128>.FromCubicMeters((Float128)2));

        // Then
        Assert.Equal("1,000.000 kg/cum", density.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString with kg/cum:0 should produce the expected result")]
    public void DensityToStringWithKgPerCubicMeterScale0ShouldProduceExpectedResult()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Equal("1,000 kg/cum", density.ToString("kg/cum:0", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString with g/L should produce the expected result")]
    public void DensityToStringWithGramsPerLiterShouldProduceExpectedResult()
    {
        // Given — 1 g/mL = 1 g per 0.001 L = 1000 g/L
        Density<Float128> density = new(Mass<Float128>.FromGrams((Float128)1), Volume<Float128>.FromMilliliters((Float128)1));

        // Then
        Assert.Equal("1,000.000 g/L", density.ToString("g/L:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString with lb/cuft should produce the expected result")]
    public void DensityToStringWithPoundsPerCubicFootShouldProduceExpectedResult()
    {
        // Given — water density: 1000 kg/m³ ≈ 62.428 lb/ft³
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Equal("62.428 lb/cuft", density.ToString("lb/cuft:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density default ToString should use kg/cum")]
    public void DensityDefaultToStringShouldUseKilogramsPerCubicMeter()
    {
        // Given — 1 g/mL = 1000 kg/m³
        Density<Float128> density = new(Mass<Float128>.FromGrams((Float128)1), Volume<Float128>.FromMilliliters((Float128)1));

        // When
        string result = density.ToString();

        // Then
        Assert.EndsWith(" kg/cum", result);
    }

    [Fact(DisplayName = "Density.ToString should honor custom culture separators")]
    public void DensityToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Density<Float128> density = new(Mass<Float128>.FromKilograms(Float128.Parse("1234.56")), Volume<Float128>.FromCubicMeters((Float128)1));

        // When
        string formatted = density.ToString("kg/cum:2", customCulture);

        // Then
        Assert.Equal("1.234,56 kg/cum", formatted);
    }

    [Fact(DisplayName = "Density.ToString should support span-interpolation format")]
    public void DensityToStringShouldSupportSpanInterpolation()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then — magnitude formatted via current culture's default scale, then unit suffix.
        Assert.Equal($"{((Float128)1000).ToString("N", CultureInfo.CurrentCulture)} kg/cum", $"{density:kg/cum}");
    }

    [Fact(DisplayName = "Density.ToString should throw when format has no slash separator")]
    public void DensityToStringShouldThrowWhenFormatHasNoSlash()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Throws<FormatException>(() => density.ToString("kgcum", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString should throw when mass specifier is empty")]
    public void DensityToStringShouldThrowWhenMassSpecifierIsEmpty()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Throws<FormatException>(() => density.ToString("/cum", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString should throw when volume specifier is empty")]
    public void DensityToStringShouldThrowWhenVolumeSpecifierIsEmpty()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Throws<FormatException>(() => density.ToString("kg/", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString should throw when scale is not numeric")]
    public void DensityToStringShouldThrowWhenScaleNotNumeric()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Throws<FormatException>(() => density.ToString("kg/cum:abc", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString should throw when mass specifier is invalid")]
    public void DensityToStringShouldThrowWhenMassSpecifierIsInvalid()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Throws<ArgumentException>(() => density.ToString("xx/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString should throw when volume specifier is invalid")]
    public void DensityToStringShouldThrowWhenVolumeSpecifierIsInvalid()
    {
        // Given
        Density<Float128> density = new(Mass<Float128>.FromKilograms((Float128)1000), Volume<Float128>.FromCubicMeters((Float128)1));

        // Then
        Assert.Throws<ArgumentException>(() => density.ToString("kg/xx:3", CultureInfo.InvariantCulture));
    }

    // Density.Magnitude is canonical_mass (qg) / canonical_volume (cuqm). For typical SI densities
    // around 1000 kg/m³ that ratio is ~1e-54 — far outside Float128's exact-power-of-10 range, so
    // the division rounds. Add chains the rounding additively; Subtract exposes ~1-3 ULP residual
    // via catastrophic cancellation. We assert relative equality at 1e-30, ~1e4 ULPs of Float128's
    // ~2e-34 unit roundoff, still ~26 orders of magnitude tighter than the old `double` test had.
    private static void AssertMagnitudeNearlyEqual(Float128 expected, Float128 actual)
    {
        Float128 diff = expected > actual ? expected - actual : actual - expected;
        Float128 reference = expected < Float128.Zero ? -expected : expected;
        if (reference == Float128.Zero)
        {
            Assert.Equal(Float128.Zero, actual);
            return;
        }
        Float128 relative = diff / reference;
        Assert.True(relative < Float128.Parse("1e-30"),
            $"Expected {expected}, got {actual} (relative error {relative} exceeds 1e-30)");
    }
}
