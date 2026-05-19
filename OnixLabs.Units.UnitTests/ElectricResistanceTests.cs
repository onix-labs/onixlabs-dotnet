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

public sealed class ElectricResistanceTests
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

    private static ElectricResistance<Float128> Ohms(Float128 r) =>
        new(Volts(r), Current<Float128>.FromAmperes((Float128)1));

    [Fact(DisplayName = "ElectricResistance should preserve its underlying Potential and Current components")]
    public void ElectricResistanceShouldPreserveUnderlyingComponents()
    {
        ElectricPotential<Float128> potential = Volts((Float128)10);
        Current<Float128> current = Current<Float128>.FromAmperes((Float128)2);
        ElectricResistance<Float128> resistance = new(potential, current);
        Assert.Equal(potential, resistance.Left);
        Assert.Equal(current, resistance.Right);
    }

    [Fact(DisplayName = "ElectricResistance.Zero should produce zero magnitude")]
    public void ElectricResistanceZeroShouldProduceZeroMagnitude() =>
        Assert.Equal(Float128.Zero, ElectricResistance<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "ElectricResistance magnitude should land at human-readable ohm scale")]
    public void ElectricResistanceMagnitudeShouldBeReadable()
    {
        // 10 V / 2 A = 5 Ω
        ElectricResistance<Float128> r = new(Volts((Float128)10), Current<Float128>.FromAmperes((Float128)2));
        Assert.Equal((Float128)5, r.Magnitude);
    }

    [Fact(DisplayName = "ElectricResistance.Add should produce the expected magnitude")]
    public void ElectricResistanceAddShouldProduceExpectedValue()
    {
        ElectricResistance<Float128> result = ElectricResistance<Float128>.Add(Ohms((Float128)5), Ohms((Float128)3));
        Assert.Equal((Float128)8, result.Magnitude);
    }

    [Fact(DisplayName = "ElectricResistance.Subtract should produce the expected magnitude")]
    public void ElectricResistanceSubtractShouldProduceExpectedValue()
    {
        ElectricResistance<Float128> result = ElectricResistance<Float128>.Subtract(Ohms((Float128)10), Ohms((Float128)3));
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "ElectricResistance equality should be by magnitude (proportional components)")]
    public void ElectricResistanceEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // 10 V / 2 A = 5 Ω == 20 V / 4 A = 5 Ω
        ElectricResistance<Float128> left = new(Volts((Float128)10), Current<Float128>.FromAmperes((Float128)2));
        ElectricResistance<Float128> right = new(Volts((Float128)20), Current<Float128>.FromAmperes((Float128)4));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "ElectricResistance.CompareTo should produce the expected results")]
    public void ElectricResistanceCompareToShouldProduceExpectedResults()
    {
        Assert.Equal(0, Ohms((Float128)5).CompareTo(Ohms((Float128)5)));
        Assert.Equal(1, Ohms((Float128)10).CompareTo(Ohms((Float128)5)));
        Assert.Equal(-1, Ohms((Float128)1).CompareTo(Ohms((Float128)5)));
        Assert.Throws<ArgumentException>(() => Ohms((Float128)5).CompareTo("not a resistance"));
    }

    [Fact(DisplayName = "ElectricResistance.ToString basic")]
    public void ElectricResistanceToStringBasic() =>
        Assert.Equal("5.000 kg*m/s²*m/A*s/A", Ohms((Float128)5).ToString("kg*m/s²*m/A*s/A:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "ElectricResistance.ToString should throw on no separator")]
    public void ElectricResistanceToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => Ohms((Float128)5).ToString("nosep", CultureInfo.InvariantCulture));
}
