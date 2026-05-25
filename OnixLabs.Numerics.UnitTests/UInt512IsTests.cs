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

public sealed class UInt512IsTests
{
    [Fact(DisplayName = "UInt512.IsZero should return true for Zero and false for non-zero values")]
    public void UInt512IsZeroShouldDetectZero()
    {
        Assert.True(UInt512.IsZero(UInt512.Zero));
        Assert.False(UInt512.IsZero(UInt512.One));
        Assert.False(UInt512.IsZero(UInt512.MaxValue));
        Assert.False(UInt512.IsZero(new UInt512(UInt256.One, UInt256.Zero)));
        Assert.False(UInt512.IsZero(new UInt512(UInt256.Zero, UInt256.One)));
    }

    [Fact(DisplayName = "UInt512.IsEvenInteger should detect even values via the lower half")]
    public void UInt512IsEvenIntegerShouldDetectEven()
    {
        Assert.True(UInt512.IsEvenInteger(UInt512.Zero));
        Assert.True(UInt512.IsEvenInteger((UInt512)2UL));
        Assert.True(UInt512.IsEvenInteger((UInt512)1024UL));
        Assert.True(UInt512.IsEvenInteger(new UInt512(UInt256.MaxValue, (UInt256)2UL)));
        Assert.False(UInt512.IsEvenInteger(UInt512.One));
        Assert.False(UInt512.IsEvenInteger((UInt512)3UL));
        Assert.False(UInt512.IsEvenInteger(UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512.IsOddInteger should detect odd values via the lower half")]
    public void UInt512IsOddIntegerShouldDetectOdd()
    {
        Assert.True(UInt512.IsOddInteger(UInt512.One));
        Assert.True(UInt512.IsOddInteger((UInt512)3UL));
        Assert.True(UInt512.IsOddInteger((UInt512)1023UL));
        Assert.True(UInt512.IsOddInteger(UInt512.MaxValue));
        Assert.False(UInt512.IsOddInteger(UInt512.Zero));
        Assert.False(UInt512.IsOddInteger((UInt512)2UL));
        Assert.False(UInt512.IsOddInteger((UInt512)1024UL));
    }

    [Fact(DisplayName = "UInt512.IsPow2 should detect positive powers of two and reject zero/non-powers")]
    public void UInt512IsPow2ShouldDetectPowersOfTwo()
    {
        Assert.True(UInt512.IsPow2(UInt512.One));
        Assert.True(UInt512.IsPow2((UInt512)2UL));
        Assert.True(UInt512.IsPow2((UInt512)1024UL));
        Assert.True(UInt512.IsPow2(UInt512.One << 100));
        Assert.True(UInt512.IsPow2(UInt512.One << 256));
        Assert.True(UInt512.IsPow2(UInt512.One << 400));
        Assert.True(UInt512.IsPow2(UInt512.One << 511));
        Assert.False(UInt512.IsPow2(UInt512.Zero));
        Assert.False(UInt512.IsPow2((UInt512)3UL));
        Assert.False(UInt512.IsPow2((UInt512)5UL));
        Assert.False(UInt512.IsPow2(UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512 INumberBase.IsCanonical should always return true")]
    public void UInt512IsCanonicalShouldAlwaysReturnTrue()
    {
        Assert.True(NumberBaseHelper.IsCanonical(UInt512.Zero));
        Assert.True(NumberBaseHelper.IsCanonical(UInt512.One));
        Assert.True(NumberBaseHelper.IsCanonical(UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512 INumberBase.IsFinite should always return true")]
    public void UInt512IsFiniteShouldAlwaysReturnTrue()
    {
        Assert.True(NumberBaseHelper.IsFinite(UInt512.Zero));
        Assert.True(NumberBaseHelper.IsFinite(UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512 INumberBase.IsInteger should always return true")]
    public void UInt512IsIntegerShouldAlwaysReturnTrue()
    {
        Assert.True(NumberBaseHelper.IsInteger(UInt512.Zero));
        Assert.True(NumberBaseHelper.IsInteger(UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512 INumberBase.IsRealNumber should always return true")]
    public void UInt512IsRealNumberShouldAlwaysReturnTrue()
    {
        Assert.True(NumberBaseHelper.IsRealNumber(UInt512.Zero));
        Assert.True(NumberBaseHelper.IsRealNumber(UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512 INumberBase.IsPositive should always return true (unsigned)")]
    public void UInt512IsPositiveShouldAlwaysReturnTrue()
    {
        Assert.True(NumberBaseHelper.IsPositive(UInt512.Zero));
        Assert.True(NumberBaseHelper.IsPositive(UInt512.One));
        Assert.True(NumberBaseHelper.IsPositive(UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512 INumberBase.IsNegative should always return false (unsigned)")]
    public void UInt512IsNegativeShouldAlwaysReturnFalse()
    {
        Assert.False(NumberBaseHelper.IsNegative(UInt512.Zero));
        Assert.False(NumberBaseHelper.IsNegative(UInt512.One));
        Assert.False(NumberBaseHelper.IsNegative(UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512 INumberBase NaN/Infinity/Complex/Imaginary/Subnormal should always be false")]
    public void UInt512UnsupportedKindsShouldAlwaysReturnFalse()
    {
        Assert.False(NumberBaseHelper.IsNaN(UInt512.MaxValue));
        Assert.False(NumberBaseHelper.IsInfinity(UInt512.MaxValue));
        Assert.False(NumberBaseHelper.IsPositiveInfinity(UInt512.MaxValue));
        Assert.False(NumberBaseHelper.IsNegativeInfinity(UInt512.MaxValue));
        Assert.False(NumberBaseHelper.IsComplexNumber(UInt512.MaxValue));
        Assert.False(NumberBaseHelper.IsImaginaryNumber(UInt512.MaxValue));
        Assert.False(NumberBaseHelper.IsSubnormal(UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512 INumberBase.IsNormal should return true for non-zero values")]
    public void UInt512IsNormalShouldReturnTrueForNonZero()
    {
        Assert.False(NumberBaseHelper.IsNormal(UInt512.Zero));
        Assert.True(NumberBaseHelper.IsNormal(UInt512.One));
        Assert.True(NumberBaseHelper.IsNormal(UInt512.MaxValue));
    }

    private static class NumberBaseHelper
    {
        public static bool IsCanonical<T>(T value) where T : INumberBase<T> => T.IsCanonical(value);
        public static bool IsFinite<T>(T value) where T : INumberBase<T> => T.IsFinite(value);
        public static bool IsInteger<T>(T value) where T : INumberBase<T> => T.IsInteger(value);
        public static bool IsRealNumber<T>(T value) where T : INumberBase<T> => T.IsRealNumber(value);
        public static bool IsPositive<T>(T value) where T : INumberBase<T> => T.IsPositive(value);
        public static bool IsNegative<T>(T value) where T : INumberBase<T> => T.IsNegative(value);
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
