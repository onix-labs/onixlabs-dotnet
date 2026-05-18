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

public sealed class Float256ArithmeticTruncateTests
{
    [Theory(DisplayName = "Float256.Truncate should match double semantics for representable values")]
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
    public void Float256TruncateShouldMatchDoubleSemantics(double value)
    {
        Float256 actual = Float256.Truncate(value);
        Float256 expected = double.Truncate(value);
        Assert.Equal(expected.RawBits.Upper, actual.RawBits.Upper);
        Assert.Equal(expected.RawBits.Lower, actual.RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.Truncate of special values should preserve them")]
    public void Float256TruncateOfSpecialValuesShouldPreserve()
    {
        Assert.Equal(Float256.Zero.RawBits.Upper, Float256.Truncate(Float256.Zero).RawBits.Upper);
        Assert.Equal(Float256.Zero.RawBits.Lower, Float256.Truncate(Float256.Zero).RawBits.Lower);
        Assert.Equal(Float256.NegativeZero.RawBits.Upper, Float256.Truncate(Float256.NegativeZero).RawBits.Upper);
        Assert.Equal(Float256.NegativeZero.RawBits.Lower, Float256.Truncate(Float256.NegativeZero).RawBits.Lower);
        Assert.Equal(Float256.PositiveInfinity.RawBits.Upper, Float256.Truncate(Float256.PositiveInfinity).RawBits.Upper);
        Assert.Equal(Float256.PositiveInfinity.RawBits.Lower, Float256.Truncate(Float256.PositiveInfinity).RawBits.Lower);
        Assert.Equal(Float256.NegativeInfinity.RawBits.Upper, Float256.Truncate(Float256.NegativeInfinity).RawBits.Upper);
        Assert.Equal(Float256.NegativeInfinity.RawBits.Lower, Float256.Truncate(Float256.NegativeInfinity).RawBits.Lower);
        Assert.True(Float256.IsNaN(Float256.Truncate(Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Truncate of subnormal should produce signed zero")]
    public void Float256TruncateOfSubnormalShouldProduceSignedZero()
    {
        Float256 truncated = Float256.Truncate(Float256.Epsilon);
        Assert.True(Float256.IsZero(truncated));
        Assert.False(Float256.IsNegative(truncated));
    }
}
