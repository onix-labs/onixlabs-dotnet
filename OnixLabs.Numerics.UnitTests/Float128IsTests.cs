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

public sealed class Float128IsTests
{
    [Fact(DisplayName = "Float128.IsNaN should detect NaN values")]
    public void Float128IsNaNShouldDetectNaN()
    {
        Assert.True(Float128.IsNaN(Float128.NaN));
        Assert.False(Float128.IsNaN(Float128.Zero));
        Assert.False(Float128.IsNaN(Float128.NegativeZero));
        Assert.False(Float128.IsNaN(Float128.One));
        Assert.False(Float128.IsNaN(Float128.NegativeOne));
        Assert.False(Float128.IsNaN(Float128.MaxValue));
        Assert.False(Float128.IsNaN(Float128.MinValue));
        Assert.False(Float128.IsNaN(Float128.Epsilon));
        Assert.False(Float128.IsNaN(Float128.PositiveInfinity));
        Assert.False(Float128.IsNaN(Float128.NegativeInfinity));
    }

    [Fact(DisplayName = "Float128.IsInfinity should detect positive and negative infinity")]
    public void Float128IsInfinityShouldDetectInfinity()
    {
        Assert.True(Float128.IsInfinity(Float128.PositiveInfinity));
        Assert.True(Float128.IsInfinity(Float128.NegativeInfinity));
        Assert.False(Float128.IsInfinity(Float128.Zero));
        Assert.False(Float128.IsInfinity(Float128.NaN));
        Assert.False(Float128.IsInfinity(Float128.MaxValue));
        Assert.False(Float128.IsInfinity(Float128.MinValue));
    }

