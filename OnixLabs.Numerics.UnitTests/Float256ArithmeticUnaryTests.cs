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
        Assert.Equal(Float256.One.Bits.Upper, (+Float256.One).Bits.Upper);
        Assert.Equal(Float256.One.Bits.Lower, (+Float256.One).Bits.Lower);
        Assert.Equal(Float256.NegativeOne.Bits.Upper, (+Float256.NegativeOne).Bits.Upper);
        Assert.Equal(Float256.NegativeOne.Bits.Lower, (+Float256.NegativeOne).Bits.Lower);
        Assert.Equal(Float256.NegativeZero.Bits.Upper, (+Float256.NegativeZero).Bits.Upper);
        Assert.Equal(Float256.NegativeZero.Bits.Lower, (+Float256.NegativeZero).Bits.Lower);
    }

    [Fact(DisplayName = "Float256 unary minus should flip the sign bit")]
    public void Float256UnaryMinusShouldFlipSignBit()
    {
        Assert.Equal(Float256.NegativeOne.Bits.Upper, (-Float256.One).Bits.Upper);
        Assert.Equal(Float256.NegativeOne.Bits.Lower, (-Float256.One).Bits.Lower);
        Assert.Equal(Float256.One.Bits.Upper, (-Float256.NegativeOne).Bits.Upper);
        Assert.Equal(Float256.One.Bits.Lower, (-Float256.NegativeOne).Bits.Lower);
        Assert.Equal(Float256.NegativeZero.Bits.Upper, (-Float256.Zero).Bits.Upper);
        Assert.Equal(Float256.NegativeZero.Bits.Lower, (-Float256.Zero).Bits.Lower);
        Assert.Equal(Float256.Zero.Bits.Upper, (-Float256.NegativeZero).Bits.Upper);
        Assert.Equal(Float256.Zero.Bits.Lower, (-Float256.NegativeZero).Bits.Lower);
        Assert.Equal(Float256.NegativeInfinity.Bits.Upper, (-Float256.PositiveInfinity).Bits.Upper);
        Assert.Equal(Float256.NegativeInfinity.Bits.Lower, (-Float256.PositiveInfinity).Bits.Lower);
        Assert.Equal(Float256.PositiveInfinity.Bits.Upper, (-Float256.NegativeInfinity).Bits.Upper);
        Assert.Equal(Float256.PositiveInfinity.Bits.Lower, (-Float256.NegativeInfinity).Bits.Lower);
        Assert.Equal(Float256.MinValue.Bits.Upper, (-Float256.MaxValue).Bits.Upper);
        Assert.Equal(Float256.MinValue.Bits.Lower, (-Float256.MaxValue).Bits.Lower);
        Assert.Equal(Float256.MaxValue.Bits.Upper, (-Float256.MinValue).Bits.Upper);
        Assert.Equal(Float256.MaxValue.Bits.Lower, (-Float256.MinValue).Bits.Lower);
    }

    [Fact(DisplayName = "Float256 unary minus of NaN should remain NaN")]
    public void Float256UnaryMinusShouldPreserveNaN()
    {
        Assert.True(Float256.IsNaN(-Float256.NaN));
    }

    [Fact(DisplayName = "Float256.Negate should mirror the unary minus operator")]
    public void Float256NegateShouldMirrorUnaryMinusOperator()
    {
        Assert.Equal((-Float256.One).Bits.Upper, Float256.Negate(Float256.One).Bits.Upper);
        Assert.Equal((-Float256.One).Bits.Lower, Float256.Negate(Float256.One).Bits.Lower);
        Assert.Equal((-Float256.NegativeOne).Bits.Upper, Float256.Negate(Float256.NegativeOne).Bits.Upper);
        Assert.Equal((-Float256.NegativeOne).Bits.Lower, Float256.Negate(Float256.NegativeOne).Bits.Lower);
    }
}
