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

using System;

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float256ArithmeticAbsTests
{
    [Fact(DisplayName = "Float256.Abs of a positive value should return the original value")]
    public void Float256AbsShouldReturnOriginalForPositive()
    {
        Assert.Equal(Float256.One.Bits.UpperBits, Float256.Abs(Float256.One).Bits.UpperBits);
        Assert.Equal(Float256.One.Bits.LowerBits, Float256.Abs(Float256.One).Bits.LowerBits);
        Assert.Equal(Float256.MaxValue.Bits.UpperBits, Float256.Abs(Float256.MaxValue).Bits.UpperBits);
        Assert.Equal(Float256.MaxValue.Bits.LowerBits, Float256.Abs(Float256.MaxValue).Bits.LowerBits);
        Assert.Equal(Float256.PositiveInfinity.Bits.UpperBits, Float256.Abs(Float256.PositiveInfinity).Bits.UpperBits);
        Assert.Equal(Float256.PositiveInfinity.Bits.LowerBits, Float256.Abs(Float256.PositiveInfinity).Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.Abs of a negative value should clear the sign bit")]
    public void Float256AbsShouldClearSignBitForNegative()
    {
        Assert.Equal(Float256.One.Bits.UpperBits, Float256.Abs(Float256.NegativeOne).Bits.UpperBits);
        Assert.Equal(Float256.One.Bits.LowerBits, Float256.Abs(Float256.NegativeOne).Bits.LowerBits);
        Assert.Equal(Float256.MaxValue.Bits.UpperBits, Float256.Abs(Float256.MinValue).Bits.UpperBits);
        Assert.Equal(Float256.MaxValue.Bits.LowerBits, Float256.Abs(Float256.MinValue).Bits.LowerBits);
        Assert.Equal(Float256.PositiveInfinity.Bits.UpperBits, Float256.Abs(Float256.NegativeInfinity).Bits.UpperBits);
        Assert.Equal(Float256.PositiveInfinity.Bits.LowerBits, Float256.Abs(Float256.NegativeInfinity).Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.Abs of negative zero should produce positive zero")]
    public void Float256AbsShouldProducePositiveZeroForNegativeZero()
    {
        Assert.Equal(Float256.Zero.Bits.UpperBits, Float256.Abs(Float256.NegativeZero).Bits.UpperBits);
        Assert.Equal(Float256.Zero.Bits.LowerBits, Float256.Abs(Float256.NegativeZero).Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.Abs of NaN should return a NaN value")]
    public void Float256AbsShouldReturnNaNForNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Abs(Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Sign should return -1, 0 and 1 for negative, zero and positive values")]
    public void Float256SignShouldReturnExpectedValues()
    {
        Assert.Equal(1, Float256.Sign(Float256.One));
        Assert.Equal(-1, Float256.Sign(Float256.NegativeOne));
        Assert.Equal(0, Float256.Sign(Float256.Zero));
        Assert.Equal(0, Float256.Sign(Float256.NegativeZero));
        Assert.Equal(1, Float256.Sign(Float256.PositiveInfinity));
        Assert.Equal(-1, Float256.Sign(Float256.NegativeInfinity));
    }

    [Fact(DisplayName = "Float256.Sign should throw for NaN")]
    public void Float256SignShouldThrowForNaN()
    {
        Assert.Throws<ArithmeticException>(() => Float256.Sign(Float256.NaN));
    }
}
