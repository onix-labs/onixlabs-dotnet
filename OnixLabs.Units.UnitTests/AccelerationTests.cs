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

public sealed class AccelerationTests
{
    // Helper: construct an acceleration of `metersPerSecondSquared` m/s² in canonical "X m/s over 1 s" form.
    private static Acceleration<Float128> MetersPerSecondSquared(Float128 metersPerSecondSquared) => new(
        new Speed<Float128>(Distance<Float128>.FromMeters(metersPerSecondSquared), Time<Float128>.FromSeconds((Float128)1)),
        Time<Float128>.FromSeconds((Float128)1));

    [Fact(DisplayName = "Acceleration should preserve its underlying Speed and Time components")]
    public void AccelerationShouldPreserveUnderlyingComponents()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMeters((Float128)20), Time<Float128>.FromSeconds((Float128)2));
        Time<Float128> time = Time<Float128>.FromSeconds((Float128)1);

        // When
        Acceleration<Float128> acceleration = new(speed, time);

        // Then
        Assert.Equal(speed, acceleration.Left);
        Assert.Equal(time, acceleration.Right);
    }

    [Fact(DisplayName = "Acceleration.Zero should produce zero magnitude (avoids 0/0 NaN)")]
    public void AccelerationZeroShouldProduceZeroMagnitude()
    {
        // Given / When — Zero stores Speed.Zero over Time.FromSeconds(1), so the magnitude is a
        // genuine 0 and not NaN from a 0/0 ratio.
        Acceleration<Float128> zero = Acceleration<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, zero.Magnitude);
        Assert.True(zero.Equals(zero));
    }

    [Fact(DisplayName = "Acceleration magnitude should land at human-readable m/s² scale")]
    public void AccelerationMagnitudeShouldBeReadableMetersPerSecondSquared()
    {
        // Given — 9.81 m/s² (gravity)
        Acceleration<Float128> gravity = MetersPerSecondSquared(Float128.Parse("9.81"));

        // Then — the magnitude is the m/s² value directly, not a scaled ratio.
        Assert.Equal(Float128.Parse("9.81"), gravity.Magnitude);
    }

    [Fact(DisplayName = "Acceleration.Add should produce the expected magnitude")]
    public void AccelerationAddShouldProduceExpectedValue()
    {
        // Given — 3 m/s² + 2 m/s² = 5 m/s²
        Acceleration<Float128> left = MetersPerSecondSquared((Float128)3);
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)2);

        // When
        Acceleration<Float128> result = Acceleration<Float128>.Add(left, right);

        // Then
        Assert.Equal((Float128)5, result.Magnitude);
    }

    [Fact(DisplayName = "Acceleration.Add should reduce magnitudes across decompositions")]
    public void AccelerationAddShouldReduceMagnitudes()
    {
        // Given — (20 m/s over 2 s) = 10 m/s² acceleration, plus (5 m/s over 1 s) = 5 m/s², total 15 m/s².
        Acceleration<Float128> tenMps2 = new(
            new Speed<Float128>(Distance<Float128>.FromMeters((Float128)20), Time<Float128>.FromSeconds((Float128)1)),
            Time<Float128>.FromSeconds((Float128)2));
        Acceleration<Float128> fiveMps2 = MetersPerSecondSquared((Float128)5);

        // When
        Acceleration<Float128> result = Acceleration<Float128>.Add(tenMps2, fiveMps2);

        // Then
        Assert.Equal((Float128)15, result.Magnitude);
    }

    [Fact(DisplayName = "Acceleration.Add should reduce across mixed speed/time units")]
    public void AccelerationAddShouldReduceAcrossMixedUnits()
    {
        // Given — 36 km/h over 1 s = 10 m/s² acceleration, plus 5 m/s² = 15 m/s². Float128 strict equality
        // holds because Speed(36 km, 1 h).Magnitude = (36×1e33) / (3600×1e30) = 10 exactly, and the cached
        // QuectosecondsPerHour factor is an exact integer × Pow10 product.
        Acceleration<Float128> left = new(
            new Speed<Float128>(Distance<Float128>.FromKilometers((Float128)36), Time<Float128>.FromHours((Float128)1)),
            Time<Float128>.FromSeconds((Float128)1));
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)5);

        // When
        Acceleration<Float128> result = Acceleration<Float128>.Add(left, right);

        // Then
        Assert.Equal((Float128)15, result.Magnitude);
    }

    [Fact(DisplayName = "Acceleration.Add with Zero should return an equal-magnitude acceleration")]
    public void AccelerationAddWithZeroShouldReturnSameMagnitude()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // When
        Acceleration<Float128> result = Acceleration<Float128>.Add(acceleration, Acceleration<Float128>.Zero);

        // Then
        Assert.Equal(acceleration, result);
    }

    [Fact(DisplayName = "Acceleration.Subtract should produce the expected magnitude")]
    public void AccelerationSubtractShouldProduceExpectedValue()
    {
        // Given — 10 m/s² - 3 m/s² = 7 m/s²
        Acceleration<Float128> left = MetersPerSecondSquared((Float128)10);
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)3);

        // When
        Acceleration<Float128> result = Acceleration<Float128>.Subtract(left, right);

        // Then — magnitude now sits at m/s² scale, so Subtract is bit-exact for integer values.
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "Acceleration.Subtract should produce a negative result when left is less than right")]
    public void AccelerationSubtractShouldProduceNegativeWhenLeftLessThanRight()
    {
        // Given — 2 m/s² - 5 m/s² = -3 m/s²
        Acceleration<Float128> left = MetersPerSecondSquared((Float128)2);
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)5);

        // When
        Acceleration<Float128> result = Acceleration<Float128>.Subtract(left, right);

        // Then
        Assert.Equal(-(Float128)3, result.Magnitude);
    }

    [Fact(DisplayName = "Acceleration + operator should produce the expected result")]
    public void AccelerationAddOperatorShouldProduceExpectedValue()
    {
        // Given
        Acceleration<Float128> left = MetersPerSecondSquared((Float128)3);
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)2);

        // When
        Acceleration<Float128> result = left + right;

        // Then
        Assert.Equal((Float128)5, result.Magnitude);
    }

    [Fact(DisplayName = "Acceleration - operator should produce the expected result")]
    public void AccelerationSubtractOperatorShouldProduceExpectedValue()
    {
        // Given
        Acceleration<Float128> left = MetersPerSecondSquared((Float128)10);
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)3);

        // When
        Acceleration<Float128> result = left - right;

        // Then
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "Acceleration instance Add should produce the expected result")]
    public void AccelerationInstanceAddShouldProduceExpectedValue()
    {
        // Given
        Acceleration<Float128> left = MetersPerSecondSquared((Float128)3);
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)2);

        // When
        Acceleration<Float128> result = left.Add(right);

        // Then
        Assert.Equal((Float128)5, result.Magnitude);
    }

    [Fact(DisplayName = "Acceleration instance Subtract should produce the expected result")]
    public void AccelerationInstanceSubtractShouldProduceExpectedValue()
    {
        // Given
        Acceleration<Float128> left = MetersPerSecondSquared((Float128)10);
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)3);

        // When
        Acceleration<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "Acceleration Add and Subtract should agree across static / operator / instance forms")]
    public void AccelerationAddAndSubtractShouldAgreeAcrossForms()
    {
        // Given
        Acceleration<Float128> left = MetersPerSecondSquared((Float128)10);
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)3);

        // Then
        Assert.Equal(Acceleration<Float128>.Add(left, right), left + right);
        Assert.Equal(Acceleration<Float128>.Add(left, right), left.Add(right));
        Assert.Equal(Acceleration<Float128>.Subtract(left, right), left - right);
        Assert.Equal(Acceleration<Float128>.Subtract(left, right), left.Subtract(right));
    }

    [Fact(DisplayName = "Acceleration equality should be by magnitude (identical components)")]
    public void AccelerationEqualityShouldBeByMagnitudeIdenticalComponents()
    {
        // Given
        Acceleration<Float128> left = MetersPerSecondSquared((Float128)9);
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)9);

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Acceleration equality should be by magnitude (proportional components)")]
    public void AccelerationEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // Given — 20 m/s over 2 s = 10 m/s², equivalent to 10 m/s over 1 s.
        Acceleration<Float128> left = new(
            new Speed<Float128>(Distance<Float128>.FromMeters((Float128)20), Time<Float128>.FromSeconds((Float128)1)),
            Time<Float128>.FromSeconds((Float128)2));
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)10);

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Acceleration equality should be by magnitude (cross-unit conversion)")]
    public void AccelerationEqualityShouldBeByMagnitudeCrossUnit()
    {
        // Given — 36 km/h per second == 10 m/s². Both decompositions reduce to the same Float128 magnitude
        // because Speed(36 km, 1 h).Magnitude = 10 exactly (the cached integer × Pow10 factors all land
        // within Float128's 10^48 exact range).
        Acceleration<Float128> left = new(
            new Speed<Float128>(Distance<Float128>.FromKilometers((Float128)36), Time<Float128>.FromHours((Float128)1)),
            Time<Float128>.FromSeconds((Float128)1));
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)10);

        // Then
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Acceleration inequality should hold for different magnitudes")]
    public void AccelerationInequalityShouldHoldForDifferentMagnitudes()
    {
        // Given
        Acceleration<Float128> left = MetersPerSecondSquared((Float128)9);
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)10);

        // Then
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
    }

    [Fact(DisplayName = "Acceleration.Equals(null) should return false")]
    public void AccelerationEqualsNullShouldReturnFalse()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then
        Assert.False(acceleration.Equals(null));
        Assert.False(acceleration.Equals((object?)null));
    }

    [Fact(DisplayName = "Acceleration.Equals with different type should return false")]
    public void AccelerationEqualsWithDifferentTypeShouldReturnFalse()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then
        Assert.False(acceleration.Equals("not an acceleration"));
    }

    [Fact(DisplayName = "Acceleration.CompareTo should produce the expected result (left equal to right)")]
    public void AccelerationCompareToShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given — 20 m/s over 2 s = 10 m/s² == 10 m/s over 1 s = 10 m/s²
        Acceleration<Float128> left = new(
            new Speed<Float128>(Distance<Float128>.FromMeters((Float128)20), Time<Float128>.FromSeconds((Float128)1)),
            Time<Float128>.FromSeconds((Float128)2));
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)10);

        // Then
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Acceleration.CompareTo should produce the expected result (left greater than right)")]
    public void AccelerationCompareToShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Acceleration<Float128> left = MetersPerSecondSquared((Float128)10);
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)1);

        // Then
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Acceleration.CompareTo should produce the expected result (left less than right)")]
    public void AccelerationCompareToShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Acceleration<Float128> left = MetersPerSecondSquared((Float128)1);
        Acceleration<Float128> right = MetersPerSecondSquared((Float128)10);

        // Then
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Acceleration.CompareTo(null) should return 1")]
    public void AccelerationCompareToNullShouldReturnOne()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then
        Assert.Equal(1, acceleration.CompareTo(null));
        Assert.Equal(1, acceleration.CompareTo((object?)null));
    }

    [Fact(DisplayName = "Acceleration.CompareTo with incompatible type should throw")]
    public void AccelerationCompareToWithIncompatibleTypeShouldThrow()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then
        Assert.Throws<ArgumentException>(() => acceleration.CompareTo("not an acceleration"));
    }

    [Fact(DisplayName = "Acceleration.ToString with m/s²:3 should produce the expected result")]
    public void AccelerationToStringWithSquaredFormScale3ShouldProduceExpectedResult()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then
        Assert.Equal("9.000 m/s²", acceleration.ToString("m/s²:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration.ToString should accept ASCII '2' as squared marker (output uses '²')")]
    public void AccelerationToStringShouldAcceptAsciiSquaredMarker()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then — input "m/s2:3" is accepted; output always uses '²'.
        Assert.Equal("9.000 m/s²", acceleration.ToString("m/s2:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration.ToString should accept compound form (speed/time)")]
    public void AccelerationToStringShouldAcceptCompoundForm()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then — "m/s/s:3" parses as speed-spec "m/s" over time-spec "s".
        Assert.Equal("9.000 m/s/s", acceleration.ToString("m/s/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration.ToString should accept mixed speed/time units (km/h/s)")]
    public void AccelerationToStringShouldAcceptMixedSpeedTimeUnits()
    {
        // Given — 10 m/s² = 36 km/h per second
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)10);

        // Then
        Assert.Equal("36.000 km/h/s", acceleration.ToString("km/h/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration.ToString should reduce magnitude (20 m/s over 2 s equals 10 m/s²)")]
    public void AccelerationToStringShouldReduceMagnitude()
    {
        // Given
        Acceleration<Float128> acceleration = new(
            new Speed<Float128>(Distance<Float128>.FromMeters((Float128)20), Time<Float128>.FromSeconds((Float128)1)),
            Time<Float128>.FromSeconds((Float128)2));

        // Then
        Assert.Equal("10.000 m/s²", acceleration.ToString("m/s²:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration.ToString with m/s²:0 should produce the expected result")]
    public void AccelerationToStringWithSquaredFormScale0ShouldProduceExpectedResult()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then
        Assert.Equal("9 m/s²", acceleration.ToString("m/s²:0", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration.ToString with km/s² should produce the expected result")]
    public void AccelerationToStringWithKilometersPerSecondSquaredShouldProduceExpectedResult()
    {
        // Given — 5000 m/s² = 5 km/s²
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)5000);

        // Then
        Assert.Equal("5.000 km/s²", acceleration.ToString("km/s²:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration default ToString should use m/s²")]
    public void AccelerationDefaultToStringShouldUseMetersPerSecondSquared()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // When
        string result = acceleration.ToString();

        // Then
        Assert.EndsWith(" m/s²", result);
    }

    [Fact(DisplayName = "Acceleration.ToString should honor custom culture separators")]
    public void AccelerationToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Acceleration<Float128> acceleration = MetersPerSecondSquared(Float128.Parse("1234.56"));

        // When
        string formatted = acceleration.ToString("m/s²:2", customCulture);

        // Then
        Assert.Equal("1.234,56 m/s²", formatted);
    }

    [Fact(DisplayName = "Acceleration.ToString should support span-interpolation format")]
    public void AccelerationToStringShouldSupportSpanInterpolation()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then — current culture default scale, then unit suffix.
        Assert.Equal($"{((Float128)9).ToString("N", CultureInfo.CurrentCulture)} m/s²", $"{acceleration:m/s²}");
    }

    [Fact(DisplayName = "Acceleration.ToString should throw when format has no slash separator")]
    public void AccelerationToStringShouldThrowWhenFormatHasNoSlash()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then — "ms²" strips ² to "ms", which has no '/'.
        Assert.Throws<FormatException>(() => acceleration.ToString("ms²", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration.ToString should throw when speed specifier is empty")]
    public void AccelerationToStringShouldThrowWhenSpeedSpecifierIsEmpty()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then — "/s²" strips ² to "/s", lastSlash=0 → speed-spec ""
        Assert.Throws<FormatException>(() => acceleration.ToString("/s²", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration.ToString should throw when time specifier is empty (compound form)")]
    public void AccelerationToStringShouldThrowWhenTimeSpecifierIsEmpty()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then — "m/s/" → speed-spec "m/s", time-spec "" (after last slash).
        Assert.Throws<FormatException>(() => acceleration.ToString("m/s/", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration.ToString should throw when unit part is only the squared marker")]
    public void AccelerationToStringShouldThrowWhenUnitPartIsOnlySquaredMarker()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then — "²" alone has nothing before the marker.
        Assert.Throws<FormatException>(() => acceleration.ToString("²", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration.ToString should throw when scale is not numeric")]
    public void AccelerationToStringShouldThrowWhenScaleNotNumeric()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then
        Assert.Throws<FormatException>(() => acceleration.ToString("m/s²:abc", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration.ToString should throw when distance specifier is invalid")]
    public void AccelerationToStringShouldThrowWhenDistanceSpecifierIsInvalid()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then — bubbles ArgumentException from Distance.ValueOf via Speed.ValueOf.
        Assert.Throws<ArgumentException>(() => acceleration.ToString("xx/s²:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration.ToString should throw when outer time specifier is invalid")]
    public void AccelerationToStringShouldThrowWhenOuterTimeSpecifierIsInvalid()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then — compound form: speed-spec "km/h" is valid, outer time-spec "xx" is rejected by Time.ValueOf.
        Assert.Throws<ArgumentException>(() => acceleration.ToString("km/h/xx:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Acceleration.ToString should throw when inner speed time specifier is invalid")]
    public void AccelerationToStringShouldThrowWhenInnerSpeedTimeSpecifierIsInvalid()
    {
        // Given
        Acceleration<Float128> acceleration = MetersPerSecondSquared((Float128)9);

        // Then — compound form: inner speed-spec "km/xx" fails Time.ValueOf when Speed.ValueOf is called.
        Assert.Throws<ArgumentException>(() => acceleration.ToString("km/xx/s:3", CultureInfo.InvariantCulture));
    }
}
