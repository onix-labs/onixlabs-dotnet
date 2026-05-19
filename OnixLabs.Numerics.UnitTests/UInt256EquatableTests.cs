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

public sealed class UInt256EquatableTests
{
    [Fact(DisplayName = "UInt256.Equals static should return true only when both halves match")]
    public void UInt256EqualsStaticShouldReturnTrueOnlyWhenBothHalvesMatch()
    {
        UInt256 a = new((UInt128)1234, (UInt128)5678);
        UInt256 b = new((UInt128)1234, (UInt128)5678);
        UInt256 differUpper = new((UInt128)1235, (UInt128)5678);
        UInt256 differLower = new((UInt128)1234, (UInt128)5679);

        Assert.True(UInt256.Equals(a, b));
        Assert.False(UInt256.Equals(a, differUpper));
        Assert.False(UInt256.Equals(a, differLower));
    }

    [Fact(DisplayName = "UInt256.Equals instance should match static Equals")]
    public void UInt256EqualsInstanceShouldMatchStaticEquals()
    {
        UInt256 a = new((UInt128)42, UInt128.MaxValue);
        UInt256 b = new((UInt128)42, UInt128.MaxValue);
        Assert.True(a.Equals(b));
        Assert.True(b.Equals(a));
    }

    [Fact(DisplayName = "UInt256.Equals object should distinguish foreign types")]
    public void UInt256EqualsObjectShouldDistinguishForeignTypes()
    {
        UInt256 a = (UInt256)42;
        object? same = (UInt256)42;
        object? other = (long)42;
        object? nullValue = null;
        Assert.True(a.Equals(same));
        Assert.False(a.Equals(other));
        Assert.False(a.Equals(nullValue));
    }

    [Fact(DisplayName = "UInt256 operator equality should match Equals")]
    public void UInt256OperatorEqualityShouldMatchEquals()
    {
        UInt256 a = UInt256.MaxValue;
        UInt256 b = UInt256.MaxValue;
        UInt256 c = UInt256.MaxValue - UInt256.One;

        Assert.True(a == b);
        Assert.False(a == c);
        Assert.False(a != b);
        Assert.True(a != c);
    }

    [Fact(DisplayName = "UInt256.GetHashCode should be consistent for equal values")]
    public void UInt256GetHashCodeShouldBeConsistentForEqualValues()
    {
        UInt256 a = new((UInt128)777, (UInt128)999);
        UInt256 b = new((UInt128)777, (UInt128)999);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact(DisplayName = "UInt256.GetHashCode should generally differ for values with swapped halves")]
    public void UInt256GetHashCodeShouldDifferForSwappedHalves()
    {
        UInt256 a = new((UInt128)100, (UInt128)200);
        UInt256 b = new((UInt128)200, (UInt128)100);
        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        Assert.False(a == b);
    }

    [Fact(DisplayName = "UInt256.Zero should equal default UInt256")]
    public void UInt256ZeroShouldEqualDefault()
    {
        UInt256 def = default;
        Assert.True(UInt256.Zero == def);
        Assert.Equal(UInt256.Zero.GetHashCode(), def.GetHashCode());
    }

    [Fact(DisplayName = "UInt256.GetHashCode should not collide for power-of-two pairs that are 32 bits apart (regression for ulong.GetHashCode XOR loss)")]
    public void UInt256GetHashCodeShouldNotCollideForPowerOfTwoPairsAcross32BitBoundary()
    {
        // ulong.GetHashCode XORs its two 32-bit halves, so for a < 32 the values 2^a and 2^(a+32)
        // map to the same int and the loss propagates through HashCode.Combine on the constituent
        // limbs. The new GetHashCode hashes the full bit pattern to avoid that collision.
        for (int shift = 0; shift < 32; shift++)
        {
            UInt256 small = UInt256.One << shift;
            UInt256 large = UInt256.One << (shift + 32);
            Assert.NotEqual(small.GetHashCode(), large.GetHashCode());
        }
    }
}
