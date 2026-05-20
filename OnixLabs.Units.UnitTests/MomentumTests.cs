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

public sealed class MomentumTests
{
    private static Speed<Float128> MetersPerSecond(Float128 mps) =>
        new(Distance<Float128>.FromMeters(mps), Time<Float128>.FromSeconds((Float128)1));

    private static Momentum<Float128> KilogramMetersPerSecond(Float128 p) =>
        new(Mass<Float128>.FromKilograms(p), MetersPerSecond((Float128)1));

    [Fact(DisplayName = "Momentum should preserve components")]
    public void MomentumShouldPreserveComponents()
    {
        Mass<Float128> mass = Mass<Float128>.FromKilograms((Float128)5);
        Speed<Float128> speed = MetersPerSecond((Float128)10);
        Momentum<Float128> p = new(mass, speed);
        Assert.Equal(mass, p.Left);
        Assert.Equal(speed, p.Right);
    }

    [Fact(DisplayName = "Momentum.Zero magnitude is zero")]
    public void MomentumZeroIsZero() => Assert.Equal(Float128.Zero, Momentum<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "Momentum magnitude lands at kg·m/s")]
    public void MomentumMagnitudeIsReadable()
    {
        // 5 kg × 10 m/s = 50 kg·m/s
        Momentum<Float128> p = new(Mass<Float128>.FromKilograms((Float128)5), MetersPerSecond((Float128)10));
        Assert.Equal((Float128)50, p.SIBaseValue);
    }

    [Fact(DisplayName = "Momentum.Add produces expected value")]
    public void MomentumAddProducesExpected()
    {
        Momentum<Float128> r = Momentum<Float128>.Add(KilogramMetersPerSecond((Float128)5), KilogramMetersPerSecond((Float128)3));
        Assert.Equal((Float128)8, r.SIBaseValue);
    }

    [Fact(DisplayName = "Momentum.Subtract produces expected value")]
    public void MomentumSubtractProducesExpected()
    {
        Momentum<Float128> r = Momentum<Float128>.Subtract(KilogramMetersPerSecond((Float128)10), KilogramMetersPerSecond((Float128)3));
        Assert.Equal((Float128)7, r.SIBaseValue);
    }

    [Fact(DisplayName = "Momentum equality is by magnitude")]
    public void MomentumEqualityIsByMagnitude()
    {
        // 2 kg × 5 m/s = 10 == 5 kg × 2 m/s = 10
        Momentum<Float128> left = new(Mass<Float128>.FromKilograms((Float128)2), MetersPerSecond((Float128)5));
        Momentum<Float128> right = new(Mass<Float128>.FromKilograms((Float128)5), MetersPerSecond((Float128)2));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Momentum CompareTo")]
    public void MomentumCompareTo()
    {
        Assert.Equal(0, KilogramMetersPerSecond((Float128)5).CompareTo(KilogramMetersPerSecond((Float128)5)));
        Assert.Equal(1, KilogramMetersPerSecond((Float128)10).CompareTo(KilogramMetersPerSecond((Float128)5)));
        Assert.Equal(-1, KilogramMetersPerSecond((Float128)1).CompareTo(KilogramMetersPerSecond((Float128)5)));
        Assert.Throws<ArgumentException>(() => KilogramMetersPerSecond((Float128)5).CompareTo("not"));
    }

    [Fact(DisplayName = "Momentum.ToString basic")]
    public void MomentumToStringBasic() =>
        Assert.Equal("50.000 kg*m/s", KilogramMetersPerSecond((Float128)50).ToString("kg*m/s:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Momentum.ToString throws on no separator")]
    public void MomentumToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => KilogramMetersPerSecond((Float128)5).ToString("kgms", CultureInfo.InvariantCulture));
}
