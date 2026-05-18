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

public sealed class Float256EquatableTests
{
    [Fact(DisplayName = "Float256.operator== should return true when bit patterns are identical and the value is not NaN")]
    public void Float256EqualityOperatorShouldReturnTrueForEqualValues()
    {
        Assert.True(Float256.One == Float256.One);
        Assert.True(Float256.MaxValue == Float256.MaxValue);
        Assert.True(Float256.PositiveInfinity == Float256.PositiveInfinity);
        Assert.True(Float256.NegativeInfinity == Float256.NegativeInfinity);
    }

    [Fact(DisplayName = "Float256.operator== should treat positive zero and negative zero as equal under IEEE 754 semantics")]
    public void Float256EqualityOperatorShouldTreatPositiveAndNegativeZeroAsEqual()
    {
        Assert.True(Float256.Zero == Float256.NegativeZero);
        Assert.True(Float256.NegativeZero == Float256.Zero);
    }

    [Fact(DisplayName = "Float256.operator== should return false when either operand is NaN under IEEE 754 semantics")]
    public void Float256EqualityOperatorShouldReturnFalseForNaN()
    {
        Assert.False(Float256.NaN == Float256.NaN);
        Assert.False(Float256.NaN == Float256.Zero);
        Assert.False(Float256.One == Float256.NaN);
    }

    [Fact(DisplayName = "Float256.operator!= should be the inverse of operator==")]
    public void Float256InequalityOperatorShouldBeInverseOfEqualityOperator()
    {
        Assert.False(Float256.One != Float256.One);
        Assert.False(Float256.Zero != Float256.NegativeZero);
        Assert.True(Float256.One != Float256.Two);
        Assert.True(Float256.NaN != Float256.NaN);
    }

    [Fact(DisplayName = "Float256.Equals(Float256) should treat NaN as equal to NaN for collection semantics")]
    public void Float256EqualsMethodShouldTreatNaNAsEqualToNaN()
    {
        Assert.True(Float256.NaN.Equals(Float256.NaN));
        Assert.True(Float256.Equals(Float256.NaN, Float256.NaN));
    }

    [Fact(DisplayName = "Float256.Equals(Float256) should treat positive zero and negative zero as equal")]
    public void Float256EqualsMethodShouldTreatPositiveAndNegativeZeroAsEqual()
    {
        Assert.True(Float256.Zero.Equals(Float256.NegativeZero));
        Assert.True(Float256.Equals(Float256.Zero, Float256.NegativeZero));
    }

    [Fact(DisplayName = "Float256.Equals(object) should return false for non-Float256 objects")]
    public void Float256EqualsObjectShouldReturnFalseForNonFloat256()
    {
        Assert.False(Float256.One.Equals((object?)null));
        Assert.False(Float256.One.Equals("not a Float256"));
        Assert.False(Float256.One.Equals(new object()));
    }

    [Fact(DisplayName = "Float256.GetHashCode should collapse positive and negative zero to a single hash")]
    public void Float256GetHashCodeShouldCollapseZeros()
    {
        Assert.Equal(Float256.Zero.GetHashCode(), Float256.NegativeZero.GetHashCode());
    }

    [Fact(DisplayName = "Float256.GetHashCode should collapse all NaN bit patterns to a single hash")]
    public void Float256GetHashCodeShouldCollapseNaN()
    {
        Float256 alternativeNaN = new(Float256.NaN.RawHighBits, Float256.NaN.RawLowBits | UInt128.One);
        Assert.Equal(Float256.NaN.GetHashCode(), alternativeNaN.GetHashCode());
    }
}
