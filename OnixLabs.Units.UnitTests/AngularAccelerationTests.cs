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

public sealed class AngularAccelerationTests
{
    private static AngularVelocity<Float128> RadiansPerSecond(Float128 omega) =>
        new(Angle<Float128>.FromRadians(omega), Time<Float128>.FromSeconds((Float128)1));

    private static AngularAcceleration<Float128> RadiansPerSecondSquared(Float128 alpha) =>
        new(RadiansPerSecond(alpha), Time<Float128>.FromSeconds((Float128)1));

    [Fact(DisplayName = "AngularAcceleration should preserve components")]
    public void AngularAccelerationShouldPreserveComponents()
    {
        AngularVelocity<Float128> omega = RadiansPerSecond((Float128)6);
        Time<Float128> time = Time<Float128>.FromSeconds((Float128)2);
        AngularAcceleration<Float128> alpha = new(omega, time);
        Assert.Equal(omega, alpha.Left);
        Assert.Equal(time, alpha.Right);
    }

    [Fact(DisplayName = "AngularAcceleration.Zero should produce zero magnitude")]
    public void AngularAccelerationZeroIsZero() =>
        Assert.Equal(Float128.Zero, AngularAcceleration<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "AngularAcceleration magnitude lands at rad/s²")]
    public void AngularAccelerationMagnitudeIsReadable()
    {
        // 6 rad/s / 2 s = 3 rad/s²
        AngularAcceleration<Float128> alpha = new(RadiansPerSecond((Float128)6), Time<Float128>.FromSeconds((Float128)2));
        Assert.Equal((Float128)3, alpha.Magnitude);
    }

    [Fact(DisplayName = "AngularAcceleration.Add produces expected value")]
    public void AngularAccelerationAddProducesExpected()
    {
        AngularAcceleration<Float128> r = AngularAcceleration<Float128>.Add(
            RadiansPerSecondSquared((Float128)5), RadiansPerSecondSquared((Float128)3));
        Assert.Equal((Float128)8, r.Magnitude);
    }

    [Fact(DisplayName = "AngularAcceleration.Subtract produces expected value")]
    public void AngularAccelerationSubtractProducesExpected()
    {
        AngularAcceleration<Float128> r = AngularAcceleration<Float128>.Subtract(
            RadiansPerSecondSquared((Float128)10), RadiansPerSecondSquared((Float128)3));
        Assert.Equal((Float128)7, r.Magnitude);
    }

    [Fact(DisplayName = "AngularAcceleration equality is by magnitude (proportional components)")]
    public void AngularAccelerationEqualityIsByMagnitude()
    {
        // 6 rad/s / 2 s = 3 == 9 rad/s / 3 s = 3
        AngularAcceleration<Float128> left = new(RadiansPerSecond((Float128)6), Time<Float128>.FromSeconds((Float128)2));
        AngularAcceleration<Float128> right = new(RadiansPerSecond((Float128)9), Time<Float128>.FromSeconds((Float128)3));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "AngularAcceleration CompareTo")]
    public void AngularAccelerationCompareTo()
    {
        Assert.Equal(0, RadiansPerSecondSquared((Float128)5).CompareTo(RadiansPerSecondSquared((Float128)5)));
        Assert.Equal(1, RadiansPerSecondSquared((Float128)10).CompareTo(RadiansPerSecondSquared((Float128)5)));
        Assert.Equal(-1, RadiansPerSecondSquared((Float128)1).CompareTo(RadiansPerSecondSquared((Float128)5)));
        Assert.Throws<ArgumentException>(() => RadiansPerSecondSquared((Float128)5).CompareTo("not"));
    }

    [Fact(DisplayName = "AngularAcceleration.ToString basic")]
    public void AngularAccelerationToStringBasic() =>
        Assert.Equal("5.000 rad/s/s", RadiansPerSecondSquared((Float128)5).ToString("rad/s/s:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "AngularAcceleration.ToString throws on no separator")]
    public void AngularAccelerationToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => RadiansPerSecondSquared((Float128)5).ToString("rads2", CultureInfo.InvariantCulture));
}
