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

public sealed class Float128ArithmeticCopySignTests
{
    [Fact(DisplayName = "Float128.CopySign should copy sign of positive sign argument")]
    public void Float128CopySignShouldCopyPositiveSign()
    {
        Assert.Equal(Float128.One.RawBits, Float128.CopySign(Float128.NegativeOne, Float128.One).RawBits);
        Assert.Equal(Float128.One.RawBits, Float128.CopySign(Float128.One, Float128.One).RawBits);
        Assert.Equal(Float128.MaxValue.RawBits, Float128.CopySign(Float128.MinValue, Float128.MaxValue).RawBits);
    }

    [Fact(DisplayName = "Float128.CopySign should copy sign of negative sign argument")]
    public void Float128CopySignShouldCopyNegativeSign()
    {
        Assert.Equal(Float128.NegativeOne.RawBits, Float128.CopySign(Float128.One, Float128.NegativeOne).RawBits);
        Assert.Equal(Float128.NegativeOne.RawBits, Float128.CopySign(Float128.NegativeOne, Float128.NegativeOne).RawBits);
        Assert.Equal(Float128.MinValue.RawBits, Float128.CopySign(Float128.MaxValue, Float128.MinValue).RawBits);
    }

    [Fact(DisplayName = "Float128.CopySign with negative zero sign should produce negative result")]
    public void Float128CopySignWithNegativeZeroSign()
    {
        Assert.Equal(Float128.NegativeOne.RawBits, Float128.CopySign(Float128.One, Float128.NegativeZero).RawBits);
    }

    [Fact(DisplayName = "Float128.CopySign should preserve NaN magnitude")]
    public void Float128CopySignShouldPreserveNaNMagnitude()
    {
        Float128 result = Float128.CopySign(Float128.NaN, Float128.NegativeOne);
        Assert.True(Float128.IsNaN(result));
        Assert.True(Float128.IsNegative(result));
    }
}
