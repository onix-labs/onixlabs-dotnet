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

public sealed class AngularVelocityTests
{
    private static AngularVelocity<Float128> RadiansPerSecond(Float128 omega) =>
        new(Angle<Float128>.FromRadians(omega), Time<Float128>.FromSeconds((Float128)1));

    [Fact(DisplayName = "AngularVelocity should preserve components")]
    public void AngularVelocityShouldPreserveComponents()
    {
        Angle<Float128> angle = Angle<Float128>.FromRadians((Float128)6);
        Time<Float128> time = Time<Float128>.FromSeconds((Float128)2);
        AngularVelocity<Float128> omega = new(angle, time);
        Assert.Equal(angle, omega.Left);
        Assert.Equal(time, omega.Right);
    }

    [Fact(DisplayName = "AngularVelocity.Zero magnitude is zero")]
    public void AngularVelocityZeroIsZero() => Assert.Equal(Float128.Zero, AngularVelocity<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "AngularVelocity magnitude lands at rad/s")]
    public void AngularVelocityMagnitudeIsReadable()
    {
        // 6 rad / 2 s = 3 rad/s
        AngularVelocity<Float128> omega = new(Angle<Float128>.FromRadians((Float128)6), Time<Float128>.FromSeconds((Float128)2));
        Assert.Equal((Float128)3, omega.SIBaseValue);
    }

    [Fact(DisplayName = "AngularVelocity.Add produces expected value")]
    public void AngularVelocityAddProducesExpected()
    {
        AngularVelocity<Float128> r = AngularVelocity<Float128>.Add(RadiansPerSecond((Float128)5), RadiansPerSecond((Float128)3));
        Assert.Equal((Float128)8, r.SIBaseValue);
    }

    [Fact(DisplayName = "AngularVelocity.Subtract produces expected value")]
    public void AngularVelocitySubtractProducesExpected()
    {
        AngularVelocity<Float128> r = AngularVelocity<Float128>.Subtract(RadiansPerSecond((Float128)10), RadiansPerSecond((Float128)3));
        Assert.Equal((Float128)7, r.SIBaseValue);
    }

    [Fact(DisplayName = "AngularVelocity equality is by magnitude")]
    public void AngularVelocityEqualityIsByMagnitude()
    {
        // 6 rad / 2 s = 3 rad/s == 9 rad / 3 s = 3 rad/s
        AngularVelocity<Float128> left = new(Angle<Float128>.FromRadians((Float128)6), Time<Float128>.FromSeconds((Float128)2));
        AngularVelocity<Float128> right = new(Angle<Float128>.FromRadians((Float128)9), Time<Float128>.FromSeconds((Float128)3));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "AngularVelocity CompareTo")]
    public void AngularVelocityCompareTo()
    {
        Assert.Equal(0, RadiansPerSecond((Float128)5).CompareTo(RadiansPerSecond((Float128)5)));
        Assert.Equal(1, RadiansPerSecond((Float128)10).CompareTo(RadiansPerSecond((Float128)5)));
        Assert.Equal(-1, RadiansPerSecond((Float128)1).CompareTo(RadiansPerSecond((Float128)5)));
        Assert.Throws<ArgumentException>(() => RadiansPerSecond((Float128)5).CompareTo("not"));
    }

    [Fact(DisplayName = "AngularVelocity.ToString basic")]
    public void AngularVelocityToStringBasic() =>
        Assert.Equal("50.000 rad/s", RadiansPerSecond((Float128)50).ToString("rad/s:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "AngularVelocity.ToString throws on no separator")]
    public void AngularVelocityToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => RadiansPerSecond((Float128)5).ToString("rads", CultureInfo.InvariantCulture));
}
