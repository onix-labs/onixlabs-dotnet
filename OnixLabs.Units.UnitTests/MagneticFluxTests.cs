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

public sealed class MagneticFluxTests
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

    private static MagneticFlux<Float128> Webers(Float128 wb) =>
        new(Volts(wb), Time<Float128>.FromSeconds((Float128)1));

    [Fact(DisplayName = "MagneticFlux should preserve its underlying Potential and Time components")]
    public void MagneticFluxShouldPreserveUnderlyingComponents()
    {
        ElectricPotential<Float128> potential = Volts((Float128)10);
        Time<Float128> time = Time<Float128>.FromSeconds((Float128)2);
        MagneticFlux<Float128> flux = new(potential, time);
        Assert.Equal(potential, flux.Left);
        Assert.Equal(time, flux.Right);
    }

    [Fact(DisplayName = "MagneticFlux.Zero should produce zero magnitude")]
    public void MagneticFluxZeroShouldProduceZeroMagnitude() =>
        Assert.Equal(Float128.Zero, MagneticFlux<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "MagneticFlux magnitude should land at human-readable weber scale")]
    public void MagneticFluxMagnitudeShouldBeReadable()
    {
        // 5 V × 2 s = 10 Wb
        MagneticFlux<Float128> wb = new(Volts((Float128)5), Time<Float128>.FromSeconds((Float128)2));
        Assert.Equal((Float128)10, wb.Magnitude);
    }

    [Fact(DisplayName = "MagneticFlux.Add should produce the expected magnitude")]
    public void MagneticFluxAddShouldProduceExpectedValue()
    {
        MagneticFlux<Float128> result = MagneticFlux<Float128>.Add(Webers((Float128)5), Webers((Float128)3));
        Assert.Equal((Float128)8, result.Magnitude);
    }

    [Fact(DisplayName = "MagneticFlux.Subtract should produce the expected magnitude")]
    public void MagneticFluxSubtractShouldProduceExpectedValue()
    {
        MagneticFlux<Float128> result = MagneticFlux<Float128>.Subtract(Webers((Float128)10), Webers((Float128)3));
        Assert.Equal((Float128)7, result.Magnitude);
    }

    [Fact(DisplayName = "MagneticFlux equality should be by magnitude (proportional components)")]
    public void MagneticFluxEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // 5 V × 2 s = 10 Wb == 2 V × 5 s = 10 Wb
        MagneticFlux<Float128> left = new(Volts((Float128)5), Time<Float128>.FromSeconds((Float128)2));
        MagneticFlux<Float128> right = new(Volts((Float128)2), Time<Float128>.FromSeconds((Float128)5));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "MagneticFlux.CompareTo should produce the expected results")]
    public void MagneticFluxCompareToShouldProduceExpectedResults()
    {
        Assert.Equal(0, Webers((Float128)5).CompareTo(Webers((Float128)5)));
        Assert.Equal(1, Webers((Float128)10).CompareTo(Webers((Float128)5)));
        Assert.Equal(-1, Webers((Float128)1).CompareTo(Webers((Float128)5)));
        Assert.Throws<ArgumentException>(() => Webers((Float128)5).CompareTo("not a flux"));
    }

    [Fact(DisplayName = "MagneticFlux.ToString basic")]
    public void MagneticFluxToStringBasic() =>
        Assert.Equal("5.000 kg*m/s²*m/A*s*s", Webers((Float128)5).ToString("kg*m/s²*m/A*s*s:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "MagneticFlux.ToString should throw on no separator")]
    public void MagneticFluxToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => Webers((Float128)5).ToString("nosep", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "MagneticFlux default ToString should use the Wb alias")]
    public void MagneticFluxDefaultToStringShouldUseWeberAlias() =>
        Assert.EndsWith(" Wb", Webers((Float128)5).ToString());

    [Fact(DisplayName = "MagneticFlux.ToString Wb alias should produce '5.000 Wb'")]
    public void MagneticFluxToStringWeberAliasShouldProduceExpected() =>
        Assert.Equal("5.000 Wb", Webers((Float128)5).ToString("Wb:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "MagneticFlux.ToString mWb alias should produce '5,000.000 mWb'")]
    public void MagneticFluxToStringMilliweberAliasShouldProduceExpected() =>
        Assert.Equal("5,000.000 mWb", Webers((Float128)5).ToString("mWb:3", CultureInfo.InvariantCulture));
}
