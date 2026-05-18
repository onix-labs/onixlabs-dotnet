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
        Assert.Equal(Float256.One.RawBits.Upper, (+Float256.One).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, (+Float256.One).RawBits.Lower);
        Assert.Equal(Float256.NegativeOne.RawBits.Upper, (+Float256.NegativeOne).RawBits.Upper);
        Assert.Equal(Float256.NegativeOne.RawBits.Lower, (+Float256.NegativeOne).RawBits.Lower);
        Assert.Equal(Float256.NegativeZero.RawBits.Upper, (+Float256.NegativeZero).RawBits.Upper);
        Assert.Equal(Float256.NegativeZero.RawBits.Lower, (+Float256.NegativeZero).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256 unary minus should flip the sign bit")]
    public void Float256UnaryMinusShouldFlipSignBit()
    {
        Assert.Equal(Float256.NegativeOne.RawBits.Upper, (-Float256.One).RawBits.Upper);
        Assert.Equal(Float256.NegativeOne.RawBits.Lower, (-Float256.One).RawBits.Lower);
        Assert.Equal(Float256.One.RawBits.Upper, (-Float256.NegativeOne).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, (-Float256.NegativeOne).RawBits.Lower);
        Assert.Equal(Float256.NegativeZero.RawBits.Upper, (-Float256.Zero).RawBits.Upper);
        Assert.Equal(Float256.NegativeZero.RawBits.Lower, (-Float256.Zero).RawBits.Lower);
        Assert.Equal(Float256.Zero.RawBits.Upper, (-Float256.NegativeZero).RawBits.Upper);
        Assert.Equal(Float256.Zero.RawBits.Lower, (-Float256.NegativeZero).RawBits.Lower);
        Assert.Equal(Float256.NegativeInfinity.RawBits.Upper, (-Float256.PositiveInfinity).RawBits.Upper);
        Assert.Equal(Float256.NegativeInfinity.RawBits.Lower, (-Float256.PositiveInfinity).RawBits.Lower);
        Assert.Equal(Float256.PositiveInfinity.RawBits.Upper, (-Float256.NegativeInfinity).RawBits.Upper);
        Assert.Equal(Float256.PositiveInfinity.RawBits.Lower, (-Float256.NegativeInfinity).RawBits.Lower);
        Assert.Equal(Float256.MinValue.RawBits.Upper, (-Float256.MaxValue).RawBits.Upper);
        Assert.Equal(Float256.MinValue.RawBits.Lower, (-Float256.MaxValue).RawBits.Lower);
        Assert.Equal(Float256.MaxValue.RawBits.Upper, (-Float256.MinValue).RawBits.Upper);
        Assert.Equal(Float256.MaxValue.RawBits.Lower, (-Float256.MinValue).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256 unary minus of NaN should remain NaN")]
    public void Float256UnaryMinusShouldPreserveNaN()
    {
        Assert.True(Float256.IsNaN(-Float256.NaN));
    }

    [Fact(DisplayName = "Float256.Negate should mirror the unary minus operator")]
    public void Float256NegateShouldMirrorUnaryMinusOperator()
    {
        Assert.Equal((-Float256.One).RawBits.Upper, Float256.Negate(Float256.One).RawBits.Upper);
        Assert.Equal((-Float256.One).RawBits.Lower, Float256.Negate(Float256.One).RawBits.Lower);
        Assert.Equal((-Float256.NegativeOne).RawBits.Upper, Float256.Negate(Float256.NegativeOne).RawBits.Upper);
        Assert.Equal((-Float256.NegativeOne).RawBits.Lower, Float256.Negate(Float256.NegativeOne).RawBits.Lower);
    }
}
