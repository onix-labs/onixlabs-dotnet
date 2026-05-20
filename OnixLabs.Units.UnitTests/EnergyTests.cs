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

public sealed class EnergyTests
{
    // Helper: 1 m/s² acceleration in canonical "1 m/s over 1 s" form.
    private static Acceleration<Float128> OneMetrePerSecondSquared => new(
        new Speed<Float128>(Distance<Float128>.FromMeters((Float128)1), Time<Float128>.FromSeconds((Float128)1)),
        Time<Float128>.FromSeconds((Float128)1));

    // Helper: Acceleration of `value` m/s² in canonical form.
    private static Acceleration<Float128> MetersPerSecondSquared(Float128 value) => new(
        new Speed<Float128>(Distance<Float128>.FromMeters(value), Time<Float128>.FromSeconds((Float128)1)),
        Time<Float128>.FromSeconds((Float128)1));

    // Helper: Force of `newtons` N in canonical "(N kg × 1 m/s²)" form.
    private static Force<Float128> NewtonsForce(Float128 newtons) =>
        new(Mass<Float128>.FromKilograms(newtons), OneMetrePerSecondSquared);

    // Helper: Energy of `joules` J in canonical "(J N × 1 m)" form.
    private static Energy<Float128> Joules(Float128 joules) =>
        new(NewtonsForce(joules), Distance<Float128>.FromMeters((Float128)1));

    [Fact(DisplayName = "Energy should preserve its underlying Force and Distance components")]
    public void EnergyShouldPreserveUnderlyingComponents()
    {
        // Given
        Force<Float128> force = NewtonsForce((Float128)10);
        Distance<Float128> distance = Distance<Float128>.FromMeters((Float128)2);

        // When
        Energy<Float128> energy = new(force, distance);

        // Then
        Assert.Equal(force, energy.Left);
        Assert.Equal(distance, energy.Right);
    }

