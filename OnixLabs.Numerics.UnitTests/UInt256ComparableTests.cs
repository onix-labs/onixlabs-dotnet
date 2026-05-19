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

public sealed class UInt256ComparableTests
{
    [Fact(DisplayName = "UInt256.Compare static should return -1, 0, 1 for less, equal, greater")]
    public void UInt256CompareStaticShouldReturnExpectedSign()
    {
        Assert.Equal(0, UInt256.Compare(UInt256.Zero, UInt256.Zero));
        Assert.Equal(-1, UInt256.Compare(UInt256.Zero, UInt256.One));
        Assert.Equal(1, UInt256.Compare(UInt256.One, UInt256.Zero));
        Assert.Equal(-1, UInt256.Compare(UInt256.Zero, UInt256.MaxValue));
        Assert.Equal(1, UInt256.Compare(UInt256.MaxValue, UInt256.Zero));
    }

    [Fact(DisplayName = "UInt256.CompareTo of UInt256 should match Compare static")]
    public void UInt256CompareToInstanceShouldMatchCompareStatic()
    {
        UInt256 a = (UInt256)10;
        UInt256 b = (UInt256)20;
        Assert.True(a.CompareTo(b) < 0);
        Assert.True(b.CompareTo(a) > 0);
        Assert.Equal(0, a.CompareTo(a));
    }

    [Fact(DisplayName = "UInt256.CompareTo of object should return 1 for null and throw for foreign type")]
    public void UInt256CompareToObjectShouldHandleNullAndForeignType()
    {
        UInt256 a = (UInt256)10;
        Assert.Equal(1, a.CompareTo((object?)null));
        Assert.Equal(0, a.CompareTo((object)(UInt256)10));
        Assert.Throws<ArgumentException>(() => a.CompareTo((object)"foo"));
    }

    [Fact(DisplayName = "UInt256 comparison with same upper and different lower should depend on lower")]
    public void UInt256ComparisonWithSameUpperShouldDependOnLower()
    {
        UInt256 small = new((UInt128)5, (UInt128)10);
        UInt256 large = new((UInt128)5, (UInt128)20);
        Assert.True(small < large);
        Assert.True(small <= large);
        Assert.False(small > large);
        Assert.False(small >= large);
        Assert.Equal(-1, UInt256.Compare(small, large));
    }

    [Fact(DisplayName = "UInt256 comparison with different upper should ignore lower")]
    public void UInt256ComparisonWithDifferentUpperShouldIgnoreLower()
    {
        UInt256 smallUpperLargeLower = new((UInt128)4, UInt128.MaxValue);
        UInt256 largeUpperSmallLower = new((UInt128)5, UInt128.Zero);
        Assert.True(smallUpperLargeLower < largeUpperSmallLower);
        Assert.True(largeUpperSmallLower > smallUpperLargeLower);
    }

    [Fact(DisplayName = "UInt256 equal values should yield true for <= and >=")]
    public void UInt256EqualValuesShouldYieldTrueForLeAndGe()
    {
        UInt256 a = (UInt256)999;
        UInt256 b = (UInt256)999;
        Assert.True(a <= b);
        Assert.True(a >= b);
        Assert.False(a < b);
        Assert.False(a > b);
    }

    [Fact(DisplayName = "UInt256.MaxValue should compare greater than any smaller value")]
    public void UInt256MaxValueShouldCompareGreaterThanAnySmallerValue()
    {
        Assert.True(UInt256.MaxValue > UInt256.Zero);
        Assert.True(UInt256.MaxValue > UInt256.One);
        Assert.True(UInt256.MaxValue > new UInt256(UInt128.MaxValue - UInt128.One, UInt128.MaxValue));
        Assert.True(UInt256.MaxValue >= UInt256.MaxValue);
    }
}
