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

public sealed class MolarMassTests
{
    private static MolarMass<Float128> KilogramsPerMole(Float128 m) =>
        new(Mass<Float128>.FromKilograms(m), AmountOfSubstance<Float128>.FromMoles((Float128)1));

    [Fact(DisplayName = "MolarMass should preserve components")]
    public void MolarMassShouldPreserveComponents()
    {
        Mass<Float128> mass = Mass<Float128>.FromKilograms((Float128)10);
        AmountOfSubstance<Float128> amount = AmountOfSubstance<Float128>.FromMoles((Float128)2);
        MolarMass<Float128> m = new(mass, amount);
        Assert.Equal(mass, m.Left);
        Assert.Equal(amount, m.Right);
    }

    [Fact(DisplayName = "MolarMass.Zero magnitude is zero")]
    public void MolarMassZeroIsZero() => Assert.Equal(Float128.Zero, MolarMass<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "MolarMass magnitude lands at kg/mol")]
    public void MolarMassMagnitudeIsReadable()
    {
        // 10 kg / 2 mol = 5 kg/mol
        MolarMass<Float128> m = new(Mass<Float128>.FromKilograms((Float128)10), AmountOfSubstance<Float128>.FromMoles((Float128)2));
        Assert.Equal((Float128)5, m.SIBaseValue);
    }

    [Fact(DisplayName = "MolarMass.Add produces expected value")]
    public void MolarMassAddProducesExpected()
    {
        MolarMass<Float128> r = MolarMass<Float128>.Add(KilogramsPerMole((Float128)5), KilogramsPerMole((Float128)3));
        Assert.Equal((Float128)8, r.SIBaseValue);
    }

    [Fact(DisplayName = "MolarMass.Subtract produces expected value")]
    public void MolarMassSubtractProducesExpected()
    {
        MolarMass<Float128> r = MolarMass<Float128>.Subtract(KilogramsPerMole((Float128)10), KilogramsPerMole((Float128)3));
        Assert.Equal((Float128)7, r.SIBaseValue);
    }

    [Fact(DisplayName = "MolarMass equality is by magnitude")]
    public void MolarMassEqualityIsByMagnitude()
    {
        // 10 kg / 2 mol = 5 kg/mol == 15 kg / 3 mol = 5 kg/mol
        MolarMass<Float128> left = new(Mass<Float128>.FromKilograms((Float128)10), AmountOfSubstance<Float128>.FromMoles((Float128)2));
        MolarMass<Float128> right = new(Mass<Float128>.FromKilograms((Float128)15), AmountOfSubstance<Float128>.FromMoles((Float128)3));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "MolarMass CompareTo")]
    public void MolarMassCompareTo()
    {
        Assert.Equal(0, KilogramsPerMole((Float128)5).CompareTo(KilogramsPerMole((Float128)5)));
        Assert.Equal(1, KilogramsPerMole((Float128)10).CompareTo(KilogramsPerMole((Float128)5)));
        Assert.Equal(-1, KilogramsPerMole((Float128)1).CompareTo(KilogramsPerMole((Float128)5)));
        Assert.Throws<ArgumentException>(() => KilogramsPerMole((Float128)5).CompareTo("not"));
    }

    [Fact(DisplayName = "MolarMass.ToString basic")]
    public void MolarMassToStringBasic() =>
        Assert.Equal("50.000 kg/mol", KilogramsPerMole((Float128)50).ToString("kg/mol:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "MolarMass.ToString throws on no separator")]
    public void MolarMassToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => KilogramsPerMole((Float128)5).ToString("kgmol", CultureInfo.InvariantCulture));
}
