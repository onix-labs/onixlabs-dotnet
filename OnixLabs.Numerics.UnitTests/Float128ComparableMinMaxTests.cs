// Copyright © 2020 ONIXLabs
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

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float128ComparableMinMaxTests
{
    [Fact(DisplayName = "Float128.Min should return the smaller of two finite values")]
    public void Float128MinShouldReturnSmaller()
    {
        Assert.Equal(Float128.One.RawBits, Float128.Min(Float128.One, Float128.Two).RawBits);
        Assert.Equal(Float128.NegativeOne.RawBits, Float128.Min(Float128.NegativeOne, Float128.One).RawBits);
        Assert.Equal(Float128.MinValue.RawBits, Float128.Min(Float128.MinValue, Float128.MaxValue).RawBits);
    }

    [Fact(DisplayName = "Float128.Min should propagate NaN")]
    public void Float128MinShouldPropagateNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Min(Float128.NaN, Float128.One)));
        Assert.True(Float128.IsNaN(Float128.Min(Float128.One, Float128.NaN)));
        Assert.True(Float128.IsNaN(Float128.Min(Float128.NaN, Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.Min should prefer negative zero over positive zero")]
    public void Float128MinShouldPreferNegativeZero()
    {
        Assert.Equal(Float128.NegativeZero.RawBits, Float128.Min(Float128.Zero, Float128.NegativeZero).RawBits);
        Assert.Equal(Float128.NegativeZero.RawBits, Float128.Min(Float128.NegativeZero, Float128.Zero).RawBits);
    }

    [Fact(DisplayName = "Float128.Max should return the larger of two finite values")]
    public void Float128MaxShouldReturnLarger()
    {
        Assert.Equal(Float128.Two.RawBits, Float128.Max(Float128.One, Float128.Two).RawBits);
        Assert.Equal(Float128.One.RawBits, Float128.Max(Float128.NegativeOne, Float128.One).RawBits);
        Assert.Equal(Float128.MaxValue.RawBits, Float128.Max(Float128.MinValue, Float128.MaxValue).RawBits);
    }

    [Fact(DisplayName = "Float128.Max should propagate NaN")]
    public void Float128MaxShouldPropagateNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Max(Float128.NaN, Float128.One)));
        Assert.True(Float128.IsNaN(Float128.Max(Float128.One, Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.Max should prefer positive zero over negative zero")]
    public void Float128MaxShouldPreferPositiveZero()
    {
        Assert.Equal(Float128.Zero.RawBits, Float128.Max(Float128.Zero, Float128.NegativeZero).RawBits);
        Assert.Equal(Float128.Zero.RawBits, Float128.Max(Float128.NegativeZero, Float128.Zero).RawBits);
    }

    [Fact(DisplayName = "Float128.MinNumber should treat NaN as missing")]
    public void Float128MinNumberShouldTreatNaNAsMissing()
    {
        Assert.Equal(Float128.One.RawBits, Float128.MinNumber(Float128.NaN, Float128.One).RawBits);
        Assert.Equal(Float128.One.RawBits, Float128.MinNumber(Float128.One, Float128.NaN).RawBits);
        Assert.True(Float128.IsNaN(Float128.MinNumber(Float128.NaN, Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.MaxNumber should treat NaN as missing")]
    public void Float128MaxNumberShouldTreatNaNAsMissing()
    {
        Assert.Equal(Float128.One.RawBits, Float128.MaxNumber(Float128.NaN, Float128.One).RawBits);
        Assert.Equal(Float128.One.RawBits, Float128.MaxNumber(Float128.One, Float128.NaN).RawBits);
        Assert.True(Float128.IsNaN(Float128.MaxNumber(Float128.NaN, Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.MinMagnitude should pick the operand with smaller absolute value")]
    public void Float128MinMagnitudeShouldPickSmallerAbsolute()
    {
        Assert.Equal(Float128.One.RawBits, Float128.MinMagnitude(Float128.One, -Float128.Two).RawBits);
        Assert.Equal(Float128.NegativeOne.RawBits, Float128.MinMagnitude(Float128.NegativeOne, Float128.Two).RawBits);
    }

    [Fact(DisplayName = "Float128.MaxMagnitude should pick the operand with larger absolute value")]
    public void Float128MaxMagnitudeShouldPickLargerAbsolute()
    {
        Assert.Equal((-Float128.Two).RawBits, Float128.MaxMagnitude(Float128.One, -Float128.Two).RawBits);
        Assert.Equal(Float128.Two.RawBits, Float128.MaxMagnitude(Float128.NegativeOne, Float128.Two).RawBits);
    }

    [Fact(DisplayName = "Float128.MinMagnitudeNumber and MaxMagnitudeNumber should treat NaN as missing")]
    public void Float128MagnitudeNumberShouldTreatNaNAsMissing()
    {
        Assert.Equal(Float128.One.RawBits, Float128.MinMagnitudeNumber(Float128.NaN, Float128.One).RawBits);
        Assert.Equal(Float128.One.RawBits, Float128.MaxMagnitudeNumber(Float128.NaN, Float128.One).RawBits);
    }
}
