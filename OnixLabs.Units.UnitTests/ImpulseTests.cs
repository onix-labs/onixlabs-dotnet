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

public sealed class ImpulseTests
{
    private static Acceleration<Float128> OneMetrePerSecondSquared => new(
        new Speed<Float128>(Distance<Float128>.FromMeters((Float128)1), Time<Float128>.FromSeconds((Float128)1)),
        Time<Float128>.FromSeconds((Float128)1));

    private static Force<Float128> NewtonsForce(Float128 newtons) =>
        new(Mass<Float128>.FromKilograms(newtons), OneMetrePerSecondSquared);

    private static Impulse<Float128> NewtonSeconds(Float128 ns) =>
        new(NewtonsForce(ns), Time<Float128>.FromSeconds((Float128)1));

    [Fact(DisplayName = "Impulse should preserve components")]
    public void ImpulseShouldPreserveComponents()
    {
        Force<Float128> force = NewtonsForce((Float128)5);
        Time<Float128> time = Time<Float128>.FromSeconds((Float128)2);
        Impulse<Float128> j = new(force, time);
        Assert.Equal(force, j.Left);
        Assert.Equal(time, j.Right);
    }

    [Fact(DisplayName = "Impulse.Zero magnitude is zero")]
    public void ImpulseZeroIsZero() => Assert.Equal(Float128.Zero, Impulse<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "Impulse magnitude lands at N·s")]
    public void ImpulseMagnitudeIsReadable()
    {
        // 5 N × 2 s = 10 N·s
        Impulse<Float128> j = new(NewtonsForce((Float128)5), Time<Float128>.FromSeconds((Float128)2));
        Assert.Equal((Float128)10, j.Magnitude);
    }

    [Fact(DisplayName = "Impulse.Add produces expected value")]
    public void ImpulseAddProducesExpected()
    {
        Impulse<Float128> r = Impulse<Float128>.Add(NewtonSeconds((Float128)5), NewtonSeconds((Float128)3));
        Assert.Equal((Float128)8, r.Magnitude);
    }

    [Fact(DisplayName = "Impulse.Subtract produces expected value")]
    public void ImpulseSubtractProducesExpected()
    {
        Impulse<Float128> r = Impulse<Float128>.Subtract(NewtonSeconds((Float128)10), NewtonSeconds((Float128)3));
        Assert.Equal((Float128)7, r.Magnitude);
    }

    [Fact(DisplayName = "Impulse equality is by magnitude")]
    public void ImpulseEqualityIsByMagnitude()
    {
        // 2 N × 5 s = 10 == 5 N × 2 s = 10
        Impulse<Float128> left = new(NewtonsForce((Float128)2), Time<Float128>.FromSeconds((Float128)5));
        Impulse<Float128> right = new(NewtonsForce((Float128)5), Time<Float128>.FromSeconds((Float128)2));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Impulse CompareTo")]
    public void ImpulseCompareTo()
    {
        Assert.Equal(0, NewtonSeconds((Float128)5).CompareTo(NewtonSeconds((Float128)5)));
        Assert.Equal(1, NewtonSeconds((Float128)10).CompareTo(NewtonSeconds((Float128)5)));
        Assert.Equal(-1, NewtonSeconds((Float128)1).CompareTo(NewtonSeconds((Float128)5)));
        Assert.Throws<ArgumentException>(() => NewtonSeconds((Float128)5).CompareTo("not"));
    }

    [Fact(DisplayName = "Impulse.ToString basic")]
    public void ImpulseToStringBasic() =>
        Assert.Equal("50.000 kg*m/s²*s", NewtonSeconds((Float128)50).ToString("kg*m/s²*s:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Impulse.ToString throws on no separator")]
    public void ImpulseToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => NewtonSeconds((Float128)5).ToString("kgms2s", CultureInfo.InvariantCulture));
}
