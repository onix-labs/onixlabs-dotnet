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

public sealed class Float256ArithmeticAbsTests
{
    [Fact(DisplayName = "Float256.Abs of a positive value should return the original value")]
    public void Float256AbsShouldReturnOriginalForPositive()
    {
        Assert.Equal(Float256.One.RawBits.Upper, Float256.Abs(Float256.One).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, Float256.Abs(Float256.One).RawBits.Lower);
        Assert.Equal(Float256.MaxValue.RawBits.Upper, Float256.Abs(Float256.MaxValue).RawBits.Upper);
        Assert.Equal(Float256.MaxValue.RawBits.Lower, Float256.Abs(Float256.MaxValue).RawBits.Lower);
        Assert.Equal(Float256.PositiveInfinity.RawBits.Upper, Float256.Abs(Float256.PositiveInfinity).RawBits.Upper);
        Assert.Equal(Float256.PositiveInfinity.RawBits.Lower, Float256.Abs(Float256.PositiveInfinity).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.Abs of a negative value should clear the sign bit")]
    public void Float256AbsShouldClearSignBitForNegative()
    {
        Assert.Equal(Float256.One.RawBits.Upper, Float256.Abs(Float256.NegativeOne).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, Float256.Abs(Float256.NegativeOne).RawBits.Lower);
        Assert.Equal(Float256.MaxValue.RawBits.Upper, Float256.Abs(Float256.MinValue).RawBits.Upper);
        Assert.Equal(Float256.MaxValue.RawBits.Lower, Float256.Abs(Float256.MinValue).RawBits.Lower);
        Assert.Equal(Float256.PositiveInfinity.RawBits.Upper, Float256.Abs(Float256.NegativeInfinity).RawBits.Upper);
        Assert.Equal(Float256.PositiveInfinity.RawBits.Lower, Float256.Abs(Float256.NegativeInfinity).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.Abs of negative zero should produce positive zero")]
    public void Float256AbsShouldProducePositiveZeroForNegativeZero()
    {
        Assert.Equal(Float256.Zero.RawBits.Upper, Float256.Abs(Float256.NegativeZero).RawBits.Upper);
        Assert.Equal(Float256.Zero.RawBits.Lower, Float256.Abs(Float256.NegativeZero).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.Abs of NaN should return a NaN value")]
    public void Float256AbsShouldReturnNaNForNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Abs(Float256.NaN)));
    }
}
