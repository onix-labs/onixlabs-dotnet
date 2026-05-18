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

public sealed class Float128ArithmeticAdditionTests
{
    [Fact(DisplayName = "Float128.Add NaN + anything should return NaN")]
    public void Float128AddNaNShouldPropagate()
    {
        Assert.True(Float128.IsNaN(Float128.NaN + Float128.One));
        Assert.True(Float128.IsNaN(Float128.One + Float128.NaN));
        Assert.True(Float128.IsNaN(Float128.NaN + Float128.PositiveInfinity));
    }

    [Fact(DisplayName = "Float128.Add positive infinity to negative infinity should return NaN")]
    public void Float128AddInfinitiesOppositeSignsShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.PositiveInfinity + Float128.NegativeInfinity));
        Assert.True(Float128.IsNaN(Float128.NegativeInfinity + Float128.PositiveInfinity));
    }

    [Fact(DisplayName = "Float128.Add infinity to finite should return infinity")]
    public void Float128AddInfinityToFiniteShouldReturnInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.PositiveInfinity + Float128.One));
        Assert.True(Float128.IsNegativeInfinity(Float128.NegativeInfinity + Float128.One));
        Assert.True(Float128.IsPositiveInfinity(Float128.One + Float128.PositiveInfinity));
    }

    [Fact(DisplayName = "Float128.Add same-signed infinities should return that infinity")]
    public void Float128AddSameSignedInfinitiesShouldReturnInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.PositiveInfinity + Float128.PositiveInfinity));
        Assert.True(Float128.IsNegativeInfinity(Float128.NegativeInfinity + Float128.NegativeInfinity));
    }

    [Fact(DisplayName = "Float128.Add zeros with same signs should preserve sign")]
    public void Float128AddZerosWithSameSignShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.RawBits, (Float128.Zero + Float128.Zero).RawBits);
        Assert.Equal(Float128.NegativeZero.RawBits, (Float128.NegativeZero + Float128.NegativeZero).RawBits);
    }

    [Fact(DisplayName = "Float128.Add zeros with opposite signs should return positive zero")]
    public void Float128AddZerosWithOppositeSignsShouldReturnPositiveZero()
    {
        Assert.Equal(Float128.Zero.RawBits, (Float128.Zero + Float128.NegativeZero).RawBits);
        Assert.Equal(Float128.Zero.RawBits, (Float128.NegativeZero + Float128.Zero).RawBits);
    }

    [Theory(DisplayName = "Float128.Add should produce the same result as double addition for exact double values")]
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
    public void Float128AddShouldMatchDoubleAdditionForExactValues(double left, double right, double expected)
    {
        Float128 actual = (Float128)left + (Float128)right;
        Float128 expectedFloat = expected;
        Assert.Equal(expectedFloat.RawBits, actual.RawBits);
    }

    [Theory(DisplayName = "Float128.Add of value to itself should produce 2*value")]
    [InlineData(1.0)]
    [InlineData(3.0)]
    [InlineData(7.0)]
    [InlineData(100.0)]
    [InlineData(0.5)]
    [InlineData(-2.5)]
    [InlineData(1e50)]
    [InlineData(1e-50)]
    public void Float128AddOfValueToItselfShouldDoubleIt(double value)
    {
        Float128 a = value;
        Float128 sum = a + a;
        Float128 expectedFromDouble = value + value;
        Assert.Equal(expectedFromDouble.RawBits, sum.RawBits);
    }

    [Fact(DisplayName = "Float128.Add catastrophic cancellation should return zero")]
    public void Float128AddCatastrophicCancellationShouldReturnZero()
    {
        Float128 a = 1000.0;
        Float128 b = -1000.0;
        Float128 result = a + b;
        Assert.True(Float128.IsZero(result));
    }

    [Fact(DisplayName = "Float128.Add of value and its negation should return zero")]
    public void Float128AddValueAndItsNegationShouldReturnZero()
    {
        Float128 value = 3.14;
        Float128 sum = value + (-value);
        Assert.True(Float128.IsZero(sum));
    }

    [Fact(DisplayName = "Float128.Add of large finite values should overflow to infinity")]
    public void Float128AddLargeValuesShouldOverflow()
    {
        Float128 max = Float128.MaxValue;
        Float128 result = max + max;
        Assert.True(Float128.IsPositiveInfinity(result));
    }
}
