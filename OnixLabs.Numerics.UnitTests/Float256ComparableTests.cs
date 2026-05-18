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

public sealed class Float256ComparableTests
{
    [Fact(DisplayName = "Float256.Compare should order finite values in ascending magnitude with sign")]
    public void Float256CompareShouldOrderFiniteValues()
    {
        Assert.True(Float256.Compare(Float256.Zero, Float256.One) < 0);
        Assert.True(Float256.Compare(Float256.One, Float256.Two) < 0);
        Assert.True(Float256.Compare(Float256.NegativeOne, Float256.One) < 0);
        Assert.True(Float256.Compare(Float256.MinValue, Float256.MaxValue) < 0);
        Assert.True(Float256.Compare(Float256.One, Float256.Zero) > 0);
        Assert.Equal(0, Float256.Compare(Float256.One, Float256.One));
    }

    [Fact(DisplayName = "Float256.Compare should treat positive and negative zero as equal")]
    public void Float256CompareShouldTreatPositiveAndNegativeZeroAsEqual()
    {
        Assert.Equal(0, Float256.Compare(Float256.Zero, Float256.NegativeZero));
        Assert.Equal(0, Float256.Compare(Float256.NegativeZero, Float256.Zero));
    }

    [Fact(DisplayName = "Float256.Compare should sort negative infinity below all finite values")]
    public void Float256CompareShouldSortNegativeInfinityLowest()
    {
        Assert.True(Float256.Compare(Float256.NegativeInfinity, Float256.MinValue) < 0);
        Assert.True(Float256.Compare(Float256.NegativeInfinity, Float256.Zero) < 0);
        Assert.True(Float256.Compare(Float256.NegativeInfinity, Float256.MaxValue) < 0);
    }

    [Fact(DisplayName = "Float256.Compare should sort positive infinity above all finite values")]
    public void Float256CompareShouldSortPositiveInfinityHighest()
    {
        Assert.True(Float256.Compare(Float256.PositiveInfinity, Float256.MaxValue) > 0);
        Assert.True(Float256.Compare(Float256.PositiveInfinity, Float256.Zero) > 0);
        Assert.True(Float256.Compare(Float256.PositiveInfinity, Float256.NegativeInfinity) > 0);
    }

    [Fact(DisplayName = "Float256.Compare should sort NaN below all numeric values, matching double.CompareTo")]
    public void Float256CompareShouldSortNaNLowest()
    {
        Assert.True(Float256.Compare(Float256.NaN, Float256.NegativeInfinity) < 0);
        Assert.True(Float256.Compare(Float256.NaN, Float256.Zero) < 0);
        Assert.True(Float256.Compare(Float256.NaN, Float256.PositiveInfinity) < 0);
        Assert.Equal(0, Float256.Compare(Float256.NaN, Float256.NaN));
    }

    [Fact(DisplayName = "Float256.CompareTo(object) should return positive for null and throw for incompatible types")]
    public void Float256CompareToObjectShouldHandleNullAndIncompatibleTypes()
    {
        Assert.Equal(1, Float256.One.CompareTo(null));
        Assert.Throws<ArgumentException>(() => Float256.One.CompareTo("not a Float256"));
    }

    [Fact(DisplayName = "Float256.operator< should return false when either operand is NaN")]
    public void Float256LessThanOperatorShouldReturnFalseForNaN()
    {
        Assert.False(Float256.NaN < Float256.One);
        Assert.False(Float256.One < Float256.NaN);
        Assert.False(Float256.NaN < Float256.NaN);
    }

    [Fact(DisplayName = "Float256.operator< should reflect Compare ordering for non-NaN values")]
    public void Float256LessThanOperatorShouldReflectCompareForNonNaN()
    {
        Assert.True(Float256.Zero < Float256.One);
        Assert.True(Float256.NegativeOne < Float256.One);
        Assert.True(Float256.NegativeInfinity < Float256.PositiveInfinity);
        Assert.False(Float256.One < Float256.One);
        Assert.False(Float256.Zero < Float256.NegativeZero);
    }

    [Fact(DisplayName = "Float256.operator<= should return false when either operand is NaN")]
    public void Float256LessThanOrEqualOperatorShouldReturnFalseForNaN()
    {
        Assert.False(Float256.NaN <= Float256.NaN);
        Assert.False(Float256.NaN <= Float256.One);
        Assert.False(Float256.One <= Float256.NaN);
    }

    [Fact(DisplayName = "Float256.operator> should return false when either operand is NaN")]
    public void Float256GreaterThanOperatorShouldReturnFalseForNaN()
    {
        Assert.False(Float256.NaN > Float256.One);
        Assert.False(Float256.One > Float256.NaN);
        Assert.False(Float256.NaN > Float256.NaN);
    }

    [Fact(DisplayName = "Float256.operator>= should return false when either operand is NaN")]
    public void Float256GreaterThanOrEqualOperatorShouldReturnFalseForNaN()
    {
        Assert.False(Float256.NaN >= Float256.NaN);
        Assert.False(Float256.NaN >= Float256.One);
        Assert.False(Float256.One >= Float256.NaN);
    }

    [Fact(DisplayName = "Float256.operator>= and operator<= should be reflexive for non-NaN values")]
    public void Float256OrderingOperatorsShouldBeReflexiveForNonNaN()
    {
        Assert.True(Float256.Zero <= Float256.Zero);
        Assert.True(Float256.Zero >= Float256.Zero);
        Assert.True(Float256.Zero <= Float256.NegativeZero);
        Assert.True(Float256.MaxValue >= Float256.MaxValue);
    }
}
