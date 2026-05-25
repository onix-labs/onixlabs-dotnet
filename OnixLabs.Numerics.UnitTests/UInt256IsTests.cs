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
using System.Numerics;

namespace OnixLabs.Numerics.UnitTests;

public sealed class UInt256IsTests
{
    [Fact(DisplayName = "UInt256.IsZero should return true only for zero")]
    public void UInt256IsZeroShouldReturnTrueOnlyForZero()
    {
        Assert.True(UInt256.IsZero(UInt256.Zero));
        Assert.True(UInt256.IsZero(UInt256.MinValue));
        Assert.False(UInt256.IsZero(UInt256.One));
        Assert.False(UInt256.IsZero(UInt256.MaxValue));
        Assert.False(UInt256.IsZero(new UInt256(UInt128.One, UInt128.Zero)));
        Assert.False(UInt256.IsZero(new UInt256(UInt128.Zero, UInt128.One)));
    }

    [Fact(DisplayName = "UInt256.IsEvenInteger should return true for even values")]
    public void UInt256IsEvenIntegerShouldReturnTrueForEvenValues()
    {
        Assert.True(UInt256.IsEvenInteger(UInt256.Zero));
        Assert.True(UInt256.IsEvenInteger((UInt256)2));
        Assert.True(UInt256.IsEvenInteger((UInt256)1024));
        Assert.True(UInt256.IsEvenInteger(new UInt256(UInt128.One, UInt128.Zero)));
        Assert.False(UInt256.IsEvenInteger(UInt256.One));
        Assert.False(UInt256.IsEvenInteger((UInt256)3));
        Assert.False(UInt256.IsEvenInteger(UInt256.MaxValue));
    }

    [Fact(DisplayName = "UInt256.IsOddInteger should be the inverse of IsEvenInteger")]
    public void UInt256IsOddIntegerShouldBeInverseOfIsEvenInteger()
    {
        Assert.False(UInt256.IsOddInteger(UInt256.Zero));
        Assert.True(UInt256.IsOddInteger(UInt256.One));
        Assert.True(UInt256.IsOddInteger((UInt256)3));
        Assert.True(UInt256.IsOddInteger(UInt256.MaxValue));
        Assert.False(UInt256.IsOddInteger((UInt256)2));
        Assert.False(UInt256.IsOddInteger(new UInt256(UInt128.MaxValue, UInt128.Zero)));
    }

    [Fact(DisplayName = "UInt256.IsPow2 should return true for powers of two across the bit range")]
    public void UInt256IsPow2ShouldReturnTrueForPowersOfTwoAcrossBitRange()
    {
        for (int shift = 0; shift < 256; shift++)
        {
            UInt256 power = UInt256.One << shift;
            Assert.True(UInt256.IsPow2(power), $"Failed at shift={shift}");
        }
    }

    [Fact(DisplayName = "UInt256.IsPow2 should return false for non-powers-of-two")]
    public void UInt256IsPow2ShouldReturnFalseForNonPowersOfTwo()
    {
        Assert.False(UInt256.IsPow2(UInt256.Zero));
        Assert.False(UInt256.IsPow2((UInt256)3));
        Assert.False(UInt256.IsPow2((UInt256)6));
        Assert.False(UInt256.IsPow2((UInt256)10));
        Assert.False(UInt256.IsPow2((UInt256)1023));
        Assert.False(UInt256.IsPow2(UInt256.MaxValue));
        Assert.False(UInt256.IsPow2(new UInt256(UInt128.One, UInt128.One)));
    }

    [Fact(DisplayName = "UInt256 INumberBase predicates should reflect the unsigned non-floating type")]
    public void UInt256INumberBasePredicatesShouldReflectUnsignedNonFloatingType()
    {
        AssertNumberBase(UInt256.Zero, isZero: true, isNormal: false);
        AssertNumberBase(UInt256.One, isZero: false, isNormal: true);
        AssertNumberBase(UInt256.MaxValue, isZero: false, isNormal: true);
    }

    private static void AssertNumberBase<T>(T value, bool isZero, bool isNormal) where T : INumberBase<T>
    {
        Assert.True(T.IsCanonical(value));
        Assert.False(T.IsComplexNumber(value));
        Assert.True(T.IsFinite(value));
        Assert.False(T.IsImaginaryNumber(value));
        Assert.False(T.IsInfinity(value));
        Assert.True(T.IsInteger(value));
        Assert.False(T.IsNaN(value));
        Assert.False(T.IsNegative(value));
        Assert.False(T.IsNegativeInfinity(value));
        Assert.Equal(isNormal, T.IsNormal(value));
        Assert.True(T.IsPositive(value));
        Assert.False(T.IsPositiveInfinity(value));
        Assert.True(T.IsRealNumber(value));
        Assert.False(T.IsSubnormal(value));
        Assert.Equal(isZero, T.IsZero(value));
    }
}
