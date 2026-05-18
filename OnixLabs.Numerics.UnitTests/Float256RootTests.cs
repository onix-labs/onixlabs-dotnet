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

using System;

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float256RootTests
{
    [Fact(DisplayName = "Float256.Cbrt of NaN should return NaN")]
    public void Float256CbrtOfNaNShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Cbrt(Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Cbrt of zero should preserve sign")]
    public void Float256CbrtOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.Bits.UpperBits, Float256.Cbrt(Float256.Zero).Bits.UpperBits);
        Assert.Equal(Float256.Zero.Bits.LowerBits, Float256.Cbrt(Float256.Zero).Bits.LowerBits);
        Assert.Equal(Float256.NegativeZero.Bits.UpperBits, Float256.Cbrt(Float256.NegativeZero).Bits.UpperBits);
        Assert.Equal(Float256.NegativeZero.Bits.LowerBits, Float256.Cbrt(Float256.NegativeZero).Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.Cbrt of infinities should preserve sign")]
    public void Float256CbrtOfInfinitiesShouldPreserveSign()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Cbrt(Float256.PositiveInfinity)));
        Assert.True(Float256.IsNegativeInfinity(Float256.Cbrt(Float256.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float256.Cbrt of perfect cubes should be approximately exact")]
    public void Float256CbrtOfPerfectCubesShouldBeExact()
    {
        AssertCloseToReference(Float256.Two, Float256.Cbrt((Float256)8), ulpTolerance: 4);
        AssertCloseToReference((Float256)3, Float256.Cbrt((Float256)27), ulpTolerance: 4);
        AssertCloseToReference((Float256)4, Float256.Cbrt((Float256)64), ulpTolerance: 4);
        AssertCloseToReference((Float256)10, Float256.Cbrt((Float256)1000), ulpTolerance: 4);
    }

    [Fact(DisplayName = "Float256.Cbrt of negative perfect cubes should be negative")]
    public void Float256CbrtOfNegativePerfectCubesShouldBeNegative()
    {
        AssertCloseToReference((Float256)(-2), Float256.Cbrt((Float256)(-8)), ulpTolerance: 4);
        AssertCloseToReference((Float256)(-3), Float256.Cbrt((Float256)(-27)), ulpTolerance: 4);
    }

    [Theory(DisplayName = "Float256.Cbrt should match Math.Cbrt within double precision")]
    [InlineData(0.5)]
    [InlineData(2.0)]
    [InlineData(15.0)]
    [InlineData(-7.0)]
    [InlineData(1e10)]
    public void Float256CbrtShouldMatchMathCbrt(double input)
    {
        Float256 actual = Float256.Cbrt((Float256)input);
        Float256 expected = (Float256)Math.Cbrt(input);
        Float256 tolerance = Float256.Max(Float256.Abs(expected) * Float256.Parse("1E-14"), Float256.Parse("1E-14"));
        Assert.True(Float256.Abs(actual - expected) <= tolerance, $"Expected {expected} but got {actual}");
    }

    [Fact(DisplayName = "Float256.RootN with n=1 should return value unchanged")]
    public void Float256RootNWithNEqualsOneShouldReturnValue()
    {
        Float256 value = (Float256)42;
        Assert.Equal(value, Float256.RootN(value, 1));
    }

    [Fact(DisplayName = "Float256.RootN with n=2 should match Sqrt")]
    public void Float256RootNWithNEqualsTwoShouldMatchSqrt()
    {
        Float256 value = (Float256)16;
        AssertCloseToReference(Float256.Sqrt(value), Float256.RootN(value, 2), ulpTolerance: 4);
    }

    [Fact(DisplayName = "Float256.RootN with n=3 should match Cbrt")]
    public void Float256RootNWithNEqualsThreeShouldMatchCbrt()
    {
        Float256 value = (Float256)27;
        AssertCloseToReference(Float256.Cbrt(value), Float256.RootN(value, 3), ulpTolerance: 4);
    }

    [Fact(DisplayName = "Float256.RootN with n=0 should return NaN")]
    public void Float256RootNWithNEqualsZeroShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.RootN(Float256.Two, 0)));
    }

    [Fact(DisplayName = "Float256.RootN of negative with even n should return NaN")]
    public void Float256RootNOfNegativeWithEvenNShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.RootN(Float256.NegativeOne, 4)));
        Assert.True(Float256.IsNaN(Float256.RootN((Float256)(-16), 2)));
    }

    [Fact(DisplayName = "Float256.RootN of negative with odd n should preserve sign")]
    public void Float256RootNOfNegativeWithOddNShouldPreserveSign()
    {
        Float256 result = Float256.RootN((Float256)(-32), 5);
        AssertCloseToReference((Float256)(-2), result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float256.RootN of zero with positive n should return zero with sign for odd n")]
    public void Float256RootNOfZeroWithPositiveNShouldReturnZero()
    {
        Assert.Equal(Float256.Zero.Bits.UpperBits, Float256.RootN(Float256.Zero, 4).Bits.UpperBits);
        Assert.Equal(Float256.Zero.Bits.LowerBits, Float256.RootN(Float256.Zero, 4).Bits.LowerBits);
        Assert.Equal(Float256.NegativeZero.Bits.UpperBits, Float256.RootN(Float256.NegativeZero, 5).Bits.UpperBits);
        Assert.Equal(Float256.NegativeZero.Bits.LowerBits, Float256.RootN(Float256.NegativeZero, 5).Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.Hypot of zero and zero should return zero")]
    public void Float256HypotOfZeroAndZeroShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.Hypot(Float256.Zero, Float256.Zero));
    }

    [Fact(DisplayName = "Float256.Hypot with one infinite input should return positive infinity")]
    public void Float256HypotWithInfiniteInputShouldReturnPositiveInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Hypot(Float256.PositiveInfinity, Float256.One)));
        Assert.True(Float256.IsPositiveInfinity(Float256.Hypot(Float256.One, Float256.NegativeInfinity)));
        Assert.True(Float256.IsPositiveInfinity(Float256.Hypot(Float256.NaN, Float256.PositiveInfinity)));
    }

    [Fact(DisplayName = "Float256.Hypot of NaN with finite should return NaN")]
    public void Float256HypotOfNaNWithFiniteShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Hypot(Float256.NaN, Float256.One)));
    }

    [Fact(DisplayName = "Float256.Hypot of 3 and 4 should return 5")]
    public void Float256HypotOf3And4ShouldReturn5()
    {
        Float256 result = Float256.Hypot((Float256)3, (Float256)4);
        AssertCloseToReference((Float256)5, result, ulpTolerance: 4);
    }

    [Fact(DisplayName = "Float256.Hypot should be symmetric")]
    public void Float256HypotShouldBeSymmetric()
    {
        Float256 a = (Float256)7;
        Float256 b = (Float256)24;
        Assert.Equal(Float256.Hypot(a, b), Float256.Hypot(b, a));
    }

    [Fact(DisplayName = "Float256.Hypot should avoid overflow for large operands")]
    public void Float256HypotShouldAvoidOverflowForLargeOperands()
    {
        Float256 huge = Float256.Parse("1E2000");
        Float256 result = Float256.Hypot(huge, huge);
        Assert.True(Float256.IsFinite(result));
        AssertCloseToReference(huge * Float256.Sqrt(Float256.Two), result, ulpTolerance: 8);
    }

    private static void AssertCloseToReference(Float256 expected, Float256 actual, int ulpTolerance)
    {
        if (expected == actual) return;

        Float256 difference = Float256.Abs(expected - actual);
        Float256 expectedUlp = Float256.IsZero(expected)
            ? Float256.Epsilon
            : Float256.Abs(Float256.BitIncrement(Float256.Abs(expected)) - Float256.Abs(expected));
        Float256 tolerance = expectedUlp * (Float256)ulpTolerance;
        Assert.True(difference <= tolerance, $"Expected {expected} but got {actual} (difference {difference}, tolerance {tolerance})");
    }
}
