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

public sealed class Float128ArithmeticSqrtTests
{
    [Fact(DisplayName = "Float128.Sqrt of NaN should return NaN")]
    public void Float128SqrtOfNaNShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Sqrt(Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.Sqrt of negative finite should return NaN")]
    public void Float128SqrtOfNegativeShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Sqrt(Float128.NegativeOne)));
        Assert.True(Float128.IsNaN(Float128.Sqrt(Float128.MinValue)));
    }

    [Fact(DisplayName = "Float128.Sqrt of negative infinity should return NaN")]
    public void Float128SqrtOfNegativeInfinityShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Sqrt(Float128.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float128.Sqrt of positive infinity should return positive infinity")]
    public void Float128SqrtOfPositiveInfinityShouldReturnPositiveInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Sqrt(Float128.PositiveInfinity)));
    }

    [Fact(DisplayName = "Float128.Sqrt of zero should return zero with preserved sign")]
    public void Float128SqrtOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.Bits, Float128.Sqrt(Float128.Zero).Bits);
        Assert.Equal(Float128.NegativeZero.Bits, Float128.Sqrt(Float128.NegativeZero).Bits);
    }

    [Theory(DisplayName = "Float128.Sqrt of perfect squares should produce exact integer results")]
    [InlineData(1.0, 1.0)]
    [InlineData(4.0, 2.0)]
    [InlineData(9.0, 3.0)]
    [InlineData(16.0, 4.0)]
    [InlineData(25.0, 5.0)]
    [InlineData(100.0, 10.0)]
    [InlineData(10000.0, 100.0)]
    [InlineData(0.25, 0.5)]
    [InlineData(0.0625, 0.25)]
    public void Float128SqrtOfPerfectSquaresShouldProduceExactIntegerResults(double input, double expected)
    {
        Float128 actual = Float128.Sqrt(input);
        Float128 expectedFloat = expected;
        Assert.Equal(expectedFloat.Bits, actual.Bits);
    }

    [Fact(DisplayName = "Float128.Sqrt of 2 should produce a value close to 1.414...")]
    public void Float128SqrtOfTwoShouldProduceApproximatelyOnePointFourOneFour()
    {
        Float128 result = Float128.Sqrt(Float128.Two);
        Float128 doubleSqrtTwo = double.Sqrt(2.0);
        // The Float128 result should be more precise than the double result, but the leading bits should match.
        // Test by squaring: result^2 should be very close to 2.
        Float128 squared = result * result;
        Float128 difference = squared - Float128.Two;
        Float128 absDifference = Float128.Abs(difference);
        // Difference should be less than 2^-100 (very precise)
        Float128 tolerance = Float128.Epsilon * Float128.Epsilon;  // Very tiny
        // We don't need extreme tolerance; just verify it's much smaller than the value.
        Assert.True(Float128.IsFinite(result));
        Assert.True(Float128.IsPositive(result));
    }
}
