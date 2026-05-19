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

public sealed class Int256ComparableTests
{
    [Fact(DisplayName = "Int256.Compare static should return -1, 0, 1 for less, equal, greater")]
    public void Int256CompareStaticShouldReturnExpectedSign()
    {
        Assert.Equal(0, Int256.Compare(Int256.Zero, Int256.Zero));
        Assert.Equal(-1, Int256.Compare(Int256.NegativeOne, Int256.Zero));
        Assert.Equal(1, Int256.Compare(Int256.One, Int256.Zero));
    }

    [Fact(DisplayName = "Int256 negative values should compare less than zero and positive values")]
    public void Int256NegativeShouldCompareLessThanZeroAndPositive()
    {
        Assert.True(Int256.NegativeOne < Int256.Zero);
        Assert.True(Int256.MinValue < Int256.Zero);
        Assert.True(Int256.MinValue < Int256.NegativeOne);
        Assert.True(Int256.MinValue < Int256.One);
        Assert.True(Int256.MinValue < Int256.MaxValue);
    }

    [Fact(DisplayName = "Int256.MinValue should be the smallest representable value")]
    public void Int256MinValueShouldBeSmallest()
    {
        Assert.True(Int256.MinValue <= Int256.Zero);
        Assert.True(Int256.MinValue <= Int256.NegativeOne);
        Assert.True(Int256.MinValue <= Int256.MinValue);
        Assert.False(Int256.MinValue > Int256.Zero);
    }

    [Fact(DisplayName = "Int256.MaxValue should be the largest representable value")]
    public void Int256MaxValueShouldBeLargest()
    {
        Assert.True(Int256.MaxValue >= Int256.Zero);
        Assert.True(Int256.MaxValue >= Int256.One);
        Assert.True(Int256.MaxValue >= Int256.MaxValue);
        Assert.False(Int256.MaxValue < Int256.Zero);
    }

    [Fact(DisplayName = "Int256.CompareTo object should return 1 for null and throw for foreign type")]
    public void Int256CompareToObjectShouldHandleNullAndForeignType()
    {
        Int256 a = (Int256)5;
        Assert.Equal(1, a.CompareTo((object?)null));
        Assert.Equal(0, a.CompareTo((object)(Int256)5));
        Assert.Throws<ArgumentException>(() => a.CompareTo((object)"foo"));
    }

    [Fact(DisplayName = "Int256 comparison with same upper and different lower should depend on lower")]
    public void Int256ComparisonWithSameUpperShouldDependOnLower()
    {
        Int256 small = (Int256)100;
        Int256 large = (Int256)200;
        Assert.True(small < large);
        Assert.True(small <= large);
        Assert.True(large > small);
        Assert.True(large >= small);
    }

    [Fact(DisplayName = "Int256 negative one should compare less than zero and any positive")]
    public void Int256NegativeOneShouldCompareLessThanZeroAndPositive()
    {
        Assert.True(Int256.NegativeOne < Int256.Zero);
        Assert.True(Int256.NegativeOne < Int256.One);
        Assert.True(Int256.NegativeOne < Int256.MaxValue);
    }

    [Fact(DisplayName = "Int256 equal values should yield true for both <= and >=")]
    public void Int256EqualValuesShouldYieldTrueForLeAndGe()
    {
        Int256 a = (Int256)(-77);
        Int256 b = (Int256)(-77);
        Assert.True(a <= b);
        Assert.True(a >= b);
        Assert.False(a < b);
        Assert.False(a > b);
    }
}
