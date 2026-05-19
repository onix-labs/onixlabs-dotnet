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

public sealed class VolumetricFlowRateTests
{
    private static VolumetricFlowRate<Float128> CubicMetersPerSecond(Float128 q) =>
        new(Volume<Float128>.FromCubicMeters(q), Time<Float128>.FromSeconds((Float128)1));

    [Fact(DisplayName = "VolumetricFlowRate should preserve components")]
    public void VolumetricFlowRateShouldPreserveComponents()
    {
        Volume<Float128> volume = Volume<Float128>.FromCubicMeters((Float128)10);
        Time<Float128> time = Time<Float128>.FromSeconds((Float128)2);
        VolumetricFlowRate<Float128> q = new(volume, time);
        Assert.Equal(volume, q.Left);
        Assert.Equal(time, q.Right);
    }

    [Fact(DisplayName = "VolumetricFlowRate.Zero magnitude is zero")]
    public void VolumetricFlowRateZeroIsZero() => Assert.Equal(Float128.Zero, VolumetricFlowRate<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "VolumetricFlowRate magnitude lands at m^3/s")]
    public void VolumetricFlowRateMagnitudeIsReadable()
    {
        // 10 m³ / 2 s = 5 m³/s
        VolumetricFlowRate<Float128> q = new(Volume<Float128>.FromCubicMeters((Float128)10), Time<Float128>.FromSeconds((Float128)2));
        Assert.Equal((Float128)5, q.Magnitude);
    }

    [Fact(DisplayName = "VolumetricFlowRate.Add produces expected value")]
    public void VolumetricFlowRateAddProducesExpected()
    {
        VolumetricFlowRate<Float128> r = VolumetricFlowRate<Float128>.Add(CubicMetersPerSecond((Float128)5), CubicMetersPerSecond((Float128)3));
        Assert.Equal((Float128)8, r.Magnitude);
    }

    [Fact(DisplayName = "VolumetricFlowRate.Subtract produces expected value")]
    public void VolumetricFlowRateSubtractProducesExpected()
    {
        VolumetricFlowRate<Float128> r = VolumetricFlowRate<Float128>.Subtract(CubicMetersPerSecond((Float128)10), CubicMetersPerSecond((Float128)3));
        Assert.Equal((Float128)7, r.Magnitude);
    }

    [Fact(DisplayName = "VolumetricFlowRate equality is by magnitude")]
    public void VolumetricFlowRateEqualityIsByMagnitude()
    {
        // 10 m³ / 2 s = 5 m³/s == 15 m³ / 3 s = 5 m³/s
        VolumetricFlowRate<Float128> left = new(Volume<Float128>.FromCubicMeters((Float128)10), Time<Float128>.FromSeconds((Float128)2));
        VolumetricFlowRate<Float128> right = new(Volume<Float128>.FromCubicMeters((Float128)15), Time<Float128>.FromSeconds((Float128)3));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "VolumetricFlowRate CompareTo")]
    public void VolumetricFlowRateCompareTo()
    {
        Assert.Equal(0, CubicMetersPerSecond((Float128)5).CompareTo(CubicMetersPerSecond((Float128)5)));
        Assert.Equal(1, CubicMetersPerSecond((Float128)10).CompareTo(CubicMetersPerSecond((Float128)5)));
        Assert.Equal(-1, CubicMetersPerSecond((Float128)1).CompareTo(CubicMetersPerSecond((Float128)5)));
        Assert.Throws<ArgumentException>(() => CubicMetersPerSecond((Float128)5).CompareTo("not"));
    }

    [Fact(DisplayName = "VolumetricFlowRate.ToString basic")]
    public void VolumetricFlowRateToStringBasic() =>
        Assert.Equal("50.000 cum/s", CubicMetersPerSecond((Float128)50).ToString("cum/s:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "VolumetricFlowRate.ToString throws on no separator")]
    public void VolumetricFlowRateToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => CubicMetersPerSecond((Float128)5).ToString("cums", CultureInfo.InvariantCulture));
}
