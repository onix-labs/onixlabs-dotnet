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

public sealed class UInt512EquatableTests
{
    [Fact(DisplayName = "UInt512 equality should be by value across both halves")]
    public void UInt512EqualityShouldBeByValue()
    {
        UInt512 a = new((UInt256)100UL, (UInt256)200UL);
        UInt512 b = new((UInt256)100UL, (UInt256)200UL);
        Assert.True(a.Equals(b));
        Assert.True(UInt512.Equals(a, b));
        Assert.Equal(a, b);
    }

    [Fact(DisplayName = "UInt512 equality should be false when upper halves differ")]
    public void UInt512InequalityShouldBeDetectedInUpper()
    {
        UInt512 a = new((UInt256)100UL, (UInt256)0UL);
        UInt512 b = new((UInt256)101UL, (UInt256)0UL);
        Assert.False(a.Equals(b));
        Assert.True(a != b);
    }

    [Fact(DisplayName = "UInt512 equality should be false when lower halves differ")]
    public void UInt512InequalityShouldBeDetectedInLower()
    {
        UInt512 a = new((UInt256)0UL, (UInt256)100UL);
        UInt512 b = new((UInt256)0UL, (UInt256)101UL);
        Assert.False(a.Equals(b));
        Assert.True(a != b);
    }

    [Fact(DisplayName = "UInt512.Zero should equal default(UInt512)")]
    public void UInt512ZeroShouldEqualDefault()
    {
        Assert.True(UInt512.Zero.Equals(default(UInt512)));
        Assert.True(UInt512.Zero == default(UInt512));
    }

    [Fact(DisplayName = "UInt512 == and != should produce opposite results")]
    public void UInt512EqualityOperatorsShouldBeOpposite()
    {
        UInt512 a = UInt512.MaxValue;
        UInt512 b = UInt512.MaxValue;
        UInt512 c = UInt512.Zero;
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.False(a == c);
        Assert.True(a != c);
    }

    [Fact(DisplayName = "UInt512.Equals(object) should return true for matching boxed UInt512")]
    public void UInt512EqualsObjectShouldReturnTrueForBoxedMatch()
    {
        UInt512 value = UInt512.MaxValue;
        object boxed = UInt512.MaxValue;
        Assert.True(value.Equals(boxed));
    }

    [Fact(DisplayName = "UInt512.Equals(object) should return false for non-UInt512 instances")]
    public void UInt512EqualsObjectShouldReturnFalseForNonUInt512()
    {
        UInt512 value = UInt512.One;
        object stringObj = "UInt512.One";
        object dateObj = DateTime.UtcNow;
        Assert.False(value.Equals(stringObj));
        Assert.False(value.Equals(dateObj));
        Assert.False(value.Equals(null));
    }

    [Fact(DisplayName = "UInt512.GetHashCode should be consistent for equal values")]
    public void UInt512GetHashCodeShouldBeConsistent()
    {
        UInt512 a = UInt512.Parse("123456789012345678901234567890");
        UInt512 b = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact(DisplayName = "UInt512.GetHashCode should differ for distinct edge values")]
    public void UInt512GetHashCodeShouldDifferForDistinctValues()
    {
        Assert.NotEqual(UInt512.Zero.GetHashCode(), UInt512.One.GetHashCode());
        Assert.NotEqual(UInt512.One.GetHashCode(), UInt512.MaxValue.GetHashCode());
    }

    [Fact(DisplayName = "UInt512 equality across the half-boundary should be exact")]
    public void UInt512EqualityShouldBeExactAtHalfBoundary()
    {
        UInt512 a = new(UInt256.One, UInt256.Zero);
        UInt512 b = new(UInt256.One, UInt256.Zero);
        UInt512 c = new(UInt256.Zero, UInt256.MaxValue);
        Assert.Equal(a, b);
        Assert.NotEqual(a, c);
        Assert.NotEqual(c, a);
    }
}
