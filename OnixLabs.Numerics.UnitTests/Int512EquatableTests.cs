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

public sealed class Int512EquatableTests
{
    [Fact(DisplayName = "Int512 equality should be by value across both halves")]
    public void Int512EqualityShouldBeByValue()
    {
        Int512 a = new((UInt256)100UL, (UInt256)200UL);
        Int512 b = new((UInt256)100UL, (UInt256)200UL);
        Assert.True(a.Equals(b));
        Assert.True(Int512.Equals(a, b));
        Assert.Equal(a, b);
    }

    [Fact(DisplayName = "Int512 equality should differentiate Zero from NegativeOne")]
    public void Int512EqualityShouldDifferentiateZeroFromNegativeOne()
    {
        Assert.NotEqual(Int512.Zero, Int512.NegativeOne);
        Assert.True(Int512.Zero != Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512.MinValue and MaxValue should not be equal")]
    public void Int512MinValueAndMaxValueShouldNotBeEqual()
    {
        Assert.NotEqual(Int512.MinValue, Int512.MaxValue);
    }

    [Fact(DisplayName = "Int512 == and != should produce opposite results")]
    public void Int512EqualityOperatorsShouldBeOpposite()
    {
        Int512 a = Int512.MaxValue;
        Int512 b = Int512.MaxValue;
        Int512 c = Int512.Zero;
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.False(a == c);
        Assert.True(a != c);
    }

    [Fact(DisplayName = "Int512.Equals(object) should return true for matching boxed Int512")]
    public void Int512EqualsObjectShouldReturnTrueForBoxedMatch()
    {
        Int512 value = Int512.MinValue;
        object boxed = Int512.MinValue;
        Assert.True(value.Equals(boxed));
    }

    [Fact(DisplayName = "Int512.Equals(object) should return false for non-Int512 instances")]
    public void Int512EqualsObjectShouldReturnFalseForNonInt512()
    {
        Int512 value = Int512.NegativeOne;
        object stringObj = "Int512.NegativeOne";
        object dateObj = DateTime.UtcNow;
        Assert.False(value.Equals(stringObj));
        Assert.False(value.Equals(dateObj));
        Assert.False(value.Equals(null));
    }

    [Fact(DisplayName = "Int512.GetHashCode should be consistent for equal values")]
    public void Int512GetHashCodeShouldBeConsistent()
    {
        Int512 a = Int512.Parse("-123456789012345678901234567890");
        Int512 b = Int512.Parse("-123456789012345678901234567890");
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact(DisplayName = "Int512.GetHashCode should be consistent for a value with itself")]
    public void Int512GetHashCodeShouldBeConsistentForSameValue()
    {
        Int512 first = Int512.MaxValue;
        Int512 second = Int512.MaxValue;
        Assert.Equal(first.GetHashCode(), second.GetHashCode());
    }

    [Fact(DisplayName = "Int512 equality across sign boundary should distinguish positive and negative")]
    public void Int512EqualityShouldDistinguishPositiveFromNegative()
    {
        Int512 positive = (Int512)42;
        Int512 negative = (Int512)(-42);
        Assert.NotEqual(positive, negative);
    }
}
