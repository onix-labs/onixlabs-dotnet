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

public sealed class TorqueTests
{
    private static Acceleration<Float128> OneMetrePerSecondSquared => new(
        new Speed<Float128>(Distance<Float128>.FromMeters((Float128)1), Time<Float128>.FromSeconds((Float128)1)),
        Time<Float128>.FromSeconds((Float128)1));

    private static Force<Float128> NewtonsForce(Float128 newtons) =>
        new(Mass<Float128>.FromKilograms(newtons), OneMetrePerSecondSquared);

    private static Torque<Float128> NewtonMetres(Float128 nm) =>
        new(NewtonsForce(nm), Distance<Float128>.FromMeters((Float128)1));

    [Fact(DisplayName = "Torque should preserve components")]
    public void TorqueShouldPreserveComponents()
    {
        Force<Float128> force = NewtonsForce((Float128)5);
        Distance<Float128> distance = Distance<Float128>.FromMeters((Float128)2);
        Torque<Float128> t = new(force, distance);
        Assert.Equal(force, t.Left);
        Assert.Equal(distance, t.Right);
    }

    [Fact(DisplayName = "Torque.Zero magnitude is zero")]
    public void TorqueZeroIsZero() => Assert.Equal(Float128.Zero, Torque<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "Torque magnitude lands at N·m")]
    public void TorqueMagnitudeIsReadable()
    {
        // 5 N × 2 m = 10 N·m
        Torque<Float128> t = new(NewtonsForce((Float128)5), Distance<Float128>.FromMeters((Float128)2));
        Assert.Equal((Float128)10, t.Magnitude);
    }

    [Fact(DisplayName = "Torque.Add produces expected value")]
    public void TorqueAddProducesExpected()
    {
        Torque<Float128> r = Torque<Float128>.Add(NewtonMetres((Float128)5), NewtonMetres((Float128)3));
        Assert.Equal((Float128)8, r.Magnitude);
    }

    [Fact(DisplayName = "Torque.Subtract produces expected value")]
    public void TorqueSubtractProducesExpected()
    {
        Torque<Float128> r = Torque<Float128>.Subtract(NewtonMetres((Float128)10), NewtonMetres((Float128)3));
        Assert.Equal((Float128)7, r.Magnitude);
    }

    [Fact(DisplayName = "Torque equality is by magnitude")]
    public void TorqueEqualityIsByMagnitude()
    {
        // 2 N × 5 m = 10 == 5 N × 2 m = 10
        Torque<Float128> left = new(NewtonsForce((Float128)2), Distance<Float128>.FromMeters((Float128)5));
        Torque<Float128> right = new(NewtonsForce((Float128)5), Distance<Float128>.FromMeters((Float128)2));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Torque CompareTo")]
    public void TorqueCompareTo()
    {
        Assert.Equal(0, NewtonMetres((Float128)5).CompareTo(NewtonMetres((Float128)5)));
        Assert.Equal(1, NewtonMetres((Float128)10).CompareTo(NewtonMetres((Float128)5)));
        Assert.Equal(-1, NewtonMetres((Float128)1).CompareTo(NewtonMetres((Float128)5)));
        Assert.Throws<ArgumentException>(() => NewtonMetres((Float128)5).CompareTo("not"));
    }

    [Fact(DisplayName = "Torque.ToString basic")]
    public void TorqueToStringBasic() =>
        Assert.Equal("50.000 kg*m/s²*m", NewtonMetres((Float128)50).ToString("kg*m/s²*m:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Torque.ToString throws on no separator")]
    public void TorqueToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => NewtonMetres((Float128)5).ToString("kgms2m", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "Torque default ToString should use 'N*m' cascading through Force alias")]
    public void TorqueDefaultToStringShouldUseNewtonMetreCascade() =>
        Assert.EndsWith(" N*m", NewtonMetres((Float128)50).ToString());

    [Fact(DisplayName = "Torque.ToString 'N*m' compound (cascade through Force alias) should produce '50.000 N*m'")]
    public void TorqueToStringNewtonMetreCascadeShouldProduceExpected() =>
        Assert.Equal("50.000 N*m", NewtonMetres((Float128)50).ToString("N*m:3", CultureInfo.InvariantCulture));
}
