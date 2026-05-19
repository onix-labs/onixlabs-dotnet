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

public sealed class MassFlowRateTests
{
    private static MassFlowRate<Float128> KilogramsPerSecond(Float128 m) =>
        new(Mass<Float128>.FromKilograms(m), Time<Float128>.FromSeconds((Float128)1));

    [Fact(DisplayName = "MassFlowRate should preserve components")]
    public void MassFlowRateShouldPreserveComponents()
    {
        Mass<Float128> mass = Mass<Float128>.FromKilograms((Float128)10);
        Time<Float128> time = Time<Float128>.FromSeconds((Float128)2);
        MassFlowRate<Float128> m = new(mass, time);
        Assert.Equal(mass, m.Left);
        Assert.Equal(time, m.Right);
    }

    [Fact(DisplayName = "MassFlowRate.Zero magnitude is zero")]
    public void MassFlowRateZeroIsZero() => Assert.Equal(Float128.Zero, MassFlowRate<Float128>.Zero.Magnitude);

    [Fact(DisplayName = "MassFlowRate magnitude lands at kg/s")]
    public void MassFlowRateMagnitudeIsReadable()
    {
        // 10 kg / 2 s = 5 kg/s
        MassFlowRate<Float128> m = new(Mass<Float128>.FromKilograms((Float128)10), Time<Float128>.FromSeconds((Float128)2));
        Assert.Equal((Float128)5, m.Magnitude);
    }

    [Fact(DisplayName = "MassFlowRate.Add produces expected value")]
    public void MassFlowRateAddProducesExpected()
    {
        MassFlowRate<Float128> r = MassFlowRate<Float128>.Add(KilogramsPerSecond((Float128)5), KilogramsPerSecond((Float128)3));
        Assert.Equal((Float128)8, r.Magnitude);
    }

    [Fact(DisplayName = "MassFlowRate.Subtract produces expected value")]
    public void MassFlowRateSubtractProducesExpected()
    {
        MassFlowRate<Float128> r = MassFlowRate<Float128>.Subtract(KilogramsPerSecond((Float128)10), KilogramsPerSecond((Float128)3));
        Assert.Equal((Float128)7, r.Magnitude);
    }

    [Fact(DisplayName = "MassFlowRate equality is by magnitude")]
    public void MassFlowRateEqualityIsByMagnitude()
    {
        // 10 kg / 2 s = 5 kg/s == 15 kg / 3 s = 5 kg/s
        MassFlowRate<Float128> left = new(Mass<Float128>.FromKilograms((Float128)10), Time<Float128>.FromSeconds((Float128)2));
        MassFlowRate<Float128> right = new(Mass<Float128>.FromKilograms((Float128)15), Time<Float128>.FromSeconds((Float128)3));
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "MassFlowRate CompareTo")]
    public void MassFlowRateCompareTo()
    {
        Assert.Equal(0, KilogramsPerSecond((Float128)5).CompareTo(KilogramsPerSecond((Float128)5)));
        Assert.Equal(1, KilogramsPerSecond((Float128)10).CompareTo(KilogramsPerSecond((Float128)5)));
        Assert.Equal(-1, KilogramsPerSecond((Float128)1).CompareTo(KilogramsPerSecond((Float128)5)));
        Assert.Throws<ArgumentException>(() => KilogramsPerSecond((Float128)5).CompareTo("not"));
    }

    [Fact(DisplayName = "MassFlowRate.ToString basic")]
    public void MassFlowRateToStringBasic() =>
        Assert.Equal("50.000 kg/s", KilogramsPerSecond((Float128)50).ToString("kg/s:3", CultureInfo.InvariantCulture));

    [Fact(DisplayName = "MassFlowRate.ToString throws on no separator")]
    public void MassFlowRateToStringThrowsOnNoSeparator() =>
        Assert.Throws<FormatException>(() => KilogramsPerSecond((Float128)5).ToString("kgs", CultureInfo.InvariantCulture));
}
