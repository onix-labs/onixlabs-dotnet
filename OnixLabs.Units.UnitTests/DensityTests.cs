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

namespace OnixLabs.Units.UnitTests;

public sealed class DensityTests
{
    [Fact(DisplayName = "Density should preserve its underlying Mass and Volume components")]
    public void DensityShouldPreserveUnderlyingComponents()
    {
        // Given
        Mass<double> mass = Mass<double>.FromKilograms(1000);
        Volume<double> volume = Volume<double>.FromCubicMeters(1);

        // When
        Density<double> density = new(mass, volume);

        // Then
        Assert.Equal(mass, density.Left);
        Assert.Equal(volume, density.Right);
    }

    [Fact(DisplayName = "Density.Zero should produce the expected result")]
    public void DensityZeroShouldProduceExpectedResult()
    {
        // Given / When
        Density<double> zero = Density<double>.Zero;

        // Then - magnitude must be 0 (avoids 0/0 NaN by using Volume.FromCubicMeters(1) for the denominator).
        Assert.Equal("0.000 kg/cum", zero.ToString("kg/cum:3", CultureInfo.InvariantCulture));
        Assert.True(zero.Equals(zero));
    }

    [Fact(DisplayName = "Density.Add should produce the expected result")]
    public void DensityAddShouldProduceExpectedValue()
    {
        // Given - 1000 kg/m³ + 500 kg/m³ = 1500 kg/m³
        Density<double> left = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));
        Density<double> right = new(Mass<double>.FromKilograms(500), Volume<double>.FromCubicMeters(1));

        // When
        Density<double> result = Density<double>.Add(left, right);

        // Then
        Assert.Equal("1,500.000 kg/cum", result.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.Add should reduce magnitudes across decompositions")]
    public void DensityAddShouldReduceMagnitudes()
    {
        // Given - (2000 kg / 2 m³) = 1000 kg/m³, plus (500 kg / 1 m³) = 500 kg/m³, total = 1500 kg/m³.
        Density<double> oneThousand = new(Mass<double>.FromKilograms(2000), Volume<double>.FromCubicMeters(2));
        Density<double> fiveHundred = new(Mass<double>.FromKilograms(500), Volume<double>.FromCubicMeters(1));

        // When
        Density<double> result = Density<double>.Add(oneThousand, fiveHundred);

        // Then
        Assert.Equal("1,500.000 kg/cum", result.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.Add should reduce across mixed mass/volume units")]
    public void DensityAddShouldReduceAcrossMixedUnits()
    {
        // Given - 1 t/m³ = 1000 kg/m³, plus 500 kg/m³ = 1500 kg/m³.
        Density<double> left = new(Mass<double>.FromTonnes(1), Volume<double>.FromCubicMeters(1));
        Density<double> right = new(Mass<double>.FromKilograms(500), Volume<double>.FromCubicMeters(1));

        // When
        Density<double> result = Density<double>.Add(left, right);

        // Then
        Assert.Equal("1,500.000 kg/cum", result.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.Add with Zero should return an equal-magnitude density")]
    public void DensityAddWithZeroShouldReturnSameMagnitude()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // When
        Density<double> result = Density<double>.Add(density, Density<double>.Zero);

        // Then
        Assert.Equal(density, result);
    }

    [Fact(DisplayName = "Density.Subtract should produce the expected result")]
    public void DensitySubtractShouldProduceExpectedValue()
    {
        // Given - 1500 kg/m³ - 500 kg/m³ = 1000 kg/m³
        Density<double> left = new(Mass<double>.FromKilograms(1500), Volume<double>.FromCubicMeters(1));
        Density<double> right = new(Mass<double>.FromKilograms(500), Volume<double>.FromCubicMeters(1));

        // When
        Density<double> result = Density<double>.Subtract(left, right);

        // Then
        Assert.Equal("1,000.000 kg/cum", result.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.Subtract should reduce magnitudes across decompositions")]
    public void DensitySubtractShouldReduceMagnitudes()
    {
        // Given - (3000 kg / 2 m³) = 1500 kg/m³, minus (500 kg / 1 m³) = 500 kg/m³, result = 1000 kg/m³.
        Density<double> fifteenHundred = new(Mass<double>.FromKilograms(3000), Volume<double>.FromCubicMeters(2));
        Density<double> fiveHundred = new(Mass<double>.FromKilograms(500), Volume<double>.FromCubicMeters(1));

        // When
        Density<double> result = Density<double>.Subtract(fifteenHundred, fiveHundred);

        // Then
        Assert.Equal("1,000.000 kg/cum", result.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.Subtract should produce a negative result when left is less than right")]
    public void DensitySubtractShouldProduceNegativeWhenLeftLessThanRight()
    {
        // Given - 500 kg/m³ - 1500 kg/m³ = -1000 kg/m³
        Density<double> left = new(Mass<double>.FromKilograms(500), Volume<double>.FromCubicMeters(1));
        Density<double> right = new(Mass<double>.FromKilograms(1500), Volume<double>.FromCubicMeters(1));

        // When
        Density<double> result = Density<double>.Subtract(left, right);

        // Then
        Assert.Equal("-1,000.000 kg/cum", result.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density + operator should produce the expected result")]
    public void DensityAddOperatorShouldProduceExpectedValue()
    {
        // Given
        Density<double> left = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));
        Density<double> right = new(Mass<double>.FromKilograms(500), Volume<double>.FromCubicMeters(1));

        // When
        Density<double> result = left + right;

        // Then
        Assert.Equal("1,500.000 kg/cum", result.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density - operator should produce the expected result")]
    public void DensitySubtractOperatorShouldProduceExpectedValue()
    {
        // Given
        Density<double> left = new(Mass<double>.FromKilograms(1500), Volume<double>.FromCubicMeters(1));
        Density<double> right = new(Mass<double>.FromKilograms(500), Volume<double>.FromCubicMeters(1));

        // When
        Density<double> result = left - right;

        // Then
        Assert.Equal("1,000.000 kg/cum", result.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density instance Add should produce the expected result")]
    public void DensityInstanceAddShouldProduceExpectedValue()
    {
        // Given
        Density<double> left = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));
        Density<double> right = new(Mass<double>.FromKilograms(500), Volume<double>.FromCubicMeters(1));

        // When
        Density<double> result = left.Add(right);

        // Then
        Assert.Equal("1,500.000 kg/cum", result.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density instance Subtract should produce the expected result")]
    public void DensityInstanceSubtractShouldProduceExpectedValue()
    {
        // Given
        Density<double> left = new(Mass<double>.FromKilograms(1500), Volume<double>.FromCubicMeters(1));
        Density<double> right = new(Mass<double>.FromKilograms(500), Volume<double>.FromCubicMeters(1));

        // When
        Density<double> result = left.Subtract(right);

        // Then
        Assert.Equal("1,000.000 kg/cum", result.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density Add and Subtract should agree across static / operator / instance forms")]
    public void DensityAddAndSubtractShouldAgreeAcrossForms()
    {
        // Given
        Density<double> left = new(Mass<double>.FromKilograms(1500), Volume<double>.FromCubicMeters(1));
        Density<double> right = new(Mass<double>.FromKilograms(500), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Equal(Density<double>.Add(left, right), left + right);
        Assert.Equal(Density<double>.Add(left, right), left.Add(right));
        Assert.Equal(Density<double>.Subtract(left, right), left - right);
        Assert.Equal(Density<double>.Subtract(left, right), left.Subtract(right));
    }

    [Fact(DisplayName = "Density equality should be by magnitude (identical components)")]
    public void DensityEqualityShouldBeByMagnitudeIdenticalComponents()
    {
        // Given
        Density<double> left = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));
        Density<double> right = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Density equality should be by magnitude (proportional components)")]
    public void DensityEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // Given - 1000 kg/m³ in two different decompositions
        Density<double> left = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));
        Density<double> right = new(Mass<double>.FromKilograms(2000), Volume<double>.FromCubicMeters(2));

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Density inequality should hold for different magnitudes")]
    public void DensityInequalityShouldHoldForDifferentMagnitudes()
    {
        // Given
        Density<double> left = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));
        Density<double> right = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(2));

        // Then
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
    }

    [Fact(DisplayName = "Density.Equals(null) should return false")]
    public void DensityEqualsNullShouldReturnFalse()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.False(density.Equals(null));
        Assert.False(density.Equals((object?)null));
    }

    [Fact(DisplayName = "Density.Equals with different type should return false")]
    public void DensityEqualsWithDifferentTypeShouldReturnFalse()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.False(density.Equals("not a density"));
    }

    [Fact(DisplayName = "Density.CompareTo should produce the expected result (left equal to right)")]
    public void DensityCompareToShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Density<double> left = new(Mass<double>.FromKilograms(2000), Volume<double>.FromCubicMeters(2));
        Density<double> right = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Density.CompareTo should produce the expected result (left greater than right)")]
    public void DensityCompareToShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Density<double> left = new(Mass<double>.FromKilograms(2000), Volume<double>.FromCubicMeters(1));
        Density<double> right = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Density.CompareTo should produce the expected result (left less than right)")]
    public void DensityCompareToShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Density<double> left = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(2));
        Density<double> right = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Density.CompareTo(null) should return 1")]
    public void DensityCompareToNullShouldReturnOne()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Equal(1, density.CompareTo(null));
        Assert.Equal(1, density.CompareTo((object?)null));
    }

    [Fact(DisplayName = "Density.CompareTo with incompatible type should throw")]
    public void DensityCompareToWithIncompatibleTypeShouldThrow()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Throws<ArgumentException>(() => density.CompareTo("not a density"));
    }

    [Fact(DisplayName = "Density.ToString with kg/cum:3 should produce the expected result")]
    public void DensityToStringWithKgPerCubicMeterScale3ShouldProduceExpectedResult()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Equal("1,000.000 kg/cum", density.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString should reduce magnitude (2000 kg / 2 m³ equals 1000 kg/m³)")]
    public void DensityToStringShouldReduceMagnitude()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(2000), Volume<double>.FromCubicMeters(2));

        // Then
        Assert.Equal("1,000.000 kg/cum", density.ToString("kg/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString with kg/cum:0 should produce the expected result")]
    public void DensityToStringWithKgPerCubicMeterScale0ShouldProduceExpectedResult()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Equal("1,000 kg/cum", density.ToString("kg/cum:0", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString with g/L should produce the expected result")]
    public void DensityToStringWithGramsPerLiterShouldProduceExpectedResult()
    {
        // Given - 1 g/mL = 1 g per 0.001 L = 1000 g/L
        Density<double> density = new(Mass<double>.FromGrams(1), Volume<double>.FromMilliliters(1));

        // Then
        Assert.Equal("1,000.000 g/L", density.ToString("g/L:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString with lb/cuft should produce the expected result")]
    public void DensityToStringWithPoundsPerCubicFootShouldProduceExpectedResult()
    {
        // Given - water density: 1000 kg/m³ ≈ 62.428 lb/ft³
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Equal("62.428 lb/cuft", density.ToString("lb/cuft:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density default ToString should use kg/cum")]
    public void DensityDefaultToStringShouldUseKilogramsPerCubicMeter()
    {
        // Given - 1 g/mL = 1000 kg/m³
        Density<double> density = new(Mass<double>.FromGrams(1), Volume<double>.FromMilliliters(1));

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
        Density<double> density = new(Mass<double>.FromKilograms(1234.56), Volume<double>.FromCubicMeters(1));

        // When
        string formatted = density.ToString("kg/cum:2", customCulture);

        // Then
        Assert.Equal("1.234,56 kg/cum", formatted);
    }

    [Fact(DisplayName = "Density.ToString should support span-interpolation format")]
    public void DensityToStringShouldSupportSpanInterpolation()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then - current culture default scale.
        Assert.Equal($"{1000.0.ToString("N", CultureInfo.CurrentCulture)} kg/cum", $"{density:kg/cum}");
    }

    [Fact(DisplayName = "Density.ToString should throw when format has no slash separator")]
    public void DensityToStringShouldThrowWhenFormatHasNoSlash()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Throws<FormatException>(() => density.ToString("kgcum", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString should throw when mass specifier is empty")]
    public void DensityToStringShouldThrowWhenMassSpecifierIsEmpty()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Throws<FormatException>(() => density.ToString("/cum", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString should throw when volume specifier is empty")]
    public void DensityToStringShouldThrowWhenVolumeSpecifierIsEmpty()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Throws<FormatException>(() => density.ToString("kg/", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString should throw when scale is not numeric")]
    public void DensityToStringShouldThrowWhenScaleNotNumeric()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Throws<FormatException>(() => density.ToString("kg/cum:abc", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString should throw when mass specifier is invalid")]
    public void DensityToStringShouldThrowWhenMassSpecifierIsInvalid()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Throws<ArgumentException>(() => density.ToString("xx/cum:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Density.ToString should throw when volume specifier is invalid")]
    public void DensityToStringShouldThrowWhenVolumeSpecifierIsInvalid()
    {
        // Given
        Density<double> density = new(Mass<double>.FromKilograms(1000), Volume<double>.FromCubicMeters(1));

        // Then
        Assert.Throws<ArgumentException>(() => density.ToString("kg/xx:3", CultureInfo.InvariantCulture));
    }
}
