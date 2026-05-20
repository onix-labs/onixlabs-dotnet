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

public sealed class IlluminanceTests
{
    private static LuminousFlux<Float128> Lumens(Float128 lm) =>
        new(LuminousIntensity<Float128>.FromCandelas(lm), SolidAngle<Float128>.FromSteradians((Float128)1));

    private static Illuminance<Float128> Lux(Float128 lx) =>
        new(Lumens(lx), Area<Float128>.FromSquareMeters((Float128)1));

    [Fact(DisplayName = "Illuminance should preserve its underlying LuminousFlux and Area components")]
    public void IlluminanceShouldPreserveUnderlyingComponents()
    {
        LuminousFlux<Float128> flux = Lumens((Float128)10);
        Area<Float128> area = Area<Float128>.FromSquareMeters((Float128)2);
        Illuminance<Float128> illuminance = new(flux, area);
        Assert.Equal(flux, illuminance.Left);
        Assert.Equal(area, illuminance.Right);
    }

    [Fact(DisplayName = "Illuminance.Zero should produce zero magnitude")]
    public void IlluminanceZeroShouldProduceZeroMagnitude() =>
        Assert.Equal(Float128.Zero, Illuminance<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "Illuminance magnitude should land at human-readable lux scale")]
    public void IlluminanceMagnitudeShouldBeReadable()
    {
        // 10 lm / 2 m² = 5 lx
        Illuminance<Float128> e = new(Lumens((Float128)10), Area<Float128>.FromSquareMeters((Float128)2));
        Assert.Equal((Float128)5, e.SIBaseValue);
    }

    [Fact(DisplayName = "Illuminance.Add should produce the expected magnitude")]
    public void IlluminanceAddShouldProduceExpectedValue()
    {
        Illuminance<Float128> result = Illuminance<Float128>.Add(Lux((Float128)5), Lux((Float128)3));
        Assert.Equal((Float128)8, result.SIBaseValue);
    }

    [Fact(DisplayName = "Illuminance.Subtract should produce the expected magnitude")]
    public void IlluminanceSubtractShouldProduceExpectedValue()
    {
        Illuminance<Float128> result = Illuminance<Float128>.Subtract(Lux((Float128)10), Lux((Float128)3));
        Assert.Equal((Float128)7, result.SIBaseValue);
    }

    [Fact(DisplayName = "Illuminance equality should be by magnitude (proportional components)")]
    public void IlluminanceEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // 10 lm / 2 m² = 5 lx == 20 lm / 4 m² = 5 lx
        Illuminance<Float128> left = new(Lumens((Float128)10), Area<Float128>.FromSquareMeters((Float128)2));
        Illuminance<Float128> right = new(Lumens((Float128)20), Area<Float128>.FromSquareMeters((Float128)4));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Illuminance.CompareTo should produce the expected results")]
    public void IlluminanceCompareToShouldProduceExpectedResults()
    {
        Assert.Equal(0, Lux((Float128)5).CompareTo(Lux((Float128)5)));
        Assert.Equal(1, Lux((Float128)10).CompareTo(Lux((Float128)5)));
        Assert.Equal(-1, Lux((Float128)1).CompareTo(Lux((Float128)5)));
        Assert.Throws<ArgumentException>(() => Lux((Float128)5).CompareTo("not an illuminance"));
    }

    [Fact(DisplayName = "Illuminance.ToString basic")]
    public void IlluminanceToStringBasic() =>
        Assert.Equal("5.000 cd*sr/sqm", Lux((Float128)5).ToString("cd*sr/sqm:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Illuminance.ToString should throw on no separator")]
    public void IlluminanceToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => Lux((Float128)5).ToString("nosep", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Illuminance default ToString should use the lx alias")]
    public void IlluminanceDefaultToStringShouldUseLuxAlias() =>
        Assert.EndsWith(" lx", Lux((Float128)5).ToString());

    [Fact(DisplayName = "Illuminance.ToString lx alias should produce '5.000 lx'")]
    public void IlluminanceToStringLuxAliasShouldProduceExpected() =>
        Assert.Equal("5.000 lx", Lux((Float128)5).ToString("lx:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Illuminance.ToString klx alias should produce '0.005 klx'")]
    public void IlluminanceToStringKiloluxAliasShouldProduceExpected() =>
        Assert.Equal("0.005 klx", Lux((Float128)5).ToString("klx:3", CultureInfo.InvariantCulture));
}
