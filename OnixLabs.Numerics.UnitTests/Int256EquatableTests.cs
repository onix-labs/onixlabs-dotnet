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

public sealed class Int256EquatableTests
{
    [Fact(DisplayName = "Int256.Equals static should return true only when both halves match")]
    public void Int256EqualsStaticShouldReturnTrueOnlyWhenBothHalvesMatch()
    {
        Int256 a = (Int256)(-12345);
        Int256 b = (Int256)(-12345);
        Int256 c = (Int256)12345;
        Assert.True(Int256.Equals(a, b));
        Assert.False(Int256.Equals(a, c));
    }

    [Fact(DisplayName = "Int256.Equals instance should match static Equals")]
    public void Int256EqualsInstanceShouldMatchStatic()
    {
        Int256 a = Int256.MinValue;
        Int256 b = Int256.MinValue;
        Assert.True(a.Equals(b));
        Assert.True(b.Equals(a));
    }

    [Fact(DisplayName = "Int256.Equals object should distinguish foreign types")]
    public void Int256EqualsObjectShouldDistinguishForeignTypes()
    {
        Int256 a = (Int256)(-42);
        Assert.True(a.Equals((object)(Int256)(-42)));
        Assert.False(a.Equals((object)(long)(-42)));
        Assert.False(a.Equals((object?)null));
    }

    [Fact(DisplayName = "Int256 operator equality should match Equals")]
    public void Int256OperatorEqualityShouldMatchEquals()
    {
        Assert.True(Int256.MinValue == Int256.MinValue);
        Assert.False(Int256.MinValue == Int256.MaxValue);
        Assert.True(Int256.NegativeOne != Int256.One);
        Assert.False(Int256.NegativeOne != Int256.NegativeOne);
    }

    [Fact(DisplayName = "Int256.GetHashCode should be consistent for equal values")]
    public void Int256GetHashCodeShouldBeConsistentForEqualValues()
    {
        Int256 a = (Int256)(-9999);
        Int256 b = (Int256)(-9999);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact(DisplayName = "Int256.GetHashCode should generally differ for opposite signs of the same magnitude")]
    public void Int256GetHashCodeShouldDifferForOppositeSigns()
    {
        Int256 positive = (Int256)42;
        Int256 negative = (Int256)(-42);
        Assert.NotEqual(positive.GetHashCode(), negative.GetHashCode());
    }

    [Fact(DisplayName = "Int256.Zero and default Int256 should be equal")]
    public void Int256ZeroAndDefaultShouldBeEqual()
    {
        Int256 def = default;
        Assert.Equal(Int256.Zero, def);
        Assert.True(Int256.Zero == def);
        Assert.Equal(Int256.Zero.GetHashCode(), def.GetHashCode());
    }
}
