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

public sealed class Int512ConstantTests
{
    [Fact(DisplayName = "Int512.Zero should have both halves equal to UInt256.Zero")]
    public void Int512ZeroShouldHaveBothHalvesEqualToUInt256Zero()
    {
        Assert.Equal(UInt256.Zero, Int512.Zero.UpperBits);
        Assert.Equal(UInt256.Zero, Int512.Zero.LowerBits);
    }

    [Fact(DisplayName = "Int512.Zero should equal default(Int512)")]
    public void Int512ZeroShouldEqualDefault()
    {
        Assert.Equal(default(Int512), Int512.Zero);
    }

    [Fact(DisplayName = "Int512.One should have upper half equal to UInt256.Zero and lower equal to UInt256.One")]
    public void Int512OneShouldHaveUpperZeroAndLowerOne()
    {
        Assert.Equal(UInt256.Zero, Int512.One.UpperBits);
        Assert.Equal(UInt256.One, Int512.One.LowerBits);
    }

    [Fact(DisplayName = "Int512.NegativeOne should have all bits set in both halves")]
    public void Int512NegativeOneShouldHaveAllBitsSet()
    {
        Assert.Equal(UInt256.MaxValue, Int512.NegativeOne.UpperBits);
        Assert.Equal(UInt256.MaxValue, Int512.NegativeOne.LowerBits);
    }

    [Fact(DisplayName = "Int512.NegativeOne should be detected as negative")]
    public void Int512NegativeOneShouldBeDetectedAsNegative()
    {
        Assert.True(Int512.IsNegative(Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512.NegativeOne should equal BigInteger -1")]
    public void Int512NegativeOneShouldEqualBigIntegerNegativeOne()
    {
        Assert.Equal(BigInteger.MinusOne, (BigInteger)Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512.MinValue should equal -2^511")]
    public void Int512MinValueShouldEqualNegativeTwoTo511()
    {
        Assert.Equal(-BigInteger.Pow(2, 511), (BigInteger)Int512.MinValue);
    }

    [Fact(DisplayName = "Int512.MinValue upper half should have only the sign bit set")]
    public void Int512MinValueUpperShouldHaveOnlySignBitSet()
    {
        Assert.Equal(UInt256.One << 255, Int512.MinValue.UpperBits);
        Assert.Equal(UInt256.Zero, Int512.MinValue.LowerBits);
    }

    [Fact(DisplayName = "Int512.MaxValue should equal 2^511 - 1")]
    public void Int512MaxValueShouldEqualTwoTo511MinusOne()
    {
        Assert.Equal(BigInteger.Pow(2, 511) - BigInteger.One, (BigInteger)Int512.MaxValue);
    }

    [Fact(DisplayName = "Int512.MaxValue upper half should have all bits below the sign bit set")]
    public void Int512MaxValueUpperShouldHaveAllBitsButSignSet()
    {
        UInt256 expectedUpper = ~(UInt256.One << 255);
        Assert.Equal(expectedUpper, Int512.MaxValue.UpperBits);
        Assert.Equal(UInt256.MaxValue, Int512.MaxValue.LowerBits);
    }

    [Fact(DisplayName = "Int512.AllBitsSet should equal Int512.NegativeOne")]
    public void Int512AllBitsSetShouldEqualNegativeOne()
    {
        Assert.Equal(Int512.NegativeOne, Int512.AllBitsSet);
    }

    [Fact(DisplayName = "Int512.MaxValue + One should wrap to MinValue")]
    public void Int512MaxValuePlusOneShouldWrapToMinValue()
    {
        Assert.Equal(Int512.MinValue, Int512.MaxValue + Int512.One);
    }

    [Fact(DisplayName = "Int512.MinValue - One should wrap to MaxValue")]
    public void Int512MinValueMinusOneShouldWrapToMaxValue()
    {
        Assert.Equal(Int512.MaxValue, Int512.MinValue - Int512.One);
    }

    [Fact(DisplayName = "Int512 Zero + NegativeOne should equal NegativeOne")]
    public void Int512ZeroPlusNegativeOneShouldEqualNegativeOne()
    {
        Assert.Equal(Int512.NegativeOne, Int512.Zero + Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 constructor should preserve upper and lower halves verbatim")]
    public void Int512ConstructorShouldPreserveHalves()
    {
        UInt256 upper = (UInt256)0xDEADBEEFCAFEBABEUL;
        UInt256 lower = (UInt256)0x1234567890ABCDEFUL;
        Int512 value = new(upper, lower);
        Assert.Equal(upper, value.UpperBits);
        Assert.Equal(lower, value.LowerBits);
    }

    [Fact(DisplayName = "Int512.AdditiveIdentity should equal Zero")]
    public void Int512AdditiveIdentityShouldEqualZero()
    {
        Assert.Equal(Int512.Zero, AdditiveIdentity<Int512, Int512>());
    }

    [Fact(DisplayName = "Int512.MultiplicativeIdentity should equal One")]
    public void Int512MultiplicativeIdentityShouldEqualOne()
    {
        Assert.Equal(Int512.One, MultiplicativeIdentity<Int512, Int512>());
    }

    [Fact(DisplayName = "Int512.Radix should be 2")]
    public void Int512RadixShouldBeTwo()
    {
        Assert.Equal(2, Radix<Int512>());
    }

    [Fact(DisplayName = "Int512 IMinMaxValue members should equal the public MinValue and MaxValue")]
    public void Int512MinMaxValueInterfaceMembersShouldEqualPublicConstants()
    {
        Assert.Equal(Int512.MinValue, MinValue<Int512>());
        Assert.Equal(Int512.MaxValue, MaxValue<Int512>());
    }

    [Fact(DisplayName = "Int512 ISignedNumber.NegativeOne should equal the public NegativeOne")]
    public void Int512SignedNumberNegativeOneShouldEqualPublicConstant()
    {
        Assert.Equal(Int512.NegativeOne, SignedNegativeOne<Int512>());
        Assert.Equal(BigInteger.MinusOne, (BigInteger)SignedNegativeOne<Int512>());
    }

    private static TResult AdditiveIdentity<T, TResult>() where T : IAdditiveIdentity<T, TResult> => T.AdditiveIdentity;

    private static TResult MultiplicativeIdentity<T, TResult>() where T : IMultiplicativeIdentity<T, TResult> => T.MultiplicativeIdentity;

    private static int Radix<T>() where T : INumberBase<T> => T.Radix;

    private static T MinValue<T>() where T : IMinMaxValue<T> => T.MinValue;

    private static T MaxValue<T>() where T : IMinMaxValue<T> => T.MaxValue;

    private static T SignedNegativeOne<T>() where T : ISignedNumber<T> => T.NegativeOne;
}
