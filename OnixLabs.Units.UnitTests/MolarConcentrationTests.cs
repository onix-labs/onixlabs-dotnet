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

public sealed class MolarConcentrationTests
{
    private static MolarConcentration<Float128> MolesPerCubicMeter(Float128 c) =>
        new(AmountOfSubstance<Float128>.FromMoles(c), Volume<Float128>.FromCubicMeters((Float128)1));

    [Fact(DisplayName = "MolarConcentration should preserve components")]
    public void MolarConcentrationShouldPreserveComponents()
    {
        AmountOfSubstance<Float128> amount = AmountOfSubstance<Float128>.FromMoles((Float128)10);
        Volume<Float128> volume = Volume<Float128>.FromCubicMeters((Float128)2);
        MolarConcentration<Float128> c = new(amount, volume);
        Assert.Equal(amount, c.Left);
        Assert.Equal(volume, c.Right);
    }

    [Fact(DisplayName = "MolarConcentration.Zero magnitude is zero")]
    public void MolarConcentrationZeroIsZero() => Assert.Equal(Float128.Zero, MolarConcentration<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "MolarConcentration magnitude lands at mol/m^3")]
    public void MolarConcentrationMagnitudeIsReadable()
    {
        // 10 mol / 2 m³ = 5 mol/m³
        MolarConcentration<Float128> c = new(AmountOfSubstance<Float128>.FromMoles((Float128)10), Volume<Float128>.FromCubicMeters((Float128)2));
        Assert.Equal((Float128)5, c.SIBaseValue);
    }

    [Fact(DisplayName = "MolarConcentration.Add produces expected value")]
    public void MolarConcentrationAddProducesExpected()
    {
        MolarConcentration<Float128> r = MolarConcentration<Float128>.Add(MolesPerCubicMeter((Float128)5), MolesPerCubicMeter((Float128)3));
        Assert.Equal((Float128)8, r.SIBaseValue);
    }

    [Fact(DisplayName = "MolarConcentration.Subtract produces expected value")]
    public void MolarConcentrationSubtractProducesExpected()
    {
        MolarConcentration<Float128> r = MolarConcentration<Float128>.Subtract(MolesPerCubicMeter((Float128)10), MolesPerCubicMeter((Float128)3));
        Assert.Equal((Float128)7, r.SIBaseValue);
    }

    [Fact(DisplayName = "MolarConcentration equality is by magnitude")]
    public void MolarConcentrationEqualityIsByMagnitude()
    {
        // 10 mol / 2 m³ = 5 mol/m³ == 15 mol / 3 m³ = 5 mol/m³
        MolarConcentration<Float128> left = new(AmountOfSubstance<Float128>.FromMoles((Float128)10), Volume<Float128>.FromCubicMeters((Float128)2));
        MolarConcentration<Float128> right = new(AmountOfSubstance<Float128>.FromMoles((Float128)15), Volume<Float128>.FromCubicMeters((Float128)3));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "MolarConcentration CompareTo")]
    public void MolarConcentrationCompareTo()
    {
        Assert.Equal(0, MolesPerCubicMeter((Float128)5).CompareTo(MolesPerCubicMeter((Float128)5)));
        Assert.Equal(1, MolesPerCubicMeter((Float128)10).CompareTo(MolesPerCubicMeter((Float128)5)));
        Assert.Equal(-1, MolesPerCubicMeter((Float128)1).CompareTo(MolesPerCubicMeter((Float128)5)));
        Assert.Throws<ArgumentException>(() => MolesPerCubicMeter((Float128)5).CompareTo("not"));
    }

    [Fact(DisplayName = "MolarConcentration.ToString basic")]
    public void MolarConcentrationToStringBasic() =>
        Assert.Equal("50.000 mol/cum", MolesPerCubicMeter((Float128)50).ToString("mol/cum:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "MolarConcentration.ToString throws on no separator")]
    public void MolarConcentrationToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => MolesPerCubicMeter((Float128)5).ToString("molcum", CultureInfo.InvariantCulture));
}
