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

public sealed class Float128ArithmeticTruncateTests
{
    [Theory(DisplayName = "Float128.Truncate should match double semantics for representable values")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(-1.0)]
    [InlineData(1.5)]
    [InlineData(-1.5)]
    [InlineData(2.7)]
    [InlineData(-2.7)]
    [InlineData(100.999)]
    [InlineData(-100.999)]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(0.1)]
    [InlineData(-0.1)]
    public void Float128TruncateShouldMatchDoubleSemantics(double value)
    {
        Float128 actual = Float128.Truncate(value);
        Float128 expected = double.Truncate(value);
        Assert.Equal(expected.Bits, actual.Bits);
    }

    [Fact(DisplayName = "Float128.Truncate of special values should preserve them")]
    public void Float128TruncateOfSpecialValuesShouldPreserve()
    {
        Assert.Equal(Float128.Zero.Bits, Float128.Truncate(Float128.Zero).Bits);
        Assert.Equal(Float128.NegativeZero.Bits, Float128.Truncate(Float128.NegativeZero).Bits);
        Assert.Equal(Float128.PositiveInfinity.Bits, Float128.Truncate(Float128.PositiveInfinity).Bits);
        Assert.Equal(Float128.NegativeInfinity.Bits, Float128.Truncate(Float128.NegativeInfinity).Bits);
        Assert.True(Float128.IsNaN(Float128.Truncate(Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.Truncate of subnormal should produce signed zero")]
    public void Float128TruncateOfSubnormalShouldProduceSignedZero()
    {
        Float128 truncated = Float128.Truncate(Float128.Epsilon);
        Assert.True(Float128.IsZero(truncated));
        Assert.False(Float128.IsNegative(truncated));
    }
}
