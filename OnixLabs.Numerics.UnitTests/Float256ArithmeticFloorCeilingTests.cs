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

public sealed class Float256ArithmeticFloorCeilingTests
{
    [Theory(DisplayName = "Float256.Floor should match double semantics")]
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
    public void Float256FloorShouldMatchDoubleSemantics(double value)
    {
        Float256 actual = Float256.Floor(value);
        Float256 expected = double.Floor(value);
        Assert.Equal(expected.Bits.UpperBits, actual.Bits.UpperBits);
        Assert.Equal(expected.Bits.LowerBits, actual.Bits.LowerBits);
    }

    [Theory(DisplayName = "Float256.Ceiling should match double semantics")]
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
    public void Float256CeilingShouldMatchDoubleSemantics(double value)
    {
        Float256 actual = Float256.Ceiling(value);
        Float256 expected = double.Ceiling(value);
        Assert.Equal(expected.Bits.UpperBits, actual.Bits.UpperBits);
        Assert.Equal(expected.Bits.LowerBits, actual.Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.Floor of special values should preserve them")]
    public void Float256FloorOfSpecialValuesShouldPreserve()
    {
        Assert.Equal(Float256.Zero.Bits.UpperBits, Float256.Floor(Float256.Zero).Bits.UpperBits);
        Assert.Equal(Float256.Zero.Bits.LowerBits, Float256.Floor(Float256.Zero).Bits.LowerBits);
        Assert.Equal(Float256.NegativeZero.Bits.UpperBits, Float256.Floor(Float256.NegativeZero).Bits.UpperBits);
        Assert.Equal(Float256.NegativeZero.Bits.LowerBits, Float256.Floor(Float256.NegativeZero).Bits.LowerBits);
        Assert.Equal(Float256.PositiveInfinity.Bits.UpperBits, Float256.Floor(Float256.PositiveInfinity).Bits.UpperBits);
        Assert.Equal(Float256.PositiveInfinity.Bits.LowerBits, Float256.Floor(Float256.PositiveInfinity).Bits.LowerBits);
        Assert.Equal(Float256.NegativeInfinity.Bits.UpperBits, Float256.Floor(Float256.NegativeInfinity).Bits.UpperBits);
        Assert.Equal(Float256.NegativeInfinity.Bits.LowerBits, Float256.Floor(Float256.NegativeInfinity).Bits.LowerBits);
        Assert.True(Float256.IsNaN(Float256.Floor(Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Ceiling of special values should preserve them")]
    public void Float256CeilingOfSpecialValuesShouldPreserve()
    {
        Assert.Equal(Float256.Zero.Bits.UpperBits, Float256.Ceiling(Float256.Zero).Bits.UpperBits);
        Assert.Equal(Float256.Zero.Bits.LowerBits, Float256.Ceiling(Float256.Zero).Bits.LowerBits);
        Assert.Equal(Float256.NegativeZero.Bits.UpperBits, Float256.Ceiling(Float256.NegativeZero).Bits.UpperBits);
        Assert.Equal(Float256.NegativeZero.Bits.LowerBits, Float256.Ceiling(Float256.NegativeZero).Bits.LowerBits);
        Assert.Equal(Float256.PositiveInfinity.Bits.UpperBits, Float256.Ceiling(Float256.PositiveInfinity).Bits.UpperBits);
        Assert.Equal(Float256.PositiveInfinity.Bits.LowerBits, Float256.Ceiling(Float256.PositiveInfinity).Bits.LowerBits);
        Assert.Equal(Float256.NegativeInfinity.Bits.UpperBits, Float256.Ceiling(Float256.NegativeInfinity).Bits.UpperBits);
        Assert.Equal(Float256.NegativeInfinity.Bits.LowerBits, Float256.Ceiling(Float256.NegativeInfinity).Bits.LowerBits);
        Assert.True(Float256.IsNaN(Float256.Ceiling(Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Floor of a negative subnormal should produce -1")]
    public void Float256FloorOfNegativeSubnormalShouldProduceNegativeOne()
    {
        Float256 negativeEpsilon = -Float256.Epsilon;
        Assert.Equal(Float256.NegativeOne.Bits.UpperBits, Float256.Floor(negativeEpsilon).Bits.UpperBits);
        Assert.Equal(Float256.NegativeOne.Bits.LowerBits, Float256.Floor(negativeEpsilon).Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.Ceiling of a positive subnormal should produce 1")]
    public void Float256CeilingOfPositiveSubnormalShouldProduceOne()
    {
        Assert.Equal(Float256.One.Bits.UpperBits, Float256.Ceiling(Float256.Epsilon).Bits.UpperBits);
        Assert.Equal(Float256.One.Bits.LowerBits, Float256.Ceiling(Float256.Epsilon).Bits.LowerBits);
    }
}
