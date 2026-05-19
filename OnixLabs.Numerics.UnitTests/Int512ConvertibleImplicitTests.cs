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

public sealed class Int512ConvertibleImplicitTests
{
    [Theory(DisplayName = "Int512 implicit conversion from sbyte should preserve value (positive)")]
    [InlineData((sbyte)0)]
    [InlineData((sbyte)1)]
    [InlineData(sbyte.MaxValue)]
    public void Int512ImplicitFromPositiveSByteShouldPreserveValue(sbyte value)
    {
        Int512 wide = value;
        Assert.Equal((BigInteger)value, (BigInteger)wide);
    }

    [Theory(DisplayName = "Int512 implicit conversion from sbyte should sign-extend negatives")]
    [InlineData((sbyte)(-1))]
    [InlineData((sbyte)(-128))]
    [InlineData((sbyte)(-42))]
    public void Int512ImplicitFromNegativeSByteShouldSignExtend(sbyte value)
    {
        Int512 wide = value;
        Assert.True(Int512.IsNegative(wide));
        Assert.Equal((BigInteger)value, (BigInteger)wide);
    }

    [Theory(DisplayName = "Int512 implicit conversion from byte should be non-negative")]
    [InlineData((byte)0)]
    [InlineData((byte)127)]
    [InlineData((byte)255)]
    public void Int512ImplicitFromByteShouldBeNonNegative(byte value)
    {
        Int512 wide = value;
        Assert.False(Int512.IsNegative(wide));
        Assert.Equal((BigInteger)value, (BigInteger)wide);
    }

    [Theory(DisplayName = "Int512 implicit conversion from int should sign-extend negatives")]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    [InlineData(-1000000)]
    public void Int512ImplicitFromNegativeIntShouldSignExtend(int value)
    {
        Int512 wide = value;
        Assert.True(Int512.IsNegative(wide));
        Assert.Equal((BigInteger)value, (BigInteger)wide);
    }

    [Fact(DisplayName = "Int512 implicit conversion from int.MaxValue should preserve value")]
    public void Int512ImplicitFromIntMaxValueShouldPreserveValue()
    {
        Int512 wide = int.MaxValue;
        Assert.Equal((BigInteger)int.MaxValue, (BigInteger)wide);
    }

    [Fact(DisplayName = "Int512 implicit conversion from long.MinValue should sign-extend")]
    public void Int512ImplicitFromLongMinValueShouldSignExtend()
    {
        Int512 wide = long.MinValue;
        Assert.True(Int512.IsNegative(wide));
        Assert.Equal((BigInteger)long.MinValue, (BigInteger)wide);
    }

    [Fact(DisplayName = "Int512 implicit conversion from Int128 MinValue should sign-extend")]
    public void Int512ImplicitFromInt128MinValueShouldSignExtend()
    {
        Int512 wide = Int128.MinValue;
        Assert.True(Int512.IsNegative(wide));
        Assert.Equal((BigInteger)Int128.MinValue, (BigInteger)wide);
    }

    [Fact(DisplayName = "Int512 implicit conversion from Int256 MinValue should sign-extend the upper half")]
    public void Int512ImplicitFromInt256MinValueShouldSignExtend()
    {
        Int256 source = Int256.MinValue;
        Int512 wide = source;
        Assert.True(Int512.IsNegative(wide));
        Assert.Equal((BigInteger)source, (BigInteger)wide);
    }

    [Fact(DisplayName = "Int512 implicit conversion from UInt128 MaxValue should be non-negative")]
    public void Int512ImplicitFromUInt128MaxValueShouldBeNonNegative()
    {
        Int512 wide = UInt128.MaxValue;
        Assert.False(Int512.IsNegative(wide));
        Assert.Equal((BigInteger)UInt128.MaxValue, (BigInteger)wide);
    }

    [Fact(DisplayName = "Int512 implicit conversion from UInt256 MaxValue should be non-negative")]
    public void Int512ImplicitFromUInt256MaxValueShouldBeNonNegative()
    {
        Int512 wide = UInt256.MaxValue;
        Assert.False(Int512.IsNegative(wide));
        Assert.Equal((BigInteger)UInt256.MaxValue, (BigInteger)wide);
    }

    [Theory(DisplayName = "Int512 implicit conversion from char should preserve unsigned value")]
    [InlineData('\0')]
    [InlineData('A')]
    [InlineData(char.MaxValue)]
    public void Int512ImplicitFromCharShouldPreserveValue(char value)
    {
        Int512 wide = value;
        Assert.False(Int512.IsNegative(wide));
        Assert.Equal((BigInteger)value, (BigInteger)wide);
    }
}
