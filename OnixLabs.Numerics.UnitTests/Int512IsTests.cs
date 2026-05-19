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

public sealed class Int512IsTests
{
    [Fact(DisplayName = "Int512.IsZero should detect Zero and reject other values")]
    public void Int512IsZeroShouldDetectZero()
    {
        Assert.True(Int512.IsZero(Int512.Zero));
        Assert.False(Int512.IsZero(Int512.One));
        Assert.False(Int512.IsZero(Int512.NegativeOne));
        Assert.False(Int512.IsZero(Int512.MaxValue));
        Assert.False(Int512.IsZero(Int512.MinValue));
    }

    [Fact(DisplayName = "Int512.IsNegative should detect values with the sign bit set")]
    public void Int512IsNegativeShouldDetectNegative()
    {
        Assert.True(Int512.IsNegative(Int512.NegativeOne));
        Assert.True(Int512.IsNegative(Int512.MinValue));
        Assert.True(Int512.IsNegative((Int512)(-42)));
        Assert.False(Int512.IsNegative(Int512.Zero));
        Assert.False(Int512.IsNegative(Int512.One));
        Assert.False(Int512.IsNegative(Int512.MaxValue));
    }

    [Fact(DisplayName = "Int512.IsPositive should be the complement of IsNegative")]
    public void Int512IsPositiveShouldBeComplementOfIsNegative()
    {
        Assert.True(Int512.IsPositive(Int512.Zero));
        Assert.True(Int512.IsPositive(Int512.One));
        Assert.True(Int512.IsPositive(Int512.MaxValue));
        Assert.False(Int512.IsPositive(Int512.NegativeOne));
        Assert.False(Int512.IsPositive(Int512.MinValue));
    }

    [Fact(DisplayName = "Int512.IsEvenInteger should detect even values across positive and negative")]
    public void Int512IsEvenIntegerShouldDetectEven()
    {
        Assert.True(Int512.IsEvenInteger(Int512.Zero));
        Assert.True(Int512.IsEvenInteger((Int512)2));
        Assert.True(Int512.IsEvenInteger((Int512)(-2)));
        Assert.True(Int512.IsEvenInteger((Int512)1024));
        Assert.True(Int512.IsEvenInteger(Int512.MinValue));
        Assert.False(Int512.IsEvenInteger(Int512.One));
        Assert.False(Int512.IsEvenInteger(Int512.NegativeOne));
        Assert.False(Int512.IsEvenInteger(Int512.MaxValue));
    }

    [Fact(DisplayName = "Int512.IsOddInteger should detect odd values across positive and negative")]
    public void Int512IsOddIntegerShouldDetectOdd()
    {
        Assert.True(Int512.IsOddInteger(Int512.One));
        Assert.True(Int512.IsOddInteger(Int512.NegativeOne));
        Assert.True(Int512.IsOddInteger((Int512)3));
        Assert.True(Int512.IsOddInteger((Int512)(-3)));
        Assert.True(Int512.IsOddInteger(Int512.MaxValue));
        Assert.False(Int512.IsOddInteger(Int512.Zero));
        Assert.False(Int512.IsOddInteger((Int512)2));
        Assert.False(Int512.IsOddInteger(Int512.MinValue));
    }

    [Fact(DisplayName = "Int512.IsPow2 should be false for non-positive values")]
    public void Int512IsPow2ShouldBeFalseForNonPositive()
    {
        Assert.False(Int512.IsPow2(Int512.Zero));
        Assert.False(Int512.IsPow2(Int512.NegativeOne));
        Assert.False(Int512.IsPow2(Int512.MinValue));
        Assert.False(Int512.IsPow2((Int512)(-2)));
        Assert.False(Int512.IsPow2((Int512)(-1024)));
    }

    [Fact(DisplayName = "Int512.IsPow2 should detect positive powers of two")]
    public void Int512IsPow2ShouldDetectPositivePowers()
    {
        Assert.True(Int512.IsPow2(Int512.One));
        Assert.True(Int512.IsPow2((Int512)2));
        Assert.True(Int512.IsPow2((Int512)1024));
        Assert.True(Int512.IsPow2(Int512.One << 100));
        Assert.True(Int512.IsPow2(Int512.One << 510));
        Assert.False(Int512.IsPow2((Int512)3));
        Assert.False(Int512.IsPow2(Int512.MaxValue));
    }

