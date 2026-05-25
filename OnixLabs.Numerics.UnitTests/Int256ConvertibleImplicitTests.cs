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

public sealed class Int256ConvertibleImplicitTests
{
    [Theory(DisplayName = "Int256 implicit conversion from sbyte should preserve sign-extension for negatives")]
    [InlineData((sbyte)0)]
    [InlineData((sbyte)1)]
    [InlineData((sbyte)127)]
    [InlineData((sbyte)(-1))]
    [InlineData((sbyte)(-128))]
    public void Int256ImplicitFromSByteShouldPreserveSignExtension(sbyte value)
    {
        Int256 converted = value;
        Assert.Equal((BigInteger)value, (BigInteger)converted);
        if (value < 0)
        {
            Assert.True(Int256.IsNegative(converted));
        }
    }

    [Theory(DisplayName = "Int256 implicit conversion from byte should produce non-negative value with zero upper")]
    [InlineData((byte)0)]
    [InlineData((byte)1)]
    [InlineData((byte)128)]
    [InlineData((byte)255)]
    public void Int256ImplicitFromByteShouldPreserveValue(byte value)
    {
        Int256 converted = value;
        Assert.Equal((BigInteger)value, (BigInteger)converted);
        Assert.Equal(UInt128.Zero, converted.UpperBits);
    }

    [Theory(DisplayName = "Int256 implicit conversion from short should sign-extend for negatives")]
    [InlineData((short)0)]
    [InlineData((short)32767)]
    [InlineData((short)(-1))]
    [InlineData((short)(-32768))]
    public void Int256ImplicitFromShortShouldSignExtend(short value)
    {
        Int256 converted = value;
        Assert.Equal((BigInteger)value, (BigInteger)converted);
    }

    [Theory(DisplayName = "Int256 implicit conversion from int should sign-extend for negatives")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void Int256ImplicitFromIntShouldSignExtend(int value)
    {
        Int256 converted = value;
        Assert.Equal((BigInteger)value, (BigInteger)converted);
    }

    [Theory(DisplayName = "Int256 implicit conversion from long should sign-extend for negatives")]
    [InlineData(0L)]
    [InlineData(1L)]
    [InlineData(long.MaxValue)]
    [InlineData(-1L)]
    [InlineData(long.MinValue)]
    public void Int256ImplicitFromLongShouldSignExtend(long value)
    {
        Int256 converted = value;
        Assert.Equal((BigInteger)value, (BigInteger)converted);
    }

    [Fact(DisplayName = "Int256 implicit conversion from Int128 should sign-extend negatives into the upper half")]
    public void Int256ImplicitFromInt128ShouldSignExtend()
    {
        Int256 fromMin = Int128.MinValue;
        Assert.Equal((BigInteger)Int128.MinValue, (BigInteger)fromMin);
        Assert.True(Int256.IsNegative(fromMin));
        Assert.Equal(UInt128.MaxValue, fromMin.UpperBits);

        Int256 fromPositive = (Int128)42;
        Assert.Equal(UInt128.Zero, fromPositive.UpperBits);
        Assert.Equal((UInt128)42, fromPositive.LowerBits);
    }

    [Fact(DisplayName = "Int256 implicit conversion from UInt128 should produce non-negative value with zero upper")]
    public void Int256ImplicitFromUInt128ShouldProduceNonNegative()
    {
        Int256 converted = UInt128.MaxValue;
        Assert.False(Int256.IsNegative(converted));
        Assert.Equal(UInt128.Zero, converted.UpperBits);
        Assert.Equal(UInt128.MaxValue, converted.LowerBits);
    }

    [Theory(DisplayName = "Int256 implicit conversion from ulong should produce non-negative value with zero upper")]
    [InlineData(0UL)]
    [InlineData(1UL)]
    [InlineData(ulong.MaxValue)]
    public void Int256ImplicitFromUlongShouldPreserveValue(ulong value)
    {
        Int256 converted = value;
        Assert.False(Int256.IsNegative(converted));
        Assert.Equal((BigInteger)value, (BigInteger)converted);
    }

    [Fact(DisplayName = "Int256 implicit conversion from char should preserve value")]
    public void Int256ImplicitFromCharShouldPreserveValue()
    {
        Int256 converted = 'A';
        Assert.Equal((Int256)65, converted);
    }
}
