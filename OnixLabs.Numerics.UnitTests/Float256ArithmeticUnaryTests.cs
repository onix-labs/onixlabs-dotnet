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

public sealed class Float256ArithmeticUnaryTests
{
    [Fact(DisplayName = "Float256 unary plus should return the original value")]
    public void Float256UnaryPlusShouldReturnOriginalValue()
    {
        Assert.Equal(Float256.One.Bits.UpperBits, (+Float256.One).Bits.UpperBits);
        Assert.Equal(Float256.One.Bits.LowerBits, (+Float256.One).Bits.LowerBits);
        Assert.Equal(Float256.NegativeOne.Bits.UpperBits, (+Float256.NegativeOne).Bits.UpperBits);
        Assert.Equal(Float256.NegativeOne.Bits.LowerBits, (+Float256.NegativeOne).Bits.LowerBits);
        Assert.Equal(Float256.NegativeZero.Bits.UpperBits, (+Float256.NegativeZero).Bits.UpperBits);
        Assert.Equal(Float256.NegativeZero.Bits.LowerBits, (+Float256.NegativeZero).Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256 unary minus should flip the sign bit")]
    public void Float256UnaryMinusShouldFlipSignBit()
    {
        Assert.Equal(Float256.NegativeOne.Bits.UpperBits, (-Float256.One).Bits.UpperBits);
        Assert.Equal(Float256.NegativeOne.Bits.LowerBits, (-Float256.One).Bits.LowerBits);
        Assert.Equal(Float256.One.Bits.UpperBits, (-Float256.NegativeOne).Bits.UpperBits);
        Assert.Equal(Float256.One.Bits.LowerBits, (-Float256.NegativeOne).Bits.LowerBits);
        Assert.Equal(Float256.NegativeZero.Bits.UpperBits, (-Float256.Zero).Bits.UpperBits);
        Assert.Equal(Float256.NegativeZero.Bits.LowerBits, (-Float256.Zero).Bits.LowerBits);
        Assert.Equal(Float256.Zero.Bits.UpperBits, (-Float256.NegativeZero).Bits.UpperBits);
        Assert.Equal(Float256.Zero.Bits.LowerBits, (-Float256.NegativeZero).Bits.LowerBits);
        Assert.Equal(Float256.NegativeInfinity.Bits.UpperBits, (-Float256.PositiveInfinity).Bits.UpperBits);
        Assert.Equal(Float256.NegativeInfinity.Bits.LowerBits, (-Float256.PositiveInfinity).Bits.LowerBits);
        Assert.Equal(Float256.PositiveInfinity.Bits.UpperBits, (-Float256.NegativeInfinity).Bits.UpperBits);
        Assert.Equal(Float256.PositiveInfinity.Bits.LowerBits, (-Float256.NegativeInfinity).Bits.LowerBits);
        Assert.Equal(Float256.MinValue.Bits.UpperBits, (-Float256.MaxValue).Bits.UpperBits);
        Assert.Equal(Float256.MinValue.Bits.LowerBits, (-Float256.MaxValue).Bits.LowerBits);
        Assert.Equal(Float256.MaxValue.Bits.UpperBits, (-Float256.MinValue).Bits.UpperBits);
        Assert.Equal(Float256.MaxValue.Bits.LowerBits, (-Float256.MinValue).Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256 unary minus of NaN should remain NaN")]
    public void Float256UnaryMinusShouldPreserveNaN()
    {
        Assert.True(Float256.IsNaN(-Float256.NaN));
    }

    [Fact(DisplayName = "Float256.Negate should mirror the unary minus operator")]
    public void Float256NegateShouldMirrorUnaryMinusOperator()
    {
        Assert.Equal((-Float256.One).Bits.UpperBits, Float256.Negate(Float256.One).Bits.UpperBits);
        Assert.Equal((-Float256.One).Bits.LowerBits, Float256.Negate(Float256.One).Bits.LowerBits);
        Assert.Equal((-Float256.NegativeOne).Bits.UpperBits, Float256.Negate(Float256.NegativeOne).Bits.UpperBits);
        Assert.Equal((-Float256.NegativeOne).Bits.LowerBits, Float256.Negate(Float256.NegativeOne).Bits.LowerBits);
    }
}
