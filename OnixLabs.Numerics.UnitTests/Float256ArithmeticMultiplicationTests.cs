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

public sealed class Float256ArithmeticMultiplicationTests
{
    [Fact(DisplayName = "Float256.Multiply NaN should propagate")]
    public void Float256MultiplyNaNShouldPropagate()
    {
        Assert.True(Float256.IsNaN(Float256.NaN * Float256.One));
        Assert.True(Float256.IsNaN(Float256.One * Float256.NaN));
    }

    [Fact(DisplayName = "Float256.Multiply zero by infinity should return NaN")]
    public void Float256MultiplyZeroByInfinityShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Zero * Float256.PositiveInfinity));
        Assert.True(Float256.IsNaN(Float256.PositiveInfinity * Float256.Zero));
        Assert.True(Float256.IsNaN(Float256.NegativeZero * Float256.NegativeInfinity));
    }

    [Fact(DisplayName = "Float256.Multiply infinity by finite non-zero should return signed infinity")]
    public void Float256MultiplyInfinityByFiniteShouldReturnSignedInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.PositiveInfinity * Float256.One));
        Assert.True(Float256.IsNegativeInfinity(Float256.PositiveInfinity * Float256.NegativeOne));
        Assert.True(Float256.IsNegativeInfinity(Float256.NegativeInfinity * Float256.One));
        Assert.True(Float256.IsPositiveInfinity(Float256.NegativeInfinity * Float256.NegativeOne));
    }

    [Fact(DisplayName = "Float256.Multiply zero by non-zero should return signed zero")]
    public void Float256MultiplyZeroByNonZeroShouldReturnSignedZero()
    {
        Assert.Equal(Float256.Zero.RawHighBits, (Float256.Zero * Float256.One).RawHighBits);
        Assert.Equal(Float256.Zero.RawLowBits, (Float256.Zero * Float256.One).RawLowBits);
        Assert.Equal(Float256.NegativeZero.RawHighBits, (Float256.NegativeZero * Float256.One).RawHighBits);
        Assert.Equal(Float256.NegativeZero.RawLowBits, (Float256.NegativeZero * Float256.One).RawLowBits);
        Assert.Equal(Float256.NegativeZero.RawHighBits, (Float256.Zero * Float256.NegativeOne).RawHighBits);
        Assert.Equal(Float256.NegativeZero.RawLowBits, (Float256.Zero * Float256.NegativeOne).RawLowBits);
        Assert.Equal(Float256.Zero.RawHighBits, (Float256.NegativeZero * Float256.NegativeOne).RawHighBits);
        Assert.Equal(Float256.Zero.RawLowBits, (Float256.NegativeZero * Float256.NegativeOne).RawLowBits);
    }

    // These data rows use only values that are exactly representable in IEEE 754 binary64 (and therefore
    // exactly representable in binary128 too). When both operands and the expected result are exact, the
    // Float256 result must match the binary128 representation of the double answer bit-for-bit. Powers of
    // ten (1e50 etc.) are intentionally excluded: those values are not exactly representable in double,
    // so multiplying them in Float256 produces a higher-precision answer than double can express.
    [Theory(DisplayName = "Float256.Multiply should match double multiplication for exact double values")]
    [InlineData(1.0, 1.0, 1.0)]
    [InlineData(2.0, 3.0, 6.0)]
    [InlineData(0.5, 2.0, 1.0)]
    [InlineData(-2.0, 3.0, -6.0)]
    [InlineData(-2.0, -3.0, 6.0)]
    [InlineData(10.0, 10.0, 100.0)]
    [InlineData(1.5, 1.5, 2.25)]
    [InlineData(0.25, 4.0, 1.0)]
    public void Float256MultiplyShouldMatchDoubleMultiplicationForExactValues(double left, double right, double expected)
    {
        Float256 actual = (Float256)left * (Float256)right;
        Float256 expectedFloat = expected;
        Assert.Equal(expectedFloat.RawHighBits, actual.RawHighBits);
        Assert.Equal(expectedFloat.RawLowBits, actual.RawLowBits);
    }

    [Fact(DisplayName = "Float256.Multiply by one should return the value")]
    public void Float256MultiplyByOneShouldReturnValue()
    {
        Float256 value = 3.14;
        Assert.Equal(value.RawHighBits, (value * Float256.One).RawHighBits);
        Assert.Equal(value.RawLowBits, (value * Float256.One).RawLowBits);
        Assert.Equal(value.RawHighBits, (Float256.One * value).RawHighBits);
        Assert.Equal(value.RawLowBits, (Float256.One * value).RawLowBits);
    }

    [Fact(DisplayName = "Float256.Multiply by two should double the value")]
    public void Float256MultiplyByTwoShouldDoubleValue()
    {
        Float256 value = 5.0;
        Float256 doubled = value * Float256.Two;
        Assert.Equal(((Float256)10.0).RawHighBits, doubled.RawHighBits);
        Assert.Equal(((Float256)10.0).RawLowBits, doubled.RawLowBits);
    }

    [Fact(DisplayName = "Float256.Multiply MaxValue by Two should overflow to infinity")]
    public void Float256MultiplyMaxByTwoShouldOverflow()
    {
        Float256 result = Float256.MaxValue * Float256.Two;
        Assert.True(Float256.IsPositiveInfinity(result));
    }

    [Fact(DisplayName = "Float256.Multiply tiny values should underflow to zero")]
    public void Float256MultiplyTinyValuesShouldUnderflowToZero()
    {
        Float256 result = Float256.Epsilon * Float256.Epsilon;
        Assert.True(Float256.IsZero(result));
    }
}
