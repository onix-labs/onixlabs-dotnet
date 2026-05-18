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

public sealed class UInt512ComparableTests
{
    [Fact(DisplayName = "UInt512.Compare should order by upper then lower half")]
    public void UInt512CompareShouldOrderByUpperThenLower()
    {
        UInt512 lessByUpper = new(UInt256.Zero, UInt256.MaxValue);
        UInt512 greaterByUpper = new(UInt256.One, UInt256.Zero);
        Assert.True(UInt512.Compare(lessByUpper, greaterByUpper) < 0);
        Assert.True(UInt512.Compare(greaterByUpper, lessByUpper) > 0);
    }

    [Fact(DisplayName = "UInt512.Compare with equal upper should fall through to lower comparison")]
    public void UInt512CompareEqualUpperShouldFallThroughToLower()
    {
        UInt512 a = new((UInt256)5UL, (UInt256)10UL);
        UInt512 b = new((UInt256)5UL, (UInt256)20UL);
        Assert.True(UInt512.Compare(a, b) < 0);
        Assert.True(UInt512.Compare(b, a) > 0);
    }

    [Fact(DisplayName = "UInt512.Compare should return zero for equal values")]
    public void UInt512CompareShouldReturnZeroForEqual()
    {
        UInt512 a = new((UInt256)5UL, (UInt256)10UL);
        UInt512 b = new((UInt256)5UL, (UInt256)10UL);
        Assert.Equal(0, UInt512.Compare(a, b));
        Assert.Equal(0, UInt512.Compare(UInt512.MaxValue, UInt512.MaxValue));
        Assert.Equal(0, UInt512.Compare(UInt512.Zero, UInt512.Zero));
    }

    [Fact(DisplayName = "UInt512.CompareTo(object) should return positive when other is null")]
    public void UInt512CompareToObjectNullShouldReturnPositive()
    {
        UInt512 value = UInt512.One;
        Assert.True(value.CompareTo((object?)null) > 0);
    }

    [Fact(DisplayName = "UInt512.CompareTo(object) should throw for non-UInt512 instances")]
    public void UInt512CompareToObjectWrongTypeShouldThrow()
    {
        UInt512 value = UInt512.One;
        Assert.Throws<ArgumentException>(() => value.CompareTo("not a UInt512"));
    }

    [Fact(DisplayName = "UInt512.CompareTo(UInt512) should match Compare")]
    public void UInt512CompareToShouldMatchCompare()
    {
        UInt512 left = (UInt512)100UL;
        UInt512 right = (UInt512)200UL;
        Assert.Equal(UInt512.Compare(left, right), left.CompareTo(right));
        Assert.Equal(UInt512.Compare(right, left), right.CompareTo(left));
    }

    [Fact(DisplayName = "UInt512 less-than across half boundary should hold")]
    public void UInt512LessThanAcrossHalfBoundary()
    {
        UInt512 smaller = new(UInt256.Zero, UInt256.MaxValue);
        UInt512 larger = new(UInt256.One, UInt256.Zero);
        Assert.True(smaller < larger);
        Assert.True(smaller <= larger);
        Assert.False(smaller > larger);
        Assert.False(smaller >= larger);
    }

    [Fact(DisplayName = "UInt512 greater-than across half boundary should hold")]
    public void UInt512GreaterThanAcrossHalfBoundary()
    {
        UInt512 larger = new(UInt256.One, UInt256.Zero);
        UInt512 smaller = new(UInt256.Zero, UInt256.MaxValue);
        Assert.True(larger > smaller);
        Assert.True(larger >= smaller);
        Assert.False(larger < smaller);
        Assert.False(larger <= smaller);
    }

    [Fact(DisplayName = "UInt512.MaxValue should be greater than every other tested value")]
    public void UInt512MaxValueShouldBeGreatest()
    {
        Assert.True(UInt512.MaxValue > UInt512.Zero);
        Assert.True(UInt512.MaxValue > UInt512.One);
        Assert.True(UInt512.MaxValue > new UInt512(UInt256.One, UInt256.MaxValue));
        Assert.True(UInt512.MaxValue >= UInt512.MaxValue);
    }

    [Fact(DisplayName = "UInt512.Zero should be less than every other tested value")]
    public void UInt512ZeroShouldBeLeast()
    {
        Assert.True(UInt512.Zero < UInt512.One);
        Assert.True(UInt512.Zero < UInt512.MaxValue);
        Assert.True(UInt512.Zero <= UInt512.Zero);
    }

    [Fact(DisplayName = "UInt512 <= and >= should be true for equal values")]
    public void UInt512LessEqualGreaterEqualShouldBeTrueForEquality()
    {
        UInt512 left = (UInt512)42UL;
        UInt512 right = (UInt512)42UL;
        Assert.True(left <= right);
        Assert.True(left >= right);
    }
}
