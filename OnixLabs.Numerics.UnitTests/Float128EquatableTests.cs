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

public sealed class Float128EquatableTests
{
    [Fact(DisplayName = "Float128.operator== should return true when bit patterns are identical and the value is not NaN")]
    public void Float128EqualityOperatorShouldReturnTrueForEqualValues()
    {
        Assert.True(Float128.One == Float128.One);
        Assert.True(Float128.MaxValue == Float128.MaxValue);
        Assert.True(Float128.PositiveInfinity == Float128.PositiveInfinity);
        Assert.True(Float128.NegativeInfinity == Float128.NegativeInfinity);
    }

    [Fact(DisplayName = "Float128.operator== should treat positive zero and negative zero as equal under IEEE 754 semantics")]
    public void Float128EqualityOperatorShouldTreatPositiveAndNegativeZeroAsEqual()
    {
        Assert.True(Float128.Zero == Float128.NegativeZero);
        Assert.True(Float128.NegativeZero == Float128.Zero);
    }

    [Fact(DisplayName = "Float128.operator== should return false when either operand is NaN under IEEE 754 semantics")]
    public void Float128EqualityOperatorShouldReturnFalseForNaN()
    {
        Assert.False(Float128.NaN == Float128.NaN);
        Assert.False(Float128.NaN == Float128.Zero);
        Assert.False(Float128.One == Float128.NaN);
    }

    [Fact(DisplayName = "Float128.operator!= should be the inverse of operator==")]
    public void Float128InequalityOperatorShouldBeInverseOfEqualityOperator()
    {
        Assert.False(Float128.One != Float128.One);
        Assert.False(Float128.Zero != Float128.NegativeZero);
        Assert.True(Float128.One != Float128.Two);
        Assert.True(Float128.NaN != Float128.NaN);
    }

    [Fact(DisplayName = "Float128.Equals(Float128) should treat NaN as equal to NaN for collection semantics")]
    public void Float128EqualsMethodShouldTreatNaNAsEqualToNaN()
    {
        Assert.True(Float128.NaN.Equals(Float128.NaN));
        Assert.True(Float128.Equals(Float128.NaN, Float128.NaN));
    }

    [Fact(DisplayName = "Float128.Equals(Float128) should treat positive zero and negative zero as equal")]
    public void Float128EqualsMethodShouldTreatPositiveAndNegativeZeroAsEqual()
    {
        Assert.True(Float128.Zero.Equals(Float128.NegativeZero));
        Assert.True(Float128.Equals(Float128.Zero, Float128.NegativeZero));
    }

    [Fact(DisplayName = "Float128.Equals(object) should return false for non-Float128 objects")]
    public void Float128EqualsObjectShouldReturnFalseForNonFloat128()
    {
        Assert.False(Float128.One.Equals((object?)null));
        Assert.False(Float128.One.Equals("not a Float128"));
        Assert.False(Float128.One.Equals(new object()));
    }

    [Fact(DisplayName = "Float128.GetHashCode should collapse positive and negative zero to a single hash")]
    public void Float128GetHashCodeShouldCollapseZeros()
    {
        Assert.Equal(Float128.Zero.GetHashCode(), Float128.NegativeZero.GetHashCode());
    }

    [Fact(DisplayName = "Float128.GetHashCode should collapse all NaN bit patterns to a single hash")]
    public void Float128GetHashCodeShouldCollapseNaN()
    {
        Float128 alternativeNaN = new(Float128.NaN.RawBits | UInt128.One);
        Assert.Equal(Float128.NaN.GetHashCode(), alternativeNaN.GetHashCode());
    }
}
