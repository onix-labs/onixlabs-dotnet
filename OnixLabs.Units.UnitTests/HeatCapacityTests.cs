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

public sealed class HeatCapacityTests
{
    private static Acceleration<Float128> OneMetrePerSecondSquared => new(
        new Speed<Float128>(Distance<Float128>.FromMeters((Float128)1), Time<Float128>.FromSeconds((Float128)1)),
        Time<Float128>.FromSeconds((Float128)1));

    private static Force<Float128> NewtonsForce(Float128 newtons) =>
        new(Mass<Float128>.FromKilograms(newtons), OneMetrePerSecondSquared);

    private static Energy<Float128> Joules(Float128 joules) =>
        new(NewtonsForce(joules), Distance<Float128>.FromMeters((Float128)1));

    private static HeatCapacity<Float128> JoulesPerKelvin(Float128 jk) =>
        new(Joules(jk), Temperature<Float128>.FromKelvin((Float128)1));

    [Fact(DisplayName = "HeatCapacity should preserve its underlying Energy and Temperature components")]
    public void HeatCapacityShouldPreserveUnderlyingComponents()
    {
        Energy<Float128> energy = Joules((Float128)10);
        Temperature<Float128> temperature = Temperature<Float128>.FromKelvin((Float128)2);
        HeatCapacity<Float128> heatCapacity = new(energy, temperature);
        Assert.Equal(energy, heatCapacity.Left);
        Assert.Equal(temperature, heatCapacity.Right);
    }

    [Fact(DisplayName = "HeatCapacity.Zero should produce zero magnitude")]
    public void HeatCapacityZeroShouldProduceZeroMagnitude() =>
        Assert.Equal(Float128.Zero, HeatCapacity<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "HeatCapacity magnitude should land at human-readable J/K scale")]
    public void HeatCapacityMagnitudeShouldBeReadable()
    {
        // 10 J / 2 K = 5 J/K
        HeatCapacity<Float128> hc = new(Joules((Float128)10), Temperature<Float128>.FromKelvin((Float128)2));
        Assert.Equal((Float128)5, hc.Magnitude);
    }

    [Fact(DisplayName = "HeatCapacity.Add should produce the expected magnitude")]
    public void HeatCapacityAddShouldProduceExpectedValue()
    {
        HeatCapacity<Float128> result = HeatCapacity<Float128>.Add(JoulesPerKelvin((Float128)5), JoulesPerKelvin((Float128)3));
        Assert.Equal((Float128)8, result.Magnitude);
    }

    [Fact(DisplayName = "HeatCapacity.Subtract should produce the expected magnitude")]
    public void HeatCapacitySubtractShouldProduceExpectedValue()
    {
        HeatCapacity<Float128> result = HeatCapacity<Float128>.Subtract(JoulesPerKelvin((Float128)10), JoulesPerKelvin((Float128)3));
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "HeatCapacity equality should be by magnitude (proportional components)")]
    public void HeatCapacityEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // 10 J / 2 K = 5 J/K == 20 J / 4 K = 5 J/K
        HeatCapacity<Float128> left = new(Joules((Float128)10), Temperature<Float128>.FromKelvin((Float128)2));
        HeatCapacity<Float128> right = new(Joules((Float128)20), Temperature<Float128>.FromKelvin((Float128)4));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "HeatCapacity.CompareTo should produce the expected results")]
    public void HeatCapacityCompareToShouldProduceExpectedResults()
    {
        Assert.Equal(0, JoulesPerKelvin((Float128)5).CompareTo(JoulesPerKelvin((Float128)5)));
        Assert.Equal(1, JoulesPerKelvin((Float128)10).CompareTo(JoulesPerKelvin((Float128)5)));
        Assert.Equal(-1, JoulesPerKelvin((Float128)1).CompareTo(JoulesPerKelvin((Float128)5)));
        Assert.Throws<ArgumentException>(() => JoulesPerKelvin((Float128)5).CompareTo("not a heat capacity"));
    }

    [Fact(DisplayName = "HeatCapacity.ToString basic")]
    public void HeatCapacityToStringBasic() =>
        Assert.Equal("5.000 kg*m/s²*m/K", JoulesPerKelvin((Float128)5).ToString("kg*m/s²*m/K:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "HeatCapacity.ToString should throw on no separator")]
    public void HeatCapacityToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => JoulesPerKelvin((Float128)5).ToString("nosep", CultureInfo.InvariantCulture));
}
