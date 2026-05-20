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

public sealed class LuminousFluxTests
{
    private static LuminousFlux<Float128> Lumens(Float128 lm) =>
        new(LuminousIntensity<Float128>.FromCandelas(lm), SolidAngle<Float128>.FromSteradians((Float128)1));

    [Fact(DisplayName = "LuminousFlux should preserve components")]
    public void LuminousFluxShouldPreserveComponents()
    {
        LuminousIntensity<Float128> intensity = LuminousIntensity<Float128>.FromCandelas((Float128)5);
        SolidAngle<Float128> solidAngle = SolidAngle<Float128>.FromSteradians((Float128)2);
        LuminousFlux<Float128> phi = new(intensity, solidAngle);
        Assert.Equal(intensity, phi.Left);
        Assert.Equal(solidAngle, phi.Right);
    }

    [Fact(DisplayName = "LuminousFlux.Zero magnitude is zero")]
    public void LuminousFluxZeroIsZero() => Assert.Equal(Float128.Zero, LuminousFlux<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "LuminousFlux magnitude lands at lumens (cd·sr)")]
    public void LuminousFluxMagnitudeIsReadable()
    {
        // 5 cd × 2 sr = 10 lm
        LuminousFlux<Float128> phi = new(LuminousIntensity<Float128>.FromCandelas((Float128)5), SolidAngle<Float128>.FromSteradians((Float128)2));
        Assert.Equal((Float128)10, phi.SIBaseValue);
    }

    [Fact(DisplayName = "LuminousFlux.Add produces expected value")]
    public void LuminousFluxAddProducesExpected()
    {
        LuminousFlux<Float128> r = LuminousFlux<Float128>.Add(Lumens((Float128)5), Lumens((Float128)3));
        Assert.Equal((Float128)8, r.SIBaseValue);
    }

    [Fact(DisplayName = "LuminousFlux.Subtract produces expected value")]
    public void LuminousFluxSubtractProducesExpected()
    {
        LuminousFlux<Float128> r = LuminousFlux<Float128>.Subtract(Lumens((Float128)10), Lumens((Float128)3));
        Assert.Equal((Float128)7, r.SIBaseValue);
    }

    [Fact(DisplayName = "LuminousFlux equality is by magnitude")]
    public void LuminousFluxEqualityIsByMagnitude()
    {
        // 2 cd × 5 sr = 10 == 5 cd × 2 sr = 10
        LuminousFlux<Float128> left = new(LuminousIntensity<Float128>.FromCandelas((Float128)2), SolidAngle<Float128>.FromSteradians((Float128)5));
        LuminousFlux<Float128> right = new(LuminousIntensity<Float128>.FromCandelas((Float128)5), SolidAngle<Float128>.FromSteradians((Float128)2));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "LuminousFlux CompareTo")]
    public void LuminousFluxCompareTo()
    {
        Assert.Equal(0, Lumens((Float128)5).CompareTo(Lumens((Float128)5)));
        Assert.Equal(1, Lumens((Float128)10).CompareTo(Lumens((Float128)5)));
        Assert.Equal(-1, Lumens((Float128)1).CompareTo(Lumens((Float128)5)));
        Assert.Throws<ArgumentException>(() => Lumens((Float128)5).CompareTo("not"));
    }

    [Fact(DisplayName = "LuminousFlux.ToString basic")]
    public void LuminousFluxToStringBasic() =>
        Assert.Equal("50.000 cd*sr", Lumens((Float128)50).ToString("cd*sr:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "LuminousFlux.ToString throws on no separator")]
    public void LuminousFluxToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => Lumens((Float128)5).ToString("cdsr", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "LuminousFlux default ToString should use the lm alias")]
    public void LuminousFluxDefaultToStringShouldUseLumenAlias() =>
        Assert.EndsWith(" lm", Lumens((Float128)50).ToString());

    [Fact(DisplayName = "LuminousFlux.ToString lm alias should produce '50.000 lm'")]
    public void LuminousFluxToStringLumenAliasShouldProduceExpected() =>
        Assert.Equal("50.000 lm", Lumens((Float128)50).ToString("lm:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "LuminousFlux.ToString klm alias should produce '0.050 klm'")]
    public void LuminousFluxToStringKilolumenAliasShouldProduceExpected() =>
        Assert.Equal("0.050 klm", Lumens((Float128)50).ToString("klm:3", CultureInfo.InvariantCulture));
}