    [Fact(DisplayName = "Energy.Zero should produce zero magnitude")]
    public void EnergyZeroShouldProduceZeroMagnitude()
    {
        // Given / When — Zero is Force.Zero × Distance.Zero = 0 × 0 = 0 (safe for product composites).
        Energy<Float128> zero = Energy<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, zero.Magnitude);
        Assert.True(zero.Equals(zero));
    }

    [Fact(DisplayName = "Energy magnitude should land at human-readable joule scale")]
    public void EnergyMagnitudeShouldBeReadableJoules()
    {
        // Given — 10 N × 2 m = 20 J
        Energy<Float128> work = new(NewtonsForce((Float128)10), Distance<Float128>.FromMeters((Float128)2));

        // Then — the magnitude is joules directly.
        Assert.Equal((Float128)20, work.Magnitude);
    }

    [Fact(DisplayName = "Energy.Add should produce the expected magnitude")]
    public void EnergyAddShouldProduceExpectedValue()
    {
        // Given — 5 J + 3 J = 8 J
        Energy<Float128> left = Joules((Float128)5);
        Energy<Float128> right = Joules((Float128)3);

        // When
        Energy<Float128> result = Energy<Float128>.Add(left, right);

        // Then
        Assert.Equal((Float128)8, result.Magnitude);
    }

    [Fact(DisplayName = "Energy.Add should reduce magnitudes across decompositions")]
    public void EnergyAddShouldReduceMagnitudes()
    {
        // Given — (5 N × 4 m) = 20 J, plus (10 N × 1 m) = 10 J, total = 30 J.
        Energy<Float128> twenty = new(NewtonsForce((Float128)5), Distance<Float128>.FromMeters((Float128)4));
        Energy<Float128> ten = new(NewtonsForce((Float128)10), Distance<Float128>.FromMeters((Float128)1));

        // When
        Energy<Float128> result = Energy<Float128>.Add(twenty, ten);

        // Then
        Assert.Equal((Float128)30, result.Magnitude);
    }

    [Fact(DisplayName = "Energy.Add should reduce across mixed force/distance units")]
    public void EnergyAddShouldReduceAcrossMixedUnits()
    {
        // Given — (1 tonne × 1 m/s²) × 1 km = 1000 N × 1000 m = 1,000,000 J, plus 100 J = 1,000,100 J. Float128
        // strict equality holds because every cached conversion factor is an integer × Pow10 within Float128's
        // 10^48 exact range.
        Force<Float128> oneTonneNewtons = new(Mass<Float128>.FromTonnes((Float128)1), OneMetrePerSecondSquared);
        Energy<Float128> left = new(oneTonneNewtons, Distance<Float128>.FromKilometers((Float128)1));
        Energy<Float128> right = Joules((Float128)100);

        // When
        Energy<Float128> result = Energy<Float128>.Add(left, right);

        // Then
        Assert.Equal((Float128)1_000_100, result.Magnitude);
    }

    [Fact(DisplayName = "Energy.Add with Zero should return an equal-magnitude energy")]
    public void EnergyAddWithZeroShouldReturnSameMagnitude()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // When
        Energy<Float128> result = Energy<Float128>.Add(energy, Energy<Float128>.Zero);

        // Then
        Assert.Equal(energy, result);
    }

    [Fact(DisplayName = "Energy.Subtract should produce the expected magnitude")]
    public void EnergySubtractShouldProduceExpectedValue()
    {
        // Given — 10 J - 3 J = 7 J
        Energy<Float128> left = Joules((Float128)10);
        Energy<Float128> right = Joules((Float128)3);

        // When
        Energy<Float128> result = Energy<Float128>.Subtract(left, right);

        // Then — magnitude at joule scale, so Subtract is bit-exact for integer values.
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "Energy.Subtract should produce a negative result when left is less than right")]
    public void EnergySubtractShouldProduceNegativeWhenLeftLessThanRight()
    {
        // Given — 2 J - 5 J = -3 J
        Energy<Float128> left = Joules((Float128)2);
        Energy<Float128> right = Joules((Float128)5);

        // When
        Energy<Float128> result = Energy<Float128>.Subtract(left, right);

        // Then
        Assert.Equal(-(Float128)3, result.Magnitude);
    }

    [Fact(DisplayName = "Energy + operator should produce the expected result")]
    public void EnergyAddOperatorShouldProduceExpectedValue()
    {
        // Given
        Energy<Float128> left = Joules((Float128)5);
        Energy<Float128> right = Joules((Float128)3);

        // When
        Energy<Float128> result = left + right;

        // Then
        Assert.Equal((Float128)8, result.Magnitude);
    }

    [Fact(DisplayName = "Energy - operator should produce the expected result")]
    public void EnergySubtractOperatorShouldProduceExpectedValue()
    {
        // Given
        Energy<Float128> left = Joules((Float128)10);
        Energy<Float128> right = Joules((Float128)3);

        // When
        Energy<Float128> result = left - right;

        // Then
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "Energy instance Add should produce the expected result")]
    public void EnergyInstanceAddShouldProduceExpectedValue()
    {
        // Given
        Energy<Float128> left = Joules((Float128)5);
        Energy<Float128> right = Joules((Float128)3);

        // When
        Energy<Float128> result = left.Add(right);

        // Then
        Assert.Equal((Float128)8, result.Magnitude);
    }

    [Fact(DisplayName = "Energy instance Subtract should produce the expected result")]
    public void EnergyInstanceSubtractShouldProduceExpectedValue()
    {
        // Given
        Energy<Float128> left = Joules((Float128)10);
        Energy<Float128> right = Joules((Float128)3);

        // When
        Energy<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "Energy Add and Subtract should agree across static / operator / instance forms")]
    public void EnergyAddAndSubtractShouldAgreeAcrossForms()
    {
        // Given
        Energy<Float128> left = Joules((Float128)10);
        Energy<Float128> right = Joules((Float128)3);

        // Then
        Assert.Equal(Energy<Float128>.Add(left, right), left + right);
        Assert.Equal(Energy<Float128>.Add(left, right), left.Add(right));
        Assert.Equal(Energy<Float128>.Subtract(left, right), left - right);
        Assert.Equal(Energy<Float128>.Subtract(left, right), left.Subtract(right));
    }

    [Fact(DisplayName = "Energy equality should be by magnitude (identical components)")]
    public void EnergyEqualityShouldBeByMagnitudeIdenticalComponents()
    {
        // Given
        Energy<Float128> left = Joules((Float128)9);
        Energy<Float128> right = Joules((Float128)9);

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Energy equality should be by magnitude (proportional components)")]
    public void EnergyEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // Given — 5 N × 4 m = 20 J == 10 N × 2 m = 20 J.
        Energy<Float128> left = new(NewtonsForce((Float128)5), Distance<Float128>.FromMeters((Float128)4));
        Energy<Float128> right = new(NewtonsForce((Float128)10), Distance<Float128>.FromMeters((Float128)2));

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Energy equality should be by magnitude (cross-unit conversion)")]
    public void EnergyEqualityShouldBeByMagnitudeCrossUnit()
    {
        // Given — 1 N × 1 km = 1000 J == 1 N × 1000 m = 1000 J. Strict Float128 equality.
        Energy<Float128> left = new(NewtonsForce((Float128)1), Distance<Float128>.FromKilometers((Float128)1));
        Energy<Float128> right = new(NewtonsForce((Float128)1), Distance<Float128>.FromMeters((Float128)1000));

        // Then
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Energy inequality should hold for different magnitudes")]
    public void EnergyInequalityShouldHoldForDifferentMagnitudes()
    {
        // Given
        Energy<Float128> left = Joules((Float128)9);
        Energy<Float128> right = Joules((Float128)10);

        // Then
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
    }

    [Fact(DisplayName = "Energy.Equals(null) should return false")]
    public void EnergyEqualsNullShouldReturnFalse()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then
        Assert.False(energy.Equals(null));
        Assert.False(energy.Equals((object?)null));
    }

    [Fact(DisplayName = "Energy.Equals with different type should return false")]
    public void EnergyEqualsWithDifferentTypeShouldReturnFalse()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then
        Assert.False(energy.Equals("not an energy"));
    }

    [Fact(DisplayName = "Energy.CompareTo should produce the expected result (left equal to right)")]
    public void EnergyCompareToShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given — 5 N × 2 m = 10 J == 10 N × 1 m = 10 J
        Energy<Float128> left = new(NewtonsForce((Float128)5), Distance<Float128>.FromMeters((Float128)2));
        Energy<Float128> right = Joules((Float128)10);

        // Then
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Energy.CompareTo should produce the expected result (left greater than right)")]
    public void EnergyCompareToShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Energy<Float128> left = Joules((Float128)10);
        Energy<Float128> right = Joules((Float128)1);

        // Then
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Energy.CompareTo should produce the expected result (left less than right)")]
    public void EnergyCompareToShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Energy<Float128> left = Joules((Float128)1);
        Energy<Float128> right = Joules((Float128)10);

        // Then
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Energy.CompareTo(null) should return 1")]
    public void EnergyCompareToNullShouldReturnOne()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then
        Assert.Equal(1, energy.CompareTo(null));
        Assert.Equal(1, energy.CompareTo((object?)null));
    }

    [Fact(DisplayName = "Energy.CompareTo with incompatible type should throw")]
    public void EnergyCompareToWithIncompatibleTypeShouldThrow()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then
        Assert.Throws<ArgumentException>(() => energy.CompareTo("not an energy"));
    }

    [Fact(DisplayName = "Energy.ToString with kg*m/s²*m:3 should produce the expected result")]
    public void EnergyToStringWithJoulesScale3ShouldProduceExpectedResult()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then
        Assert.Equal("9.000 kg*m/s²*m", energy.ToString("kg*m/s²*m:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Energy.ToString should accept compound acceleration form (kg*m/s/s*m)")]
    public void EnergyToStringShouldAcceptCompoundAccelerationForm()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then — last '*' separates force-spec "kg*m/s/s" from distance-spec "m".
        Assert.Equal("9.000 kg*m/s/s*m", energy.ToString("kg*m/s/s*m:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Energy.ToString should accept mixed force/distance units (kg*m/s²*km)")]
    public void EnergyToStringShouldAcceptMixedForceDistanceUnits()
    {
        // Given — 1000 J = 1 N × 1 km
        Energy<Float128> energy = Joules((Float128)1000);

        // Then
        Assert.Equal("1.000 kg*m/s²*km", energy.ToString("kg*m/s²*km:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Energy.ToString should reduce magnitude (5 N × 4 m equals 20 J)")]
    public void EnergyToStringShouldReduceMagnitude()
    {
        // Given
        Energy<Float128> energy = new(NewtonsForce((Float128)5), Distance<Float128>.FromMeters((Float128)4));

        // Then
        Assert.Equal("20.000 kg*m/s²*m", energy.ToString("kg*m/s²*m:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Energy.ToString with kg*m/s²*m:0 should produce the expected result")]
    public void EnergyToStringWithJoulesScale0ShouldProduceExpectedResult()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then
        Assert.Equal("9 kg*m/s²*m", energy.ToString("kg*m/s²*m:0", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Energy default ToString should use the J alias")]
    public void EnergyDefaultToStringShouldUseJouleAlias()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // When
        string result = energy.ToString();

        // Then
        Assert.EndsWith(" J", result);
    }

    [Fact(DisplayName = "Energy.ToString J alias should produce '9.000 J'")]
    public void EnergyToStringJouleAliasShouldProduceExpected() =>
        Assert.Equal("9.000 J", Joules((Float128)9).ToString("J:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Energy.ToString kJ alias should produce '0.009 kJ'")]
    public void EnergyToStringKilojouleAliasShouldProduceExpected() =>
        Assert.Equal("0.009 kJ", Joules((Float128)9).ToString("kJ:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Energy.ToString MJ alias should produce '0.000009 MJ'")]
    public void EnergyToStringMegajouleAliasShouldProduceExpected() =>
        Assert.Equal("0.000009 MJ", Joules((Float128)9).ToString("MJ:6", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Energy compound 'N*m' cascades through Force N alias")]
    public void EnergyCompoundNewtonMetreCascadesThroughForceAlias() =>
        Assert.Equal("9.000 N*m", Joules((Float128)9).ToString("N*m:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Energy.ToString should honor custom culture separators")]
    public void EnergyToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Energy<Float128> energy = Joules(Float128.Parse("1234.56"));

        // When
        string formatted = energy.ToString("kg*m/s²*m:2", customCulture);

        // Then
        Assert.Equal("1.234,56 kg*m/s²*m", formatted);
    }

    [Fact(DisplayName = "Energy.ToString should support span-interpolation format")]
    public void EnergyToStringShouldSupportSpanInterpolation()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then
        Assert.Equal($"{((Float128)9).ToString("N", CultureInfo.CurrentCulture)} kg*m/s²*m", $"{energy:kg*m/s²*m}");
    }

    [Fact(DisplayName = "Energy.ToString should throw when format has no star separator")]
    public void EnergyToStringShouldThrowWhenFormatHasNoStar()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then
        Assert.Throws<FormatException>(() => energy.ToString("kgms2m", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Energy.ToString should throw when force specifier is empty")]
    public void EnergyToStringShouldThrowWhenForceSpecifierIsEmpty()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then — "*m" has lastStar=0, force-spec = "".
        Assert.Throws<FormatException>(() => energy.ToString("*m", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Energy.ToString should throw when distance specifier is empty")]
    public void EnergyToStringShouldThrowWhenDistanceSpecifierIsEmpty()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then
        Assert.Throws<FormatException>(() => energy.ToString("kg*m/s²*", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Energy.ToString should throw when scale is not numeric")]
    public void EnergyToStringShouldThrowWhenScaleNotNumeric()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then
        Assert.Throws<FormatException>(() => energy.ToString("kg*m/s²*m:abc", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Energy.ToString should throw when distance specifier is invalid")]
    public void EnergyToStringShouldThrowWhenDistanceSpecifierIsInvalid()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then — bubbles ArgumentException from Distance.ValueOf.
        Assert.Throws<ArgumentException>(() => energy.ToString("kg*m/s²*xx:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Energy.ToString should throw when force specifier is invalid")]
    public void EnergyToStringShouldThrowWhenForceSpecifierIsInvalid()
    {
        // Given
        Energy<Float128> energy = Joules((Float128)9);

        // Then — bubbles ArgumentException from Mass.ValueOf via Force.ValueOf.
        Assert.Throws<ArgumentException>(() => energy.ToString("xx*m/s²*m:3", CultureInfo.InvariantCulture));
    }
}
