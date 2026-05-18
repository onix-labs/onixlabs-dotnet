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

public sealed class Float256IsTests
{
    [Fact(DisplayName = "Float256.IsNaN should detect NaN values")]
    public void Float256IsNaNShouldDetectNaN()
    {
        Assert.True(Float256.IsNaN(Float256.NaN));
        Assert.False(Float256.IsNaN(Float256.Zero));
        Assert.False(Float256.IsNaN(Float256.NegativeZero));
        Assert.False(Float256.IsNaN(Float256.One));
        Assert.False(Float256.IsNaN(Float256.NegativeOne));
        Assert.False(Float256.IsNaN(Float256.MaxValue));
        Assert.False(Float256.IsNaN(Float256.MinValue));
        Assert.False(Float256.IsNaN(Float256.Epsilon));
        Assert.False(Float256.IsNaN(Float256.PositiveInfinity));
        Assert.False(Float256.IsNaN(Float256.NegativeInfinity));
    }

    [Fact(DisplayName = "Float256.IsInfinity should detect positive and negative infinity")]
    public void Float256IsInfinityShouldDetectInfinity()
    {
        Assert.True(Float256.IsInfinity(Float256.PositiveInfinity));
        Assert.True(Float256.IsInfinity(Float256.NegativeInfinity));
        Assert.False(Float256.IsInfinity(Float256.Zero));
        Assert.False(Float256.IsInfinity(Float256.NaN));
        Assert.False(Float256.IsInfinity(Float256.MaxValue));
        Assert.False(Float256.IsInfinity(Float256.MinValue));
    }

