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

public sealed class ElectricCapacitanceTests
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

    private static ElectricCapacitance<Float128> Farads(Float128 f) =>
        new(Coulombs(f), Volts((Float128)1));

    [Fact(DisplayName = "ElectricCapacitance should preserve its underlying Charge and Potential components")]
    public void ElectricCapacitanceShouldPreserveUnderlyingComponents()
    {
        ElectricCharge<Float128> charge = Coulombs((Float128)10);
        ElectricPotential<Float128> potential = Volts((Float128)2);
        ElectricCapacitance<Float128> capacitance = new(charge, potential);
        Assert.Equal(charge, capacitance.Left);
        Assert.Equal(potential, capacitance.Right);
    }

    [Fact(DisplayName = "ElectricCapacitance.Zero should produce zero magnitude")]
    public void ElectricCapacitanceZeroShouldProduceZeroMagnitude() =>
        Assert.Equal(Float128.Zero, ElectricCapacitance<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "ElectricCapacitance magnitude should land at human-readable farad scale")]
    public void ElectricCapacitanceMagnitudeShouldBeReadable()
    {
        // 10 C / 2 V = 5 F
        ElectricCapacitance<Float128> c = new(Coulombs((Float128)10), Volts((Float128)2));
        Assert.Equal((Float128)5, c.SIBaseValue);
    }

    [Fact(DisplayName = "ElectricCapacitance.Add should produce the expected magnitude")]
    public void ElectricCapacitanceAddShouldProduceExpectedValue()
    {
        ElectricCapacitance<Float128> result = ElectricCapacitance<Float128>.Add(Farads((Float128)5), Farads((Float128)3));
        Assert.Equal((Float128)8, result.SIBaseValue);
    }

    [Fact(DisplayName = "ElectricCapacitance.Subtract should produce the expected magnitude")]
    public void ElectricCapacitanceSubtractShouldProduceExpectedValue()
    {
        ElectricCapacitance<Float128> result = ElectricCapacitance<Float128>.Subtract(Farads((Float128)10), Farads((Float128)3));
        Assert.Equal((Float128)7, result.SIBaseValue);
    }

    [Fact(DisplayName = "ElectricCapacitance equality should be by magnitude (proportional components)")]
    public void ElectricCapacitanceEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // 10 C / 2 V = 5 F == 20 C / 4 V = 5 F
        ElectricCapacitance<Float128> left = new(Coulombs((Float128)10), Volts((Float128)2));
        ElectricCapacitance<Float128> right = new(Coulombs((Float128)20), Volts((Float128)4));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "ElectricCapacitance.CompareTo should produce the expected results")]
    public void ElectricCapacitanceCompareToShouldProduceExpectedResults()
    {
        Assert.Equal(0, Farads((Float128)5).CompareTo(Farads((Float128)5)));
        Assert.Equal(1, Farads((Float128)10).CompareTo(Farads((Float128)5)));
        Assert.Equal(-1, Farads((Float128)1).CompareTo(Farads((Float128)5)));
        Assert.Throws<ArgumentException>(() => Farads((Float128)5).CompareTo("not a capacitance"));
    }

    [Fact(DisplayName = "ElectricCapacitance.ToString basic")]
    public void ElectricCapacitanceToStringBasic() =>
        Assert.Equal("5.000 A*s/kg*m/s²*m/A*s", Farads((Float128)5).ToString("A*s/kg*m/s²*m/A*s:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "ElectricCapacitance.ToString should throw on no separator")]
    public void ElectricCapacitanceToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => Farads((Float128)5).ToString("nosep", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "ElectricCapacitance default ToString should use the F alias")]
    public void ElectricCapacitanceDefaultToStringShouldUseFaradAlias() =>
        Assert.EndsWith(" F", Farads((Float128)5).ToString());

    [Fact(DisplayName = "ElectricCapacitance.ToString F alias should produce '5.000 F'")]
    public void ElectricCapacitanceToStringFaradAliasShouldProduceExpected() =>
        Assert.Equal("5.000 F", Farads((Float128)5).ToString("F:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "ElectricCapacitance.ToString uF alias renders µF and should produce '5,000,000.000 µF'")]
    public void ElectricCapacitanceToStringMicrofaradAliasShouldProduceExpected() =>
        Assert.Equal("5,000,000.000 µF", Farads((Float128)5).ToString("uF:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "ElectricCapacitance.ToString nF alias should produce '5,000,000,000.000 nF'")]
    public void ElectricCapacitanceToStringNanofaradAliasShouldProduceExpected() =>
        Assert.Equal("5,000,000,000.000 nF", Farads((Float128)5).ToString("nF:3", CultureInfo.InvariantCulture));
}
