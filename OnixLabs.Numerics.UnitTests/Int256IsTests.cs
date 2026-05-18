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

namespace OnixLabs.Numerics.UnitTests;

public sealed class Int256IsTests
{
    [Fact(DisplayName = "Int256.IsZero should return true only for zero")]
    public void Int256IsZeroShouldReturnTrueOnlyForZero()
    {
        Assert.True(Int256.IsZero(Int256.Zero));
        Assert.False(Int256.IsZero(Int256.One));
        Assert.False(Int256.IsZero(Int256.NegativeOne));
        Assert.False(Int256.IsZero(Int256.MinValue));
        Assert.False(Int256.IsZero(Int256.MaxValue));
    }

    [Fact(DisplayName = "Int256.IsNegative should return true only for values with the sign bit set")]
    public void Int256IsNegativeShouldReturnTrueOnlyForSignBitSet()
    {
        Assert.True(Int256.IsNegative(Int256.NegativeOne));
        Assert.True(Int256.IsNegative(Int256.MinValue));
        Assert.True(Int256.IsNegative((Int256)(-1000)));
        Assert.False(Int256.IsNegative(Int256.Zero));
        Assert.False(Int256.IsNegative(Int256.One));
        Assert.False(Int256.IsNegative(Int256.MaxValue));
    }

    [Fact(DisplayName = "Int256.IsPositive should be inverse of IsNegative")]
    public void Int256IsPositiveShouldBeInverseOfIsNegative()
    {
        Assert.True(Int256.IsPositive(Int256.Zero));
        Assert.True(Int256.IsPositive(Int256.One));
        Assert.True(Int256.IsPositive(Int256.MaxValue));
        Assert.False(Int256.IsPositive(Int256.NegativeOne));
        Assert.False(Int256.IsPositive(Int256.MinValue));
    }

    [Fact(DisplayName = "Int256.IsEvenInteger should return true for even values including negatives")]
    public void Int256IsEvenIntegerShouldReturnTrueForEvenValues()
    {
        Assert.True(Int256.IsEvenInteger(Int256.Zero));
        Assert.True(Int256.IsEvenInteger((Int256)2));
        Assert.True(Int256.IsEvenInteger((Int256)(-2)));
        Assert.True(Int256.IsEvenInteger((Int256)(-1024)));
        Assert.True(Int256.IsEvenInteger(Int256.MinValue));
        Assert.False(Int256.IsEvenInteger(Int256.One));
        Assert.False(Int256.IsEvenInteger(Int256.NegativeOne));
        Assert.False(Int256.IsEvenInteger((Int256)3));
        Assert.False(Int256.IsEvenInteger(Int256.MaxValue));
    }

    [Fact(DisplayName = "Int256.IsOddInteger should be inverse of IsEvenInteger")]
    public void Int256IsOddIntegerShouldBeInverseOfIsEvenInteger()
    {
        Assert.True(Int256.IsOddInteger(Int256.One));
        Assert.True(Int256.IsOddInteger(Int256.NegativeOne));
        Assert.True(Int256.IsOddInteger((Int256)3));
        Assert.True(Int256.IsOddInteger((Int256)(-3)));
        Assert.True(Int256.IsOddInteger(Int256.MaxValue));
        Assert.False(Int256.IsOddInteger(Int256.Zero));
        Assert.False(Int256.IsOddInteger((Int256)2));
        Assert.False(Int256.IsOddInteger(Int256.MinValue));
    }

    [Theory(DisplayName = "Int256.IsPow2 should return true for positive powers of two and false for non-positive values")]
    [InlineData(0L, false)]
    [InlineData(1L, true)]
    [InlineData(2L, true)]
    [InlineData(3L, false)]
    [InlineData(1024L, true)]
    [InlineData(1023L, false)]
    [InlineData(-1L, false)]
    [InlineData(-2L, false)]
    [InlineData(-1024L, false)]
    public void Int256IsPow2ShouldReturnTrueOnlyForPositivePowersOfTwo(long value, bool expected)
    {
        Assert.Equal(expected, Int256.IsPow2((Int256)value));
    }

    [Fact(DisplayName = "Int256.IsPow2 should return false for MinValue and NegativeOne")]
    public void Int256IsPow2ShouldReturnFalseForMinValueAndNegativeOne()
    {
        Assert.False(Int256.IsPow2(Int256.NegativeOne));
        Assert.False(Int256.IsPow2(Int256.MinValue));
    }

    [Fact(DisplayName = "Int256.IsPow2 should return true for One shifted left to positive bit positions")]
    public void Int256IsPow2ShouldReturnTrueForPositivePowerShifts()
    {
        for (int shift = 0; shift < 255; shift++)
        {
            Int256 value = Int256.One << shift;
            Assert.True(Int256.IsPow2(value), $"Failed at shift={shift}");
        }
    }
}