    [Fact(DisplayName = "Float128.IsPositiveInfinity should detect only positive infinity")]
    public void Float128IsPositiveInfinityShouldDetectOnlyPositiveInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.PositiveInfinity));
        Assert.False(Float128.IsPositiveInfinity(Float128.NegativeInfinity));
        Assert.False(Float128.IsPositiveInfinity(Float128.NaN));
        Assert.False(Float128.IsPositiveInfinity(Float128.MaxValue));
    }

    [Fact(DisplayName = "Float128.IsNegativeInfinity should detect only negative infinity")]
    public void Float128IsNegativeInfinityShouldDetectOnlyNegativeInfinity()
    {
        Assert.True(Float128.IsNegativeInfinity(Float128.NegativeInfinity));
        Assert.False(Float128.IsNegativeInfinity(Float128.PositiveInfinity));
        Assert.False(Float128.IsNegativeInfinity(Float128.NaN));
        Assert.False(Float128.IsNegativeInfinity(Float128.MinValue));
    }

    [Fact(DisplayName = "Float128.IsFinite should detect finite values")]
    public void Float128IsFiniteShouldDetectFiniteValues()
    {
        Assert.True(Float128.IsFinite(Float128.Zero));
        Assert.True(Float128.IsFinite(Float128.NegativeZero));
        Assert.True(Float128.IsFinite(Float128.One));
        Assert.True(Float128.IsFinite(Float128.NegativeOne));
        Assert.True(Float128.IsFinite(Float128.MaxValue));
        Assert.True(Float128.IsFinite(Float128.MinValue));
        Assert.True(Float128.IsFinite(Float128.Epsilon));
        Assert.False(Float128.IsFinite(Float128.PositiveInfinity));
        Assert.False(Float128.IsFinite(Float128.NegativeInfinity));
        Assert.False(Float128.IsFinite(Float128.NaN));
    }

    [Fact(DisplayName = "Float128.IsNormal should detect normal values")]
    public void Float128IsNormalShouldDetectNormalValues()
    {
        Assert.True(Float128.IsNormal(Float128.One));
        Assert.True(Float128.IsNormal(Float128.NegativeOne));
        Assert.True(Float128.IsNormal(Float128.Two));
        Assert.True(Float128.IsNormal(Float128.Ten));
        Assert.True(Float128.IsNormal(Float128.MaxValue));
        Assert.True(Float128.IsNormal(Float128.MinValue));
        Assert.False(Float128.IsNormal(Float128.Zero));
        Assert.False(Float128.IsNormal(Float128.NegativeZero));
        Assert.False(Float128.IsNormal(Float128.Epsilon));
        Assert.False(Float128.IsNormal(Float128.PositiveInfinity));
        Assert.False(Float128.IsNormal(Float128.NegativeInfinity));
        Assert.False(Float128.IsNormal(Float128.NaN));
    }

    [Fact(DisplayName = "Float128.IsSubnormal should detect subnormal non-zero values")]
    public void Float128IsSubnormalShouldDetectSubnormalValues()
    {
        Assert.True(Float128.IsSubnormal(Float128.Epsilon));
        Assert.False(Float128.IsSubnormal(Float128.Zero));
        Assert.False(Float128.IsSubnormal(Float128.NegativeZero));
        Assert.False(Float128.IsSubnormal(Float128.One));
        Assert.False(Float128.IsSubnormal(Float128.MaxValue));
        Assert.False(Float128.IsSubnormal(Float128.PositiveInfinity));
        Assert.False(Float128.IsSubnormal(Float128.NaN));
    }

    [Fact(DisplayName = "Float128.IsZero should detect both positive and negative zero")]
    public void Float128IsZeroShouldDetectBothZeros()
    {
        Assert.True(Float128.IsZero(Float128.Zero));
        Assert.True(Float128.IsZero(Float128.NegativeZero));
        Assert.False(Float128.IsZero(Float128.One));
        Assert.False(Float128.IsZero(Float128.Epsilon));
        Assert.False(Float128.IsZero(Float128.NaN));
        Assert.False(Float128.IsZero(Float128.PositiveInfinity));
    }

    [Fact(DisplayName = "Float128.IsNegative should inspect the sign bit only")]
    public void Float128IsNegativeShouldInspectSignBitOnly()
    {
        Assert.True(Float128.IsNegative(Float128.NegativeZero));
        Assert.True(Float128.IsNegative(Float128.NegativeOne));
        Assert.True(Float128.IsNegative(Float128.MinValue));
        Assert.True(Float128.IsNegative(Float128.NegativeInfinity));
        Assert.False(Float128.IsNegative(Float128.Zero));
        Assert.False(Float128.IsNegative(Float128.One));
        Assert.False(Float128.IsNegative(Float128.MaxValue));
        Assert.False(Float128.IsNegative(Float128.PositiveInfinity));
        Assert.False(Float128.IsNegative(Float128.NaN));
    }

    [Fact(DisplayName = "Float128.IsPositive should be the inverse of IsNegative")]
    public void Float128IsPositiveShouldBeInverseOfIsNegative()
    {
        Assert.True(Float128.IsPositive(Float128.Zero));
        Assert.True(Float128.IsPositive(Float128.One));
        Assert.True(Float128.IsPositive(Float128.MaxValue));
        Assert.True(Float128.IsPositive(Float128.PositiveInfinity));
        Assert.True(Float128.IsPositive(Float128.NaN));
        Assert.False(Float128.IsPositive(Float128.NegativeZero));
        Assert.False(Float128.IsPositive(Float128.NegativeOne));
        Assert.False(Float128.IsPositive(Float128.MinValue));
        Assert.False(Float128.IsPositive(Float128.NegativeInfinity));
    }

    [Theory(DisplayName = "Float128.IsInteger should match the integer-ness of integer-valued doubles")]
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
    public void Float128IsIntegerShouldMatchDoubleSemantics(double value, bool expected)
    {
        Assert.Equal(expected, Float128.IsInteger(value));
    }

    [Fact(DisplayName = "Float128.IsInteger should return false for non-finite values")]
    public void Float128IsIntegerShouldReturnFalseForNonFinite()
    {
        Assert.False(Float128.IsInteger(Float128.PositiveInfinity));
        Assert.False(Float128.IsInteger(Float128.NegativeInfinity));
        Assert.False(Float128.IsInteger(Float128.NaN));
        Assert.False(Float128.IsInteger(Float128.Epsilon));
    }

    [Theory(DisplayName = "Float128.IsEvenInteger should detect even integral values")]
    [InlineData(0.0, true)]
    [InlineData(2.0, true)]
    [InlineData(-2.0, true)]
    [InlineData(10.0, true)]
    [InlineData(1024.0, true)]
    [InlineData(1.0, false)]
    [InlineData(-1.0, false)]
    [InlineData(3.0, false)]
    [InlineData(0.5, false)]
    public void Float128IsEvenIntegerShouldMatchDoubleSemantics(double value, bool expected)
    {
        Assert.Equal(expected, Float128.IsEvenInteger(value));
    }

    [Theory(DisplayName = "Float128.IsOddInteger should detect odd integral values")]
    [InlineData(1.0, true)]
    [InlineData(-1.0, true)]
    [InlineData(3.0, true)]
    [InlineData(7.0, true)]
    [InlineData(0.0, false)]
    [InlineData(2.0, false)]
    [InlineData(10.0, false)]
    [InlineData(0.5, false)]
    public void Float128IsOddIntegerShouldMatchDoubleSemantics(double value, bool expected)
    {
        Assert.Equal(expected, Float128.IsOddInteger(value));
    }
}
