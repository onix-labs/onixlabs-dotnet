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
        Assert.Equal(Float256.One.RawHighBits, (+Float256.One).RawHighBits);
        Assert.Equal(Float256.One.RawLowBits, (+Float256.One).RawLowBits);
        Assert.Equal(Float256.NegativeOne.RawHighBits, (+Float256.NegativeOne).RawHighBits);
        Assert.Equal(Float256.NegativeOne.RawLowBits, (+Float256.NegativeOne).RawLowBits);
        Assert.Equal(Float256.NegativeZero.RawHighBits, (+Float256.NegativeZero).RawHighBits);
        Assert.Equal(Float256.NegativeZero.RawLowBits, (+Float256.NegativeZero).RawLowBits);
    }

    [Fact(DisplayName = "Float256 unary minus should flip the sign bit")]
    public void Float256UnaryMinusShouldFlipSignBit()
    {
        Assert.Equal(Float256.NegativeOne.RawHighBits, (-Float256.One).RawHighBits);
        Assert.Equal(Float256.NegativeOne.RawLowBits, (-Float256.One).RawLowBits);
        Assert.Equal(Float256.One.RawHighBits, (-Float256.NegativeOne).RawHighBits);
        Assert.Equal(Float256.One.RawLowBits, (-Float256.NegativeOne).RawLowBits);
        Assert.Equal(Float256.NegativeZero.RawHighBits, (-Float256.Zero).RawHighBits);
        Assert.Equal(Float256.NegativeZero.RawLowBits, (-Float256.Zero).RawLowBits);
        Assert.Equal(Float256.Zero.RawHighBits, (-Float256.NegativeZero).RawHighBits);
        Assert.Equal(Float256.Zero.RawLowBits, (-Float256.NegativeZero).RawLowBits);
        Assert.Equal(Float256.NegativeInfinity.RawHighBits, (-Float256.PositiveInfinity).RawHighBits);
        Assert.Equal(Float256.NegativeInfinity.RawLowBits, (-Float256.PositiveInfinity).RawLowBits);
        Assert.Equal(Float256.PositiveInfinity.RawHighBits, (-Float256.NegativeInfinity).RawHighBits);
        Assert.Equal(Float256.PositiveInfinity.RawLowBits, (-Float256.NegativeInfinity).RawLowBits);
        Assert.Equal(Float256.MinValue.RawHighBits, (-Float256.MaxValue).RawHighBits);
        Assert.Equal(Float256.MinValue.RawLowBits, (-Float256.MaxValue).RawLowBits);
        Assert.Equal(Float256.MaxValue.RawHighBits, (-Float256.MinValue).RawHighBits);
        Assert.Equal(Float256.MaxValue.RawLowBits, (-Float256.MinValue).RawLowBits);
    }

    [Fact(DisplayName = "Float256 unary minus of NaN should remain NaN")]
    public void Float256UnaryMinusShouldPreserveNaN()
    {
        Assert.True(Float256.IsNaN(-Float256.NaN));
    }

    [Fact(DisplayName = "Float256.Negate should mirror the unary minus operator")]
    public void Float256NegateShouldMirrorUnaryMinusOperator()
    {
        Assert.Equal((-Float256.One).RawHighBits, Float256.Negate(Float256.One).RawHighBits);
        Assert.Equal((-Float256.One).RawLowBits, Float256.Negate(Float256.One).RawLowBits);
        Assert.Equal((-Float256.NegativeOne).RawHighBits, Float256.Negate(Float256.NegativeOne).RawHighBits);
        Assert.Equal((-Float256.NegativeOne).RawLowBits, Float256.Negate(Float256.NegativeOne).RawLowBits);
    }
}