    [Fact(DisplayName = "Int512 INumberBase.IsCanonical should always return true")]
    public void Int512IsCanonicalShouldAlwaysReturnTrue()
    {
        Assert.True(NumberBaseHelper.IsCanonical(Int512.Zero));
        Assert.True(NumberBaseHelper.IsCanonical(Int512.MinValue));
        Assert.True(NumberBaseHelper.IsCanonical(Int512.MaxValue));
    }

    [Fact(DisplayName = "Int512 INumberBase.IsFinite should always return true")]
    public void Int512IsFiniteShouldAlwaysReturnTrue()
    {
        Assert.True(NumberBaseHelper.IsFinite(Int512.MinValue));
        Assert.True(NumberBaseHelper.IsFinite(Int512.MaxValue));
    }

    [Fact(DisplayName = "Int512 INumberBase.IsInteger should always return true")]
    public void Int512IsIntegerShouldAlwaysReturnTrue()
    {
        Assert.True(NumberBaseHelper.IsInteger(Int512.Zero));
        Assert.True(NumberBaseHelper.IsInteger(Int512.MaxValue));
    }

    [Fact(DisplayName = "Int512 INumberBase.IsRealNumber should always return true")]
    public void Int512IsRealNumberShouldAlwaysReturnTrue()
    {
        Assert.True(NumberBaseHelper.IsRealNumber(Int512.Zero));
        Assert.True(NumberBaseHelper.IsRealNumber(Int512.MaxValue));
    }

    [Fact(DisplayName = "Int512 INumberBase unsupported kinds should always be false")]
    public void Int512UnsupportedKindsShouldAlwaysReturnFalse()
    {
        Assert.False(NumberBaseHelper.IsNaN(Int512.MaxValue));
        Assert.False(NumberBaseHelper.IsInfinity(Int512.MaxValue));
        Assert.False(NumberBaseHelper.IsPositiveInfinity(Int512.MaxValue));
        Assert.False(NumberBaseHelper.IsNegativeInfinity(Int512.MinValue));
        Assert.False(NumberBaseHelper.IsComplexNumber(Int512.MaxValue));
        Assert.False(NumberBaseHelper.IsImaginaryNumber(Int512.MaxValue));
        Assert.False(NumberBaseHelper.IsSubnormal(Int512.MaxValue));
    }

    [Fact(DisplayName = "Int512 INumberBase.IsNormal should be true for non-zero values")]
    public void Int512IsNormalShouldBeTrueForNonZero()
    {
        Assert.False(NumberBaseHelper.IsNormal(Int512.Zero));
        Assert.True(NumberBaseHelper.IsNormal(Int512.One));
        Assert.True(NumberBaseHelper.IsNormal(Int512.NegativeOne));
    }

    private static class NumberBaseHelper
    {
        public static bool IsCanonical<T>(T value) where T : INumberBase<T> => T.IsCanonical(value);
        public static bool IsFinite<T>(T value) where T : INumberBase<T> => T.IsFinite(value);
        public static bool IsInteger<T>(T value) where T : INumberBase<T> => T.IsInteger(value);
        public static bool IsRealNumber<T>(T value) where T : INumberBase<T> => T.IsRealNumber(value);
        public static bool IsNaN<T>(T value) where T : INumberBase<T> => T.IsNaN(value);
        public static bool IsInfinity<T>(T value) where T : INumberBase<T> => T.IsInfinity(value);
        public static bool IsPositiveInfinity<T>(T value) where T : INumberBase<T> => T.IsPositiveInfinity(value);
        public static bool IsNegativeInfinity<T>(T value) where T : INumberBase<T> => T.IsNegativeInfinity(value);
        public static bool IsComplexNumber<T>(T value) where T : INumberBase<T> => T.IsComplexNumber(value);
        public static bool IsImaginaryNumber<T>(T value) where T : INumberBase<T> => T.IsImaginaryNumber(value);
        public static bool IsSubnormal<T>(T value) where T : INumberBase<T> => T.IsSubnormal(value);
        public static bool IsNormal<T>(T value) where T : INumberBase<T> => T.IsNormal(value);
    }
}
