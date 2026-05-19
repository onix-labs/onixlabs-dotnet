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

public sealed class UInt256ConvertibleImplicitTests
{
    [Theory(DisplayName = "UInt256 implicit conversion from byte should preserve value and zero upper half")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(127)]
    [InlineData(255)]
    public void UInt256ImplicitFromByteShouldPreserveValue(byte value)
    {
        UInt256 converted = value;
        Assert.Equal(UInt128.Zero, converted.UpperBits);
        Assert.Equal((UInt128)value, converted.LowerBits);
        Assert.Equal((BigInteger)value, (BigInteger)converted);
    }

    [Theory(DisplayName = "UInt256 implicit conversion from ushort should preserve value")]
    [InlineData((ushort)0)]
    [InlineData((ushort)1)]
    [InlineData((ushort)32767)]
    [InlineData((ushort)65535)]
    public void UInt256ImplicitFromUshortShouldPreserveValue(ushort value)
    {
        UInt256 converted = value;
        Assert.Equal(UInt128.Zero, converted.UpperBits);
        Assert.Equal((UInt128)value, converted.LowerBits);
    }

    [Theory(DisplayName = "UInt256 implicit conversion from uint should preserve value")]
    [InlineData(0u)]
    [InlineData(1u)]
    [InlineData(0xFFFFFFFFu)]
    public void UInt256ImplicitFromUintShouldPreserveValue(uint value)
    {
        UInt256 converted = value;
        Assert.Equal(UInt128.Zero, converted.UpperBits);
        Assert.Equal((UInt128)value, converted.LowerBits);
    }

    [Theory(DisplayName = "UInt256 implicit conversion from ulong should preserve value")]
    [InlineData(0UL)]
    [InlineData(1UL)]
    [InlineData(ulong.MaxValue)]
    public void UInt256ImplicitFromUlongShouldPreserveValue(ulong value)
    {
        UInt256 converted = value;
        Assert.Equal(UInt128.Zero, converted.UpperBits);
        Assert.Equal((UInt128)value, converted.LowerBits);
    }

    [Fact(DisplayName = "UInt256 implicit conversion from UInt128 should preserve value and zero upper half")]
    public void UInt256ImplicitFromUInt128ShouldPreserveValueAndZeroUpper()
    {
        UInt128 value = UInt128.MaxValue;
        UInt256 converted = value;
        Assert.Equal(UInt128.Zero, converted.UpperBits);
        Assert.Equal(value, converted.LowerBits);
        Assert.Equal((BigInteger)value, (BigInteger)converted);
    }

    [Theory(DisplayName = "UInt256 implicit conversion from char should preserve value")]
    [InlineData((char)0)]
    [InlineData('A')]
    [InlineData((char)65535)]
    public void UInt256ImplicitFromCharShouldPreserveValue(char value)
    {
        UInt256 converted = value;
        Assert.Equal(UInt128.Zero, converted.UpperBits);
        Assert.Equal((UInt128)value, converted.LowerBits);
    }

    [Fact(DisplayName = "UInt256 implicit conversion from char should round-trip back to char via explicit cast")]
    public void UInt256ImplicitFromCharShouldRoundTripBack()
    {
        char input = 'Z';
        UInt256 converted = input;
        Assert.Equal(input, (char)converted);
    }
}
