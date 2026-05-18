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
        Assert.Equal(Float128.One.RawBits, Float128.Abs(Float128.One).RawBits);
        Assert.Equal(Float128.MaxValue.RawBits, Float128.Abs(Float128.MaxValue).RawBits);
        Assert.Equal(Float128.PositiveInfinity.RawBits, Float128.Abs(Float128.PositiveInfinity).RawBits);
    }

    [Fact(DisplayName = "Float128.Abs of a negative value should clear the sign bit")]
    public void Float128AbsShouldClearSignBitForNegative()
    {
        Assert.Equal(Float128.One.RawBits, Float128.Abs(Float128.NegativeOne).RawBits);
        Assert.Equal(Float128.MaxValue.RawBits, Float128.Abs(Float128.MinValue).RawBits);
        Assert.Equal(Float128.PositiveInfinity.RawBits, Float128.Abs(Float128.NegativeInfinity).RawBits);
    }

    [Fact(DisplayName = "Float128.Abs of negative zero should produce positive zero")]
    public void Float128AbsShouldProducePositiveZeroForNegativeZero()
    {
        Assert.Equal(Float128.Zero.RawBits, Float128.Abs(Float128.NegativeZero).RawBits);
    }

    [Fact(DisplayName = "Float128.Abs of NaN should return a NaN value")]
    public void Float128AbsShouldReturnNaNForNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Abs(Float128.NaN)));
    }
}
