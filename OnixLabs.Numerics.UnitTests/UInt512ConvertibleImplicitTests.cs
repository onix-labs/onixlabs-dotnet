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

public sealed class UInt512ConvertibleImplicitTests
{
    [Theory(DisplayName = "UInt512 implicit conversion from byte should preserve value")]
    [InlineData((byte)0)]
    [InlineData((byte)1)]
    [InlineData((byte)127)]
    [InlineData((byte)255)]
    public void UInt512ImplicitFromByteShouldPreserveValue(byte value)
    {
        UInt512 wide = value;
        Assert.Equal(UInt256.Zero, wide.UpperBits);
        Assert.Equal((UInt256)value, wide.LowerBits);
    }

    [Theory(DisplayName = "UInt512 implicit conversion from ushort should preserve value")]
    [InlineData((ushort)0)]
    [InlineData((ushort)1)]
    [InlineData((ushort)32768)]
    [InlineData(ushort.MaxValue)]
    public void UInt512ImplicitFromUShortShouldPreserveValue(ushort value)
    {
        UInt512 wide = value;
        Assert.Equal(UInt256.Zero, wide.UpperBits);
        Assert.Equal((UInt256)value, wide.LowerBits);
    }

    [Theory(DisplayName = "UInt512 implicit conversion from uint should preserve value")]
    [InlineData(0u)]
    [InlineData(1u)]
    [InlineData(0x1234_5678u)]
    [InlineData(uint.MaxValue)]
    public void UInt512ImplicitFromUIntShouldPreserveValue(uint value)
    {
        UInt512 wide = value;
        Assert.Equal(UInt256.Zero, wide.UpperBits);
        Assert.Equal((UInt256)value, wide.LowerBits);
    }

    [Theory(DisplayName = "UInt512 implicit conversion from ulong should preserve value")]
    [InlineData(0UL)]
    [InlineData(1UL)]
    [InlineData(0x1234_5678_9ABC_DEF0UL)]
    [InlineData(ulong.MaxValue)]
    public void UInt512ImplicitFromULongShouldPreserveValue(ulong value)
    {
        UInt512 wide = value;
        Assert.Equal(UInt256.Zero, wide.UpperBits);
        Assert.Equal((UInt256)value, wide.LowerBits);
    }

    [Fact(DisplayName = "UInt512 implicit conversion from UInt128 MaxValue should preserve value")]
    public void UInt512ImplicitFromUInt128MaxValueShouldPreserveValue()
    {
        UInt128 source = UInt128.MaxValue;
        UInt512 wide = source;
        Assert.Equal(UInt256.Zero, wide.UpperBits);
        Assert.Equal((UInt256)source, wide.LowerBits);
    }

    [Fact(DisplayName = "UInt512 implicit conversion from UInt256 MaxValue should preserve value")]
    public void UInt512ImplicitFromUInt256MaxValueShouldPreserveValue()
    {
        UInt256 source = UInt256.MaxValue;
        UInt512 wide = source;
        Assert.Equal(UInt256.Zero, wide.UpperBits);
        Assert.Equal(source, wide.LowerBits);
    }

    [Theory(DisplayName = "UInt512 implicit conversion from char should preserve numeric value")]
    [InlineData('\0')]
    [InlineData('A')]
    [InlineData('Z')]
    [InlineData(char.MaxValue)]
    public void UInt512ImplicitFromCharShouldPreserveValue(char value)
    {
        UInt512 wide = value;
        Assert.Equal(UInt256.Zero, wide.UpperBits);
        Assert.Equal((UInt256)value, wide.LowerBits);
    }

    [Fact(DisplayName = "UInt512 implicit conversion round-trips through BigInteger for ulong")]
    public void UInt512ImplicitConversionRoundTripsThroughBigInteger()
    {
        ulong source = 1234567890123456789UL;
        UInt512 wide = source;
        Assert.Equal((BigInteger)source, (BigInteger)wide);
    }
}
