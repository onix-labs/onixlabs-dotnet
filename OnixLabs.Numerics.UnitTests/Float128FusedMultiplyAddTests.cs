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

public sealed class Float128FusedMultiplyAddTests
{
    [Fact(DisplayName = "FusedMultiplyAdd(2, 3, 4) should equal 10")]
    public void FusedMultiplyAddSimpleShouldEqual10()
    {
        Float128 result = Float128.FusedMultiplyAdd((Float128)2, (Float128)3, (Float128)4);
        Assert.Equal((Float128)10, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd of small integers should match exact computation")]
    public void FusedMultiplyAddSmallIntegersShouldMatchExact()
    {
        for (int a = -10; a <= 10; a++)
        {
            for (int b = -10; b <= 10; b++)
            {
                for (int c = -10; c <= 10; c++)
                {
                    Float128 result = Float128.FusedMultiplyAdd((Float128)a, (Float128)b, (Float128)c);
                    Float128 expected = (Float128)(a * b + c);
                    Assert.Equal(expected, result);
                }
            }
        }
    }

    [Fact(DisplayName = "FusedMultiplyAdd with c=0 should equal product")]
    public void FusedMultiplyAddWithZeroAddendShouldEqualProduct()
    {
        Float128 a = Float128.Parse("3.14159265358979323846264338327950288");
        Float128 b = Float128.Parse("2.71828182845904523536028747135266250");
        Float128 expected = a * b;
        Float128 result = Float128.FusedMultiplyAdd(a, b, Float128.Zero);
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with a=0 should equal c")]
    public void FusedMultiplyAddWithZeroMultiplicandShouldEqualAddend()
    {
        Float128 c = Float128.Parse("123.456789012345678901234567890123456");
        Float128 result = Float128.FusedMultiplyAdd(Float128.Zero, Float128.Parse("99999.9"), c);
        Assert.Equal(c, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with cancellation should produce small magnitude")]
    public void FusedMultiplyAddWithCancellationShouldProduceSmallMagnitude()
    {
        Float128 a = (Float128)1000;
        Float128 b = (Float128)1000;
        Float128 c = (Float128)(-1000000);
        Float128 result = Float128.FusedMultiplyAdd(a, b, c);
        Assert.Equal(Float128.Zero, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with near-cancellation preserves small contribution")]
    public void FusedMultiplyAddWithNearCancellationPreservesSmallContribution()
    {
        Float128 a = (Float128)1000;
        Float128 b = (Float128)1000;
        Float128 c = (Float128)(-1000000) + Float128.Parse("0.5");
        Float128 result = Float128.FusedMultiplyAdd(a, b, c);
        Assert.Equal(Float128.Parse("0.5"), result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with same signs should add magnitudes")]
    public void FusedMultiplyAddWithSameSignsShouldAddMagnitudes()
    {
        Float128 a = Float128.Parse("2.5");
        Float128 b = Float128.Parse("4");
        Float128 c = Float128.Parse("3.0");
        Float128 result = Float128.FusedMultiplyAdd(a, b, c);
        Assert.Equal((Float128)13, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with opposite signs should subtract magnitudes")]
    public void FusedMultiplyAddWithOppositeSignsShouldSubtractMagnitudes()
    {
        Float128 a = Float128.Parse("2.5");
        Float128 b = Float128.Parse("4");
        Float128 c = Float128.Parse("-3");
        Float128 result = Float128.FusedMultiplyAdd(a, b, c);
        Assert.Equal((Float128)7, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd of -2 * -3 + -4 should equal 2")]
    public void FusedMultiplyAddNegativeMultiplicandsShouldGivePositiveProduct()
    {
        Float128 result = Float128.FusedMultiplyAdd((Float128)(-2), (Float128)(-3), (Float128)(-4));
        Assert.Equal((Float128)2, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd preserves precision better than separate multiply+add")]
    public void FusedMultiplyAddPreservesPrecisionBetterThanSeparateOps()
    {
        Float128 a = Float128.Parse("1.0000000000000000000000000000000001");
        Float128 b = (Float128)2;
        Float128 c = Float128.Parse("-2.0000000000000000000000000000000002");
        Float128 result = Float128.FusedMultiplyAdd(a, b, c);
        Assert.Equal(Float128.Zero, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with c much larger than product should equal c")]
    public void FusedMultiplyAddWithCMuchLargerThanProductShouldRoundToC()
    {
        Float128 a = Float128.Epsilon;
        Float128 b = Float128.Epsilon;
        Float128 c = (Float128)1000;
        Float128 result = Float128.FusedMultiplyAdd(a, b, c);
        Assert.Equal(c, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with c much smaller than product should equal product")]
    public void FusedMultiplyAddWithCMuchSmallerThanProductShouldRoundToProduct()
    {
        Float128 a = (Float128)1000;
        Float128 b = (Float128)1000;
        Float128 c = Float128.Epsilon;
        Float128 expected = a * b;
        Float128 result = Float128.FusedMultiplyAdd(a, b, c);
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd of fractional values should round once")]
    public void FusedMultiplyAddOfFractionalValuesShouldRoundOnce()
    {
        Float128 a = Float128.Parse("0.1");
        Float128 b = Float128.Parse("0.1");
        Float128 c = Float128.Parse("0.01");
        Float128 expected = Float128.Parse("0.02");
        Float128 result = Float128.FusedMultiplyAdd(a, b, c);
        Float128 difference = Float128.Abs(result - expected);
        Float128 tolerance = Float128.Parse("1E-33");
        Assert.True(difference < tolerance);
    }

    [Fact(DisplayName = "FusedMultiplyAdd produces same sign zero rules")]
    public void FusedMultiplyAddZeroSignRules()
    {
        Assert.Equal(Float128.Zero, Float128.FusedMultiplyAdd(Float128.Zero, Float128.Zero, Float128.Zero));
        Assert.Equal(Float128.NegativeZero, Float128.FusedMultiplyAdd(Float128.NegativeZero, Float128.Zero, Float128.NegativeZero));
        Assert.Equal(Float128.Zero, Float128.FusedMultiplyAdd(Float128.Zero, Float128.NegativeZero, Float128.Zero));
    }

    [Fact(DisplayName = "FusedMultiplyAdd respects infinity propagation")]
    public void FusedMultiplyAddRespectsInfinityPropagation()
    {
        Assert.Equal(Float128.PositiveInfinity, Float128.FusedMultiplyAdd(Float128.PositiveInfinity, Float128.One, Float128.Zero));
        Assert.Equal(Float128.NegativeInfinity, Float128.FusedMultiplyAdd(Float128.NegativeInfinity, Float128.One, Float128.Zero));
        Assert.Equal(Float128.PositiveInfinity, Float128.FusedMultiplyAdd(Float128.One, Float128.One, Float128.PositiveInfinity));
    }

    [Fact(DisplayName = "FusedMultiplyAdd of (Inf, 1, -Inf) returns NaN")]
    public void FusedMultiplyAddOfInfMinusInfReturnsNaN()
    {
        Assert.True(Float128.IsNaN(Float128.FusedMultiplyAdd(Float128.PositiveInfinity, Float128.One, Float128.NegativeInfinity)));
    }

    [Fact(DisplayName = "FusedMultiplyAdd with subnormal addend handles correctly")]
    public void FusedMultiplyAddWithSubnormalAddendHandlesCorrectly()
    {
        Float128 a = (Float128)2;
        Float128 b = (Float128)3;
        Float128 c = Float128.Epsilon;
        Float128 expected = (Float128)6 + c;
        Float128 result = Float128.FusedMultiplyAdd(a, b, c);
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd matches BigDecimal oracle for assorted finite values")]
    public void FusedMultiplyAddMatchesBigDecimalOracleForAssortedValues()
    {
        (string a, string b, string c)[] cases =
        {
            ("3.14159265358979323846264338327950288", "2.71828182845904523536028747135266250", "1.41421356237309504880168872420969807"),
            ("1.5", "2.5", "3.5"),
            ("1E10", "1E10", "1E20"),
            ("1E-10", "1E10", "1"),
            ("-1.5", "1.5", "2.25"),
            ("1.000000000000000000000000000000001", "1.000000000000000000000000000000001", "-1"),
            ("0.5", "0.5", "0.25"),
            ("7", "11", "13"),
            ("1.7976931348623157E308", "1E-308", "1"),
            ("1.0001", "1.0001", "-1.0002"),
        };

        foreach ((string aStr, string bStr, string cStr) in cases)
        {
            Float128 a = Float128.Parse(aStr);
            Float128 b = Float128.Parse(bStr);
            Float128 c = Float128.Parse(cStr);

            BigDecimal exact = (BigDecimal)a * (BigDecimal)b + (BigDecimal)c;
            Float128 expected = (Float128)exact;
            Float128 actual = Float128.FusedMultiplyAdd(a, b, c);

            Assert.Equal(expected, actual);
        }
    }
}
