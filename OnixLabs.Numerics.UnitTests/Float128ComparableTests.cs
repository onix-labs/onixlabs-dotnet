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

public sealed class Float128ComparableTests
{
    [Fact(DisplayName = "Float128.Compare should order finite values in ascending magnitude with sign")]
    public void Float128CompareShouldOrderFiniteValues()
    {
        Assert.True(Float128.Compare(Float128.Zero, Float128.One) < 0);
        Assert.True(Float128.Compare(Float128.One, Float128.Two) < 0);
        Assert.True(Float128.Compare(Float128.NegativeOne, Float128.One) < 0);
        Assert.True(Float128.Compare(Float128.MinValue, Float128.MaxValue) < 0);
        Assert.True(Float128.Compare(Float128.One, Float128.Zero) > 0);
        Assert.Equal(0, Float128.Compare(Float128.One, Float128.One));
    }

    [Fact(DisplayName = "Float128.Compare should treat positive and negative zero as equal")]
    public void Float128CompareShouldTreatPositiveAndNegativeZeroAsEqual()
    {
        Assert.Equal(0, Float128.Compare(Float128.Zero, Float128.NegativeZero));
        Assert.Equal(0, Float128.Compare(Float128.NegativeZero, Float128.Zero));
    }

    [Fact(DisplayName = "Float128.Compare should sort negative infinity below all finite values")]
    public void Float128CompareShouldSortNegativeInfinityLowest()
    {
        Assert.True(Float128.Compare(Float128.NegativeInfinity, Float128.MinValue) < 0);
        Assert.True(Float128.Compare(Float128.NegativeInfinity, Float128.Zero) < 0);
        Assert.True(Float128.Compare(Float128.NegativeInfinity, Float128.MaxValue) < 0);
    }

    [Fact(DisplayName = "Float128.Compare should sort positive infinity above all finite values")]
    public void Float128CompareShouldSortPositiveInfinityHighest()
    {
        Assert.True(Float128.Compare(Float128.PositiveInfinity, Float128.MaxValue) > 0);
        Assert.True(Float128.Compare(Float128.PositiveInfinity, Float128.Zero) > 0);
        Assert.True(Float128.Compare(Float128.PositiveInfinity, Float128.NegativeInfinity) > 0);
    }

    [Fact(DisplayName = "Float128.Compare should sort NaN below all numeric values, matching double.CompareTo")]
    public void Float128CompareShouldSortNaNLowest()
    {
        Assert.True(Float128.Compare(Float128.NaN, Float128.NegativeInfinity) < 0);
        Assert.True(Float128.Compare(Float128.NaN, Float128.Zero) < 0);
        Assert.True(Float128.Compare(Float128.NaN, Float128.PositiveInfinity) < 0);
        Assert.Equal(0, Float128.Compare(Float128.NaN, Float128.NaN));
    }

    [Fact(DisplayName = "Float128.CompareTo(object) should return positive for null and throw for incompatible types")]
    public void Float128CompareToObjectShouldHandleNullAndIncompatibleTypes()
    {
        Assert.Equal(1, Float128.One.CompareTo(null));
        Assert.Throws<ArgumentException>(() => Float128.One.CompareTo("not a Float128"));
    }

    [Fact(DisplayName = "Float128.operator< should return false when either operand is NaN")]
    public void Float128LessThanOperatorShouldReturnFalseForNaN()
    {
        Assert.False(Float128.NaN < Float128.One);
        Assert.False(Float128.One < Float128.NaN);
        Assert.False(Float128.NaN < Float128.NaN);
    }

    [Fact(DisplayName = "Float128.operator< should reflect Compare ordering for non-NaN values")]
    public void Float128LessThanOperatorShouldReflectCompareForNonNaN()
    {
        Assert.True(Float128.Zero < Float128.One);
        Assert.True(Float128.NegativeOne < Float128.One);
        Assert.True(Float128.NegativeInfinity < Float128.PositiveInfinity);
        Assert.False(Float128.One < Float128.One);
        Assert.False(Float128.Zero < Float128.NegativeZero);
    }

    [Fact(DisplayName = "Float128.operator<= should return false when either operand is NaN")]
    public void Float128LessThanOrEqualOperatorShouldReturnFalseForNaN()
    {
        Assert.False(Float128.NaN <= Float128.NaN);
        Assert.False(Float128.NaN <= Float128.One);
        Assert.False(Float128.One <= Float128.NaN);
    }

    [Fact(DisplayName = "Float128.operator> should return false when either operand is NaN")]
    public void Float128GreaterThanOperatorShouldReturnFalseForNaN()
    {
        Assert.False(Float128.NaN > Float128.One);
        Assert.False(Float128.One > Float128.NaN);
        Assert.False(Float128.NaN > Float128.NaN);
    }

    [Fact(DisplayName = "Float128.operator>= should return false when either operand is NaN")]
    public void Float128GreaterThanOrEqualOperatorShouldReturnFalseForNaN()
    {
        Assert.False(Float128.NaN >= Float128.NaN);
        Assert.False(Float128.NaN >= Float128.One);
        Assert.False(Float128.One >= Float128.NaN);
    }

    [Fact(DisplayName = "Float128.operator>= and operator<= should be reflexive for non-NaN values")]
    public void Float128OrderingOperatorsShouldBeReflexiveForNonNaN()
    {
        Assert.True(Float128.Zero <= Float128.Zero);
        Assert.True(Float128.Zero >= Float128.Zero);
        Assert.True(Float128.Zero <= Float128.NegativeZero);
        Assert.True(Float128.MaxValue >= Float128.MaxValue);
    }
}
