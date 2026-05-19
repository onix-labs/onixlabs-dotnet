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

public sealed class ElectricChargeTests
{
    private static ElectricCharge<Float128> Coulombs(Float128 c) =>
        new(Current<Float128>.FromAmperes(c), Time<Float128>.FromSeconds((Float128)1));

    [Fact(DisplayName = "ElectricCharge should preserve components")]
    public void ElectricChargeShouldPreserveComponents()
    {
        Current<Float128> current = Current<Float128>.FromAmperes((Float128)5);
        Time<Float128> time = Time<Float128>.FromSeconds((Float128)2);
        ElectricCharge<Float128> q = new(current, time);
        Assert.Equal(current, q.Left);
        Assert.Equal(time, q.Right);
    }

    [Fact(DisplayName = "ElectricCharge.Zero magnitude is zero")]
    public void ElectricChargeZeroIsZero() => Assert.Equal(Float128.Zero, ElectricCharge<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "ElectricCharge magnitude lands at coulombs (A·s)")]
    public void ElectricChargeMagnitudeIsReadable()
    {
        // 5 A × 2 s = 10 C
        ElectricCharge<Float128> q = new(Current<Float128>.FromAmperes((Float128)5), Time<Float128>.FromSeconds((Float128)2));
        Assert.Equal((Float128)10, q.Magnitude);
    }

    [Fact(DisplayName = "ElectricCharge.Add produces expected value")]
    public void ElectricChargeAddProducesExpected()
    {
        ElectricCharge<Float128> r = ElectricCharge<Float128>.Add(Coulombs((Float128)5), Coulombs((Float128)3));
        Assert.Equal((Float128)8, r.Magnitude);
    }

    [Fact(DisplayName = "ElectricCharge.Subtract produces expected value")]
    public void ElectricChargeSubtractProducesExpected()
    {
        ElectricCharge<Float128> r = ElectricCharge<Float128>.Subtract(Coulombs((Float128)10), Coulombs((Float128)3));
        Assert.Equal((Float128)7, r.Magnitude);
    }

    [Fact(DisplayName = "ElectricCharge equality is by magnitude")]
    public void ElectricChargeEqualityIsByMagnitude()
    {
        // 2 A × 5 s = 10 == 5 A × 2 s = 10
        ElectricCharge<Float128> left = new(Current<Float128>.FromAmperes((Float128)2), Time<Float128>.FromSeconds((Float128)5));
        ElectricCharge<Float128> right = new(Current<Float128>.FromAmperes((Float128)5), Time<Float128>.FromSeconds((Float128)2));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "ElectricCharge CompareTo")]
    public void ElectricChargeCompareTo()
    {
        Assert.Equal(0, Coulombs((Float128)5).CompareTo(Coulombs((Float128)5)));
        Assert.Equal(1, Coulombs((Float128)10).CompareTo(Coulombs((Float128)5)));
        Assert.Equal(-1, Coulombs((Float128)1).CompareTo(Coulombs((Float128)5)));
        Assert.Throws<ArgumentException>(() => Coulombs((Float128)5).CompareTo("not"));
    }

    [Fact(DisplayName = "ElectricCharge.ToString basic")]
    public void ElectricChargeToStringBasic() =>
        Assert.Equal("50.000 A*s", Coulombs((Float128)50).ToString("A*s:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "ElectricCharge.ToString throws on no separator")]
    public void ElectricChargeToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => Coulombs((Float128)5).ToString("As", CultureInfo.InvariantCulture));
}
