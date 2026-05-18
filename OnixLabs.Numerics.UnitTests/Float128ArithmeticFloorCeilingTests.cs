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

public sealed class Float128ArithmeticFloorCeilingTests
{
    [Theory(DisplayName = "Float128.Floor should match double semantics")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(1.5)]
    [InlineData(1.9)]
    [InlineData(-1.0)]
    [InlineData(-1.5)]
    [InlineData(-1.1)]
    [InlineData(2.7)]
    [InlineData(-2.7)]
    [InlineData(100.999)]
    [InlineData(-100.999)]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(0.1)]
    [InlineData(-0.1)]
    public void Float128FloorShouldMatchDoubleSemantics(double value)
    {
        Float128 actual = Float128.Floor(value);
        Float128 expected = double.Floor(value);
        Assert.Equal(expected.Bits, actual.Bits);
    }

    [Theory(DisplayName = "Float128.Ceiling should match double semantics")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(1.5)]
    [InlineData(1.1)]
    [InlineData(-1.0)]
    [InlineData(-1.5)]
    [InlineData(-1.9)]
    [InlineData(2.7)]
    [InlineData(-2.7)]
    [InlineData(100.999)]
    [InlineData(-100.999)]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(0.1)]
    [InlineData(-0.1)]
    public void Float128CeilingShouldMatchDoubleSemantics(double value)
    {
        Float128 actual = Float128.Ceiling(value);
        Float128 expected = double.Ceiling(value);
        Assert.Equal(expected.Bits, actual.Bits);
    }

    [Fact(DisplayName = "Float128.Floor of special values should preserve them")]
    public void Float128FloorOfSpecialValuesShouldPreserve()
    {
        Assert.Equal(Float128.Zero.Bits, Float128.Floor(Float128.Zero).Bits);
        Assert.Equal(Float128.NegativeZero.Bits, Float128.Floor(Float128.NegativeZero).Bits);
        Assert.Equal(Float128.PositiveInfinity.Bits, Float128.Floor(Float128.PositiveInfinity).Bits);
        Assert.Equal(Float128.NegativeInfinity.Bits, Float128.Floor(Float128.NegativeInfinity).Bits);
        Assert.True(Float128.IsNaN(Float128.Floor(Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.Ceiling of special values should preserve them")]
    public void Float128CeilingOfSpecialValuesShouldPreserve()
    {
        Assert.Equal(Float128.Zero.Bits, Float128.Ceiling(Float128.Zero).Bits);
        Assert.Equal(Float128.NegativeZero.Bits, Float128.Ceiling(Float128.NegativeZero).Bits);
        Assert.Equal(Float128.PositiveInfinity.Bits, Float128.Ceiling(Float128.PositiveInfinity).Bits);
        Assert.Equal(Float128.NegativeInfinity.Bits, Float128.Ceiling(Float128.NegativeInfinity).Bits);
        Assert.True(Float128.IsNaN(Float128.Ceiling(Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.Floor of a negative subnormal should produce -1")]
    public void Float128FloorOfNegativeSubnormalShouldProduceNegativeOne()
    {
        Float128 negativeEpsilon = -Float128.Epsilon;
        Assert.Equal(Float128.NegativeOne.Bits, Float128.Floor(negativeEpsilon).Bits);
    }

    [Fact(DisplayName = "Float128.Ceiling of a positive subnormal should produce 1")]
    public void Float128CeilingOfPositiveSubnormalShouldProduceOne()
    {
        Assert.Equal(Float128.One.Bits, Float128.Ceiling(Float128.Epsilon).Bits);
    }
}
