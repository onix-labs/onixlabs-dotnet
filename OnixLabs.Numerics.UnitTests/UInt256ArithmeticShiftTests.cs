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

public sealed class UInt256ArithmeticShiftTests
{
    [Theory(DisplayName = "UInt256 left shift of One by various positions should match BigInteger")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(63)]
    [InlineData(64)]
    [InlineData(127)]
    [InlineData(128)]
    [InlineData(129)]
    [InlineData(192)]
    [InlineData(255)]
    public void UInt256LeftShiftOfOneByPositionsShouldMatchBigInteger(int shift)
    {
        UInt256 actual = UInt256.One << shift;
        BigInteger expected = BigInteger.One << shift;
        Assert.Equal(expected, (BigInteger)actual);
    }

    [Theory(DisplayName = "UInt256 left shift by amounts at or beyond BitWidth should wrap (shiftAmount mod 256)")]
    [InlineData(256, 0)]
    [InlineData(257, 1)]
    [InlineData(384, 128)]
    [InlineData(511, 255)]
    public void UInt256LeftShiftAtOrBeyondBitWidthShouldWrap(int shift, int effective)
    {
        UInt256 actual = UInt256.One << shift;
        UInt256 expected = UInt256.One << effective;
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "UInt256 right shift of MaxValue should match BigInteger")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(63)]
    [InlineData(64)]
    [InlineData(127)]
    [InlineData(128)]
    [InlineData(129)]
    [InlineData(192)]
    [InlineData(255)]
    public void UInt256RightShiftOfMaxValueShouldMatchBigInteger(int shift)
    {
        UInt256 actual = UInt256.MaxValue >> shift;
        BigInteger expected = ((BigInteger.One << 256) - BigInteger.One) >> shift;
        Assert.Equal(expected, (BigInteger)actual);
    }

    [Theory(DisplayName = "UInt256 unsigned right shift of MaxValue should match BigInteger and equal signed right shift")]
    [InlineData(0)]
    [InlineData(64)]
    [InlineData(127)]
    [InlineData(128)]
    [InlineData(129)]
    [InlineData(255)]
    public void UInt256UnsignedRightShiftOfMaxValueShouldMatchBigInteger(int shift)
    {
        UInt256 logical = UInt256.MaxValue >>> shift;
        UInt256 arithmetic = UInt256.MaxValue >> shift;
        Assert.Equal(arithmetic, logical);
        BigInteger expected = ((BigInteger.One << 256) - BigInteger.One) >> shift;
        Assert.Equal(expected, (BigInteger)logical);
    }

    [Fact(DisplayName = "UInt256 left shift by 128 should move lower into upper")]
    public void UInt256LeftShiftBy128ShouldMoveLowerIntoUpper()
    {
        UInt256 value = new(UInt128.Zero, UInt128.MaxValue);
        UInt256 shifted = value << 128;
        Assert.Equal(UInt128.MaxValue, shifted.UpperBits);
        Assert.Equal(UInt128.Zero, shifted.LowerBits);
    }

    [Fact(DisplayName = "UInt256 right shift by 128 should move upper into lower")]
    public void UInt256RightShiftBy128ShouldMoveUpperIntoLower()
    {
        UInt256 value = new(UInt128.MaxValue, UInt128.Zero);
        UInt256 shifted = value >> 128;
        Assert.Equal(UInt128.Zero, shifted.UpperBits);
        Assert.Equal(UInt128.MaxValue, shifted.LowerBits);
    }

    [Fact(DisplayName = "UInt256 left shift by 1 across half-boundary should carry the high bit of lower into upper")]
    public void UInt256LeftShiftBy1AcrossHalfBoundaryShouldCarryHighBitOfLower()
    {
        UInt256 value = new(UInt128.Zero, UInt128.One << 127);
        UInt256 shifted = value << 1;
        Assert.Equal(UInt128.One, shifted.UpperBits);
        Assert.Equal(UInt128.Zero, shifted.LowerBits);
    }

    [Fact(DisplayName = "UInt256 right shift by 1 across half-boundary should carry the low bit of upper into lower")]
    public void UInt256RightShiftBy1AcrossHalfBoundaryShouldCarryLowBitOfUpper()
    {
        UInt256 value = new(UInt128.One, UInt128.Zero);
        UInt256 shifted = value >> 1;
        Assert.Equal(UInt128.Zero, shifted.UpperBits);
        Assert.Equal(UInt128.One << 127, shifted.LowerBits);
    }

    [Fact(DisplayName = "UInt256 left shift by full BitWidth should wrap to zero shift (return value unchanged)")]
    public void UInt256LeftShiftByFullBitWidthShouldReturnValueUnchanged()
    {
        UInt256 value = UInt256.MaxValue;
        Assert.Equal(value, value << 256);
    }

    [Fact(DisplayName = "UInt256 right shift by zero should return the value unchanged")]
    public void UInt256RightShiftByZeroShouldReturnValueUnchanged()
    {
        UInt256 value = UInt256.MaxValue;
        Assert.Equal(value, value >> 0);
        Assert.Equal(value, value >>> 0);
    }
}
