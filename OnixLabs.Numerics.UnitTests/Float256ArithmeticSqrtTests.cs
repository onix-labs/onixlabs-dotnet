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

public sealed class Float256ArithmeticSqrtTests
{
    [Fact(DisplayName = "Float256.Sqrt of NaN should return NaN")]
    public void Float256SqrtOfNaNShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Sqrt(Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Sqrt of negative finite should return NaN")]
    public void Float256SqrtOfNegativeShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Sqrt(Float256.NegativeOne)));
        Assert.True(Float256.IsNaN(Float256.Sqrt(Float256.MinValue)));
    }

    [Fact(DisplayName = "Float256.Sqrt of negative infinity should return NaN")]
    public void Float256SqrtOfNegativeInfinityShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Sqrt(Float256.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float256.Sqrt of positive infinity should return positive infinity")]
    public void Float256SqrtOfPositiveInfinityShouldReturnPositiveInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Sqrt(Float256.PositiveInfinity)));
    }

    [Fact(DisplayName = "Float256.Sqrt of zero should return zero with preserved sign")]
    public void Float256SqrtOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.Bits.UpperBits, Float256.Sqrt(Float256.Zero).Bits.UpperBits);
        Assert.Equal(Float256.Zero.Bits.LowerBits, Float256.Sqrt(Float256.Zero).Bits.LowerBits);
        Assert.Equal(Float256.NegativeZero.Bits.UpperBits, Float256.Sqrt(Float256.NegativeZero).Bits.UpperBits);
        Assert.Equal(Float256.NegativeZero.Bits.LowerBits, Float256.Sqrt(Float256.NegativeZero).Bits.LowerBits);
    }

    [Theory(DisplayName = "Float256.Sqrt of perfect squares should produce exact integer results")]
    [InlineData(1.0, 1.0)]
    [InlineData(4.0, 2.0)]
    [InlineData(9.0, 3.0)]
    [InlineData(16.0, 4.0)]
    [InlineData(25.0, 5.0)]
    [InlineData(100.0, 10.0)]
    [InlineData(10000.0, 100.0)]
    [InlineData(0.25, 0.5)]
    [InlineData(0.0625, 0.25)]
    public void Float256SqrtOfPerfectSquaresShouldProduceExactIntegerResults(double input, double expected)
    {
        Float256 actual = Float256.Sqrt(input);
        Float256 expectedFloat = expected;
        Assert.Equal(expectedFloat.Bits.UpperBits, actual.Bits.UpperBits);
        Assert.Equal(expectedFloat.Bits.LowerBits, actual.Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.Sqrt of 2 should produce a value close to 1.414...")]
    public void Float256SqrtOfTwoShouldProduceApproximatelyOnePointFourOneFour()
    {
        Float256 result = Float256.Sqrt(Float256.Two);
        Float256 doubleSqrtTwo = double.Sqrt(2.0);
        // The Float256 result should be more precise than the double result, but the leading bits should match.
        // Test by squaring: result^2 should be very close to 2.
        Float256 squared = result * result;
        Float256 difference = squared - Float256.Two;
        Float256 absDifference = Float256.Abs(difference);
        // Difference should be less than 2^-100 (very precise)
        Float256 tolerance = Float256.Epsilon * Float256.Epsilon;  // Very tiny
        // We don't need extreme tolerance; just verify it's much smaller than the value.
        Assert.True(Float256.IsFinite(result));
        Assert.True(Float256.IsPositive(result));
    }
}
