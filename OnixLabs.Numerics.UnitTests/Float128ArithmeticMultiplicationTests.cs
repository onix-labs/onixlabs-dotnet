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

public sealed class Float128ArithmeticMultiplicationTests
{
    [Fact(DisplayName = "Float128.Multiply NaN should propagate")]
    public void Float128MultiplyNaNShouldPropagate()
    {
        Assert.True(Float128.IsNaN(Float128.NaN * Float128.One));
        Assert.True(Float128.IsNaN(Float128.One * Float128.NaN));
    }

    [Fact(DisplayName = "Float128.Multiply zero by infinity should return NaN")]
    public void Float128MultiplyZeroByInfinityShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Zero * Float128.PositiveInfinity));
        Assert.True(Float128.IsNaN(Float128.PositiveInfinity * Float128.Zero));
        Assert.True(Float128.IsNaN(Float128.NegativeZero * Float128.NegativeInfinity));
    }

    [Fact(DisplayName = "Float128.Multiply infinity by finite non-zero should return signed infinity")]
    public void Float128MultiplyInfinityByFiniteShouldReturnSignedInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.PositiveInfinity * Float128.One));
        Assert.True(Float128.IsNegativeInfinity(Float128.PositiveInfinity * Float128.NegativeOne));
        Assert.True(Float128.IsNegativeInfinity(Float128.NegativeInfinity * Float128.One));
        Assert.True(Float128.IsPositiveInfinity(Float128.NegativeInfinity * Float128.NegativeOne));
    }

    [Fact(DisplayName = "Float128.Multiply zero by non-zero should return signed zero")]
    public void Float128MultiplyZeroByNonZeroShouldReturnSignedZero()
    {
        Assert.Equal(Float128.Zero.RawBits, (Float128.Zero * Float128.One).RawBits);
        Assert.Equal(Float128.NegativeZero.RawBits, (Float128.NegativeZero * Float128.One).RawBits);
        Assert.Equal(Float128.NegativeZero.RawBits, (Float128.Zero * Float128.NegativeOne).RawBits);
        Assert.Equal(Float128.Zero.RawBits, (Float128.NegativeZero * Float128.NegativeOne).RawBits);
    }

    // These data rows use only values that are exactly representable in IEEE 754 binary64 (and therefore
    // exactly representable in binary128 too). When both operands and the expected result are exact, the
    // Float128 result must match the binary128 representation of the double answer bit-for-bit. Powers of
    // ten (1e50 etc.) are intentionally excluded: those values are not exactly representable in double,
    // so multiplying them in Float128 produces a higher-precision answer than double can express.
    [Theory(DisplayName = "Float128.Multiply should match double multiplication for exact double values")]
    [InlineData(1.0, 1.0, 1.0)]
    [InlineData(2.0, 3.0, 6.0)]
    [InlineData(0.5, 2.0, 1.0)]
    [InlineData(-2.0, 3.0, -6.0)]
    [InlineData(-2.0, -3.0, 6.0)]
    [InlineData(10.0, 10.0, 100.0)]
    [InlineData(1.5, 1.5, 2.25)]
    [InlineData(0.25, 4.0, 1.0)]
    public void Float128MultiplyShouldMatchDoubleMultiplicationForExactValues(double left, double right, double expected)
    {
        Float128 actual = (Float128)left * (Float128)right;
        Float128 expectedFloat = expected;
        Assert.Equal(expectedFloat.RawBits, actual.RawBits);
    }

    [Fact(DisplayName = "Float128.Multiply by one should return the value")]
    public void Float128MultiplyByOneShouldReturnValue()
    {
        Float128 value = 3.14;
        Assert.Equal(value.RawBits, (value * Float128.One).RawBits);
        Assert.Equal(value.RawBits, (Float128.One * value).RawBits);
    }

    [Fact(DisplayName = "Float128.Multiply by two should double the value")]
    public void Float128MultiplyByTwoShouldDoubleValue()
    {
        Float128 value = 5.0;
        Float128 doubled = value * Float128.Two;
        Assert.Equal(((Float128)10.0).RawBits, doubled.RawBits);
    }

    [Fact(DisplayName = "Float128.Multiply MaxValue by Two should overflow to infinity")]
    public void Float128MultiplyMaxByTwoShouldOverflow()
    {
        Float128 result = Float128.MaxValue * Float128.Two;
        Assert.True(Float128.IsPositiveInfinity(result));
    }

    [Fact(DisplayName = "Float128.Multiply tiny values should underflow to zero")]
    public void Float128MultiplyTinyValuesShouldUnderflowToZero()
    {
        Float128 result = Float128.Epsilon * Float128.Epsilon;
        Assert.True(Float128.IsZero(result));
    }
}
