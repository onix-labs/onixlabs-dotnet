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

public sealed class ElectricPotentialTests
{
    private static Acceleration<Float128> OneMetrePerSecondSquared => new(
        new Speed<Float128>(Distance<Float128>.FromMeters((Float128)1), Time<Float128>.FromSeconds((Float128)1)),
        Time<Float128>.FromSeconds((Float128)1));

    private static Force<Float128> NewtonsForce(Float128 newtons) =>
        new(Mass<Float128>.FromKilograms(newtons), OneMetrePerSecondSquared);

    private static Energy<Float128> Joules(Float128 joules) =>
        new(NewtonsForce(joules), Distance<Float128>.FromMeters((Float128)1));

    private static ElectricCharge<Float128> Coulombs(Float128 c) =>
        new(Current<Float128>.FromAmperes(c), Time<Float128>.FromSeconds((Float128)1));

    private static ElectricPotential<Float128> Volts(Float128 v) =>
        new(Joules(v), Coulombs((Float128)1));

    [Fact(DisplayName = "ElectricPotential should preserve its underlying Energy and Charge components")]
    public void ElectricPotentialShouldPreserveUnderlyingComponents()
    {
        Energy<Float128> energy = Joules((Float128)10);
        ElectricCharge<Float128> charge = Coulombs((Float128)2);
        ElectricPotential<Float128> potential = new(energy, charge);
        Assert.Equal(energy, potential.Left);
        Assert.Equal(charge, potential.Right);
    }

    [Fact(DisplayName = "ElectricPotential.Zero should produce zero magnitude")]
    public void ElectricPotentialZeroShouldProduceZeroMagnitude() =>
        Assert.Equal(Float128.Zero, ElectricPotential<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "ElectricPotential magnitude should land at human-readable volt scale")]
    public void ElectricPotentialMagnitudeShouldBeReadableVolts()
    {
        // 10 J / 2 C = 5 V
        ElectricPotential<Float128> v = new(Joules((Float128)10), Coulombs((Float128)2));
        Assert.Equal((Float128)5, v.Magnitude);
    }

    [Fact(DisplayName = "ElectricPotential.Add should produce the expected magnitude")]
    public void ElectricPotentialAddShouldProduceExpectedValue()
    {
        ElectricPotential<Float128> result = ElectricPotential<Float128>.Add(Volts((Float128)5), Volts((Float128)3));
        Assert.Equal((Float128)8, result.Magnitude);
    }

    [Fact(DisplayName = "ElectricPotential.Subtract should produce the expected magnitude")]
    public void ElectricPotentialSubtractShouldProduceExpectedValue()
    {
        ElectricPotential<Float128> result = ElectricPotential<Float128>.Subtract(Volts((Float128)10), Volts((Float128)3));
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "ElectricPotential equality should be by magnitude (proportional components)")]
    public void ElectricPotentialEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // 10 J / 2 C = 5 V == 20 J / 4 C = 5 V
        ElectricPotential<Float128> left = new(Joules((Float128)10), Coulombs((Float128)2));
        ElectricPotential<Float128> right = new(Joules((Float128)20), Coulombs((Float128)4));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "ElectricPotential.CompareTo should produce the expected results")]
    public void ElectricPotentialCompareToShouldProduceExpectedResults()
    {
        Assert.Equal(0, Volts((Float128)5).CompareTo(Volts((Float128)5)));
        Assert.Equal(1, Volts((Float128)10).CompareTo(Volts((Float128)5)));
        Assert.Equal(-1, Volts((Float128)1).CompareTo(Volts((Float128)5)));
        Assert.Throws<ArgumentException>(() => Volts((Float128)5).CompareTo("not a potential"));
    }

    [Fact(DisplayName = "ElectricPotential.ToString basic")]
    public void ElectricPotentialToStringBasic() =>
        Assert.Equal("5.000 kg*m/s²*m/A*s", Volts((Float128)5).ToString("kg*m/s²*m/A*s:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "ElectricPotential.ToString should throw on no separator")]
    public void ElectricPotentialToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => Volts((Float128)5).ToString("nosep", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "ElectricPotential default ToString should use the V alias")]
    public void ElectricPotentialDefaultToStringShouldUseVoltAlias() =>
        Assert.EndsWith(" V", Volts((Float128)5).ToString());

    [Fact(DisplayName = "ElectricPotential.ToString V alias should produce '5.000 V'")]
    public void ElectricPotentialToStringVoltAliasShouldProduceExpected() =>
        Assert.Equal("5.000 V", Volts((Float128)5).ToString("V:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "ElectricPotential.ToString mV alias should produce '5,000.000 mV'")]
    public void ElectricPotentialToStringMillivoltAliasShouldProduceExpected() =>
        Assert.Equal("5,000.000 mV", Volts((Float128)5).ToString("mV:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "ElectricPotential.ToString kV alias should produce '0.005 kV'")]
    public void ElectricPotentialToStringKilovoltAliasShouldProduceExpected() =>
        Assert.Equal("0.005 kV", Volts((Float128)5).ToString("kV:3", CultureInfo.InvariantCulture));
}
