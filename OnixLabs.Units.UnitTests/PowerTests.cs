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

public sealed class PowerTests
{
    private static Acceleration<Float128> OneMetrePerSecondSquared => new(
        new Speed<Float128>(Distance<Float128>.FromMeters((Float128)1), Time<Float128>.FromSeconds((Float128)1)),
        Time<Float128>.FromSeconds((Float128)1));

    private static Force<Float128> NewtonsForce(Float128 newtons) =>
        new(Mass<Float128>.FromKilograms(newtons), OneMetrePerSecondSquared);

    private static Energy<Float128> Joules(Float128 joules) =>
        new(NewtonsForce(joules), Distance<Float128>.FromMeters((Float128)1));

    private static Power<Float128> Watts(Float128 watts) =>
        new(Joules(watts), Time<Float128>.FromSeconds((Float128)1));

    [Fact(DisplayName = "Power should preserve its underlying Energy and Time components")]
    public void PowerShouldPreserveUnderlyingComponents()
    {
        Energy<Float128> energy = Joules((Float128)10);
        Time<Float128> time = Time<Float128>.FromSeconds((Float128)2);
        Power<Float128> power = new(energy, time);
        Assert.Equal(energy, power.Left);
        Assert.Equal(time, power.Right);
    }

    [Fact(DisplayName = "Power.Zero should produce zero magnitude")]
    public void PowerZeroShouldProduceZeroMagnitude()
    {
        Assert.Equal(Float128.Zero, Power<Float128>.Zero.Magnitude);
    }

    [Fact(DisplayName = "Power magnitude should land at human-readable watt scale")]
    public void PowerMagnitudeShouldBeReadableWatts()
    {
        // 10 J / 2 s = 5 W
        Power<Float128> p = new(Joules((Float128)10), Time<Float128>.FromSeconds((Float128)2));
        Assert.Equal((Float128)5, p.SIBaseValue);
    }

    [Fact(DisplayName = "Power.Add should produce the expected magnitude")]
    public void PowerAddShouldProduceExpectedValue()
    {
        Power<Float128> result = Power<Float128>.Add(Watts((Float128)5), Watts((Float128)3));
        Assert.Equal((Float128)8, result.SIBaseValue);
    }

    [Fact(DisplayName = "Power.Subtract should produce the expected magnitude")]
    public void PowerSubtractShouldProduceExpectedValue()
    {
        Power<Float128> result = Power<Float128>.Subtract(Watts((Float128)10), Watts((Float128)3));
        Assert.Equal((Float128)7, result.SIBaseValue);
    }

    [Fact(DisplayName = "Power equality should be by magnitude (proportional components)")]
    public void PowerEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // 10 J / 2 s = 5 W == 20 J / 4 s = 5 W
        Power<Float128> left = new(Joules((Float128)10), Time<Float128>.FromSeconds((Float128)2));
        Power<Float128> right = new(Joules((Float128)20), Time<Float128>.FromSeconds((Float128)4));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Power.CompareTo should produce the expected results")]
    public void PowerCompareToShouldProduceExpectedResults()
    {
        Assert.Equal(0, Watts((Float128)5).CompareTo(Watts((Float128)5)));
        Assert.Equal(1, Watts((Float128)10).CompareTo(Watts((Float128)5)));
        Assert.Equal(-1, Watts((Float128)1).CompareTo(Watts((Float128)5)));
        Assert.Throws<ArgumentException>(() => Watts((Float128)5).CompareTo("not a power"));
    }

    [Fact(DisplayName = "Power.ToString basic")]
    public void PowerToStringBasic() =>
        Assert.Equal("5.000 kg*m/s²*m/s", Watts((Float128)5).ToString("kg*m/s²*m/s:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Power.ToString should throw on no separator")]
    public void PowerToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => Watts((Float128)5).ToString("nosep", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Power default ToString should use the W alias")]
    public void PowerDefaultToStringShouldUseWattAlias() =>
        Assert.EndsWith(" W", Watts((Float128)9).ToString());

    [Fact(DisplayName = "Power.ToString W alias should produce '9.000 W'")]
    public void PowerToStringWattAliasShouldProduceExpected() =>
        Assert.Equal("9.000 W", Watts((Float128)9).ToString("W:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Power.ToString kW alias should produce '0.009 kW'")]
    public void PowerToStringKilowattAliasShouldProduceExpected() =>
        Assert.Equal("0.009 kW", Watts((Float128)9).ToString("kW:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Power.ToString MW alias should produce '0.000009 MW'")]
    public void PowerToStringMegawattAliasShouldProduceExpected() =>
        Assert.Equal("0.000009 MW", Watts((Float128)9).ToString("MW:6", CultureInfo.InvariantCulture));
}
