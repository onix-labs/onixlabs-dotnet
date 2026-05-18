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

public sealed class Float256ArithmeticCopySignTests
{
    [Fact(DisplayName = "Float256.CopySign should copy sign of positive sign argument")]
    public void Float256CopySignShouldCopyPositiveSign()
    {
        Assert.Equal(Float256.One.RawHighBits, Float256.CopySign(Float256.NegativeOne, Float256.One).RawHighBits);
        Assert.Equal(Float256.One.RawLowBits, Float256.CopySign(Float256.NegativeOne, Float256.One).RawLowBits);
        Assert.Equal(Float256.One.RawHighBits, Float256.CopySign(Float256.One, Float256.One).RawHighBits);
        Assert.Equal(Float256.One.RawLowBits, Float256.CopySign(Float256.One, Float256.One).RawLowBits);
        Assert.Equal(Float256.MaxValue.RawHighBits, Float256.CopySign(Float256.MinValue, Float256.MaxValue).RawHighBits);
        Assert.Equal(Float256.MaxValue.RawLowBits, Float256.CopySign(Float256.MinValue, Float256.MaxValue).RawLowBits);
    }

    [Fact(DisplayName = "Float256.CopySign should copy sign of negative sign argument")]
    public void Float256CopySignShouldCopyNegativeSign()
    {
        Assert.Equal(Float256.NegativeOne.RawHighBits, Float256.CopySign(Float256.One, Float256.NegativeOne).RawHighBits);
        Assert.Equal(Float256.NegativeOne.RawLowBits, Float256.CopySign(Float256.One, Float256.NegativeOne).RawLowBits);
        Assert.Equal(Float256.NegativeOne.RawHighBits, Float256.CopySign(Float256.NegativeOne, Float256.NegativeOne).RawHighBits);
        Assert.Equal(Float256.NegativeOne.RawLowBits, Float256.CopySign(Float256.NegativeOne, Float256.NegativeOne).RawLowBits);
        Assert.Equal(Float256.MinValue.RawHighBits, Float256.CopySign(Float256.MaxValue, Float256.MinValue).RawHighBits);
        Assert.Equal(Float256.MinValue.RawLowBits, Float256.CopySign(Float256.MaxValue, Float256.MinValue).RawLowBits);
    }

    [Fact(DisplayName = "Float256.CopySign with negative zero sign should produce negative result")]
    public void Float256CopySignWithNegativeZeroSign()
    {
        Assert.Equal(Float256.NegativeOne.RawHighBits, Float256.CopySign(Float256.One, Float256.NegativeZero).RawHighBits);
        Assert.Equal(Float256.NegativeOne.RawLowBits, Float256.CopySign(Float256.One, Float256.NegativeZero).RawLowBits);
    }

    [Fact(DisplayName = "Float256.CopySign should preserve NaN magnitude")]
    public void Float256CopySignShouldPreserveNaNMagnitude()
    {
        Float256 result = Float256.CopySign(Float256.NaN, Float256.NegativeOne);
        Assert.True(Float256.IsNaN(result));
        Assert.True(Float256.IsNegative(result));
    }
}
