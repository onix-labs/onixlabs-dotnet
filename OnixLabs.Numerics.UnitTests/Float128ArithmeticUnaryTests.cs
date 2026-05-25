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

public sealed class Float128ArithmeticUnaryTests
{
    [Fact(DisplayName = "Float128 unary plus should return the original value")]
    public void Float128UnaryPlusShouldReturnOriginalValue()
    {
        Assert.Equal(Float128.One.Bits, (+Float128.One).Bits);
        Assert.Equal(Float128.NegativeOne.Bits, (+Float128.NegativeOne).Bits);
        Assert.Equal(Float128.NegativeZero.Bits, (+Float128.NegativeZero).Bits);
    }

    [Fact(DisplayName = "Float128 unary minus should flip the sign bit")]
    public void Float128UnaryMinusShouldFlipSignBit()
    {
        Assert.Equal(Float128.NegativeOne.Bits, (-Float128.One).Bits);
        Assert.Equal(Float128.One.Bits, (-Float128.NegativeOne).Bits);
        Assert.Equal(Float128.NegativeZero.Bits, (-Float128.Zero).Bits);
        Assert.Equal(Float128.Zero.Bits, (-Float128.NegativeZero).Bits);
        Assert.Equal(Float128.NegativeInfinity.Bits, (-Float128.PositiveInfinity).Bits);
        Assert.Equal(Float128.PositiveInfinity.Bits, (-Float128.NegativeInfinity).Bits);
        Assert.Equal(Float128.MinValue.Bits, (-Float128.MaxValue).Bits);
        Assert.Equal(Float128.MaxValue.Bits, (-Float128.MinValue).Bits);
    }

    [Fact(DisplayName = "Float128 unary minus of NaN should remain NaN")]
    public void Float128UnaryMinusShouldPreserveNaN()
    {
        Assert.True(Float128.IsNaN(-Float128.NaN));
    }

    [Fact(DisplayName = "Float128.UnaryAdd and UnarySubtract should mirror the operators")]
    public void Float128UnaryNamedMethodsShouldMirrorOperators()
    {
        Assert.Equal((+Float128.One).Bits, Float128.UnaryAdd(Float128.One).Bits);
        Assert.Equal((-Float128.One).Bits, Float128.UnarySubtract(Float128.One).Bits);
    }

    [Fact(DisplayName = "Float128.Negate should mirror the unary minus operator")]
    public void Float128NegateShouldMirrorUnaryMinusOperator()
    {
        Assert.Equal((-Float128.One).Bits, Float128.Negate(Float128.One).Bits);
        Assert.Equal((-Float128.NegativeOne).Bits, Float128.Negate(Float128.NegativeOne).Bits);
    }
}
