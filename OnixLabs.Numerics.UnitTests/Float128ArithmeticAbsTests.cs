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

public sealed class Float128ArithmeticAbsTests
{
    [Fact(DisplayName = "Float128.Abs of a positive value should return the original value")]
    public void Float128AbsShouldReturnOriginalForPositive()
    {
        Assert.Equal(Float128.One.Bits, Float128.Abs(Float128.One).Bits);
        Assert.Equal(Float128.MaxValue.Bits, Float128.Abs(Float128.MaxValue).Bits);
        Assert.Equal(Float128.PositiveInfinity.Bits, Float128.Abs(Float128.PositiveInfinity).Bits);
    }

    [Fact(DisplayName = "Float128.Abs of a negative value should clear the sign bit")]
    public void Float128AbsShouldClearSignBitForNegative()
    {
        Assert.Equal(Float128.One.Bits, Float128.Abs(Float128.NegativeOne).Bits);
        Assert.Equal(Float128.MaxValue.Bits, Float128.Abs(Float128.MinValue).Bits);
        Assert.Equal(Float128.PositiveInfinity.Bits, Float128.Abs(Float128.NegativeInfinity).Bits);
    }

    [Fact(DisplayName = "Float128.Abs of negative zero should produce positive zero")]
    public void Float128AbsShouldProducePositiveZeroForNegativeZero()
    {
        Assert.Equal(Float128.Zero.Bits, Float128.Abs(Float128.NegativeZero).Bits);
    }

    [Fact(DisplayName = "Float128.Abs of NaN should return a NaN value")]
    public void Float128AbsShouldReturnNaNForNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Abs(Float128.NaN)));
    }
}