    [Fact(DisplayName = "Float256.IsPositiveInfinity should detect only positive infinity")]
    public void Float256IsPositiveInfinityShouldDetectOnlyPositiveInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.PositiveInfinity));
        Assert.False(Float256.IsPositiveInfinity(Float256.NegativeInfinity));
        Assert.False(Float256.IsPositiveInfinity(Float256.NaN));
        Assert.False(Float256.IsPositiveInfinity(Float256.MaxValue));
    }

    [Fact(DisplayName = "Float256.IsNegativeInfinity should detect only negative infinity")]
    public void Float256IsNegativeInfinityShouldDetectOnlyNegativeInfinity()
    {
        Assert.True(Float256.IsNegativeInfinity(Float256.NegativeInfinity));
        Assert.False(Float256.IsNegativeInfinity(Float256.PositiveInfinity));
        Assert.False(Float256.IsNegativeInfinity(Float256.NaN));
        Assert.False(Float256.IsNegativeInfinity(Float256.MinValue));
    }

    [Fact(DisplayName = "Float256.IsFinite should detect finite values")]
    public void Float256IsFiniteShouldDetectFiniteValues()
    {
        Assert.True(Float256.IsFinite(Float256.Zero));
        Assert.True(Float256.IsFinite(Float256.NegativeZero));
        Assert.True(Float256.IsFinite(Float256.One));
        Assert.True(Float256.IsFinite(Float256.NegativeOne));
        Assert.True(Float256.IsFinite(Float256.MaxValue));
        Assert.True(Float256.IsFinite(Float256.MinValue));
        Assert.True(Float256.IsFinite(Float256.Epsilon));
        Assert.False(Float256.IsFinite(Float256.PositiveInfinity));
        Assert.False(Float256.IsFinite(Float256.NegativeInfinity));
        Assert.False(Float256.IsFinite(Float256.NaN));
    }

    [Fact(DisplayName = "Float256.IsNormal should detect normal values")]
    public void Float256IsNormalShouldDetectNormalValues()
    {
        Assert.True(Float256.IsNormal(Float256.One));
        Assert.True(Float256.IsNormal(Float256.NegativeOne));
        Assert.True(Float256.IsNormal(Float256.Two));
        Assert.True(Float256.IsNormal(Float256.Ten));
        Assert.True(Float256.IsNormal(Float256.MaxValue));
        Assert.True(Float256.IsNormal(Float256.MinValue));
        Assert.False(Float256.IsNormal(Float256.Zero));
        Assert.False(Float256.IsNormal(Float256.NegativeZero));
        Assert.False(Float256.IsNormal(Float256.Epsilon));
        Assert.False(Float256.IsNormal(Float256.PositiveInfinity));
        Assert.False(Float256.IsNormal(Float256.NegativeInfinity));
        Assert.False(Float256.IsNormal(Float256.NaN));
    }

    [Fact(DisplayName = "Float256.IsSubnormal should detect subnormal non-zero values")]
    public void Float256IsSubnormalShouldDetectSubnormalValues()
    {
        Assert.True(Float256.IsSubnormal(Float256.Epsilon));
        Assert.False(Float256.IsSubnormal(Float256.Zero));
        Assert.False(Float256.IsSubnormal(Float256.NegativeZero));
        Assert.False(Float256.IsSubnormal(Float256.One));
        Assert.False(Float256.IsSubnormal(Float256.MaxValue));
        Assert.False(Float256.IsSubnormal(Float256.PositiveInfinity));
        Assert.False(Float256.IsSubnormal(Float256.NaN));
    }

    [Fact(DisplayName = "Float256.IsZero should detect both positive and negative zero")]
    public void Float256IsZeroShouldDetectBothZeros()
    {
        Assert.True(Float256.IsZero(Float256.Zero));
        Assert.True(Float256.IsZero(Float256.NegativeZero));
        Assert.False(Float256.IsZero(Float256.One));
        Assert.False(Float256.IsZero(Float256.Epsilon));
        Assert.False(Float256.IsZero(Float256.NaN));
        Assert.False(Float256.IsZero(Float256.PositiveInfinity));
    }

    [Fact(DisplayName = "Float256.IsNegative should inspect the sign bit only")]
    public void Float256IsNegativeShouldInspectSignBitOnly()
    {
        Assert.True(Float256.IsNegative(Float256.NegativeZero));
        Assert.True(Float256.IsNegative(Float256.NegativeOne));
        Assert.True(Float256.IsNegative(Float256.MinValue));
        Assert.True(Float256.IsNegative(Float256.NegativeInfinity));
        Assert.False(Float256.IsNegative(Float256.Zero));
        Assert.False(Float256.IsNegative(Float256.One));
        Assert.False(Float256.IsNegative(Float256.MaxValue));
        Assert.False(Float256.IsNegative(Float256.PositiveInfinity));
        Assert.False(Float256.IsNegative(Float256.NaN));
    }

    [Fact(DisplayName = "Float256.IsPositive should be the inverse of IsNegative")]
    public void Float256IsPositiveShouldBeInverseOfIsNegative()
    {
        Assert.True(Float256.IsPositive(Float256.Zero));
        Assert.True(Float256.IsPositive(Float256.One));
        Assert.True(Float256.IsPositive(Float256.MaxValue));
        Assert.True(Float256.IsPositive(Float256.PositiveInfinity));
        Assert.True(Float256.IsPositive(Float256.NaN));
        Assert.False(Float256.IsPositive(Float256.NegativeZero));
        Assert.False(Float256.IsPositive(Float256.NegativeOne));
        Assert.False(Float256.IsPositive(Float256.MinValue));
        Assert.False(Float256.IsPositive(Float256.NegativeInfinity));
    }

    [Theory(DisplayName = "Float256.IsInteger should match the integer-ness of integer-valued doubles")]
    [InlineData(0.0, true)]
    [InlineData(1.0, true)]
    [InlineData(-1.0, true)]
    [InlineData(2.0, true)]
    [InlineData(10.0, true)]
    [InlineData(-7.0, true)]
    [InlineData(1234567890.0, true)]
    [InlineData(0.5, false)]
    [InlineData(-0.5, false)]
    [InlineData(1.5, false)]
    [InlineData(3.14, false)]
    [InlineData(0.1, false)]
    public void Float256IsIntegerShouldMatchDoubleSemantics(double value, bool expected)
    {
        Assert.Equal(expected, Float256.IsInteger(value));
    }

    [Fact(DisplayName = "Float256.IsInteger should return false for non-finite values")]
    public void Float256IsIntegerShouldReturnFalseForNonFinite()
    {
        Assert.False(Float256.IsInteger(Float256.PositiveInfinity));
        Assert.False(Float256.IsInteger(Float256.NegativeInfinity));
        Assert.False(Float256.IsInteger(Float256.NaN));
        Assert.False(Float256.IsInteger(Float256.Epsilon));
    }

    [Theory(DisplayName = "Float256.IsEvenInteger should detect even integral values")]
    [InlineData(0.0, true)]
    [InlineData(2.0, true)]
    [InlineData(-2.0, true)]
    [InlineData(10.0, true)]
    [InlineData(1024.0, true)]
    [InlineData(1.0, false)]
    [InlineData(-1.0, false)]
    [InlineData(3.0, false)]
    [InlineData(0.5, false)]
    public void Float256IsEvenIntegerShouldMatchDoubleSemantics(double value, bool expected)
    {
        Assert.Equal(expected, Float256.IsEvenInteger(value));
    }

    [Theory(DisplayName = "Float256.IsOddInteger should detect odd integral values")]
    [InlineData(1.0, true)]
    [InlineData(-1.0, true)]
    [InlineData(3.0, true)]
    [InlineData(7.0, true)]
    [InlineData(0.0, false)]
    [InlineData(2.0, false)]
    [InlineData(10.0, false)]
    [InlineData(0.5, false)]
    public void Float256IsOddIntegerShouldMatchDoubleSemantics(double value, bool expected)
    {
        Assert.Equal(expected, Float256.IsOddInteger(value));
    }
}
