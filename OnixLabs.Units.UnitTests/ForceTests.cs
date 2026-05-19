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

public sealed class ForceTests
{
    // Helper: construct an acceleration of `value` m/s² in canonical "X m/s over 1 s" form.
    private static Acceleration<Float128> MetersPerSecondSquared(Float128 value) => new(
        new Speed<Float128>(Distance<Float128>.FromMeters(value), Time<Float128>.FromSeconds((Float128)1)),
        Time<Float128>.FromSeconds((Float128)1));

    // Helper: construct a force of `newtons` N in canonical "X kg × 1 m/s²" form.
    private static Force<Float128> Newtons(Float128 newtons) =>
        new(Mass<Float128>.FromKilograms(newtons), MetersPerSecondSquared((Float128)1));

    [Fact(DisplayName = "Force should preserve its underlying Mass and Acceleration components")]
    public void ForceShouldPreserveUnderlyingComponents()
    {
        // Given
        Mass<Float128> mass = Mass<Float128>.FromKilograms((Float128)10);
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)2);

        // When
        Force<Float128> force = new(mass, acceleration);

        // Then
        Assert.Equal(mass, force.Left);
        Assert.Equal(acceleration, force.Right);
    }

    [Fact(DisplayName = "Force.Zero should produce zero magnitude")]
    public void ForceZeroShouldProduceZeroMagnitude()
    {
        // Given / When — Zero is the product of Mass.Zero and Acceleration.Zero. Multiplication of zeros
        // gives a genuine zero without the 0/0 NaN hazard that quotient composites guard against.
        Force<Float128> zero = Force<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, zero.Magnitude);
        Assert.True(zero.Equals(zero));
    }

    [Fact(DisplayName = "Force magnitude should land at human-readable Newton scale")]
    public void ForceMagnitudeShouldBeReadableNewtons()
    {
        // Given — 1 kg × 9.81 m/s² (1 kg in Earth gravity)
        Force<Float128> weight = new(Mass<Float128>.FromKilograms((Float128)1), MetersPerSecondSquared(Float128.Parse("9.81")));

        // Then — the magnitude is Newtons directly.
        Assert.Equal(Float128.Parse("9.81"), weight.Magnitude);
    }

    [Fact(DisplayName = "Force.Add should produce the expected magnitude")]
    public void ForceAddShouldProduceExpectedValue()
    {
        // Given — 5 N + 3 N = 8 N
        Force<Float128> left = Newtons((Float128)5);
        Force<Float128> right = Newtons((Float128)3);

        // When
        Force<Float128> result = Force<Float128>.Add(left, right);

        // Then
        Assert.Equal((Float128)8, result.Magnitude);
    }

    [Fact(DisplayName = "Force.Add should reduce magnitudes across decompositions")]
    public void ForceAddShouldReduceMagnitudes()
    {
        // Given — (2 kg × 5 m/s²) = 10 N, plus (3 kg × 1 m/s²) = 3 N, total = 13 N.
        Force<Float128> tenNewtons = new(Mass<Float128>.FromKilograms((Float128)2), MetersPerSecondSquared((Float128)5));
        Force<Float128> threeNewtons = new(Mass<Float128>.FromKilograms((Float128)3), MetersPerSecondSquared((Float128)1));

        // When
        Force<Float128> result = Force<Float128>.Add(tenNewtons, threeNewtons);

        // Then
        Assert.Equal((Float128)13, result.Magnitude);
    }

    [Fact(DisplayName = "Force.Add should reduce across mixed mass/acceleration units")]
    public void ForceAddShouldReduceAcrossMixedUnits()
    {
        // Given — 1 tonne × 1 m/s² = 1000 N (1 tonne = 1000 kg), plus 500 N = 1500 N. Float128 strict
        // equality holds because Mass.FromTonnes(1).KiloGrams = 1000 exactly (cached integer × Pow10 product).
        Force<Float128> left = new(Mass<Float128>.FromTonnes((Float128)1), MetersPerSecondSquared((Float128)1));
        Force<Float128> right = Newtons((Float128)500);

        // When
        Force<Float128> result = Force<Float128>.Add(left, right);

        // Then
        Assert.Equal((Float128)1500, result.Magnitude);
    }

    [Fact(DisplayName = "Force.Add with Zero should return an equal-magnitude force")]
    public void ForceAddWithZeroShouldReturnSameMagnitude()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // When
        Force<Float128> result = Force<Float128>.Add(force, Force<Float128>.Zero);

        // Then
        Assert.Equal(force, result);
    }

    [Fact(DisplayName = "Force.Subtract should produce the expected magnitude")]
    public void ForceSubtractShouldProduceExpectedValue()
    {
        // Given — 10 N - 3 N = 7 N
        Force<Float128> left = Newtons((Float128)10);
        Force<Float128> right = Newtons((Float128)3);

        // When
        Force<Float128> result = Force<Float128>.Subtract(left, right);

        // Then — magnitude at Newton scale, so Subtract is bit-exact for integer values.
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "Force.Subtract should produce a negative result when left is less than right")]
    public void ForceSubtractShouldProduceNegativeWhenLeftLessThanRight()
    {
        // Given — 2 N - 5 N = -3 N
        Force<Float128> left = Newtons((Float128)2);
        Force<Float128> right = Newtons((Float128)5);

        // When
        Force<Float128> result = Force<Float128>.Subtract(left, right);

        // Then
        Assert.Equal(-(Float128)3, result.Magnitude);
    }

    [Fact(DisplayName = "Force + operator should produce the expected result")]
    public void ForceAddOperatorShouldProduceExpectedValue()
    {
        // Given
        Force<Float128> left = Newtons((Float128)5);
        Force<Float128> right = Newtons((Float128)3);

        // When
        Force<Float128> result = left + right;

        // Then
        Assert.Equal((Float128)8, result.Magnitude);
    }

    [Fact(DisplayName = "Force - operator should produce the expected result")]
    public void ForceSubtractOperatorShouldProduceExpectedValue()
    {
        // Given
        Force<Float128> left = Newtons((Float128)10);
        Force<Float128> right = Newtons((Float128)3);

        // When
        Force<Float128> result = left - right;

        // Then
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "Force instance Add should produce the expected result")]
    public void ForceInstanceAddShouldProduceExpectedValue()
    {
        // Given
        Force<Float128> left = Newtons((Float128)5);
        Force<Float128> right = Newtons((Float128)3);

        // When
        Force<Float128> result = left.Add(right);

        // Then
        Assert.Equal((Float128)8, result.Magnitude);
    }

    [Fact(DisplayName = "Force instance Subtract should produce the expected result")]
    public void ForceInstanceSubtractShouldProduceExpectedValue()
    {
        // Given
        Force<Float128> left = Newtons((Float128)10);
        Force<Float128> right = Newtons((Float128)3);

        // When
        Force<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "Force Add and Subtract should agree across static / operator / instance forms")]
    public void ForceAddAndSubtractShouldAgreeAcrossForms()
    {
        // Given
        Force<Float128> left = Newtons((Float128)10);
        Force<Float128> right = Newtons((Float128)3);

        // Then
        Assert.Equal(Force<Float128>.Add(left, right), left + right);
        Assert.Equal(Force<Float128>.Add(left, right), left.Add(right));
        Assert.Equal(Force<Float128>.Subtract(left, right), left - right);
        Assert.Equal(Force<Float128>.Subtract(left, right), left.Subtract(right));
    }

    [Fact(DisplayName = "Force equality should be by magnitude (identical components)")]
    public void ForceEqualityShouldBeByMagnitudeIdenticalComponents()
    {
        // Given
        Force<Float128> left = Newtons((Float128)9);
        Force<Float128> right = Newtons((Float128)9);

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Force equality should be by magnitude (proportional components)")]
    public void ForceEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // Given — 2 kg × 5 m/s² = 10 N, equivalent to 5 kg × 2 m/s² = 10 N.
        Force<Float128> left = new(Mass<Float128>.FromKilograms((Float128)2), MetersPerSecondSquared((Float128)5));
        Force<Float128> right = new(Mass<Float128>.FromKilograms((Float128)5), MetersPerSecondSquared((Float128)2));

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Force equality should be by magnitude (cross-unit conversion)")]
    public void ForceEqualityShouldBeByMagnitudeCrossUnit()
    {
        // Given — 1 tonne × 1 m/s² = 1000 N == 1000 kg × 1 m/s² = 1000 N. Strict Float128 equality.
        Force<Float128> left = new(Mass<Float128>.FromTonnes((Float128)1), MetersPerSecondSquared((Float128)1));
        Force<Float128> right = new(Mass<Float128>.FromKilograms((Float128)1000), MetersPerSecondSquared((Float128)1));

        // Then
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Force inequality should hold for different magnitudes")]
    public void ForceInequalityShouldHoldForDifferentMagnitudes()
    {
        // Given
        Force<Float128> left = Newtons((Float128)9);
        Force<Float128> right = Newtons((Float128)10);

        // Then
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
    }

    [Fact(DisplayName = "Force.Equals(null) should return false")]
    public void ForceEqualsNullShouldReturnFalse()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then
        Assert.False(force.Equals(null));
        Assert.False(force.Equals((object?)null));
    }

    [Fact(DisplayName = "Force.Equals with different type should return false")]
    public void ForceEqualsWithDifferentTypeShouldReturnFalse()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then
        Assert.False(force.Equals("not a force"));
    }

    [Fact(DisplayName = "Force.CompareTo should produce the expected result (left equal to right)")]
    public void ForceCompareToShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given — 2 kg × 5 m/s² = 10 N == 10 kg × 1 m/s² = 10 N
        Force<Float128> left = new(Mass<Float128>.FromKilograms((Float128)2), MetersPerSecondSquared((Float128)5));
        Force<Float128> right = Newtons((Float128)10);

        // Then
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Force.CompareTo should produce the expected result (left greater than right)")]
    public void ForceCompareToShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Force<Float128> left = Newtons((Float128)10);
        Force<Float128> right = Newtons((Float128)1);

        // Then
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Force.CompareTo should produce the expected result (left less than right)")]
    public void ForceCompareToShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Force<Float128> left = Newtons((Float128)1);
        Force<Float128> right = Newtons((Float128)10);

        // Then
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Force.CompareTo(null) should return 1")]
    public void ForceCompareToNullShouldReturnOne()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then
        Assert.Equal(1, force.CompareTo(null));
        Assert.Equal(1, force.CompareTo((object?)null));
    }

    [Fact(DisplayName = "Force.CompareTo with incompatible type should throw")]
    public void ForceCompareToWithIncompatibleTypeShouldThrow()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then
        Assert.Throws<ArgumentException>(() => force.CompareTo("not a force"));
    }

    [Fact(DisplayName = "Force.ToString with kg*m/s²:3 should produce the expected result")]
    public void ForceToStringWithNewtonsScale3ShouldProduceExpectedResult()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then
        Assert.Equal("9.000 kg*m/s²", force.ToString("kg*m/s²:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Force.ToString should accept compound acceleration form (kg*m/s/s)")]
    public void ForceToStringShouldAcceptCompoundAccelerationForm()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then — acceleration's compound form bubbles through Force's parser.
        Assert.Equal("9.000 kg*m/s/s", force.ToString("kg*m/s/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Force.ToString should accept mixed mass/acceleration units (lb*ft/s²)")]
    public void ForceToStringShouldAcceptMixedMassAccelerationUnits()
    {
        // Given — 1 N = 1 kg × 1 m/s². In pound-feet/s² (poundals): 1 N ≈ 7.233 lb·ft/s²
        // — but we just check the formatting works; numeric value computed via the same chain.
        Force<Float128> force = Newtons((Float128)1);

        // Then
        string result = force.ToString("lb*ft/s²:3", CultureInfo.InvariantCulture);
        Assert.EndsWith(" lb*ft/s²", result);
    }

    [Fact(DisplayName = "Force.ToString should reduce magnitude (2 kg × 5 m/s² equals 10 kg*m/s²)")]
    public void ForceToStringShouldReduceMagnitude()
    {
        // Given
        Force<Float128> force = new(Mass<Float128>.FromKilograms((Float128)2), MetersPerSecondSquared((Float128)5));

        // Then
        Assert.Equal("10.000 kg*m/s²", force.ToString("kg*m/s²:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Force.ToString with kg*m/s²:0 should produce the expected result")]
    public void ForceToStringWithNewtonsScale0ShouldProduceExpectedResult()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then
        Assert.Equal("9 kg*m/s²", force.ToString("kg*m/s²:0", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Force default ToString should use kg*m/s²")]
    public void ForceDefaultToStringShouldUseKilogramsMetersPerSecondSquared()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // When
        string result = force.ToString();

        // Then
        Assert.EndsWith(" kg*m/s²", result);
    }

    [Fact(DisplayName = "Force.ToString should honor custom culture separators")]
    public void ForceToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Force<Float128> force = Newtons(Float128.Parse("1234.56"));

        // When
        string formatted = force.ToString("kg*m/s²:2", customCulture);

        // Then
        Assert.Equal("1.234,56 kg*m/s²", formatted);
    }

    [Fact(DisplayName = "Force.ToString should support span-interpolation format")]
    public void ForceToStringShouldSupportSpanInterpolation()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then — current culture default scale, then unit suffix.
        Assert.Equal($"{((Float128)9).ToString("N", CultureInfo.CurrentCulture)} kg*m/s²", $"{force:kg*m/s²}");
    }

    [Fact(DisplayName = "Force.ToString should throw when format has no star separator")]
    public void ForceToStringShouldThrowWhenFormatHasNoStar()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then
        Assert.Throws<FormatException>(() => force.ToString("kgms2", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Force.ToString should throw when mass specifier is empty")]
    public void ForceToStringShouldThrowWhenMassSpecifierIsEmpty()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then
        Assert.Throws<FormatException>(() => force.ToString("*m/s²", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Force.ToString should throw when acceleration specifier is empty")]
    public void ForceToStringShouldThrowWhenAccelerationSpecifierIsEmpty()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then
        Assert.Throws<FormatException>(() => force.ToString("kg*", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Force.ToString should throw when scale is not numeric")]
    public void ForceToStringShouldThrowWhenScaleNotNumeric()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then
        Assert.Throws<FormatException>(() => force.ToString("kg*m/s²:abc", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Force.ToString should throw when mass specifier is invalid")]
    public void ForceToStringShouldThrowWhenMassSpecifierIsInvalid()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then — bubbles ArgumentException from Mass.ValueOf.
        Assert.Throws<ArgumentException>(() => force.ToString("xx*m/s²:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Force.ToString should throw when acceleration specifier is invalid")]
    public void ForceToStringShouldThrowWhenAccelerationSpecifierIsInvalid()
    {
        // Given
        Force<Float128> force = Newtons((Float128)9);

        // Then — bubbles ArgumentException from Distance.ValueOf via Acceleration.ValueOf → Speed.ValueOf.
        Assert.Throws<ArgumentException>(() => force.ToString("kg*xx/s²:3", CultureInfo.InvariantCulture));
    }
}
