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

public sealed class UInt512ConstantTests
{
    [Fact(DisplayName = "UInt512.Zero should have both halves equal to UInt256.Zero")]
    public void UInt512ZeroShouldHaveBothHalvesEqualToUInt256Zero()
    {
        Assert.Equal(UInt256.Zero, UInt512.Zero.UpperBits);
        Assert.Equal(UInt256.Zero, UInt512.Zero.LowerBits);
    }

    [Fact(DisplayName = "UInt512.Zero should equal default(UInt512)")]
    public void UInt512ZeroShouldEqualDefault()
    {
        Assert.Equal(default(UInt512), UInt512.Zero);
    }

    [Fact(DisplayName = "UInt512.One should have upper half equal to UInt256.Zero and lower half equal to UInt256.One")]
    public void UInt512OneShouldHaveUpperZeroAndLowerOne()
    {
        Assert.Equal(UInt256.Zero, UInt512.One.UpperBits);
        Assert.Equal(UInt256.One, UInt512.One.LowerBits);
    }

    [Fact(DisplayName = "UInt512.One should equal BigInteger.One")]
    public void UInt512OneShouldEqualBigIntegerOne()
    {
        Assert.Equal(BigInteger.One, (BigInteger)UInt512.One);
    }

    [Fact(DisplayName = "UInt512.MinValue should equal UInt512.Zero")]
    public void UInt512MinValueShouldEqualZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.MinValue);
    }

    [Fact(DisplayName = "UInt512.MinValue should equal BigInteger.Zero")]
    public void UInt512MinValueShouldEqualBigIntegerZero()
    {
        Assert.Equal(BigInteger.Zero, (BigInteger)UInt512.MinValue);
    }

    [Fact(DisplayName = "UInt512.MaxValue should have both halves equal to UInt256.MaxValue")]
    public void UInt512MaxValueShouldHaveBothHalvesMax()
    {
        Assert.Equal(UInt256.MaxValue, UInt512.MaxValue.UpperBits);
        Assert.Equal(UInt256.MaxValue, UInt512.MaxValue.LowerBits);
    }

    [Fact(DisplayName = "UInt512.MaxValue should equal 2^512 - 1")]
    public void UInt512MaxValueShouldEqualTwoTo512MinusOne()
    {
        BigInteger expected = (BigInteger.One << 512) - BigInteger.One;
        Assert.Equal(expected, (BigInteger)UInt512.MaxValue);
    }

    [Fact(DisplayName = "UInt512.AllBitsSet should equal UInt512.MaxValue")]
    public void UInt512AllBitsSetShouldEqualMaxValue()
    {
        Assert.Equal(UInt512.MaxValue, UInt512.AllBitsSet);
    }

    [Fact(DisplayName = "UInt512.AllBitsSet should have all 512 bits set")]
    public void UInt512AllBitsSetShouldHaveAll512BitsSet()
    {
        Assert.Equal((UInt512)512UL, UInt512.PopCount(UInt512.AllBitsSet));
    }

    [Fact(DisplayName = "UInt512.AllBitsSet plus UInt512.One should wrap to UInt512.Zero")]
    public void UInt512AllBitsSetPlusOneShouldWrapToZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.AllBitsSet + UInt512.One);
    }

    [Fact(DisplayName = "UInt512.Zero minus UInt512.One should wrap to UInt512.MaxValue")]
    public void UInt512ZeroMinusOneShouldWrapToMaxValue()
    {
        Assert.Equal(UInt512.MaxValue, UInt512.Zero - UInt512.One);
    }

    [Fact(DisplayName = "Constructor should preserve upper and lower halves verbatim")]
    public void UInt512ConstructorShouldPreserveHalves()
    {
        UInt256 upper = (UInt256)0x1234_5678_9ABC_DEF0UL;
        UInt256 lower = (UInt256)0xFEDC_BA98_7654_3210UL;
        UInt512 value = new(upper, lower);
        Assert.Equal(upper, value.UpperBits);
        Assert.Equal(lower, value.LowerBits);
    }

    [Fact(DisplayName = "UInt512 constructed with upper UInt256.MaxValue and lower UInt256.Zero should equal MaxValue minus low UInt256")]
    public void UInt512WithUpperOnlyShouldRepresentHighHalf()
    {
        UInt512 value = new(UInt256.MaxValue, UInt256.Zero);
        BigInteger expected = ((BigInteger.One << 512) - BigInteger.One) - ((BigInteger.One << 256) - BigInteger.One);
        Assert.Equal(expected, (BigInteger)value);
    }

    [Fact(DisplayName = "UInt512.AdditiveIdentity should equal Zero")]
    public void UInt512AdditiveIdentityShouldEqualZero()
    {
        Assert.Equal(UInt512.Zero, AdditiveIdentity<UInt512, UInt512>());
    }

    [Fact(DisplayName = "UInt512.MultiplicativeIdentity should equal One")]
    public void UInt512MultiplicativeIdentityShouldEqualOne()
    {
        Assert.Equal(UInt512.One, MultiplicativeIdentity<UInt512, UInt512>());
    }

    [Fact(DisplayName = "UInt512.Radix should be 2")]
    public void UInt512RadixShouldBeTwo()
    {
        Assert.Equal(2, Radix<UInt512>());
    }

    [Fact(DisplayName = "UInt512 IMinMaxValue members should equal the public MinValue and MaxValue")]
    public void UInt512MinMaxValueInterfaceMembersShouldEqualPublicConstants()
    {
        Assert.Equal(UInt512.MinValue, MinValue<UInt512>());
        Assert.Equal(UInt512.MaxValue, MaxValue<UInt512>());
        Assert.Equal((BigInteger.One << 512) - BigInteger.One, (BigInteger)MaxValue<UInt512>());
    }

    private static TResult AdditiveIdentity<T, TResult>() where T : IAdditiveIdentity<T, TResult> => T.AdditiveIdentity;

    private static TResult MultiplicativeIdentity<T, TResult>() where T : IMultiplicativeIdentity<T, TResult> => T.MultiplicativeIdentity;

    private static int Radix<T>() where T : INumberBase<T> => T.Radix;

    private static T MinValue<T>() where T : IMinMaxValue<T> => T.MinValue;

    private static T MaxValue<T>() where T : IMinMaxValue<T> => T.MaxValue;
}
