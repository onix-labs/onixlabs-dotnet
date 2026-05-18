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

public sealed class Float256ComparableMinMaxTests
{
    [Fact(DisplayName = "Float256.Min should return the smaller of two finite values")]
    public void Float256MinShouldReturnSmaller()
    {
        Assert.Equal(Float256.One.RawBits.Upper, Float256.Min(Float256.One, Float256.Two).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, Float256.Min(Float256.One, Float256.Two).RawBits.Lower);
        Assert.Equal(Float256.NegativeOne.RawBits.Upper, Float256.Min(Float256.NegativeOne, Float256.One).RawBits.Upper);
        Assert.Equal(Float256.NegativeOne.RawBits.Lower, Float256.Min(Float256.NegativeOne, Float256.One).RawBits.Lower);
        Assert.Equal(Float256.MinValue.RawBits.Upper, Float256.Min(Float256.MinValue, Float256.MaxValue).RawBits.Upper);
        Assert.Equal(Float256.MinValue.RawBits.Lower, Float256.Min(Float256.MinValue, Float256.MaxValue).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.Min should propagate NaN")]
    public void Float256MinShouldPropagateNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Min(Float256.NaN, Float256.One)));
        Assert.True(Float256.IsNaN(Float256.Min(Float256.One, Float256.NaN)));
        Assert.True(Float256.IsNaN(Float256.Min(Float256.NaN, Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Min should prefer negative zero over positive zero")]
    public void Float256MinShouldPreferNegativeZero()
    {
        Assert.Equal(Float256.NegativeZero.RawBits.Upper, Float256.Min(Float256.Zero, Float256.NegativeZero).RawBits.Upper);
        Assert.Equal(Float256.NegativeZero.RawBits.Lower, Float256.Min(Float256.Zero, Float256.NegativeZero).RawBits.Lower);
        Assert.Equal(Float256.NegativeZero.RawBits.Upper, Float256.Min(Float256.NegativeZero, Float256.Zero).RawBits.Upper);
        Assert.Equal(Float256.NegativeZero.RawBits.Lower, Float256.Min(Float256.NegativeZero, Float256.Zero).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.Max should return the larger of two finite values")]
    public void Float256MaxShouldReturnLarger()
    {
        Assert.Equal(Float256.Two.RawBits.Upper, Float256.Max(Float256.One, Float256.Two).RawBits.Upper);
        Assert.Equal(Float256.Two.RawBits.Lower, Float256.Max(Float256.One, Float256.Two).RawBits.Lower);
        Assert.Equal(Float256.One.RawBits.Upper, Float256.Max(Float256.NegativeOne, Float256.One).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, Float256.Max(Float256.NegativeOne, Float256.One).RawBits.Lower);
        Assert.Equal(Float256.MaxValue.RawBits.Upper, Float256.Max(Float256.MinValue, Float256.MaxValue).RawBits.Upper);
        Assert.Equal(Float256.MaxValue.RawBits.Lower, Float256.Max(Float256.MinValue, Float256.MaxValue).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.Max should propagate NaN")]
    public void Float256MaxShouldPropagateNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Max(Float256.NaN, Float256.One)));
        Assert.True(Float256.IsNaN(Float256.Max(Float256.One, Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Max should prefer positive zero over negative zero")]
    public void Float256MaxShouldPreferPositiveZero()
    {
        Assert.Equal(Float256.Zero.RawBits.Upper, Float256.Max(Float256.Zero, Float256.NegativeZero).RawBits.Upper);
        Assert.Equal(Float256.Zero.RawBits.Lower, Float256.Max(Float256.Zero, Float256.NegativeZero).RawBits.Lower);
        Assert.Equal(Float256.Zero.RawBits.Upper, Float256.Max(Float256.NegativeZero, Float256.Zero).RawBits.Upper);
        Assert.Equal(Float256.Zero.RawBits.Lower, Float256.Max(Float256.NegativeZero, Float256.Zero).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.MinNumber should treat NaN as missing")]
    public void Float256MinNumberShouldTreatNaNAsMissing()
    {
        Assert.Equal(Float256.One.RawBits.Upper, Float256.MinNumber(Float256.NaN, Float256.One).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, Float256.MinNumber(Float256.NaN, Float256.One).RawBits.Lower);
        Assert.Equal(Float256.One.RawBits.Upper, Float256.MinNumber(Float256.One, Float256.NaN).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, Float256.MinNumber(Float256.One, Float256.NaN).RawBits.Lower);
        Assert.True(Float256.IsNaN(Float256.MinNumber(Float256.NaN, Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.MaxNumber should treat NaN as missing")]
    public void Float256MaxNumberShouldTreatNaNAsMissing()
    {
        Assert.Equal(Float256.One.RawBits.Upper, Float256.MaxNumber(Float256.NaN, Float256.One).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, Float256.MaxNumber(Float256.NaN, Float256.One).RawBits.Lower);
        Assert.Equal(Float256.One.RawBits.Upper, Float256.MaxNumber(Float256.One, Float256.NaN).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, Float256.MaxNumber(Float256.One, Float256.NaN).RawBits.Lower);
        Assert.True(Float256.IsNaN(Float256.MaxNumber(Float256.NaN, Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.MinMagnitude should pick the operand with smaller absolute value")]
    public void Float256MinMagnitudeShouldPickSmallerAbsolute()
    {
        Assert.Equal(Float256.One.RawBits.Upper, Float256.MinMagnitude(Float256.One, -Float256.Two).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, Float256.MinMagnitude(Float256.One, -Float256.Two).RawBits.Lower);
        Assert.Equal(Float256.NegativeOne.RawBits.Upper, Float256.MinMagnitude(Float256.NegativeOne, Float256.Two).RawBits.Upper);
        Assert.Equal(Float256.NegativeOne.RawBits.Lower, Float256.MinMagnitude(Float256.NegativeOne, Float256.Two).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.MaxMagnitude should pick the operand with larger absolute value")]
    public void Float256MaxMagnitudeShouldPickLargerAbsolute()
    {
        Assert.Equal((-Float256.Two).RawBits.Upper, Float256.MaxMagnitude(Float256.One, -Float256.Two).RawBits.Upper);
        Assert.Equal((-Float256.Two).RawBits.Lower, Float256.MaxMagnitude(Float256.One, -Float256.Two).RawBits.Lower);
        Assert.Equal(Float256.Two.RawBits.Upper, Float256.MaxMagnitude(Float256.NegativeOne, Float256.Two).RawBits.Upper);
        Assert.Equal(Float256.Two.RawBits.Lower, Float256.MaxMagnitude(Float256.NegativeOne, Float256.Two).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.MinMagnitudeNumber and MaxMagnitudeNumber should treat NaN as missing")]
    public void Float256MagnitudeNumberShouldTreatNaNAsMissing()
    {
        Assert.Equal(Float256.One.RawBits.Upper, Float256.MinMagnitudeNumber(Float256.NaN, Float256.One).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, Float256.MinMagnitudeNumber(Float256.NaN, Float256.One).RawBits.Lower);
        Assert.Equal(Float256.One.RawBits.Upper, Float256.MaxMagnitudeNumber(Float256.NaN, Float256.One).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, Float256.MaxMagnitudeNumber(Float256.NaN, Float256.One).RawBits.Lower);
    }
}
