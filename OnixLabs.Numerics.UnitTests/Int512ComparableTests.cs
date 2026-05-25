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

public sealed class Int512ComparableTests
{
    [Fact(DisplayName = "Int512.Compare should treat negatives as less than positives")]
    public void Int512CompareNegativesShouldBeLess()
    {
        Assert.True(Int512.Compare(Int512.NegativeOne, Int512.Zero) < 0);
        Assert.True(Int512.Compare(Int512.MinValue, Int512.Zero) < 0);
        Assert.True(Int512.Compare(Int512.MinValue, Int512.MaxValue) < 0);
        Assert.True(Int512.Compare(Int512.NegativeOne, Int512.MaxValue) < 0);
    }

    [Fact(DisplayName = "Int512.Compare should treat positives as greater than negatives")]
    public void Int512ComparePositivesShouldBeGreater()
    {
        Assert.True(Int512.Compare(Int512.MaxValue, Int512.NegativeOne) > 0);
        Assert.True(Int512.Compare(Int512.MaxValue, Int512.MinValue) > 0);
        Assert.True(Int512.Compare(Int512.One, Int512.NegativeOne) > 0);
    }

    [Fact(DisplayName = "Int512.Compare equal values should return zero")]
    public void Int512CompareEqualShouldReturnZero()
    {
        Assert.Equal(0, Int512.Compare(Int512.Zero, Int512.Zero));
        Assert.Equal(0, Int512.Compare(Int512.MaxValue, Int512.MaxValue));
        Assert.Equal(0, Int512.Compare(Int512.MinValue, Int512.MinValue));
        Assert.Equal(0, Int512.Compare(Int512.NegativeOne, Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512.Compare should order two negatives by their two's-complement upper")]
    public void Int512CompareTwoNegativesByMagnitude()
    {
        Int512 closerToZero = (Int512)(-1);
        Int512 farFromZero = (Int512)(-1_000_000);
        Assert.True(Int512.Compare(farFromZero, closerToZero) < 0);
        Assert.True(Int512.Compare(closerToZero, farFromZero) > 0);
    }

    [Fact(DisplayName = "Int512.CompareTo(object) should return positive when other is null")]
    public void Int512CompareToObjectNullShouldReturnPositive()
    {
        Int512 value = Int512.One;
        Assert.True(value.CompareTo((object?)null) > 0);
    }

    [Fact(DisplayName = "Int512.CompareTo(object) should throw for non-Int512")]
    public void Int512CompareToObjectWrongTypeShouldThrow()
    {
        Int512 value = Int512.One;
        Assert.Throws<ArgumentException>(() => value.CompareTo("not an Int512"));
    }

    [Fact(DisplayName = "Int512 less-than across the sign boundary should hold")]
    public void Int512LessThanAcrossSignBoundary()
    {
        Assert.True(Int512.MinValue < Int512.Zero);
        Assert.True(Int512.NegativeOne < Int512.One);
        Assert.True(Int512.MinValue < Int512.MaxValue);
    }

    [Fact(DisplayName = "Int512 greater-than across the sign boundary should hold")]
    public void Int512GreaterThanAcrossSignBoundary()
    {
        Assert.True(Int512.MaxValue > Int512.MinValue);
        Assert.True(Int512.One > Int512.NegativeOne);
        Assert.True(Int512.Zero > Int512.MinValue);
    }

    [Fact(DisplayName = "Int512 <= and >= should hold for equal values")]
    public void Int512LessEqualGreaterEqualShouldHoldForEqual()
    {
        Int512 a = (Int512)42;
        Int512 b = (Int512)42;
        Assert.True(a <= b);
        Assert.True(a >= b);
    }

    [Fact(DisplayName = "Int512.MaxValue should be the greatest")]
    public void Int512MaxValueShouldBeGreatest()
    {
        Assert.True(Int512.MaxValue >= Int512.MaxValue);
        Assert.True(Int512.MaxValue > Int512.Zero);
        Assert.True(Int512.MaxValue > Int512.MinValue);
    }

    [Fact(DisplayName = "Int512.MinValue should be the least")]
    public void Int512MinValueShouldBeLeast()
    {
        Assert.True(Int512.MinValue <= Int512.MinValue);
        Assert.True(Int512.MinValue < Int512.NegativeOne);
        Assert.True(Int512.MinValue < Int512.Zero);
    }
}
