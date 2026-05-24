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

public sealed class Float128RootTests
{
    [Fact(DisplayName = "Float128.Cbrt of NaN should return NaN")]
    public void Float128CbrtOfNaNShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Cbrt(Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.Cbrt of zero should preserve sign")]
    public void Float128CbrtOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.Bits, Float128.Cbrt(Float128.Zero).Bits);
        Assert.Equal(Float128.NegativeZero.Bits, Float128.Cbrt(Float128.NegativeZero).Bits);
    }

    [Fact(DisplayName = "Float128.Cbrt of infinities should preserve sign")]
    public void Float128CbrtOfInfinitiesShouldPreserveSign()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Cbrt(Float128.PositiveInfinity)));
        Assert.True(Float128.IsNegativeInfinity(Float128.Cbrt(Float128.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float128.Cbrt of perfect cubes should be approximately exact")]
    public void Float128CbrtOfPerfectCubesShouldBeExact()
    {
        AssertCloseToReference(Float128.Two, Float128.Cbrt((Float128)8), ulpTolerance: 4);
        AssertCloseToReference((Float128)3, Float128.Cbrt((Float128)27), ulpTolerance: 4);
        AssertCloseToReference((Float128)4, Float128.Cbrt((Float128)64), ulpTolerance: 4);
        AssertCloseToReference((Float128)10, Float128.Cbrt((Float128)1000), ulpTolerance: 4);
    }

    [Fact(DisplayName = "Float128.Cbrt of negative perfect cubes should be negative")]
    public void Float128CbrtOfNegativePerfectCubesShouldBeNegative()
    {
        AssertCloseToReference((Float128)(-2), Float128.Cbrt((Float128)(-8)), ulpTolerance: 4);
        AssertCloseToReference((Float128)(-3), Float128.Cbrt((Float128)(-27)), ulpTolerance: 4);
    }

    [Fact(DisplayName = "Float128.Cbrt of a value beyond the double range should not saturate to infinity")]
    public void Float128CbrtBeyondDoubleRangeShouldNotSaturate()
    {
        Float128 value = Float128.ScaleB(Float128.One, 3000);
        Float128 expected = Float128.ScaleB(Float128.One, 1000);
        Assert.Equal(expected.Bits, Float128.Cbrt(value).Bits);
    }

    [Fact(DisplayName = "Float128.Cbrt of a value below the double range should not collapse to zero")]
    public void Float128CbrtBelowDoubleRangeShouldNotCollapse()
    {
        Float128 value = Float128.ScaleB(Float128.One, -3000);
        Float128 expected = Float128.ScaleB(Float128.One, -1000);
        Assert.Equal(expected.Bits, Float128.Cbrt(value).Bits);
    }

    [Theory(DisplayName = "Float128.Cbrt should match Math.Cbrt within double precision")]
    [InlineData(0.5)]
    [InlineData(2.0)]
    [InlineData(15.0)]
    [InlineData(-7.0)]
    [InlineData(1e10)]
    public void Float128CbrtShouldMatchMathCbrt(double input)
    {
        Float128 actual = Float128.Cbrt((Float128)input);
        double expected = Math.Cbrt(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float128.RootN with n=1 should return value unchanged")]
    public void Float128RootNWithNEqualsOneShouldReturnValue()
    {
        Float128 value = (Float128)42;
        Assert.Equal(value, Float128.RootN(value, 1));
    }

    [Fact(DisplayName = "Float128.RootN with n=2 should match Sqrt")]
    public void Float128RootNWithNEqualsTwoShouldMatchSqrt()
    {
        Float128 value = (Float128)16;
        AssertCloseToReference(Float128.Sqrt(value), Float128.RootN(value, 2), ulpTolerance: 4);
    }

    [Fact(DisplayName = "Float128.RootN with n=3 should match Cbrt")]
    public void Float128RootNWithNEqualsThreeShouldMatchCbrt()
    {
        Float128 value = (Float128)27;
        AssertCloseToReference(Float128.Cbrt(value), Float128.RootN(value, 3), ulpTolerance: 4);
    }

    [Fact(DisplayName = "Float128.RootN with n=0 should return NaN")]
    public void Float128RootNWithNEqualsZeroShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.RootN(Float128.Two, 0)));
    }

    [Fact(DisplayName = "Float128.RootN of negative with even n should return NaN")]
    public void Float128RootNOfNegativeWithEvenNShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.RootN(Float128.NegativeOne, 4)));
        Assert.True(Float128.IsNaN(Float128.RootN((Float128)(-16), 2)));
    }

    [Fact(DisplayName = "Float128.RootN of negative with odd n should preserve sign")]
    public void Float128RootNOfNegativeWithOddNShouldPreserveSign()
    {
        Float128 result = Float128.RootN((Float128)(-32), 5);
        AssertCloseToReference((Float128)(-2), result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float128.RootN of zero with positive n should return zero with sign for odd n")]
    public void Float128RootNOfZeroWithPositiveNShouldReturnZero()
    {
        Assert.Equal(Float128.Zero.Bits, Float128.RootN(Float128.Zero, 4).Bits);
        Assert.Equal(Float128.NegativeZero.Bits, Float128.RootN(Float128.NegativeZero, 5).Bits);
    }

    [Fact(DisplayName = "Float128.Hypot of zero and zero should return zero")]
    public void Float128HypotOfZeroAndZeroShouldReturnZero()
    {
        Assert.Equal(Float128.Zero, Float128.Hypot(Float128.Zero, Float128.Zero));
    }

    [Fact(DisplayName = "Float128.Hypot with one infinite input should return positive infinity")]
    public void Float128HypotWithInfiniteInputShouldReturnPositiveInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Hypot(Float128.PositiveInfinity, Float128.One)));
        Assert.True(Float128.IsPositiveInfinity(Float128.Hypot(Float128.One, Float128.NegativeInfinity)));
        Assert.True(Float128.IsPositiveInfinity(Float128.Hypot(Float128.NaN, Float128.PositiveInfinity)));
    }

    [Fact(DisplayName = "Float128.Hypot of NaN with finite should return NaN")]
    public void Float128HypotOfNaNWithFiniteShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Hypot(Float128.NaN, Float128.One)));
    }

    [Fact(DisplayName = "Float128.Hypot of 3 and 4 should return 5")]
    public void Float128HypotOf3And4ShouldReturn5()
    {
        Float128 result = Float128.Hypot((Float128)3, (Float128)4);
        AssertCloseToReference((Float128)5, result, ulpTolerance: 4);
    }

    [Fact(DisplayName = "Float128.Hypot should be symmetric")]
    public void Float128HypotShouldBeSymmetric()
    {
        Float128 a = (Float128)7;
        Float128 b = (Float128)24;
        Assert.Equal(Float128.Hypot(a, b), Float128.Hypot(b, a));
    }

    [Fact(DisplayName = "Float128.Hypot should avoid overflow for large operands")]
    public void Float128HypotShouldAvoidOverflowForLargeOperands()
    {
        Float128 huge = Float128.Parse("1E2000");
        Float128 result = Float128.Hypot(huge, huge);
        Assert.True(Float128.IsFinite(result));
        AssertCloseToReference(huge * Float128.Sqrt(Float128.Two), result, ulpTolerance: 8);
    }

    private static void AssertCloseToReference(Float128 expected, Float128 actual, int ulpTolerance)
    {
        if (expected == actual) return;

        Float128 difference = Float128.Abs(expected - actual);
        Float128 expectedUlp = Float128.IsZero(expected)
            ? Float128.Epsilon
            : Float128.Abs(Float128.BitIncrement(Float128.Abs(expected)) - Float128.Abs(expected));
        Float128 tolerance = expectedUlp * (Float128)ulpTolerance;
        Assert.True(difference <= tolerance, $"Expected {expected} but got {actual} (difference {difference}, tolerance {tolerance})");
    }
}
