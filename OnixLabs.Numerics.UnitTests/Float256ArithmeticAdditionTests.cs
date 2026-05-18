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

public sealed class Float256ArithmeticAdditionTests
{
    [Fact(DisplayName = "Float256.Add NaN + anything should return NaN")]
    public void Float256AddNaNShouldPropagate()
    {
        Assert.True(Float256.IsNaN(Float256.NaN + Float256.One));
        Assert.True(Float256.IsNaN(Float256.One + Float256.NaN));
        Assert.True(Float256.IsNaN(Float256.NaN + Float256.PositiveInfinity));
    }

    [Fact(DisplayName = "Float256.Add positive infinity to negative infinity should return NaN")]
    public void Float256AddInfinitiesOppositeSignsShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.PositiveInfinity + Float256.NegativeInfinity));
        Assert.True(Float256.IsNaN(Float256.NegativeInfinity + Float256.PositiveInfinity));
    }

    [Fact(DisplayName = "Float256.Add infinity to finite should return infinity")]
    public void Float256AddInfinityToFiniteShouldReturnInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.PositiveInfinity + Float256.One));
        Assert.True(Float256.IsNegativeInfinity(Float256.NegativeInfinity + Float256.One));
        Assert.True(Float256.IsPositiveInfinity(Float256.One + Float256.PositiveInfinity));
    }

    [Fact(DisplayName = "Float256.Add same-signed infinities should return that infinity")]
    public void Float256AddSameSignedInfinitiesShouldReturnInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.PositiveInfinity + Float256.PositiveInfinity));
        Assert.True(Float256.IsNegativeInfinity(Float256.NegativeInfinity + Float256.NegativeInfinity));
    }

    [Fact(DisplayName = "Float256.Add zeros with same signs should preserve sign")]
    public void Float256AddZerosWithSameSignShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.Bits.Upper, (Float256.Zero + Float256.Zero).Bits.Upper);
        Assert.Equal(Float256.Zero.Bits.Lower, (Float256.Zero + Float256.Zero).Bits.Lower);
        Assert.Equal(Float256.NegativeZero.Bits.Upper, (Float256.NegativeZero + Float256.NegativeZero).Bits.Upper);
        Assert.Equal(Float256.NegativeZero.Bits.Lower, (Float256.NegativeZero + Float256.NegativeZero).Bits.Lower);
    }

    [Fact(DisplayName = "Float256.Add zeros with opposite signs should return positive zero")]
    public void Float256AddZerosWithOppositeSignsShouldReturnPositiveZero()
    {
        Assert.Equal(Float256.Zero.Bits.Upper, (Float256.Zero + Float256.NegativeZero).Bits.Upper);
        Assert.Equal(Float256.Zero.Bits.Lower, (Float256.Zero + Float256.NegativeZero).Bits.Lower);
        Assert.Equal(Float256.Zero.Bits.Upper, (Float256.NegativeZero + Float256.Zero).Bits.Upper);
        Assert.Equal(Float256.Zero.Bits.Lower, (Float256.NegativeZero + Float256.Zero).Bits.Lower);
    }

    [Theory(DisplayName = "Float256.Add should produce the same result as double addition for exact double values")]
    [InlineData(1.0, 1.0, 2.0)]
    [InlineData(1.0, 2.0, 3.0)]
    [InlineData(0.5, 0.5, 1.0)]
    [InlineData(1.5, 2.5, 4.0)]
    [InlineData(10.0, 20.0, 30.0)]
    [InlineData(-1.0, 1.0, 0.0)]
    [InlineData(-1.0, -1.0, -2.0)]
    [InlineData(100.0, 0.5, 100.5)]
    [InlineData(1e100, 1e100, 2e100)]
    [InlineData(1e-100, 1e-100, 2e-100)]
    public void Float256AddShouldMatchDoubleAdditionForExactValues(double left, double right, double expected)
    {
        Float256 actual = (Float256)left + (Float256)right;
        Float256 expectedFloat = expected;
        Assert.Equal(expectedFloat.Bits.Upper, actual.Bits.Upper);
        Assert.Equal(expectedFloat.Bits.Lower, actual.Bits.Lower);
    }

    [Theory(DisplayName = "Float256.Add of value to itself should produce 2*value")]
    [InlineData(1.0)]
    [InlineData(3.0)]
    [InlineData(7.0)]
    [InlineData(100.0)]
    [InlineData(0.5)]
    [InlineData(-2.5)]
    [InlineData(1e50)]
    [InlineData(1e-50)]
    public void Float256AddOfValueToItselfShouldDoubleIt(double value)
    {
        Float256 a = value;
        Float256 sum = a + a;
        Float256 expectedFromDouble = value + value;
        Assert.Equal(expectedFromDouble.Bits.Upper, sum.Bits.Upper);
        Assert.Equal(expectedFromDouble.Bits.Lower, sum.Bits.Lower);
    }

    [Fact(DisplayName = "Float256.Add catastrophic cancellation should return zero")]
    public void Float256AddCatastrophicCancellationShouldReturnZero()
    {
        Float256 a = 1000.0;
        Float256 b = -1000.0;
        Float256 result = a + b;
        Assert.True(Float256.IsZero(result));
    }

    [Fact(DisplayName = "Float256.Add of value and its negation should return zero")]
    public void Float256AddValueAndItsNegationShouldReturnZero()
    {
        Float256 value = 3.14;
        Float256 sum = value + (-value);
        Assert.True(Float256.IsZero(sum));
    }

    [Fact(DisplayName = "Float256.Add of large finite values should overflow to infinity")]
    public void Float256AddLargeValuesShouldOverflow()
    {
        Float256 max = Float256.MaxValue;
        Float256 result = max + max;
        Assert.True(Float256.IsPositiveInfinity(result));
    }
}
