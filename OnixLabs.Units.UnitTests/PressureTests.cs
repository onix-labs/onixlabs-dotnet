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

public sealed class PressureTests
{
    private static Acceleration<Float128> OneMetrePerSecondSquared => new(
        new Speed<Float128>(Distance<Float128>.FromMeters((Float128)1), Time<Float128>.FromSeconds((Float128)1)),
        Time<Float128>.FromSeconds((Float128)1));

    private static Force<Float128> NewtonsForce(Float128 newtons) =>
        new(Mass<Float128>.FromKilograms(newtons), OneMetrePerSecondSquared);

    private static Pressure<Float128> Pascals(Float128 pascals) =>
        new(NewtonsForce(pascals), Area<Float128>.FromSquareMeters((Float128)1));

    [Fact(DisplayName = "Pressure should preserve its underlying Force and Area components")]
    public void PressureShouldPreserveUnderlyingComponents()
    {
        Force<Float128> force = NewtonsForce((Float128)10);
        Area<Float128> area = Area<Float128>.FromSquareMeters((Float128)2);
        Pressure<Float128> pressure = new(force, area);
        Assert.Equal(force, pressure.Left);
        Assert.Equal(area, pressure.Right);
    }

    [Fact(DisplayName = "Pressure.Zero should produce zero magnitude")]
    public void PressureZeroShouldProduceZeroMagnitude()
    {
        Assert.Equal(Float128.Zero, Pressure<Float128>.Zero.Magnitude);
    }

    [Fact(DisplayName = "Pressure magnitude should land at human-readable pascal scale")]
    public void PressureMagnitudeShouldBeReadablePascals()
    {
        // 10 N / 2 m² = 5 Pa
        Pressure<Float128> p = new(NewtonsForce((Float128)10), Area<Float128>.FromSquareMeters((Float128)2));
        Assert.Equal((Float128)5, p.Magnitude);
    }

    [Fact(DisplayName = "Pressure.Add should produce the expected magnitude")]
    public void PressureAddShouldProduceExpectedValue()
    {
        Pressure<Float128> result = Pressure<Float128>.Add(Pascals((Float128)5), Pascals((Float128)3));
        Assert.Equal((Float128)8, result.Magnitude);
    }

    [Fact(DisplayName = "Pressure.Subtract should produce the expected magnitude")]
    public void PressureSubtractShouldProduceExpectedValue()
    {
        Pressure<Float128> result = Pressure<Float128>.Subtract(Pascals((Float128)10), Pascals((Float128)3));
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "Pressure + operator should produce the expected result")]
    public void PressureAddOperatorShouldProduceExpectedValue()
    {
        Pressure<Float128> result = Pascals((Float128)5) + Pascals((Float128)3);
        Assert.Equal((Float128)8, result.Magnitude);
    }

    [Fact(DisplayName = "Pressure - operator should produce the expected result")]
    public void PressureSubtractOperatorShouldProduceExpectedValue()
    {
        Pressure<Float128> result = Pascals((Float128)10) - Pascals((Float128)3);
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "Pressure equality should be by magnitude (proportional components)")]
    public void PressureEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // 10 N / 2 m² = 5 Pa == 20 N / 4 m² = 5 Pa
        Pressure<Float128> left = new(NewtonsForce((Float128)10), Area<Float128>.FromSquareMeters((Float128)2));
        Pressure<Float128> right = new(NewtonsForce((Float128)20), Area<Float128>.FromSquareMeters((Float128)4));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Pressure inequality should hold for different magnitudes")]
    public void PressureInequalityShouldHoldForDifferentMagnitudes()
    {
        Assert.False(Pascals((Float128)5).Equals(Pascals((Float128)6)));
    }

    [Fact(DisplayName = "Pressure.CompareTo should produce the expected results")]
    public void PressureCompareToShouldProduceExpectedResults()
    {
        Assert.Equal(0, Pascals((Float128)5).CompareTo(Pascals((Float128)5)));
        Assert.Equal(1, Pascals((Float128)10).CompareTo(Pascals((Float128)5)));
        Assert.Equal(-1, Pascals((Float128)1).CompareTo(Pascals((Float128)5)));
        Assert.Equal(1, Pascals((Float128)5).CompareTo(null));
        Assert.Throws<ArgumentException>(() => Pascals((Float128)5).CompareTo("not a pressure"));
    }

    [Fact(DisplayName = "Pressure.ToString with kg*m/s²/sqm:3 should produce the expected result")]
    public void PressureToStringShouldProduceExpectedResult()
    {
        Assert.Equal("5.000 kg*m/s²/sqm", Pascals((Float128)5).ToString("kg*m/s²/sqm:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Pressure.ToString should throw on invalid format")]
    public void PressureToStringShouldThrowOnInvalidFormat()
    {
        Assert.Throws<FormatException>(() => Pascals((Float128)5).ToString("noseparator", CultureInfo.InvariantCulture));
        Assert.Throws<FormatException>(() => Pascals((Float128)5).ToString("kg*m/s²/sqm:abc", CultureInfo.InvariantCulture));
    }
}
